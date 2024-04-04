using System;
using System.Windows.Forms;
using System.IO;
using CefSharp;
using System.Net.Http;
using System.Security.Cryptography;
using System.Configuration;

namespace YTMusicWidget
{
    partial class Authorize
    {
        private readonly Form1 form1;
        internal static readonly string AccessTokenFilePath = "access_token.txt";
        
        //DI를 위한 클래스 생성
        public Authorize(Form1 form1)
        {
            this.form1 = form1;
        }
        
        internal void Authenticate()
        {
            //file이 없으면 다시 업데이트 시도
            if (File.Exists(AccessTokenFilePath))
            {
                form1.UpdateUI();
            }
            else
            {
                try
                {
                    form1.music_player.Invoke((MethodInvoker)delegate {
                        form1.music_player.Visible = true;
                        form1.music_player.Load("https://accounts.google.com/o/oauth2/auth?" +
                            "client_id=" + ConfigurationManager.AppSettings["client_id"]+
                            "&redirect_uri=" + ConfigurationManager.AppSettings["redirect_uri"]+
                            "&response_type=token" +
                            "&scope=https://www.googleapis.com/auth/youtube");
                        form1.music_player.FrameLoadEnd += Browser_FrameLoadEnd;
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"로그인 중 오류가 발생했습니다: {ex.Message}");
                }
            }
        }


        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {

            if (e.Frame.IsMain)
            {
                Uri url = new Uri(e.Url);
                if (url.AbsoluteUri.StartsWith(ConfigurationManager.AppSettings["redirect_uri"]))
                {
                    string token = GetAuthorizationCode(url);
                    ExchangeCodeForAccessToken(token);
                }
            }
        }

        private async void ExchangeCodeForAccessToken(string token)
        {
            string apiUrl = "https://www.googleapis.com/auth/youtube";
            using (HttpClient client = new HttpClient())
            {
                // 인증 헤더 추가
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // API 호출 및 응답 받기
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    SaveAccessToken(token);
                    await form1.GetUserName();
                    form1.UpdateUI();
                }
                else
                {
                    // 오류 처리
                    MessageBox.Show($"로그인 중 오류가 발생했습니다: " + response.ReasonPhrase);
                    string errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("상세 정보: " + errorContent);
                }
            }
        }


        //보안을 위해서 액세스 토큰 저장시 암호화
        public static void SaveAccessToken(string accessToken)
        {
            using (FileStream fs = new FileStream(AccessTokenFilePath, FileMode.OpenOrCreate))
            {
                // 암호화된 액세스 토큰 생성
                byte[] encryptedToken = ProtectData(accessToken);
                fs.Write(encryptedToken, 0, encryptedToken.Length);
                fs.Close();
            }
        }

        private static byte[] ProtectData(string data)
        {
            // 데이터를 바이트 배열로 변환하여 보호
            byte[] plaintextBytes = System.Text.Encoding.UTF8.GetBytes(data);
            return ProtectedData.Protect(plaintextBytes, null, DataProtectionScope.CurrentUser);
        }

        //복호화
        private static string UnprotectData(byte[] protectedData)
        {
            // 암호화된 데이터를 복호화하여 원래 데이터로 변환
            byte[] plaintextBytes = ProtectedData.Unprotect(protectedData, null, DataProtectionScope.CurrentUser);
            return System.Text.Encoding.UTF8.GetString(plaintextBytes);
        }


        public static string GetAccessToken()
        {
            //파일 확인
            if (File.Exists(AccessTokenFilePath))
            {
                byte[] encryptedToken = File.ReadAllBytes(AccessTokenFilePath);
                // 복호화
                return UnprotectData(encryptedToken);
            }
            else
            {
                return null;
            }
        }


        internal string GetAuthorizationCode(Uri url)
        {

            string queryString = url.Fragment.TrimStart('#');

            string[] queryParts = queryString.Split('&');
            foreach (string part in queryParts)
            {
                if (part.StartsWith("access_token="))
                {
                    return part.Split('=')[1];
                }
            }

            throw new Exception("Access token not found in the URL.");
        }
    }
}
