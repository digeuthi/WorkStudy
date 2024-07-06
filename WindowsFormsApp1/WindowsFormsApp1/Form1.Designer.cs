namespace WindowsFormsApp1
{
    partial class Form
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
            this.Button_disconnect_Click = new System.Windows.Forms.Button();
            this.Button_send_Click = new System.Windows.Forms.Button();
            this.textBox_send = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Button_connect_Click = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_port = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Button_disconnect_Click
            // 
            this.Button_disconnect_Click.Location = new System.Drawing.Point(434, 210);
            this.Button_disconnect_Click.Name = "Button_disconnect_Click";
            this.Button_disconnect_Click.Size = new System.Drawing.Size(138, 68);
            this.Button_disconnect_Click.TabIndex = 0;
            this.Button_disconnect_Click.Text = "연결해제";
            this.Button_disconnect_Click.UseVisualStyleBackColor = true;
            this.Button_disconnect_Click.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Button_send_Click
            // 
            this.Button_send_Click.Location = new System.Drawing.Point(263, 243);
            this.Button_send_Click.Name = "Button_send_Click";
            this.Button_send_Click.Size = new System.Drawing.Size(126, 35);
            this.Button_send_Click.TabIndex = 0;
            this.Button_send_Click.Text = "보내기";
            this.Button_send_Click.UseVisualStyleBackColor = true;
            this.Button_send_Click.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox_send
            // 
            this.textBox_send.Location = new System.Drawing.Point(31, 243);
            this.textBox_send.Name = "textBox_send";
            this.textBox_send.Size = new System.Drawing.Size(226, 35);
            this.textBox_send.TabIndex = 1;
            this.textBox_send.TextChanged += new System.EventHandler(this.textBox_send_TextChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(31, 23);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(358, 204);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // Button_connect_Click
            // 
            this.Button_connect_Click.Location = new System.Drawing.Point(434, 136);
            this.Button_connect_Click.Name = "Button_connect_Click";
            this.Button_connect_Click.Size = new System.Drawing.Size(138, 68);
            this.Button_connect_Click.TabIndex = 0;
            this.Button_connect_Click.Text = "연결하기";
            this.Button_connect_Click.UseVisualStyleBackColor = true;
            this.Button_connect_Click.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(430, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "포트 설정";
            // 
            // comboBox_port
            // 
            this.comboBox_port.FormattingEnabled = true;
            this.comboBox_port.Location = new System.Drawing.Point(434, 72);
            this.comboBox_port.Name = "comboBox_port";
            this.comboBox_port.Size = new System.Drawing.Size(206, 32);
            this.comboBox_port.TabIndex = 4;
            this.comboBox_port.SelectedIndexChanged += new System.EventHandler(this.comboBox_port_SelectedIndexChanged);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 293);
            this.Controls.Add(this.comboBox_port);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox_send);
            this.Controls.Add(this.Button_send_Click);
            this.Controls.Add(this.Button_connect_Click);
            this.Controls.Add(this.Button_disconnect_Click);
            this.Name = "Form";
            this.Text = "Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_disconnect_Click;
        private System.Windows.Forms.Button Button_send_Click;
        private System.Windows.Forms.TextBox textBox_send;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button Button_connect_Click;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_port;
    }
}

