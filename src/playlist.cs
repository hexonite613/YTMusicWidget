﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static YTMusicWidget.Form1;
using System.Windows.Forms;

namespace YTMusicWidget
{
    internal class playlist
    {
        private readonly Form1 form1;


        //DI를 위한 클래스 생성
        public playlist(Form1 form1)
        {
            this.form1 = form1;

        }


        internal async Task GetPlaylists()
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

                var response = await request.ExecuteAsync();

                // 플레이리스트를 ListBox에 추가
                var playlistsToAdd = new List<PlaylistItems>();

                foreach (var playlist in response.Items)
                {
                    // 이미 ListBox에 추가된 플레이리스트인지 확인
                    if (!form1.playlistListBox.Items.Cast<PlaylistItems>().Any(p => p.Title == playlist.Snippet.Title))
                    {
                        var thumbnailUrl = playlist.Snippet.Thumbnails.Maxres.Url;
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
                form1.Invoke((MethodInvoker)delegate
                {
                    form1.playlistListBox.Clear();
                    form1.playlistListBox.Columns.Add(" ", 250);
                    form1.playlistListBox.View = View.Details;

                    ImageList thumbnailImageList = new ImageList();
                    thumbnailImageList.ImageSize = new Size(130, 85);

                    foreach (var playlistItem in playlistsToAdd)
                    {
                        thumbnailImageList.Images.Add(playlistItem.Image);

                        ListViewItem item = new ListViewItem(playlistItem.Title);
                        item.ImageIndex = thumbnailImageList.Images.Count - 1;
                        item.Tag = playlistItem.Id;

                        form1.playlistListBox.Items.Add(item);
                    }
                    //컬럼 숨기기
                    form1.playlistListBox.HeaderStyle = ColumnHeaderStyle.None;
                    form1.playlistListBox.SmallImageList = thumbnailImageList;
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show($"플레이리스트 가져오기 중 오류가 발생했습니다: {ex.Message}");
            }
        }


        //thumbnail 가져오기
        internal async Task<Image> GetImageFromUrl(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    Image play_thumb = Image.FromStream(stream);
                    return play_thumb;
                }
            }
        }


    }
}
