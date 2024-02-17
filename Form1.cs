using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTMusicWidget
{
    public partial class Form1 : Form
    {
        private static readonly string[] Scopes = { "https://www.googleapis.com/auth/youtube.readonly" };
        private static UserCredential _credential;
        public Form1()
        {
            InitializeComponent();
        }
        private void login_text_Click(object sender, EventArgs e)
        {
            Task.Run(() => Authenticate());
        }

        private async Task Authenticate()
        {
            try
            {
                using (var stream= new FileStream("credential.json", FileMode.Open, FileAccess.Read))
                {
                    _credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                           GoogleClientSecrets.FromStream(stream).Secrets,
                           Scopes,
                           "user",
                           CancellationToken.None,
                           new FileDataStore("token.json", true));
                }

                MessageBox.Show("로그인 성공");

                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"로그인 중 오류가 발생했습니다: {ex.Message}");
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
            });
        }

        private void Login_com_label_Click(object sender, EventArgs e)
        {
            Main.Visible=false;
            main_content.Visible=true;
        }

        private void pos_change_Click(object sender, EventArgs e)
        {
            Main.Location = new System.Drawing.Point(0, Main.Location.Y+12);
            pos_change.Visible = false;
            pos_complete.Visible = true;
        }

        private void pos_complete_Click(object sender, EventArgs e)
        {
            Main.Location = new System.Drawing.Point(0,0);
            pos_change.Visible = true;
            pos_complete.Visible = false;
        }

    }
}
