﻿using System;
using System.Windows.Forms;
using System.IO;
using CefSharp;
using System.Net.Http;
using System.Security.Cryptography;
using System.Configuration;
using Google.Apis.Auth.OAuth2.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace YTMusicWidget
{
    partial class Authorize
    {
       

        private readonly Form1 form1;
        private readonly playlist playlist;
        internal static readonly string AccessTokenFilePath = "access_token.txt";
        internal static readonly string RefreshTokenFilePath = "refresh_token.txt";

        private String client_id = ConfigurationManager.AppSettings["client_id"];
        private String redirect_uri = ConfigurationManager.AppSettings["redirect_uri"];
        private String client_secret = ConfigurationManager.AppSettings["client_secret"];
        
        //DI를 위한 클래스 생성
        public Authorize(Form1 form1)
        {
            this.form1 = form1;
        }



        internal async Task AuthenticateAsync()
        {
            //리프레시 토큰이 있을 때 항상 access token 갱신
            if (File.Exists(AccessTokenFilePath)&&File.Exists(RefreshTokenFilePath))
            {
                try
                {
                    // 리프레시 토큰을 사용하여 새로운 액세스 토큰 가져오기
                    string refreshToken = GetRefreshToken();
                    string newAccessToken = await GetNewAccessTokenUsingRefreshToken(refreshToken);
                    SaveAccessToken(newAccessToken);

                    // 새로운 액세스 토큰을 사용하여 UI 업데이트 다시 시도
                    form1.UpdateUI();
                }
                catch (Exception ex2)
                {
                    MessageBox.Show($"토큰 리프레시 중 오류가 발생하였습니다: {ex2.Message}");
                }
            }
            else
            {
                try
                {
                    form1.music_player.Invoke((MethodInvoker)delegate {
                        
                        form1.music_player.Load("https://accounts.google.com/o/oauth2/auth?" +
                            "client_id=" + client_id +
                            "&redirect_uri=" + redirect_uri +
                            "&response_type=code" +
                            "&scope=https://www.googleapis.com/auth/youtube" +
                            "&access_type=offline" +
                            "&prompt=consent");
                        form1.music_player.FrameLoadEnd += Browser_FrameLoadEnd;
                        form1.music_player.Visible = true;
                    });

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"로그인 중 오류가 발생했습니다: {ex.Message}");
                }
            }
        }


        private async void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                var url = new Uri(e.Url);

                string authorizationCode = GetAuthorizationCode(url);

                if (!string.IsNullOrEmpty(authorizationCode))
                {
                    await ExchangeCodeForTokens(authorizationCode);
                }
            }
        }

        private string GetAuthorizationCode(Uri url)
        {
            var query = System.Web.HttpUtility.ParseQueryString(url.Query);
            return query.Get("code");
        }


        private async Task ExchangeCodeForTokens(string authorizationCode)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestData = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("code", authorizationCode),
                new KeyValuePair<string, string>("client_id", client_id),
                new KeyValuePair<string, string>("client_secret", client_secret),
                new KeyValuePair<string, string>("redirect_uri", redirect_uri),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
            });

                HttpResponseMessage response = await client.PostAsync("https://oauth2.googleapis.com/token", requestData);
                string responseContent = await response.Content.ReadAsStringAsync();

                var tokenResponse1 = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(responseContent);


                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(responseContent);
                    SaveAccessToken(tokenResponse.AccessToken);
                    SaveRefreshToken(tokenResponse.RefreshToken);

                    await form1.GetUserName();
                    form1.UpdateUI();
                }
                else
                {
                    // 오류 처리
                    throw new Exception("Failed to exchange authorization code for tokens.");

                }
            }
        }


        public async Task<string> GetNewAccessTokenUsingRefreshToken(string refreshToken)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", client_id),
                    new KeyValuePair<string, string>("client_secret", client_secret),
                    new KeyValuePair<string, string>("refresh_token", refreshToken),
                    new KeyValuePair<string, string>("grant_type", "refresh_token")
                });

                HttpResponseMessage response = await client.PostAsync("https://oauth2.googleapis.com/token", requestData);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = JObject.Parse(responseContent);
                    return tokenResponse["access_token"].ToString();
                }
                else
                {
                    // 오류 처리
                    throw new Exception($"Failed to refresh access token. Error: {responseContent}");
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

        private void SaveRefreshToken(string refreshToken)
        {
            using (FileStream fs = new FileStream(RefreshTokenFilePath, FileMode.OpenOrCreate))
            {
                // 암호화된 액세스 토큰 생성
                byte[] encryptedToken = ProtectData(refreshToken);
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


        internal static string GetAccessToken()
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

        internal static string GetRefreshToken()
        {
            //파일 확인
            if (File.Exists(RefreshTokenFilePath))
            {
                byte[] encryptedToken = File.ReadAllBytes(RefreshTokenFilePath);
                // 복호화
                return UnprotectData(encryptedToken);
            }
            else
            {
                return null;
            }
        }
    }
}
