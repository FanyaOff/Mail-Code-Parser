namespace SRM
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.login = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.server = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.host = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ssl = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(12, 38);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(294, 23);
            this.login.TabIndex = 0;
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(12, 87);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(294, 23);
            this.pass.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(38, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Get Last Message";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(160, 116);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Open Output Form";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Server";
            // 
            // server
            // 
            this.server.FormattingEnabled = true;
            this.server.Items.AddRange(new object[] {
            "rambler.ru",
            "gmail.com",
            "Other"});
            this.server.Location = new System.Drawing.Point(12, 183);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(121, 23);
            this.server.TabIndex = 7;
            this.server.Text = "rambler.ru";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "IMAP Host";
            // 
            // host
            // 
            this.host.Location = new System.Drawing.Point(178, 183);
            this.host.Name = "host";
            this.host.Size = new System.Drawing.Size(128, 23);
            this.host.TabIndex = 9;
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(178, 236);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(128, 23);
            this.port.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(204, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "IMAP Port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "IMAP SSL";
            // 
            // ssl
            // 
            this.ssl.FormattingEnabled = true;
            this.ssl.Items.AddRange(new object[] {
            "true",
            "false"});
            this.ssl.Location = new System.Drawing.Point(12, 236);
            this.ssl.Name = "ssl";
            this.ssl.Size = new System.Drawing.Size(121, 23);
            this.ssl.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 287);
            this.Controls.Add(this.ssl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.port);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.host);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.server);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.login);
            this.Name = "Form1";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox login;
        private TextBox pass;
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
        private Label label3;
        private ComboBox server;
        private Label label4;
        private TextBox host;
        private TextBox port;
        private Label label5;
        private Label label6;
        private ComboBox ssl;
    }
}