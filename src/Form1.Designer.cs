﻿namespace YTMusicWidget
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2VScrollBar1 = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.Inplay_playlist = new System.Windows.Forms.ListView();
            this.Main = new Guna.UI2.WinForms.Guna2GroupBox();
            this.main_content = new Guna.UI2.WinForms.Guna2GroupBox();
            this.playlist_music_loading = new Guna.UI2.WinForms.Guna2PictureBox();
            this.Music_Controller = new Guna.UI2.WinForms.Guna2GroupBox();
            this.Music_player_hide = new Guna.UI2.WinForms.Guna2Button();
            this.Music_Volume_Bar = new Guna.UI2.WinForms.Guna2TrackBar();
            this.Music_Next_Button = new Guna.UI2.WinForms.Guna2GradientButton();
            this.Music_Play_Pause_Button = new Guna.UI2.WinForms.Guna2CircleButton();
            this.Music_Before_Button = new Guna.UI2.WinForms.Guna2GradientButton();
            this.Music_ProgressBar = new Guna.UI2.WinForms.Guna2TrackBar();
            this.Music_Image = new Guna.UI2.WinForms.Guna2PictureBox();
            this.music_player = new CefSharp.WinForms.ChromiumWebBrowser();
            this.change_pos_com_playlist = new System.Windows.Forms.Label();
            this.change_pos_playlist = new System.Windows.Forms.Label();
            this.Music_player_visible = new Guna.UI2.WinForms.Guna2Button();
            this.Mus_page_label = new System.Windows.Forms.Label();
            this.Next_page_mus = new Guna.UI2.WinForms.Guna2Button();
            this.Before_page_mus = new Guna.UI2.WinForms.Guna2Button();
            this.playlist_music_label = new System.Windows.Forms.Label();
            this.playlist_label = new System.Windows.Forms.Label();
            this.playlistListBox_ScrollBar = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.playlistListBox = new System.Windows.Forms.ListView();
            this.playlist_music_list_ScrollBar = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.playlist_music_list = new System.Windows.Forms.ListView();
            this.Login_Button = new Guna.UI2.WinForms.Guna2Button();
            this.Logout_label = new System.Windows.Forms.Label();
            this.Login_com_label = new System.Windows.Forms.Label();
            this.pos_complete = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pos_change = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.Main_Background = new System.Windows.Forms.PictureBox();
            this.guna2VScrollBar2 = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.Inplay_playlist_loading = new Guna.UI2.WinForms.Guna2PictureBox();
            this.Main.SuspendLayout();
            this.main_content.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playlist_music_loading)).BeginInit();
            this.Music_Controller.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Music_Image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Main_Background)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inplay_playlist_loading)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.AnimateWindow = true;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockForm = false;
            this.guna2BorderlessForm1.DockIndicatorColor = System.Drawing.Color.LightGray;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.ResizeForm = false;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2VScrollBar1
            // 
            this.guna2VScrollBar1.AutoRoundedCorners = true;
            this.guna2VScrollBar1.AutoScroll = true;
            this.guna2VScrollBar1.BorderRadius = 8;
            this.guna2VScrollBar1.FillColor = System.Drawing.Color.Black;
            this.guna2VScrollBar1.InUpdate = false;
            this.guna2VScrollBar1.LargeChange = 10;
            this.guna2VScrollBar1.Location = new System.Drawing.Point(270, 38);
            this.guna2VScrollBar1.Name = "guna2VScrollBar1";
            this.guna2VScrollBar1.ScrollbarSize = 18;
            this.guna2VScrollBar1.Size = new System.Drawing.Size(18, 338);
            this.guna2VScrollBar1.TabIndex = 7;
            this.guna2VScrollBar1.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.guna2VScrollBar1.ThumbSize = 5F;
            this.guna2VScrollBar1.ThumbStyle = Guna.UI2.WinForms.Enums.ThumbStyle.Inset;
            // 
            // Inplay_playlist
            // 
            this.Inplay_playlist.AutoArrange = false;
            this.Inplay_playlist.BackColor = System.Drawing.Color.Black;
            this.Inplay_playlist.HideSelection = false;
            this.Inplay_playlist.Location = new System.Drawing.Point(584, 0);
            this.Inplay_playlist.Name = "Inplay_playlist";
            this.Inplay_playlist.Size = new System.Drawing.Size(304, 450);
            this.Inplay_playlist.TabIndex = 7;
            this.Inplay_playlist.UseCompatibleStateImageBehavior = false;
            // 
            // Main
            // 
            this.Main.BackColor = System.Drawing.Color.Black;
            this.Main.BorderColor = System.Drawing.Color.Black;
            this.Main.Controls.Add(this.main_content);
            this.Main.Controls.Add(this.Login_Button);
            this.Main.Controls.Add(this.Logout_label);
            this.Main.Controls.Add(this.Login_com_label);
            this.Main.Controls.Add(this.pos_complete);
            this.Main.Controls.Add(this.pictureBox1);
            this.Main.Controls.Add(this.pos_change);
            this.Main.Controls.Add(this.Title);
            this.Main.Controls.Add(this.Main_Background);
            this.Main.CustomBorderThickness = new System.Windows.Forms.Padding(0);
            this.Main.FillColor = System.Drawing.Color.Black;
            this.Main.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Main.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.Main.Location = new System.Drawing.Point(0, 0);
            this.Main.Name = "Main";
            this.Main.Size = new System.Drawing.Size(586, 450);
            this.Main.TabIndex = 6;
            // 
            // main_content
            // 
            this.main_content.BorderColor = System.Drawing.Color.Black;
            this.main_content.Controls.Add(this.Music_Controller);
            this.main_content.Controls.Add(this.playlist_music_loading);
            this.main_content.Controls.Add(this.change_pos_com_playlist);
            this.main_content.Controls.Add(this.change_pos_playlist);
            this.main_content.Controls.Add(this.Music_player_visible);
            this.main_content.Controls.Add(this.Mus_page_label);
            this.main_content.Controls.Add(this.Next_page_mus);
            this.main_content.Controls.Add(this.Before_page_mus);
            this.main_content.Controls.Add(this.playlist_music_label);
            this.main_content.Controls.Add(this.playlist_label);
            this.main_content.Controls.Add(this.playlistListBox_ScrollBar);
            this.main_content.Controls.Add(this.playlistListBox);
            this.main_content.Controls.Add(this.playlist_music_list_ScrollBar);
            this.main_content.Controls.Add(this.playlist_music_list);
            this.main_content.CustomBorderThickness = new System.Windows.Forms.Padding(0);
            this.main_content.FillColor = System.Drawing.Color.Black;
            this.main_content.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.main_content.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.main_content.Location = new System.Drawing.Point(0, 0);
            this.main_content.Name = "main_content";
            this.main_content.Size = new System.Drawing.Size(583, 450);
            this.main_content.TabIndex = 7;
            this.main_content.Visible = false;
            // 
            // playlist_music_loading
            // 
            this.playlist_music_loading.AutoRoundedCorners = true;
            this.playlist_music_loading.BackColor = System.Drawing.Color.Transparent;
            this.playlist_music_loading.BorderRadius = 31;
            this.playlist_music_loading.FillColor = System.Drawing.Color.Transparent;
            this.playlist_music_loading.Image = global::YTMusicWidget.Properties.Resources.Animation___1716430543043;
            this.playlist_music_loading.ImageRotate = 0F;
            this.playlist_music_loading.Location = new System.Drawing.Point(395, 167);
            this.playlist_music_loading.Name = "playlist_music_loading";
            this.playlist_music_loading.Size = new System.Drawing.Size(68, 65);
            this.playlist_music_loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.playlist_music_loading.TabIndex = 22;
            this.playlist_music_loading.TabStop = false;
            this.playlist_music_loading.UseTransparentBackground = true;
            this.playlist_music_loading.Visible = false;
            // 
            // Music_Controller
            // 
            this.Music_Controller.Controls.Add(this.Music_player_hide);
            this.Music_Controller.Controls.Add(this.Music_Volume_Bar);
            this.Music_Controller.Controls.Add(this.Music_Next_Button);
            this.Music_Controller.Controls.Add(this.Music_Play_Pause_Button);
            this.Music_Controller.Controls.Add(this.Music_Before_Button);
            this.Music_Controller.Controls.Add(this.Music_ProgressBar);
            this.Music_Controller.Controls.Add(this.Music_Image);
            this.Music_Controller.Controls.Add(this.music_player);
            this.Music_Controller.CustomBorderColor = System.Drawing.Color.Black;
            this.Music_Controller.CustomBorderThickness = new System.Windows.Forms.Padding(0);
            this.Music_Controller.FillColor = System.Drawing.Color.Black;
            this.Music_Controller.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Music_Controller.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.Music_Controller.Location = new System.Drawing.Point(3, 3);
            this.Music_Controller.Name = "Music_Controller";
            this.Music_Controller.Size = new System.Drawing.Size(887, 450);
            this.Music_Controller.TabIndex = 15;
            this.Music_Controller.Text = "guna2GroupBox1";
            this.Music_Controller.Visible = false;
            // 
            // Music_player_hide
            // 
            this.Music_player_hide.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Music_player_hide.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Music_player_hide.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Music_player_hide.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Music_player_hide.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Music_player_hide.ForeColor = System.Drawing.Color.White;
            this.Music_player_hide.Location = new System.Drawing.Point(0, 0);
            this.Music_player_hide.Name = "Music_player_hide";
            this.Music_player_hide.Size = new System.Drawing.Size(71, 35);
            this.Music_player_hide.TabIndex = 19;
            this.Music_player_hide.Text = "숨기기";
            // 
            // Music_Volume_Bar
            // 
            this.Music_Volume_Bar.Location = new System.Drawing.Point(31, 234);
            this.Music_Volume_Bar.Name = "Music_Volume_Bar";
            this.Music_Volume_Bar.Size = new System.Drawing.Size(128, 28);
            this.Music_Volume_Bar.TabIndex = 20;
            this.Music_Volume_Bar.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(113)))), ((int)(((byte)(255)))));
            // 
            // Music_Next_Button
            // 
            this.Music_Next_Button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Music_Next_Button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Music_Next_Button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Music_Next_Button.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Music_Next_Button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Music_Next_Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Music_Next_Button.ForeColor = System.Drawing.Color.White;
            this.Music_Next_Button.Location = new System.Drawing.Point(341, 331);
            this.Music_Next_Button.Name = "Music_Next_Button";
            this.Music_Next_Button.Size = new System.Drawing.Size(80, 34);
            this.Music_Next_Button.TabIndex = 16;
            this.Music_Next_Button.Text = "guna2GradientButton1";
            // 
            // Music_Play_Pause_Button
            // 
            this.Music_Play_Pause_Button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Music_Play_Pause_Button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Music_Play_Pause_Button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Music_Play_Pause_Button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Music_Play_Pause_Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Music_Play_Pause_Button.ForeColor = System.Drawing.Color.White;
            this.Music_Play_Pause_Button.Location = new System.Drawing.Point(251, 320);
            this.Music_Play_Pause_Button.Name = "Music_Play_Pause_Button";
            this.Music_Play_Pause_Button.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.Music_Play_Pause_Button.Size = new System.Drawing.Size(56, 55);
            this.Music_Play_Pause_Button.TabIndex = 15;
            this.Music_Play_Pause_Button.Text = "guna2CircleButton1";
            // 
            // Music_Before_Button
            // 
            this.Music_Before_Button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Music_Before_Button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Music_Before_Button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Music_Before_Button.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Music_Before_Button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Music_Before_Button.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.Music_Before_Button.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Music_Before_Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Music_Before_Button.ForeColor = System.Drawing.Color.White;
            this.Music_Before_Button.Location = new System.Drawing.Point(150, 331);
            this.Music_Before_Button.Name = "Music_Before_Button";
            this.Music_Before_Button.Size = new System.Drawing.Size(80, 34);
            this.Music_Before_Button.TabIndex = 17;
            this.Music_Before_Button.Text = "guna2GradientButton2";
            // 
            // Music_ProgressBar
            // 
            this.Music_ProgressBar.Location = new System.Drawing.Point(131, 268);
            this.Music_ProgressBar.Name = "Music_ProgressBar";
            this.Music_ProgressBar.Size = new System.Drawing.Size(300, 31);
            this.Music_ProgressBar.TabIndex = 14;
            this.Music_ProgressBar.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(113)))), ((int)(((byte)(255)))));
            // 
            // Music_Image
            // 
            this.Music_Image.ImageRotate = 0F;
            this.Music_Image.Location = new System.Drawing.Point(180, 62);
            this.Music_Image.Name = "Music_Image";
            this.Music_Image.Size = new System.Drawing.Size(200, 200);
            this.Music_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Music_Image.TabIndex = 18;
            this.Music_Image.TabStop = false;
            // 
            // music_player
            // 
            this.music_player.ActivateBrowserOnCreation = false;
            this.music_player.Location = new System.Drawing.Point(141, 62);
            this.music_player.Name = "music_player";
            this.music_player.Size = new System.Drawing.Size(280, 200);
            this.music_player.TabIndex = 13;
            this.music_player.Visible = false;
            // 
            // change_pos_com_playlist
            // 
            this.change_pos_com_playlist.AutoSize = true;
            this.change_pos_com_playlist.BackColor = System.Drawing.Color.Transparent;
            this.change_pos_com_playlist.Font = new System.Drawing.Font("YouTube Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.change_pos_com_playlist.ForeColor = System.Drawing.Color.White;
            this.change_pos_com_playlist.Location = new System.Drawing.Point(30, 0);
            this.change_pos_com_playlist.Name = "change_pos_com_playlist";
            this.change_pos_com_playlist.Size = new System.Drawing.Size(33, 19);
            this.change_pos_com_playlist.TabIndex = 11;
            this.change_pos_com_playlist.Text = "완료";
            this.change_pos_com_playlist.Visible = false;
            // 
            // change_pos_playlist
            // 
            this.change_pos_playlist.AutoSize = true;
            this.change_pos_playlist.BackColor = System.Drawing.Color.Transparent;
            this.change_pos_playlist.Font = new System.Drawing.Font("YouTube Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.change_pos_playlist.ForeColor = System.Drawing.Color.White;
            this.change_pos_playlist.Location = new System.Drawing.Point(16, 0);
            this.change_pos_playlist.Name = "change_pos_playlist";
            this.change_pos_playlist.Size = new System.Drawing.Size(73, 19);
            this.change_pos_playlist.TabIndex = 10;
            this.change_pos_playlist.Text = "위치 옮기기";
            // 
            // Music_player_visible
            // 
            this.Music_player_visible.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Music_player_visible.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Music_player_visible.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Music_player_visible.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Music_player_visible.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Music_player_visible.ForeColor = System.Drawing.Color.White;
            this.Music_player_visible.Location = new System.Drawing.Point(232, 0);
            this.Music_player_visible.Name = "Music_player_visible";
            this.Music_player_visible.Size = new System.Drawing.Size(112, 31);
            this.Music_player_visible.TabIndex = 16;
            this.Music_player_visible.Text = "재생 중인 음악";
            this.Music_player_visible.Visible = false;
            this.Music_player_visible.Click += new System.EventHandler(this.Music_player_visible_Click);
            // 
            // Mus_page_label
            // 
            this.Mus_page_label.AutoSize = true;
            this.Mus_page_label.Font = new System.Drawing.Font("YouTube Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mus_page_label.ForeColor = System.Drawing.Color.White;
            this.Mus_page_label.Location = new System.Drawing.Point(427, 395);
            this.Mus_page_label.Name = "Mus_page_label";
            this.Mus_page_label.Size = new System.Drawing.Size(16, 18);
            this.Mus_page_label.TabIndex = 12;
            this.Mus_page_label.Text = "0";
            // 
            // Next_page_mus
            // 
            this.Next_page_mus.AutoRoundedCorners = true;
            this.Next_page_mus.BorderRadius = 12;
            this.Next_page_mus.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Next_page_mus.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Next_page_mus.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Next_page_mus.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Next_page_mus.Enabled = false;
            this.Next_page_mus.FillColor = System.Drawing.Color.Black;
            this.Next_page_mus.Font = new System.Drawing.Font("YouTube Sans", 11F);
            this.Next_page_mus.ForeColor = System.Drawing.Color.White;
            this.Next_page_mus.Location = new System.Drawing.Point(473, 393);
            this.Next_page_mus.Name = "Next_page_mus";
            this.Next_page_mus.Size = new System.Drawing.Size(87, 26);
            this.Next_page_mus.TabIndex = 11;
            this.Next_page_mus.Text = "다음";
            // 
            // Before_page_mus
            // 
            this.Before_page_mus.AutoRoundedCorners = true;
            this.Before_page_mus.BorderRadius = 12;
            this.Before_page_mus.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Before_page_mus.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Before_page_mus.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Before_page_mus.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Before_page_mus.Enabled = false;
            this.Before_page_mus.FillColor = System.Drawing.Color.Black;
            this.Before_page_mus.Font = new System.Drawing.Font("YouTube Sans", 11F);
            this.Before_page_mus.ForeColor = System.Drawing.Color.White;
            this.Before_page_mus.Location = new System.Drawing.Point(313, 391);
            this.Before_page_mus.Name = "Before_page_mus";
            this.Before_page_mus.Size = new System.Drawing.Size(87, 26);
            this.Before_page_mus.TabIndex = 9;
            this.Before_page_mus.Text = "이전";
            // 
            // playlist_music_label
            // 
            this.playlist_music_label.AutoSize = true;
            this.playlist_music_label.Font = new System.Drawing.Font("YouTube Sans", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playlist_music_label.ForeColor = System.Drawing.Color.White;
            this.playlist_music_label.Location = new System.Drawing.Point(378, 6);
            this.playlist_music_label.Name = "playlist_music_label";
            this.playlist_music_label.Size = new System.Drawing.Size(113, 29);
            this.playlist_music_label.TabIndex = 5;
            this.playlist_music_label.Text = "음악 리스트";
            // 
            // playlist_label
            // 
            this.playlist_label.AutoSize = true;
            this.playlist_label.Font = new System.Drawing.Font("YouTube Sans", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playlist_label.ForeColor = System.Drawing.Color.White;
            this.playlist_label.Location = new System.Drawing.Point(91, 5);
            this.playlist_label.Name = "playlist_label";
            this.playlist_label.Size = new System.Drawing.Size(127, 29);
            this.playlist_label.TabIndex = 2;
            this.playlist_label.Text = "플레이리스트";
            // 
            // playlistListBox_ScrollBar
            // 
            this.playlistListBox_ScrollBar.BindingContainer = this.playlistListBox;
            this.playlistListBox_ScrollBar.FillColor = System.Drawing.Color.Gray;
            this.playlistListBox_ScrollBar.InUpdate = false;
            this.playlistListBox_ScrollBar.LargeChange = 3;
            this.playlistListBox_ScrollBar.Location = new System.Drawing.Point(273, 33);
            this.playlistListBox_ScrollBar.Name = "playlistListBox_ScrollBar";
            this.playlistListBox_ScrollBar.ScrollbarSize = 18;
            this.playlistListBox_ScrollBar.Size = new System.Drawing.Size(18, 355);
            this.playlistListBox_ScrollBar.TabIndex = 1;
            this.playlistListBox_ScrollBar.ThumbColor = System.Drawing.Color.Black;
            this.playlistListBox_ScrollBar.ThumbStyle = Guna.UI2.WinForms.Enums.ThumbStyle.Inset;
            // 
            // playlistListBox
            // 
            this.playlistListBox.BackColor = System.Drawing.Color.Black;
            this.playlistListBox.ForeColor = System.Drawing.Color.White;
            this.playlistListBox.HideSelection = false;
            this.playlistListBox.Location = new System.Drawing.Point(12, 37);
            this.playlistListBox.MultiSelect = false;
            this.playlistListBox.Name = "playlistListBox";
            this.playlistListBox.Size = new System.Drawing.Size(279, 355);
            this.playlistListBox.TabIndex = 18;
            this.playlistListBox.UseCompatibleStateImageBehavior = false;
            // 
            // playlist_music_list_ScrollBar
            // 
            this.playlist_music_list_ScrollBar.BindingContainer = this.playlist_music_list;
            this.playlist_music_list_ScrollBar.FillColor = System.Drawing.Color.Gray;
            this.playlist_music_list_ScrollBar.InUpdate = false;
            this.playlist_music_list_ScrollBar.LargeChange = 10;
            this.playlist_music_list_ScrollBar.Location = new System.Drawing.Point(558, 37);
            this.playlist_music_list_ScrollBar.Name = "playlist_music_list_ScrollBar";
            this.playlist_music_list_ScrollBar.ScrollbarSize = 18;
            this.playlist_music_list_ScrollBar.Size = new System.Drawing.Size(18, 355);
            this.playlist_music_list_ScrollBar.TabIndex = 20;
            this.playlist_music_list_ScrollBar.ThumbColor = System.Drawing.Color.Black;
            this.playlist_music_list_ScrollBar.ThumbStyle = Guna.UI2.WinForms.Enums.ThumbStyle.Inset;
            // 
            // playlist_music_list
            // 
            this.playlist_music_list.BackColor = System.Drawing.Color.Black;
            this.playlist_music_list.ForeColor = System.Drawing.Color.White;
            this.playlist_music_list.HideSelection = false;
            this.playlist_music_list.Location = new System.Drawing.Point(297, 37);
            this.playlist_music_list.MultiSelect = false;
            this.playlist_music_list.Name = "playlist_music_list";
            this.playlist_music_list.Size = new System.Drawing.Size(279, 355);
            this.playlist_music_list.TabIndex = 17;
            this.playlist_music_list.UseCompatibleStateImageBehavior = false;
            // 
            // Login_Button
            // 
            this.Login_Button.AutoRoundedCorners = true;
            this.Login_Button.BackColor = System.Drawing.Color.Transparent;
            this.Login_Button.BorderRadius = 21;
            this.Login_Button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Login_Button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Login_Button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Login_Button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Login_Button.FillColor = System.Drawing.Color.Transparent;
            this.Login_Button.Font = new System.Drawing.Font("YouTube Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_Button.ForeColor = System.Drawing.Color.White;
            this.Login_Button.Location = new System.Drawing.Point(227, 343);
            this.Login_Button.Name = "Login_Button";
            this.Login_Button.ShadowDecoration.Color = System.Drawing.Color.IndianRed;
            this.Login_Button.Size = new System.Drawing.Size(120, 45);
            this.Login_Button.TabIndex = 8;
            this.Login_Button.Text = "Login";
            this.Login_Button.Click += new System.EventHandler(this.Login_Button_Click);
            // 
            // Logout_label
            // 
            this.Logout_label.AutoSize = true;
            this.Logout_label.BackColor = System.Drawing.Color.Transparent;
            this.Logout_label.Font = new System.Drawing.Font("YouTube Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Logout_label.ForeColor = System.Drawing.Color.White;
            this.Logout_label.Location = new System.Drawing.Point(260, 391);
            this.Logout_label.Name = "Logout_label";
            this.Logout_label.Size = new System.Drawing.Size(50, 18);
            this.Logout_label.TabIndex = 1;
            this.Logout_label.Text = "Logout";
            this.Logout_label.Visible = false;
            this.Logout_label.Click += new System.EventHandler(this.Logout_label_Click);
            // 
            // Login_com_label
            // 
            this.Login_com_label.AutoSize = true;
            this.Login_com_label.BackColor = System.Drawing.Color.Transparent;
            this.Login_com_label.Font = new System.Drawing.Font("YouTube Sans", 18F);
            this.Login_com_label.ForeColor = System.Drawing.Color.White;
            this.Login_com_label.Location = new System.Drawing.Point(222, 320);
            this.Login_com_label.Name = "Login_com_label";
            this.Login_com_label.Size = new System.Drawing.Size(137, 29);
            this.Login_com_label.TabIndex = 3;
            this.Login_com_label.Text = "님, 환영합니다";
            this.Login_com_label.Visible = false;
            this.Login_com_label.Click += new System.EventHandler(this.Login_com_label_Click);
            // 
            // pos_complete
            // 
            this.pos_complete.AutoSize = true;
            this.pos_complete.BackColor = System.Drawing.Color.Transparent;
            this.pos_complete.Font = new System.Drawing.Font("YouTube Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pos_complete.ForeColor = System.Drawing.Color.White;
            this.pos_complete.Location = new System.Drawing.Point(32, 28);
            this.pos_complete.Name = "pos_complete";
            this.pos_complete.Size = new System.Drawing.Size(33, 19);
            this.pos_complete.TabIndex = 5;
            this.pos_complete.Text = "완료";
            this.pos_complete.Visible = false;
            this.pos_complete.Click += new System.EventHandler(this.pos_complete_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::YTMusicWidget.Properties.Resources.ytmusic_logo_removebg_preview;
            this.pictureBox1.Location = new System.Drawing.Point(215, 149);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pos_change
            // 
            this.pos_change.AutoSize = true;
            this.pos_change.BackColor = System.Drawing.Color.Transparent;
            this.pos_change.Font = new System.Drawing.Font("YouTube Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pos_change.ForeColor = System.Drawing.Color.White;
            this.pos_change.Location = new System.Drawing.Point(12, 9);
            this.pos_change.Name = "pos_change";
            this.pos_change.Size = new System.Drawing.Size(73, 19);
            this.pos_change.TabIndex = 0;
            this.pos_change.Text = "위치 옮기기";
            this.pos_change.Click += new System.EventHandler(this.pos_change_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.Font = new System.Drawing.Font("YouTube Sans", 30F, System.Drawing.FontStyle.Bold);
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(107, 69);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(387, 48);
            this.Title.TabIndex = 0;
            this.Title.Text = "Youtube Music Widget";
            // 
            // Main_Background
            // 
            this.Main_Background.Image = global::YTMusicWidget.Properties.Resources.revised_main;
            this.Main_Background.InitialImage = null;
            this.Main_Background.Location = new System.Drawing.Point(0, 3);
            this.Main_Background.Name = "Main_Background";
            this.Main_Background.Size = new System.Drawing.Size(586, 447);
            this.Main_Background.TabIndex = 9;
            this.Main_Background.TabStop = false;
            // 
            // guna2VScrollBar2
            // 
            this.guna2VScrollBar2.BindingContainer = this.Inplay_playlist;
            this.guna2VScrollBar2.FillColor = System.Drawing.Color.Gray;
            this.guna2VScrollBar2.InUpdate = false;
            this.guna2VScrollBar2.LargeChange = 3;
            this.guna2VScrollBar2.Location = new System.Drawing.Point(864, 3);
            this.guna2VScrollBar2.Name = "guna2VScrollBar2";
            this.guna2VScrollBar2.ScrollbarSize = 23;
            this.guna2VScrollBar2.Size = new System.Drawing.Size(23, 447);
            this.guna2VScrollBar2.TabIndex = 21;
            this.guna2VScrollBar2.ThumbColor = System.Drawing.Color.Black;
            this.guna2VScrollBar2.ThumbStyle = Guna.UI2.WinForms.Enums.ThumbStyle.Inset;
            // 
            // Inplay_playlist_loading
            // 
            this.Inplay_playlist_loading.AutoRoundedCorners = true;
            this.Inplay_playlist_loading.BackColor = System.Drawing.Color.Transparent;
            this.Inplay_playlist_loading.BorderRadius = 31;
            this.Inplay_playlist_loading.FillColor = System.Drawing.Color.Transparent;
            this.Inplay_playlist_loading.Image = global::YTMusicWidget.Properties.Resources.Animation___1716430543043;
            this.Inplay_playlist_loading.ImageRotate = 0F;
            this.Inplay_playlist_loading.Location = new System.Drawing.Point(691, 175);
            this.Inplay_playlist_loading.Name = "Inplay_playlist_loading";
            this.Inplay_playlist_loading.Size = new System.Drawing.Size(68, 65);
            this.Inplay_playlist_loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Inplay_playlist_loading.TabIndex = 23;
            this.Inplay_playlist_loading.TabStop = false;
            this.Inplay_playlist_loading.UseTransparentBackground = true;
            this.Inplay_playlist_loading.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 450);
            this.Controls.Add(this.Inplay_playlist_loading);
            this.Controls.Add(this.guna2VScrollBar2);
            this.Controls.Add(this.Inplay_playlist);
            this.Controls.Add(this.Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Main.ResumeLayout(false);
            this.Main.PerformLayout();
            this.main_content.ResumeLayout(false);
            this.main_content.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playlist_music_loading)).EndInit();
            this.Music_Controller.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Music_Image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Main_Background)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inplay_playlist_loading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2GroupBox Main;
        private System.Windows.Forms.Label pos_complete;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label pos_change;
        private System.Windows.Forms.Label Title;
        internal System.Windows.Forms.Label Login_com_label;
        private System.Windows.Forms.Label Logout_label;
        internal Guna.UI2.WinForms.Guna2GroupBox main_content;
        private System.Windows.Forms.Label playlist_label;
        private System.Windows.Forms.Label playlist_music_label;
        private Guna.UI2.WinForms.Guna2VScrollBar guna2VScrollBar1;
        public Guna.UI2.WinForms.Guna2Button Login_Button;
        public Guna.UI2.WinForms.Guna2Button Next_page_mus;
        public Guna.UI2.WinForms.Guna2Button Before_page_mus;
        public System.Windows.Forms.Label Mus_page_label;
        internal CefSharp.WinForms.ChromiumWebBrowser music_player;
        public Guna.UI2.WinForms.Guna2GroupBox Music_Controller;
        internal Guna.UI2.WinForms.Guna2TrackBar Music_ProgressBar;
        internal Guna.UI2.WinForms.Guna2GradientButton Music_Before_Button;
        internal Guna.UI2.WinForms.Guna2GradientButton Music_Next_Button;
        internal Guna.UI2.WinForms.Guna2CircleButton Music_Play_Pause_Button;
        internal Guna.UI2.WinForms.Guna2PictureBox Music_Image;
        public Guna.UI2.WinForms.Guna2Button Music_player_hide;
        public Guna.UI2.WinForms.Guna2Button Music_player_visible;
        private Guna.UI2.WinForms.Guna2TrackBar Music_Volume_Bar;
        internal System.Windows.Forms.ListView Inplay_playlist;
        internal System.Windows.Forms.ListView playlist_music_list;
        internal System.Windows.Forms.ListView playlistListBox;
        private System.Windows.Forms.PictureBox Main_Background;
        private System.Windows.Forms.Label change_pos_com_playlist;
        private System.Windows.Forms.Label change_pos_playlist;
        private Guna.UI2.WinForms.Guna2VScrollBar playlistListBox_ScrollBar;
        public Guna.UI2.WinForms.Guna2VScrollBar playlist_music_list_ScrollBar;
        public Guna.UI2.WinForms.Guna2PictureBox playlist_music_loading;
        public Guna.UI2.WinForms.Guna2PictureBox Inplay_playlist_loading;
        private Guna.UI2.WinForms.Guna2VScrollBar guna2VScrollBar2;
    }
}

