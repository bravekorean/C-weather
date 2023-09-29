namespace WeatheUtopia
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Rid_text = new System.Windows.Forms.TextBox();
            this.Rpw_text = new System.Windows.Forms.TextBox();
            this.Rname_text = new System.Windows.Forms.TextBox();
            this.Registerbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(423, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "회원가입";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "아이디";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(312, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "비밀번호";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(326, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "이름";
            // 
            // Rid_text
            // 
            this.Rid_text.Location = new System.Drawing.Point(399, 174);
            this.Rid_text.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Rid_text.Name = "Rid_text";
            this.Rid_text.Size = new System.Drawing.Size(114, 25);
            this.Rid_text.TabIndex = 4;
            this.Rid_text.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Rpw_text
            // 
            this.Rpw_text.Location = new System.Drawing.Point(399, 225);
            this.Rpw_text.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Rpw_text.Name = "Rpw_text";
            this.Rpw_text.Size = new System.Drawing.Size(114, 25);
            this.Rpw_text.TabIndex = 5;
            this.Rpw_text.TextChanged += new System.EventHandler(this.Rpw_text_TextChanged);
            // 
            // Rname_text
            // 
            this.Rname_text.Location = new System.Drawing.Point(399, 268);
            this.Rname_text.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Rname_text.Name = "Rname_text";
            this.Rname_text.Size = new System.Drawing.Size(114, 25);
            this.Rname_text.TabIndex = 6;
            this.Rname_text.TextChanged += new System.EventHandler(this.Rname_text_TextChanged);
            // 
            // Registerbtn
            // 
            this.Registerbtn.Location = new System.Drawing.Point(549, 178);
            this.Registerbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Registerbtn.Name = "Registerbtn";
            this.Registerbtn.Size = new System.Drawing.Size(86, 116);
            this.Registerbtn.TabIndex = 7;
            this.Registerbtn.Text = "회원가입";
            this.Registerbtn.UseVisualStyleBackColor = true;
            this.Registerbtn.Click += new System.EventHandler(this.Registerbtn_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 562);
            this.Controls.Add(this.Registerbtn);
            this.Controls.Add(this.Rname_text);
            this.Controls.Add(this.Rpw_text);
            this.Controls.Add(this.Rid_text);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form2";
            this.Text = "회원가입";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Rid_text;
        private System.Windows.Forms.TextBox Rpw_text;
        private System.Windows.Forms.TextBox Rname_text;
        private System.Windows.Forms.Button Registerbtn;
    }
}