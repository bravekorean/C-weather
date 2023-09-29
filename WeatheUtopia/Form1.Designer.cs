namespace WeatheUtopia
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
            this.Loginbtn = new System.Windows.Forms.Button();
            this.Lid_textbox = new System.Windows.Forms.TextBox();
            this.Lpw_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RegisterBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Loginbtn
            // 
            this.Loginbtn.Location = new System.Drawing.Point(574, 222);
            this.Loginbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Loginbtn.Name = "Loginbtn";
            this.Loginbtn.Size = new System.Drawing.Size(86, 62);
            this.Loginbtn.TabIndex = 0;
            this.Loginbtn.Text = "로그인";
            this.Loginbtn.UseVisualStyleBackColor = true;
            this.Loginbtn.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Lid_textbox
            // 
            this.Lid_textbox.Location = new System.Drawing.Point(345, 222);
            this.Lid_textbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Lid_textbox.Name = "Lid_textbox";
            this.Lid_textbox.Size = new System.Drawing.Size(191, 25);
            this.Lid_textbox.TabIndex = 1;
            this.Lid_textbox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Lpw_textbox
            // 
            this.Lpw_textbox.Location = new System.Drawing.Point(345, 259);
            this.Lpw_textbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Lpw_textbox.Name = "Lpw_textbox";
            this.Lpw_textbox.Size = new System.Drawing.Size(191, 25);
            this.Lpw_textbox.TabIndex = 2;
            this.Lpw_textbox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "아이디 :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "비밀번호 :";
            // 
            // RegisterBtn
            // 
            this.RegisterBtn.Location = new System.Drawing.Point(574, 312);
            this.RegisterBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RegisterBtn.Name = "RegisterBtn";
            this.RegisterBtn.Size = new System.Drawing.Size(86, 29);
            this.RegisterBtn.TabIndex = 6;
            this.RegisterBtn.Text = "회원가입";
            this.RegisterBtn.UseVisualStyleBackColor = true;
            this.RegisterBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(268, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 163);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 562);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.RegisterBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Lpw_textbox);
            this.Controls.Add(this.Lid_textbox);
            this.Controls.Add(this.Loginbtn);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "로그인";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Loginbtn;
        private System.Windows.Forms.TextBox Lid_textbox;
        private System.Windows.Forms.TextBox Lpw_textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RegisterBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

