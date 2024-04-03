using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;


namespace YTMusicWidget
{

    public partial class Form1 : Form
    {
        //authorize 객체 생성
        private readonly Authorize Authorize;

        public Form1()
        {
            InitializeComponent();
            InitializeCefSharp();
            playlistListBox.DrawMode = DrawMode.OwnerDrawVariable;
            playlistListBox.MeasureItem += Playlist_MeasureItem;
            playlistListBox.DrawItem += Playlist_DrawItem;
            playlistListBox.SelectedIndexChanged += playlist_music_list_SelectedIndexChangedAsync;
            playlist_music_list.DrawItem += playlist_music_list_DrawItem;
            playlist_music_list.DrawMode = DrawMode.OwnerDrawVariable;
            playlist_music_list.MeasureItem += Playlist_Music_MeasureItem;
            Authorize = new Authorize(this);
            Task.Run(() => Authorize.Authenticate());

        }

        private void InitializeCefSharp()
        {
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("autoplay-policy", "no-user-gesture-required");
            settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36 /CefSharp Browser" + Cef.CefSharpVersion;

            Cef.Initialize(settings, true, browserProcessHandler: null);
        }


        private void Login_Button_Click(object sender, EventArgs e)
        {
            music_player.Parent = Main;
            music_player.Location = new Point(0, 0);
            music_player.Size = new Size(Main.Size.Width, Main.Size.Height);
            Task.Run(() => Authorize.Authenticate());
        }




        internal async Task GetUserName()
        {
            string token = Authorize.GetAccessToken();
            {
                try
                {
                    // GoogleCredential 객체 생성
                    GoogleCredential credential = GoogleCredential.FromAccessToken(token);

                    // YouTube API 서비스 생성
                    var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = "ytmusicwidget"
                    });

                    // 현재 사용자의 채널 정보 요청
                    var channelsListRequest = youtubeService.Channels.List("snippet");
                    channelsListRequest.Mine = true;

                    // API 호출 및 응답 받기
                    var channelsListResponse = await channelsListRequest.ExecuteAsync();

                    // 채널 정보 출력
                    var channel = channelsListResponse.Items[0];
                    string userName = channel.Snippet.Title;
                    Invoke((MethodInvoker)delegate
                    {

                        Login_Button.Visible = false;
                        Login_com_label.Text = userName + "님, 환영합니다";
                        Login_com_label.Visible = true;
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("오류 발생: " + ex.Message);
                }
            }
        }

        private async Task Logout()
        {
            try
            {
                if (File.Exists(Authorize.AccessTokenFilePath))
                {
                    Invoke((MethodInvoker)delegate
                    {
                        Cef.GetGlobalCookieManager().DeleteCookies(string.Empty, string.Empty);
                        File.Delete(Authorize.AccessTokenFilePath);
                        Login_com_label.Visible = false;
                        Login_Button.Visible = true;
                        MessageBox.Show("로그아웃 되었습니다.");
                    });
                }
                else
                {
                    MessageBox.Show("로그인되어 있지 않습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"로그아웃 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void Logout_label_Click(object sender, EventArgs e)
        {
            Task.Run(() => Logout());
        }

        internal void UpdateUI()
        {
            this.Invoke((MethodInvoker)delegate
            {
                Login_Button.Visible = false;
                Login_com_label.Visible = true;
                Task.Run(() => GetPlaylists());
            });
        }

        private void Login_com_label_Click(object sender, EventArgs e)
        {
            main_content.Visible = true;
            music_player.Parent = main_content;
        }

        private void pos_change_Click(object sender, EventArgs e)
        {
            Main.Location = new System.Drawing.Point(0, Main.Location.Y + 12);
            pos_change.Visible = false;
            pos_complete.Visible = true;
        }

        private void pos_complete_Click(object sender, EventArgs e)
        {
            Main.Location = new System.Drawing.Point(0, 0);
            pos_change.Visible = true;
            pos_complete.Visible = false;
        }


        public class Playlist
        {
            public string Title { get; set; }
            public List<PlaylistItems> Items { get; } = new List<PlaylistItems>();
        }


        public class PlaylistItems
        {
            public string Title { get; }
            public Image Image { get; }
            public int thumbheight { get; }
            public int thumbwidth { get; }
            public string Id { get; }

            public PlaylistItems(string title, Image image, int thumbheight, int thumbwidth, string id)
            {
                Title = title;
                Image = image;
                this.thumbheight = thumbheight;
                this.thumbwidth = thumbwidth;
                Id = id;
            }
        }

        private async Task GetPlaylists()
        {
            try
            {
                string token = Authorize.GetAccessToken();
                GoogleCredential credential = GoogleCredential.FromAccessToken(token);
                var service = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "ytmusicwidget"
                });

                var request = service.Playlists.List("snippet");
                request.Mine = true;
                request.MaxResults = 50;

                var response = await request.ExecuteAsync();

                // 플레이리스트를 ListBox에 추가
                var playlistsToAdd = new List<PlaylistItems>();
                foreach (var playlist in response.Items)
                {
                    // 이미 ListBox에 추가된 플레이리스트인지 확인
                    if (!playlistListBox.Items.Cast<PlaylistItems>().Any(p => p.Title == playlist.Snippet.Title))
                    {
                        var thumbnailUrl = playlist.Snippet.Thumbnails.High.Url;
                        var playlistImage = await GetImageFromUrl(thumbnailUrl);
                        var thumbheight = playlistImage.Height;
                        var thumbwidth = playlistImage.Width;

                        // 플레이리스트 아이템 생성
                        var playlistItem = new PlaylistItems(playlist.Snippet.Title, playlistImage, thumbheight, thumbwidth, playlist.Id);

                        // 임시 목록에 플레이리스트 아이템 추가
                        playlistsToAdd.Add(playlistItem);
                    }
                }

                // UI 업데이트를 UI 스레드에서 수행
                Invoke((MethodInvoker)delegate
                {
                    foreach (var playlistItem in playlistsToAdd)
                    {
                        playlistListBox.Items.Add(playlistItem);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"플레이리스트 가져오기 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        //썸네일 이미지 수정
        private Image ResizeImage(Image image, Size size)
        {
            Bitmap result = new Bitmap(size.Width, size.Height);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.DrawImage(image, new Rectangle(Point.Empty, size));
            }
            return result;
        }


        //thumbnail 가져오기
        private async Task<Image> GetImageFromUrl(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    Image play_thumb = Image.FromStream(stream);
                    return ResizeImage(play_thumb, new Size(177, 100));
                }
            }
        }

        private void Playlist_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            var listBox = (ListBox)sender;
            var playlistItem = (PlaylistItems)listBox.Items[e.Index];
            e.ItemHeight = playlistItem.thumbheight;
        }

        private void Playlist_DrawItem(object sender, DrawItemEventArgs e)
        {

            if (e.Index < 0)
                return;

            var listBox = (ListBox)sender;
            var playlistItem = (PlaylistItems)listBox.Items[e.Index];

            if (playlistItem == null)
                return;

            e.DrawBackground();

            if (playlistItem.Image != null)
            {
                var imageBounds = new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Height, e.Bounds.Height);
                e.Graphics.DrawImage(playlistItem.Image, imageBounds);
            }

            var textBounds = new Rectangle(e.Bounds.Left + e.Bounds.Height, e.Bounds.Top, e.Bounds.Width - e.Bounds.Height, e.Bounds.Height);
            TextRenderer.DrawText(e.Graphics, playlistItem.Title, listBox.Font, textBounds, listBox.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        //Playlist 고른 후 음악 아이템 추가
        public class Playlist_Music_Items
        {
            public string Title { get; }
            public Image Image { get; }
            public string VideoId { get; }

            public Playlist_Music_Items(string title, Image image, string videoId)
            {
                Title = title;
                Image = image;
                VideoId = videoId;
            }
        }


        private const int PageSize = 10; // 페이지 당 아이템 수
        private int currentPage = 1; // 현재 페이지 번호
        private int totalMusicCount; // 총 음악 수
        private int totalPageCount; // 총 페이지 수
        private string nextPageToken = null; // 다음 페이지 토큰
        private PlaylistItems selectedPlaylist; // 선택된 플레이리스트


        private async void playlist_music_list_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            if (playlistListBox.SelectedItem != null)
            {
                selectedPlaylist = (PlaylistItems)playlistListBox.SelectedItem;
                currentPage = 1; // 페이지 초기화
                await GetPlaylist_Music(selectedPlaylist.Id, currentPage);
            }
        }


        private async Task GetPlaylist_Music(string playlistId, int page)
        {
            try
            {
                string token = Authorize.GetAccessToken();
                GoogleCredential credential = GoogleCredential.FromAccessToken(token);
                var service = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "ytmusicwidget"
                });

                var request = service.PlaylistItems.List("snippet");
                request.PlaylistId = playlistId;
                request.MaxResults = PageSize;
                request.PageToken = (page > 1) ? nextPageToken : null;

                var response = await request.ExecuteAsync();

                // 총 음악 수와 총 페이지 수 업데이트
                totalMusicCount = (int)response.PageInfo.TotalResults;
                totalPageCount = (int)Math.Ceiling((double)totalMusicCount / PageSize);

                // 다음 페이지 토큰 설정
                nextPageToken = response.NextPageToken;


                // 플레이리스트 음악들을 ListBox에 추가
                var musicItemsToAdd = new List<Playlist_Music_Items>();
                foreach (var item in response.Items)
                {
                    var thumbnailUrl = item.Snippet.Thumbnails.High.Url;
                    var musicImage = await GetImageFromUrl(thumbnailUrl);
                    var music_thumbheight = musicImage.Height;
                    var music_thumbwidth = musicImage.Width;

                    var musicItem = new Playlist_Music_Items(item.Snippet.Title, musicImage, item.Snippet.ResourceId.VideoId);
                    musicItemsToAdd.Add(musicItem);
                }

                // UI 업데이트를 UI 스레드에서 수행
                Invoke((MethodInvoker)delegate
                {
                    playlist_music_list.Items.Clear();
                    foreach (var musicItem in musicItemsToAdd)
                    {
                        playlist_music_list.Items.Add(musicItem);
                    }
                    UpdatePageInfo();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"플레이리스트 음악 가져오기 중 오류가 발생했습니다: {ex.Message}");
            }
        }


        private void UpdatePageInfo()
        {
            Mus_page_label.Text = $"{currentPage} / {totalPageCount}";
            Before_page_mus.Enabled = (currentPage > 1);
            Next_page_mus.Enabled = (currentPage < totalPageCount);
        }


        private void Playlist_Music_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            var listBox = (ListBox)sender;
            var musicItem = (Playlist_Music_Items)listBox.Items[e.Index];
            e.ItemHeight = musicItem.Image.Height;
        }


        private void playlist_music_list_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            var listBox = (ListBox)sender;
            var musicItem = (Playlist_Music_Items)listBox.Items[e.Index];

            if (musicItem == null)
                return;

            e.DrawBackground();


            // 썸네일 이미지 그리기
            if (musicItem.Image != null)
            {
                var imageBounds = new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Height, e.Bounds.Height);
                e.Graphics.DrawImage(musicItem.Image, imageBounds);
            }

            // 음악 이름 그리기
            var textBounds = new Rectangle(e.Bounds.Left + e.Bounds.Height, e.Bounds.Top, e.Bounds.Width - e.Bounds.Height, e.Bounds.Height);
            TextRenderer.DrawText(e.Graphics, musicItem.Title, listBox.Font, textBounds, listBox.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }


        //음악 선택시
        private void playlist_music_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playlist_music_list.SelectedItem != null)
            {
                // 선택한 음악 항목 가져오기
                Playlist_Music_Items selectedMusic = (Playlist_Music_Items)playlist_music_list.SelectedItem;

                // 음악 재생
                PlayMusic(selectedMusic.VideoId);
            }
        }


        private void PlayMusic(string videoId)
        {
            Playlist_Music_Items selectedMusic = (Playlist_Music_Items)playlist_music_list.SelectedItem;
            string url = $"https://www.youtube.com/watch?v={videoId}?autoplay=1";
            music_player.Load(url);
            music_player.Visible = true;
        }

        //이전 페이지(음악)
        private async void Before_page_mus_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                var selectedPlaylist = (PlaylistItems)playlistListBox.SelectedItem;
                await GetPlaylist_Music(selectedPlaylist.Id, currentPage);
            }
        }

        //다음 페이지(음악)
        private async void Next_page_mus_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPageCount)
            {
                currentPage++;
                await GetPlaylist_Music(selectedPlaylist.Id, currentPage);
            }
        }
    }
}
