using CefSharp;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static YTMusicWidget.Form1;

namespace YTMusicWidget.src
{
    internal class Internal_player
    {
        private readonly Form1 form1;

        //DI를 위한 클래스 생성
        public Internal_player(Form1 form1)
        {
            this.form1 = form1;
            form1.Music_ProgressBar.Scroll += (sender, e) => Music_ProgressBar_Scroll(sender, e);
            form1.Music_player_hide.Click += (sender, e) => Music_player_hide_Click(sender, e);
            form1.Music_Play_Pause_Button.Click += (sender, e) => Music_Play_Pause_Button_Click(sender, e);
            form1.Music_Next_Button.Click += (sender, e) => Music_Next_Button_Click(sender, e);
            form1.Music_Before_Button.Click += (sender, e) => Music_Before_Button_Click(sender, e);
        }


        private void Music_player_hide_Click(object sender, EventArgs e)
        {
            form1.Music_Controller.Visible = false;
        }

        private async void Music_Play_Pause_Button_Click(object sender, EventArgs e)
        {
            var script = "player.getPlayerState() == YT.PlayerState.PLAYING;";
            var result = await form1.music_player.EvaluateScriptAsync(script);

            // 결과 확인
            if ((bool)result.Result)
            {
                form1.music_player.ExecuteScriptAsync("player.pauseVideo();");
            }
            else
            {
                form1.music_player.ExecuteScriptAsync("player.playVideo();");
            }
        }



        private async void Music_Next_Button_Click(object sender, EventArgs e)
        {

        }

        private async void Music_Before_Button_Click(object sender, EventArgs e)
        {

        }


        //send scroll bar value to iframe player
        private async void Music_ProgressBar_Scroll(object sender, ScrollEventArgs e)
        {
            int sliderValue = form1.Music_ProgressBar.Value;
            await form1.music_player.EvaluateScriptAsync($"player.seekTo({sliderValue}, true);");
        }


        //update progress bar for each second
        public async void UpdateVideoProgress()
        {
            while (true)
            {
                var result = await form1.music_player.GetMainFrame().EvaluateScriptAsync("player.getCurrentTime();");
                form1.Music_ProgressBar.Value = Convert.ToInt32(result.Result);
                await Task.Delay(1000);
            }
        }

        //bring playlists into internal player Inplay_playlist
        internal async void internal_playlist(List<Playlist_Music_Items> playlist, String sel_videoid)
        {
            // Inplay_playlist에 현재 선택된 음악과 전처리된 플레이리스트 추가
            // 예시로 현재 선택된 음악을 가장 먼저 추가하고 나머지 플레이리스트를 추가하기
            // imagelist 생성하고 image 넣어야 한다
            // 현재 list에는 string 형식만 가능
            List<string> songs = new List<string>();
            //form1.Inplay_playlist.Add(selectedMusic.VideoId); // 현재 선택된 음악 추가
            // 썸네일 컬럼 추가
            form1.Inplay_playlist.Columns.Add(" ",400);

            // 세부 정보 보기 모드로 변경
            form1.Inplay_playlist.View = View.Details;

            // 이미지를 담을 ImageList 생성
            ImageList thumbnailImageList = new ImageList();
            thumbnailImageList.ImageSize = new Size(180, 101); // 이미지 크기 설정

            // 이미지 추가 및 썸네일 컬럼에 이미지 할당
            foreach (var musicItem in playlist)
            {
                // 썸네일 이미지 ImageList에 추가
                thumbnailImageList.Images.Add(musicItem.Image);

                // ListViewItem 생성
                ListViewItem item = new ListViewItem(musicItem.Title);
                item.ImageIndex = thumbnailImageList.Images.Count - 1; // ImageList에 추가된 이미지의 인덱스 설정

                // ListViewItem을 ListView에 추가
                form1.Inplay_playlist.Items.Add(item);
            }

            // ImageList를 ListView에 연결하여 썸네일 이미지 표시
            form1.Inplay_playlist.SmallImageList = thumbnailImageList;

        }

    }
}
