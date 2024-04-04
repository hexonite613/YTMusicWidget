namespace YTMusicWidget
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
            this.Main = new Guna.UI2.WinForms.Guna2GroupBox();
            this.main_content = new Guna.UI2.WinForms.Guna2GroupBox();
            this.guna2VScrollBar2 = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.playlist_music_list = new System.Windows.Forms.ListBox();
            this.Mus_page_label = new System.Windows.Forms.Label();
            this.Next_page_mus = new Guna.UI2.WinForms.Guna2Button();
            this.Before_page_mus = new Guna.UI2.WinForms.Guna2Button();
            this.playlist_music_label = new System.Windows.Forms.Label();
            this.playlistListBox = new System.Windows.Forms.ListBox();
            this.playlist_label = new System.Windows.Forms.Label();
            this.Login_Button = new Guna.UI2.WinForms.Guna2Button();
            this.Logout_label = new System.Windows.Forms.Label();
            this.Login_com_label = new System.Windows.Forms.Label();
            this.pos_complete = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pos_change = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.guna2VScrollBar1 = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.music_player = new CefSharp.WinForms.ChromiumWebBrowser();
            this.Main.SuspendLayout();
            this.main_content.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.main_content.Controls.Add(this.music_player);
            this.main_content.Controls.Add(this.guna2VScrollBar2);
            this.main_content.Controls.Add(this.Mus_page_label);
            this.main_content.Controls.Add(this.Next_page_mus);
            this.main_content.Controls.Add(this.Before_page_mus);
            this.main_content.Controls.Add(this.playlist_music_list);
            this.main_content.Controls.Add(this.playlist_music_label);
            this.main_content.Controls.Add(this.playlistListBox);
            this.main_content.Controls.Add(this.playlist_label);
            this.main_content.CustomBorderThickness = new System.Windows.Forms.Padding(0);
            this.main_content.FillColor = System.Drawing.Color.Black;
            this.main_content.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.main_content.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.main_content.Location = new System.Drawing.Point(0, 3);
            this.main_content.Name = "main_content";
            this.main_content.Size = new System.Drawing.Size(586, 450);
            this.main_content.TabIndex = 7;
            this.main_content.Visible = false;
            // 
            // guna2VScrollBar2
            // 
            this.guna2VScrollBar2.AutoRoundedCorners = true;
            this.guna2VScrollBar2.AutoScroll = true;
            this.guna2VScrollBar2.BindingContainer = this.playlist_music_list;
            this.guna2VScrollBar2.BorderRadius = 8;
            this.guna2VScrollBar2.FillColor = System.Drawing.Color.Black;
            this.guna2VScrollBar2.InUpdate = false;
            this.guna2VScrollBar2.LargeChange = 10;
            this.guna2VScrollBar2.Location = new System.Drawing.Point(558, 39);
            this.guna2VScrollBar2.Name = "guna2VScrollBar2";
            this.guna2VScrollBar2.ScrollbarSize = 18;
            this.guna2VScrollBar2.Size = new System.Drawing.Size(18, 347);
            this.guna2VScrollBar2.TabIndex = 8;
            this.guna2VScrollBar2.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.guna2VScrollBar2.ThumbSize = 5F;
            this.guna2VScrollBar2.ThumbStyle = Guna.UI2.WinForms.Enums.ThumbStyle.Inset;
            // 
            // playlist_music_list
            // 
            this.playlist_music_list.BackColor = System.Drawing.Color.Black;
            this.playlist_music_list.ForeColor = System.Drawing.Color.White;
            this.playlist_music_list.FormattingEnabled = true;
            this.playlist_music_list.ItemHeight = 15;
            this.playlist_music_list.Location = new System.Drawing.Point(304, 38);
            this.playlist_music_list.Name = "playlist_music_list";
            this.playlist_music_list.Size = new System.Drawing.Size(273, 349);
            this.playlist_music_list.TabIndex = 6;
            this.playlist_music_list.SelectedIndexChanged += new System.EventHandler(this.playlist_music_list_SelectedIndexChanged);
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
            this.Next_page_mus.Click += new System.EventHandler(this.Next_page_mus_Click);
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
            this.Before_page_mus.Click += new System.EventHandler(this.Before_page_mus_Click);
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
            // playlistListBox
            // 
            this.playlistListBox.BackColor = System.Drawing.Color.Black;
            this.playlistListBox.ForeColor = System.Drawing.Color.White;
            this.playlistListBox.FormattingEnabled = true;
            this.playlistListBox.ItemHeight = 15;
            this.playlistListBox.Location = new System.Drawing.Point(16, 37);
            this.playlistListBox.Name = "playlistListBox";
            this.playlistListBox.Size = new System.Drawing.Size(273, 349);
            this.playlistListBox.TabIndex = 3;
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
            // Login_Button
            // 
            this.Login_Button.AutoRoundedCorners = true;
            this.Login_Button.BorderRadius = 21;
            this.Login_Button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Login_Button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Login_Button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Login_Button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Login_Button.FillColor = System.Drawing.Color.Black;
            this.Login_Button.Font = new System.Drawing.Font("YouTube Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_Button.ForeColor = System.Drawing.Color.White;
            this.Login_Button.Location = new System.Drawing.Point(224, 346);
            this.Login_Button.Name = "Login_Button";
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
            this.Logout_label.Location = new System.Drawing.Point(257, 394);
            this.Logout_label.Name = "Logout_label";
            this.Logout_label.Size = new System.Drawing.Size(50, 18);
            this.Logout_label.TabIndex = 1;
            this.Logout_label.Text = "Logout";
            this.Logout_label.Click += new System.EventHandler(this.Logout_label_Click);
            // 
            // Login_com_label
            // 
            this.Login_com_label.AutoSize = true;
            this.Login_com_label.BackColor = System.Drawing.Color.Transparent;
            this.Login_com_label.Font = new System.Drawing.Font("YouTube Sans", 18F);
            this.Login_com_label.ForeColor = System.Drawing.Color.White;
            this.Login_com_label.Location = new System.Drawing.Point(207, 323);
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
            this.pos_complete.Font = new System.Drawing.Font("YouTube Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pos_complete.ForeColor = System.Drawing.Color.White;
            this.pos_complete.Location = new System.Drawing.Point(32, 9);
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
            this.pictureBox1.Location = new System.Drawing.Point(212, 152);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pos_change
            // 
            this.pos_change.AutoSize = true;
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
            this.Title.Location = new System.Drawing.Point(104, 72);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(387, 48);
            this.Title.TabIndex = 0;
            this.Title.Text = "Youtube Music Widget";
            // 
            // guna2VScrollBar1
            // 
            this.guna2VScrollBar1.AutoRoundedCorners = true;
            this.guna2VScrollBar1.AutoScroll = true;
            this.guna2VScrollBar1.BindingContainer = this.playlistListBox;
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
            // music_player
            // 
            this.music_player.ActivateBrowserOnCreation = false;
            this.music_player.Location = new System.Drawing.Point(112, 71);
            this.music_player.Name = "music_player";
            this.music_player.Size = new System.Drawing.Size(365, 278);
            this.music_player.TabIndex = 13;
            this.music_player.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 450);
            this.Controls.Add(this.Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Main.ResumeLayout(false);
            this.Main.PerformLayout();
            this.main_content.ResumeLayout(false);
            this.main_content.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2GroupBox Main;
        private System.Windows.Forms.Label pos_complete;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label pos_change;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label Login_com_label;
        private System.Windows.Forms.Label Logout_label;
        private Guna.UI2.WinForms.Guna2GroupBox main_content;
        private System.Windows.Forms.Label playlist_label;
        internal System.Windows.Forms.ListBox playlistListBox;
        internal System.Windows.Forms.ListBox playlist_music_list;
        private System.Windows.Forms.Label playlist_music_label;
        private Guna.UI2.WinForms.Guna2VScrollBar guna2VScrollBar1;
        private Guna.UI2.WinForms.Guna2VScrollBar guna2VScrollBar2;
        private Guna.UI2.WinForms.Guna2Button Login_Button;
        private Guna.UI2.WinForms.Guna2Button Next_page_mus;
        private Guna.UI2.WinForms.Guna2Button Before_page_mus;
        private System.Windows.Forms.Label Mus_page_label;
        internal CefSharp.WinForms.ChromiumWebBrowser music_player;
    }
}

