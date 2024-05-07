using Google.Apis.Auth.OAuth2;
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
using YTMusicWidget.src;

namespace YTMusicWidget
{
    internal class playlist
    {
        private readonly Form1 form1;


        //DI를 위한 클래스 생성
        public playlist(Form1 form1)
        {
            this.form1 = form1;

            form1.playlistListBox.MeasureItem += (sender, e) => Playlist_MeasureItem(sender, e);
            form1.playlistListBox.DrawItem += (sender, e) => Playlist_DrawItem(sender, e);
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
                form1.Invoke((MethodInvoker)delegate
                {
                    foreach (var playlistItem in playlistsToAdd)
                    {
                        form1.playlistListBox.Items.Add(playlistItem);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"플레이리스트 가져오기 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        //썸네일 이미지 수정
        public Image ResizeImage(Image image, Size size)
        {
            Bitmap result = new Bitmap(size.Width, size.Height);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.DrawImage(image, new Rectangle(Point.Empty, size));
            }
            return result;
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
                    return ResizeImage(play_thumb, new Size(177, 100));
                }
            }
        }


        internal void Playlist_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            var listBox = (ListBox)sender;
            var playlistItem = (PlaylistItems)listBox.Items[e.Index];
            e.ItemHeight = playlistItem.thumbheight;
        }

        internal void Playlist_DrawItem(object sender, DrawItemEventArgs e)
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


    }
}
