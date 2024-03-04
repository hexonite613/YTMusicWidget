using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static YTMusicWidget.Form1;

namespace YTMusicWidget
{
    public partial class Form1 : Form
    {
        private static readonly string[] Scopes = { YouTubeService.Scope.Youtube };
        private static UserCredential _credential;
        public Form1()
        {
            InitializeComponent();
            playlistListBox.DrawMode = DrawMode.OwnerDrawVariable;
            playlistListBox.MeasureItem += Playlist_MeasureItem;
            playlistListBox.DrawItem += Playlist_DrawItem;
            playlistListBox.SelectedIndexChanged += playlist_music_list_SelectedIndexChangedAsync;
            playlist_music_list.DrawItem += playlist_music_list_DrawItem;
            playlist_music_list.DrawMode = DrawMode.OwnerDrawVariable;
            playlist_music_list.MeasureItem += Playlist_Music_MeasureItem;
            Task.Run(() => Authenticate().Wait());

        }

        private void login_text_Click(object sender, EventArgs e)
        {
            Task.Run(() => Authenticate());
        }

        private async Task Authenticate()
        {
            if (_credential == null)
            {
                try
                {
                    using (var stream = new FileStream("credential.json", FileMode.Open, FileAccess.Read))
                    {
                        _credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                               GoogleClientSecrets.FromStream(stream).Secrets,
                               Scopes,
                               "user",
                               CancellationToken.None,
                               new FileDataStore("token.json", true));
                    }
                    UpdateUI();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"로그인 중 오류가 발생했습니다: {ex.Message}");
                }
            }
            else
            {
                UpdateUI();
            }
        }

        private async Task GetUserName()
        {
            var service = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = "YoutubeAPIExample"
            });

            var request = service.Channels.List("snippet");
            request.Mine = true;

            var response = await request.ExecuteAsync();
            if (response.Items != null && response.Items.Count > 0)
            {
                var channel = response.Items[0];
                var userName = channel.Snippet.Title;
                this.Invoke((MethodInvoker)delegate
                {
                    Login_com_label.Text = $"{userName}님, 환영합니다!";
                });
            }
        }

        private async Task Logout()
        {
            try
            {
                if (_credential != null)
                {
                    await _credential.RevokeTokenAsync(CancellationToken.None);
                    _credential = null;
                    MessageBox.Show("로그아웃이 성공적으로 완료되었습니다.");

                    this.Invoke((MethodInvoker)delegate
                    {
                        Login_com_label.Text = "로그인을 해주세요";
                        login_text.Visible = true;
                        Login_com_label.Visible = false;
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

        private void Logout_label_Click_1(object sender, EventArgs e)
        {
            Task.Run(() => Logout());
        }

        private void UpdateUI()
        {
            this.Invoke((MethodInvoker)delegate
            {
                login_text.Visible = false;
                Login_com_label.Visible = true;
                Task.Run(() => GetUserName());
                Task.Run(() => GetPlaylists());
            });
        }

        private void Login_com_label_Click(object sender, EventArgs e)
        {
            main_content.Visible = true;
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
                var service = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = _credential,
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
                        var thumbheight= playlistImage.Height;
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
                    Image play_thumb=Image.FromStream(stream);
                    return ResizeImage(play_thumb, new Size(177, 100));
                }
            }
        }

        private void Playlist_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            var listBox = (ListBox)sender;
            var playlistItem = (PlaylistItems)listBox.Items[e.Index];
            e.ItemHeight =playlistItem.thumbheight; 
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

            public Playlist_Music_Items(string title, Image image)
            {
                Title = title;
                Image = image;
            }
        }

        private async void playlist_music_list_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            if (playlistListBox.SelectedItem != null)
            {
                var selectedPlaylist = (PlaylistItems)playlistListBox.SelectedItem;
                await GetPlaylist_Music(selectedPlaylist.Id);
            }
        }

        private async Task GetPlaylist_Music(string playlistId)
        {
            try
            {
                var service = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = _credential,
                    ApplicationName = "ytmusicwidget"
                });

                var request = service.PlaylistItems.List("snippet");
                request.PlaylistId = playlistId;
                request.MaxResults = 50;

                var response = await request.ExecuteAsync();

                // 플레이리스트 음악들을 ListBox에 추가
                var musicItemsToAdd = new List<Playlist_Music_Items>(); 
                foreach (var item in response.Items)
                {
                    var thumbnailUrl = item.Snippet.Thumbnails.High.Url;
                    var musicImage = await GetImageFromUrl(thumbnailUrl);
                    var music_thumbheight = musicImage.Height;
                    var music_thumbwidth = musicImage.Width;

                    var musicItem = new Playlist_Music_Items(item.Snippet.Title, musicImage);
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
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"플레이리스트 음악 가져오기 중 오류가 발생했습니다: {ex.Message}");
            }
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
    }
}
