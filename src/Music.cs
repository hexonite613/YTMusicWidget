using CefSharp;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static YTMusicWidget.Form1;

namespace YTMusicWidget.src
{
    internal class Music
    {
        private readonly Form1 form1;
        private readonly playlist playlist;
        private readonly Internal_player internal_player;
   

        public Music(Form1 form1)
        {
            this.form1 = form1;
            internal_player = new Internal_player(form1);
            playlist = new playlist(form1);



            form1.playlist_music_list.SelectedIndexChanged += playlist_music_list_SelectedIndexChanged;
            form1.playlistListBox.SelectedIndexChanged += playlist_SelectedIndexChangedAsync;


            form1.Next_page_mus.Click += Next_page_mus_Click;
            form1.Before_page_mus.Click += Before_page_mus_Click;


        }



        private List<Playlist_Music_Items> musicitemstoadd = new List<Playlist_Music_Items>();

        internal async Task GetPlaylist_Music(string playlistId, int page)
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
                request.PageToken = (page > 1) ? form1.nextPageToken : null;

                var response = await request.ExecuteAsync();

                // 총 음악 수와 총 페이지 수 업데이트
                form1.totalMusicCount = (int)response.PageInfo.TotalResults;
                form1.totalPageCount = (int)Math.Ceiling((double)form1.totalMusicCount / PageSize);

                // 다음 페이지 토큰 설정
                form1.nextPageToken = response.NextPageToken;



                // 플레이리스트 음악들을 ListBox에 추가
                musicitemstoadd = new List<Playlist_Music_Items>();
                foreach (var item in response.Items)
                {

                    var thumbnailUrl = item.Snippet.Thumbnails.High.Url;
                    
                    var musicImage = await playlist.GetImageFromUrl(thumbnailUrl);


                    var musicItem = new Playlist_Music_Items(item.Snippet.Title, musicImage, item.Snippet.ResourceId.VideoId);

                    musicitemstoadd.Add(musicItem);

                }

                form1.playlist_music_list.Items.Clear();
                form1.playlist_music_list.Columns.Add(" ", 400);
                form1.playlist_music_list.View = View.Details;
                ImageList thumbnailImageList_1 = new ImageList();
                thumbnailImageList_1.ImageSize = new Size(180, 101);
                form1.playlist_music_list.LargeImageList = thumbnailImageList_1;

                foreach (var musicItem in musicitemstoadd)
                {
                    thumbnailImageList_1.Images.Add(musicItem.Image);

                    // ListViewItem 생성
                    ListViewItem item = new ListViewItem();
                    item.Text = musicItem.Title; // 타이틀 설정
                    item.ImageIndex = thumbnailImageList_1.Images.Count - 1;
                    item.Tag = musicItem.VideoId; // VideoId를 문자열로 할당

                    form1.playlist_music_list.Items.Add(item);
                }
                // UI 업데이트를 UI 스레드에서 수행
                form1.Invoke((MethodInvoker)delegate
                {
                    UpdatePageInfo();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"플레이리스트 음악 가져오기 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        //플레이리스트에 따른 음악 목록들 가져오기
        public async void playlist_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            if (form1.playlistListBox.SelectedItem != null)
            {
                form1.selectedPlaylist = (PlaylistItems)form1.playlistListBox.SelectedItem;
                form1.currentPage = 1; // 페이지 초기화
                await GetPlaylist_Music(form1.selectedPlaylist.Id, form1.currentPage);
            }
            else
            {
                MessageBox.Show("플레이리스트가 안 골라짐");
            }
        }



        private void UpdatePageInfo()
        {
            form1.Mus_page_label.Text = $"{form1.currentPage} / {form1.totalPageCount}";
            form1.Before_page_mus.Enabled = (form1.currentPage > 1);
            form1.Next_page_mus.Enabled = (form1.currentPage < form1.totalPageCount);
        }



        //음악 선택시 음악 재생하기
        private void playlist_music_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form1.playlist_music_list.SelectedItems.Count > 0)
            {
                String selectedMusic = form1.playlist_music_list.SelectedItems[0].Tag.ToString();

                // 음악 재생
                PlayMusic(selectedMusic);
                internal_player.internal_playlist(musicitemstoadd, selectedMusic);
                form1.Music_player_visible.Visible = true;
                form1.Music_Controller.Visible = true;
                form1.Music_ProgressBar.Maximum = (int)getVideoLength(selectedMusic);
            }
        }



        private string GetHTMLContent(string videoId, Size videoSize)
        {
            var sb = new StringBuilder(@"
        <html>
        <head>
            <!-- Include the YouTube iframe API script using HTTPS -->
            <script src='https://www.youtube.com/iframe_api'></script>
            <script>
            var player;
            function onYouTubeIframeAPIReady() {
                player = new YT.Player('playerframe', {
                events: {
                    'onReady': onPlayerReady
                },
                videoId: '[VIDEO-ID]',
                height: '[VIDEO-HEIGHT]',
                width: '[VIDEO-WIDTH]',
                playerVars : {
                    'enablejsapi' : 1
                }});
            }

            function PauseVideo() { player.pauseVideo(); }
            function ResumeVideo() { player.playVideo(); }

            function onPlayerReady(event) {
                event.target.playVideo();
            }
            </script>
        </head>
        <body>
            <!-- Element replaced with an IFrame when the YouType Player is initialized -->
            <div id='playerframe'></div>
        </body>
        </html>
    ");

            sb = sb.Replace("[VIDEO-ID]", videoId)
                   .Replace("[VIDEO-HEIGHT]", videoSize.Height.ToString())
                   .Replace("[VIDEO-WIDTH]", videoSize.Width.ToString());

            return sb.ToString();
        }

        private void PlayMusic(string videoId)
        {
            string url = $"https://www.youtube.com/watch?v={videoId}?autoplay=1";
            form1.music_player.Load(url);
            form1.music_player.LoadHtml(GetHTMLContent(videoId, new Size(30, 30)));
            internal_player.UpdateVideoProgress();
        }

        internal double getVideoLength(string videoid)
        { 
            string apiKey = ConfigurationManager.AppSettings["api_key"];

            string videoId = videoid;

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "YouTube Video Length"
            });

            var videoRequest = youtubeService.Videos.List("contentDetails");
            videoRequest.Id = videoId;

            var videoResponse = videoRequest.Execute();
            var video = videoResponse.Items[0];
            var duration = video.ContentDetails.Duration;
            int tot_sec = ConvertISO8601DurationToSeconds(duration);
            return tot_sec;
        }


        //시간 parse
        static int ConvertISO8601DurationToSeconds(string durationString)
        {
            durationString = durationString.ToUpper();
            int totalSeconds = 0;

            int indexOfM = durationString.IndexOf('M');
            int indexOfS = durationString.IndexOf('S');

            if (indexOfM != -1)
            {
                int minutes = int.Parse(durationString.Substring(2, indexOfM - 2)); // "PT" 제외하고 분 추출
                totalSeconds += minutes * 60; // 분을 초로 변환하여 더함
            }

            if (indexOfS != -1)
            {
                int seconds = int.Parse(durationString.Substring(indexOfM + 1, indexOfS - indexOfM - 1));
                totalSeconds += seconds; // 초를 더함
            }

            return totalSeconds;
        }

        //다음 페이지(음악)
        private async void Next_page_mus_Click(object sender, EventArgs e)
        {
            if (form1.currentPage < form1.totalPageCount)
            {
                form1.currentPage++;
                await GetPlaylist_Music(form1.selectedPlaylist.Id, form1.currentPage);
            }
        }


        //이전 페이지(음악)
        private async void Before_page_mus_Click(object sender, EventArgs e)
        {
            if (form1.currentPage > 1)
            {
                form1.currentPage--;
                await GetPlaylist_Music(form1.selectedPlaylist.Id, form1.currentPage);
            }
        }
    }
}
