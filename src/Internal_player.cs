﻿using CefSharp;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static YTMusicWidget.Form1;
using System.Linq;

namespace YTMusicWidget.src
{
    internal class Internal_player
    {
        private readonly Form1 form1;
        private readonly Music music;
        List<Playlist_Music_Items> glob_playlist;

        //DI를 위한 클래스 생성
        public Internal_player(Form1 form1, Music music)

        {
            this.form1 = form1;
            this.music=music;
            form1.Music_ProgressBar.Scroll += (sender, e) => Music_ProgressBar_Scroll(sender, e);
            form1.Music_player_hide.Click += (sender, e) => Music_player_hide_Click(sender, e);
            form1.Music_Play_Pause_Button.Click += (sender, e) => Music_Play_Pause_Button_Click(sender, e);
            form1.Music_Next_Button.Click += (sender, e) => Music_Next_Button_Click(sender, e);
            form1.Music_Before_Button.Click += (sender, e) => Music_Before_Button_Click(sender, e);
            form1.Inplay_playlist.SelectedIndexChanged+= (sender, e) => Inplay_playlist_SelectedIndexChanged(sender, e);


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
        internal void internal_playlist(List<Playlist_Music_Items> playlist, string sel_videoid)
        {
            form1.Inplay_playlist.Clear();
            form1.Inplay_playlist.VirtualListSize = 0;
            glob_playlist = playlist;
            form1.Inplay_playlist.Columns.Add(" ", 400);
            form1.Inplay_playlist.View = View.Details;

            ImageList thumbnailImageList = new ImageList
            {
                ImageSize = new Size(180, 101)
            };

            // 원본 리스트를 변경하지 않도록 복사본 생성
            var playlistCopy = new List<Playlist_Music_Items>(playlist);

            // 선택한 비디오 아이템을 찾아서 리스트의 처음에 추가
            Playlist_Music_Items selectedMusicItem = playlistCopy.FirstOrDefault(item => item.VideoId == sel_videoid);
            if (selectedMusicItem != null)
            {
                thumbnailImageList.Images.Add(selectedMusicItem.VideoId, selectedMusicItem.Image);
                ListViewItem selectedItem = new ListViewItem(selectedMusicItem.Title)
                {
                    ImageIndex = thumbnailImageList.Images.IndexOfKey(selectedMusicItem.VideoId),
                    Tag = selectedMusicItem.VideoId // Tag에 VideoId를 저장
                };
                form1.Inplay_playlist.Items.Add(selectedItem);
                playlistCopy.Remove(selectedMusicItem);
            }

            // 나머지 아이템들을 랜덤으로 섞기
            var random = new Random();
            var shuffledPlaylist = playlistCopy.OrderBy(item => random.Next()).ToList();

            foreach (var musicItem in shuffledPlaylist)
            {
                thumbnailImageList.Images.Add(musicItem.VideoId, musicItem.Image);
                ListViewItem item = new ListViewItem(musicItem.Title)
                {
                    ImageIndex = thumbnailImageList.Images.IndexOfKey(musicItem.VideoId),
                    Tag = musicItem.VideoId // Tag에 VideoId를 저장
                };
                form1.Inplay_playlist.Items.Add(item);
            }

            form1.Inplay_playlist.SmallImageList = thumbnailImageList;
            form1.Inplay_playlist.VirtualListSize = form1.Inplay_playlist.Items.Count;
        }





        private void Inplay_playlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (form1.Inplay_playlist.SelectedItems.Count==1)
                {
                    var selectedItem = form1.Inplay_playlist.SelectedItems[0];
                    
                    string selectedMusicId = selectedItem.Tag.ToString();

                    form1.Music_Image.Image = glob_playlist.FirstOrDefault(item => item.VideoId == selectedMusicId).Image;
                    music.PlayMusic(selectedMusicId);
                    form1.Music_player_visible.Visible = true;
                    form1.Music_Controller.Visible = true;
                    form1.Music_ProgressBar.Maximum = (int)music.getVideoLength(selectedMusicId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Inplay_playlist 항목 선택 중 오류가 발생했습니다: {ex.Message}\n\n{ex.StackTrace}");
            }
        }


    }
}
