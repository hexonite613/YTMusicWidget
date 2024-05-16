using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Configuration;
using YTMusicWidget.src;
using System.Reflection;

namespace YTMusicWidget
{

    public partial class Form1 : Form
    {
        //authorize 객체 생성
        private readonly Authorize Authorize;
        //playlist 객체 생성
        private readonly playlist playlist;
        //music 객체 생성
        private readonly Music music;
        



        public Form1()
        {
            InitializeComponent();
            InitializeCefSharp();
            this.Size = new Size(586, 450);

            music_player.FrameLoadEnd += Music_Control_JS;

            Authorize = new Authorize(this);
            playlist = new playlist(this);
            music = new Music(this);

            //Flicker 현상 방지
            playlistListBox.GetType().GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playlistListBox, true);
            playlist_music_list.GetType().GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(playlist_music_list, true);
            Inplay_playlist.GetType().GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(Inplay_playlist, true);

            //이미지 백그라운드 보이게 하기
            pos_change.Parent = Main_Background;
            pos_complete.Parent = Main_Background;
            Title.Parent = Main_Background;
            pictureBox1.Parent = Main_Background;
            Login_com_label.Parent = Main_Background;
            Login_Button.Parent = Main_Background;
            Logout_label.Parent = Main_Background;


            Delete_TokenFile();


        }

        private void InitializeCefSharp()
        {
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("autoplay-policy", "no-user-gesture-required");
            settings.UserAgent = ConfigurationManager.AppSettings["cef_useragent"] + Cef.CefSharpVersion;

            Cef.Initialize(settings, true, browserProcessHandler: null);

        }


        private async void Music_Control_JS(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                string url = e.Frame.Url;

                if (url.Contains("?autoplay=1"))
                {
                    await music_player.EvaluateScriptAsync(@"
                    function playPauseVideo() {
                        var iframe = document.querySelector('iframe');
                        var player = new YT.Player(iframe);
                        
                        if (player.getPlayerState() === YT.PlayerState.PLAYING) {
                            player.pauseVideo();
                        } else {
                            player.playVideo();
                        }
                    }
                ");
                    music_player.ShowDevTools();
                }
            }
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            music_player.Parent = Main;
            music_player.Location = new Point(0, 0);
            music_player.Size = new Size(Main.Size.Width, Main.Size.Height);
            music_player.BringToFront();
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
                        Login_com_label.Text = userName + "님"+"\n"+" 환영합니다";
                        Login_com_label.Location = new Point((this.ClientSize.Width-Login_com_label.Width) / 2, 320);
                        Login_com_label.Visible = true;
                        Logout_label.Visible = true;
                        music_player.Visible = false;
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
            this.Invoke((MethodInvoker)async delegate
            {
                await GetUserName();
                Login_Button.Visible = false;
                Login_com_label.Visible = true;
                Task.Run(() => playlist.GetPlaylists());
            });
        }

        private void Login_com_label_Click(object sender, EventArgs e)
        {
            main_content.Visible = true;
            music_player.Parent = main_content;
            //music_player.Visible = true;
            music_player.Location = new Point(0, 100);
            music_player.BringToFront();
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




        private void Delete_TokenFile()
        {
            // 프로그램 종료 시 파일 삭제
            if (File.Exists(Authorize.AccessTokenFilePath))
            {
                try
                {
                    File.Delete(Authorize.AccessTokenFilePath);
                }
                catch (Exception ex)
                {
                    // 파일 삭제 실패 시 예외 처리
                    MessageBox.Show($"파일 삭제 중 오류 발생: {ex.Message}");
                }
            }
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
            public string Id { get; set; }

            public PlaylistItems(string title, Image image, int thumbheight, int thumbwidth, string id)
            {
                Title = title;
                Image = image;
                this.thumbheight = thumbheight;
                this.thumbwidth = thumbwidth;
                Id = id;
            }
        }

        //음악 부분
        public const int PageSize = 10; // 페이지 당 아이템 수
        public int currentPage = 1; // 현재 페이지 번호
        internal int totalMusicCount; // 총 음악 수
        internal int totalPageCount; // 총 페이지 수
        internal string nextPageToken = null; // 다음 페이지 토큰
        internal PlaylistItems selectedPlaylist; // 선택된 플레이리스트





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


        private void Music_player_visible_Click(object sender, EventArgs e)
        {
            Music_Controller.Visible = true;
        }


    }
}
