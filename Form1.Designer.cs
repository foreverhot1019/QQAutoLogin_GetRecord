namespace QQHockTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTXGuiFoundation = new System.Windows.Forms.Button();
            this.tabCtrlMessage = new System.Windows.Forms.TabControl();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTXGuiFoundation
            // 
            this.btnTXGuiFoundation.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnTXGuiFoundation.Location = new System.Drawing.Point(9, 12);
            this.btnTXGuiFoundation.Name = "btnTXGuiFoundation";
            this.btnTXGuiFoundation.Size = new System.Drawing.Size(75, 23);
            this.btnTXGuiFoundation.TabIndex = 0;
            this.btnTXGuiFoundation.Text = "读取消息";
            this.btnTXGuiFoundation.UseVisualStyleBackColor = true;
            this.btnTXGuiFoundation.Click += new System.EventHandler(this.btnTXGuiFoundation_Click);
            // 
            // tabCtrlMessage
            // 
            this.tabCtrlMessage.Location = new System.Drawing.Point(9, 41);
            this.tabCtrlMessage.Name = "tabCtrlMessage";
            this.tabCtrlMessage.SelectedIndex = 0;
            this.tabCtrlMessage.Size = new System.Drawing.Size(339, 311);
            this.tabCtrlMessage.TabIndex = 1;
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Location = new System.Drawing.Point(105, 12);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(75, 23);
            this.btnSendMsg.TabIndex = 2;
            this.btnSendMsg.Text = "模拟发送消息";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(354, 41);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(229, 311);
            this.textBox1.TabIndex = 3;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(210, 12);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "模拟登陆";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 364);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSendMsg);
            this.Controls.Add(this.tabCtrlMessage);
            this.Controls.Add(this.btnTXGuiFoundation);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTXGuiFoundation;
        private System.Windows.Forms.TabControl tabCtrlMessage;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnLogin;
    }
}

