using CefSharp;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
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
        private List<Playlist_Music_Items> musicitemstoadd = new List<Playlist_Music_Items>();
        private String selectedplaylist_id;
        private string nextPageToken = null;
        private bool isLoading = false;
        private Dictionary<string, List<Playlist_Music_Items>> musicCache = new Dictionary<string, List<Playlist_Music_Items>>();


        public Music(Form1 form1)
        {
            this.form1 = form1;
            internal_player = new Internal_player(form1);
            playlist = new playlist(form1);


            //리스트 대용량 처리
            form1.playlist_music_list.VirtualMode = true;
            form1.playlist_music_list.VirtualListSize = musicitemstoadd.Count;

            //playlist_music_list 형식 설정
            form1.playlist_music_list.Columns.Add(" ", 250);
            form1.playlist_music_list.View = View.Details;
            form1.playlist_music_list.HeaderStyle = ColumnHeaderStyle.None;


            form1.playlist_music_list.SelectedIndexChanged += playlist_music_list_SelectedIndexChanged;
            form1.playlistListBox.SelectedIndexChanged += playlist_SelectedIndexChangedAsync;
            form1.playlist_music_list.RetrieveVirtualItem += playlist_musiclist_RetrieveVirtualItem;
            form1.playlist_music_list_ScrollBar.Scroll += playlist_musiclist_Scroll;


        }




        internal async Task<string> FetchAndCachePlaylistAsync(string playlistId, string pageToken = null)
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
                request.MaxResults = 20;
                request.PageToken = pageToken;

                var response = await request.ExecuteAsync();
                nextPageToken = response.NextPageToken;

                var newItems = new List<Playlist_Music_Items>();
                foreach (var item in response.Items)
                {
                    var thumbnailUrl = item.Snippet.Thumbnails?.High?.Url;
                    if (thumbnailUrl == null) continue;

                    var musicImage = await playlist.GetImageFromUrl(thumbnailUrl);
                    var musicItem = new Playlist_Music_Items(item.Snippet.Title, musicImage, item.Snippet.ResourceId.VideoId);
                    newItems.Add(musicItem);
                }

                if (!musicCache.ContainsKey(playlistId))
                {
                    musicCache[playlistId] = new List<Playlist_Music_Items>();
                }
                musicCache[playlistId].AddRange(newItems);

                // Update the UI with the new items
                form1.Invoke((MethodInvoker)delegate
                {
                    musicitemstoadd.AddRange(newItems);
                    form1.playlist_music_list.VirtualListSize = musicitemstoadd.Count;

                    ImageList thumbnailImageList = form1.playlist_music_list.SmallImageList ?? new ImageList
                    {
                        ImageSize = new Size(130, 85)
                    };

                    foreach (var musicItem in newItems)
                    {
                        thumbnailImageList.Images.Add(musicItem.Image);
                    }

                    form1.playlist_music_list.SmallImageList = thumbnailImageList;
                    form1.playlist_music_list.Invalidate();
                });

                return nextPageToken; // Return the nextPageToken to continue fetching
            }
            catch (Exception ex)
            {
                MessageBox.Show($"플레이리스트 음악 가져오기 중 오류가 발생했습니다: {ex.Message}\n\n{ex.InnerException?.Message}");
                return null;
            }
        }



        private void playlist_musiclist_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            var musicItem = musicitemstoadd[e.ItemIndex];
            var item = new ListViewItem(musicItem.Title)
                {
                    Tag = musicItem.VideoId,
                    ImageIndex = e.ItemIndex
                };
            e.Item = item;
        }

        private void playlist_musiclist_Scroll(object sender, ScrollEventArgs e)
        {
 
            int visibleItemsCount = form1.playlist_music_list.ClientSize.Height/form1.playlist_music_list.TopItem.Bounds.Height;
            if (form1.playlist_music_list.TopItem.Index + visibleItemsCount >= form1.playlist_music_list.VirtualListSize - 1)
            {
                if (!isLoading && !string.IsNullOrEmpty(nextPageToken))
                {
                    _ =LoadMoreItems();
                }
            }

        }

        private async Task LoadMoreItems()
        {
            try
            {
                isLoading = true;
                form1.playlist_music_loading.Visible = true;
                await FetchAndCacheAllPlaylistItemsAsync(selectedplaylist_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"플레이리스트 음악 가져오기 중 오류가 발생했습니다: {ex.Message}\n\n{ex.InnerException?.Message}");
            }
            finally
            {
                isLoading = false;
                form1.playlist_music_loading.Visible = false;
            }
        }

        internal async Task FetchAndCacheAllPlaylistItemsAsync(string playlistId)
        {
            string pageToken = nextPageToken;
            do
            {
                pageToken = await FetchAndCachePlaylistAsync(playlistId, pageToken);
            } while (!string.IsNullOrEmpty(pageToken));
        }

        private async void LoadPlaylistsInBackground(List<string> playlistIds)
        {
            foreach (var playlistId in playlistIds)
            {
                await FetchAndCachePlaylistAsync(playlistId);
            }
        }




        public async void playlist_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            try
            {
                if (form1.playlistListBox.SelectedItems.Count > 0)
                {
                    form1.playlist_music_list.Clear();
                    form1.playlist_music_list.SmallImageList = null; // 이미지 리스트 초기화
                    /*musicItemCache.Clear(); // 캐시 초기화*/
                    isLoading = true;
                    form1.playlist_music_loading.Visible = true;

                    ListViewItem selectedListViewItem = form1.playlistListBox.SelectedItems[0];
                    string playlistId = selectedListViewItem.Tag.ToString();
                    selectedplaylist_id = playlistId;

                    if (musicCache.ContainsKey(playlistId))
                    {
                        // 캐시에서 데이터 로드
                        musicitemstoadd = musicCache[playlistId];
                        UpdatePlaylistView(musicitemstoadd);
                    }
                    else
                    {
                        // 캐시에 없는 경우 데이터를 가져옴
                        await FetchAndCacheAllPlaylistItemsAsync(playlistId);
                        if (musicCache.ContainsKey(playlistId))
                        {
                            musicitemstoadd = musicCache[playlistId];
                            UpdatePlaylistView(musicitemstoadd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"플레이리스트 선택 처리 중 오류가 발생했습니다: {ex.Message}\n\n{ex.InnerException?.Message}");
            }
            finally
            {
                isLoading = false;
                form1.playlist_music_loading.Visible = false;
            }
        }

        private void UpdatePlaylistView(List<Playlist_Music_Items> musicItems)
        {
            form1.playlist_music_list.VirtualListSize = musicItems.Count;

            ImageList thumbnailImageList = new ImageList
            {
                ImageSize = new Size(130, 85)
            };

            foreach (var musicItem in musicItems)
            {
                thumbnailImageList.Images.Add(musicItem.Image);
            }

            form1.playlist_music_list.SmallImageList = thumbnailImageList;
            form1.playlist_music_list.Invalidate();
        }




        //음악 선택시 음악 재생하기
        private void playlist_music_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form1.playlist_music_list.SelectedIndices.Count > 0)
            {
                int selectedIndex = form1.playlist_music_list.SelectedIndices[0];
                if (selectedIndex >= 0 && selectedIndex < musicitemstoadd.Count)
                {
                    var selectedMusicItem = musicitemstoadd[selectedIndex];
                    var selectedMusicId = selectedMusicItem.VideoId;

                    // 음악 재생
                    PlayMusic(selectedMusicId);
                    form1.Music_Image.Image=musicitemstoadd[selectedIndex].Image;
                    form1.Music_player_visible.Visible = true;
                    /*internal_player.internal_playlist(musicitemstoadd.ToList(), selectedMusicId);*/
                    form1.Music_Controller.Visible = true;
                    form1.Music_ProgressBar.Maximum = (int)getVideoLength(selectedMusicId);

                }
            }
        }



        private string GetHTMLContent(string videoId, Size videoSize)
        {
            var sb = new StringBuilder(@"
        <html>
        <head>
            <!-- Include the YouTube iframe API script using javascript -->
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

        internal void PlayMusic(string videoId)
        {
            form1.Size = new Size(887, 450);
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
    }
}
