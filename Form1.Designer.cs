namespace Hyun_JeongSocket
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPagesync = new System.Windows.Forms.TabPage();
            this.tabControlSync = new System.Windows.Forms.TabControl();
            this.tabPageSyncServ = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonSyncServFileSend = new System.Windows.Forms.Button();
            this.listBoxSyncServ = new System.Windows.Forms.ListBox();
            this.textBoxSyncServPort = new System.Windows.Forms.TextBox();
            this.buttonSyncServOpen = new System.Windows.Forms.Button();
            this.labelSyncServ2 = new System.Windows.Forms.Label();
            this.labelSyncServ1 = new System.Windows.Forms.Label();
            this.buttonSyncServSend = new System.Windows.Forms.Button();
            this.textBoxSyncServSend = new System.Windows.Forms.TextBox();
            this.textBoxSyncServChatArea = new System.Windows.Forms.TextBox();
            this.tabPageSyncClient = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonSyncClientFileSend = new System.Windows.Forms.Button();
            this.listBoxSyncClient = new System.Windows.Forms.ListBox();
            this.buttonSyncClientConnect = new System.Windows.Forms.Button();
            this.textBoxSyncClientPort = new System.Windows.Forms.TextBox();
            this.textBoxSyncClientIP = new System.Windows.Forms.TextBox();
            this.labelSyncClient2 = new System.Windows.Forms.Label();
            this.labelSyncClient1 = new System.Windows.Forms.Label();
            this.buttonSyncClientSend = new System.Windows.Forms.Button();
            this.textBoxSyncClientSend = new System.Windows.Forms.TextBox();
            this.textBoxSyncClientChatArea = new System.Windows.Forms.TextBox();
            this.tabPageAsync = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAsyncServ = new System.Windows.Forms.TabPage();
            this.buttonAsyncServReFresh = new System.Windows.Forms.Button();
            this.pictureBoxAsyncServ = new System.Windows.Forms.PictureBox();
            this.buttonAsyncServSendPicture = new System.Windows.Forms.Button();
            this.buttonAsyncServFileSend = new System.Windows.Forms.Button();
            this.listBoxAsyncServ = new System.Windows.Forms.ListBox();
            this.textBoxAsyncServPort = new System.Windows.Forms.TextBox();
            this.buttonAsyncServOpen = new System.Windows.Forms.Button();
            this.labelAsyncServ2 = new System.Windows.Forms.Label();
            this.labelAsyncServ1 = new System.Windows.Forms.Label();
            this.buttonAsyncServSend = new System.Windows.Forms.Button();
            this.textBoxAsyncServSend = new System.Windows.Forms.TextBox();
            this.textBoxAsyncServChatArea = new System.Windows.Forms.TextBox();
            this.tabPageAsyncClient = new System.Windows.Forms.TabPage();
            this.buttonAsyncClientReFresh = new System.Windows.Forms.Button();
            this.pictureBoxAsyncClient = new System.Windows.Forms.PictureBox();
            this.textBoxAsyncClientPort = new System.Windows.Forms.TextBox();
            this.textBoxAsyncClientIP = new System.Windows.Forms.TextBox();
            this.buttonAsyncClientSendPicture = new System.Windows.Forms.Button();
            this.buttonAsyncClientFileSend = new System.Windows.Forms.Button();
            this.listBoxAsyncClient = new System.Windows.Forms.ListBox();
            this.buttonAsyncClientConnect = new System.Windows.Forms.Button();
            this.labelAsyncClient2 = new System.Windows.Forms.Label();
            this.labelAsyncClient1 = new System.Windows.Forms.Label();
            this.buttonAsyncClientSend = new System.Windows.Forms.Button();
            this.textBoxAsyncClientSend = new System.Windows.Forms.TextBox();
            this.textBoxAsyncClientChatArea = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControlMain.SuspendLayout();
            this.tabPagesync.SuspendLayout();
            this.tabControlSync.SuspendLayout();
            this.tabPageSyncServ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPageSyncClient.SuspendLayout();
            this.tabPageAsync.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageAsyncServ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAsyncServ)).BeginInit();
            this.tabPageAsyncClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAsyncClient)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlMain.Controls.Add(this.tabPagesync);
            this.tabControlMain.Controls.Add(this.tabPageAsync);
            this.tabControlMain.Location = new System.Drawing.Point(4, 35);
            this.tabControlMain.Multiline = true;
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(810, 448);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPagesync
            // 
            this.tabPagesync.Controls.Add(this.tabControlSync);
            this.tabPagesync.Location = new System.Drawing.Point(22, 4);
            this.tabPagesync.Name = "tabPagesync";
            this.tabPagesync.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagesync.Size = new System.Drawing.Size(784, 440);
            this.tabPagesync.TabIndex = 0;
            this.tabPagesync.Text = "동기방식";
            this.tabPagesync.UseVisualStyleBackColor = true;
            // 
            // tabControlSync
            // 
            this.tabControlSync.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControlSync.Controls.Add(this.tabPageSyncServ);
            this.tabControlSync.Controls.Add(this.tabPageSyncClient);
            this.tabControlSync.Location = new System.Drawing.Point(3, 0);
            this.tabControlSync.Name = "tabControlSync";
            this.tabControlSync.SelectedIndex = 0;
            this.tabControlSync.Size = new System.Drawing.Size(778, 434);
            this.tabControlSync.TabIndex = 0;
            // 
            // tabPageSyncServ
            // 
            this.tabPageSyncServ.Controls.Add(this.pictureBox1);
            this.tabPageSyncServ.Controls.Add(this.button2);
            this.tabPageSyncServ.Controls.Add(this.buttonSyncServFileSend);
            this.tabPageSyncServ.Controls.Add(this.listBoxSyncServ);
            this.tabPageSyncServ.Controls.Add(this.textBoxSyncServPort);
            this.tabPageSyncServ.Controls.Add(this.buttonSyncServOpen);
            this.tabPageSyncServ.Controls.Add(this.labelSyncServ2);
            this.tabPageSyncServ.Controls.Add(this.labelSyncServ1);
            this.tabPageSyncServ.Controls.Add(this.buttonSyncServSend);
            this.tabPageSyncServ.Controls.Add(this.textBoxSyncServSend);
            this.tabPageSyncServ.Controls.Add(this.textBoxSyncServChatArea);
            this.tabPageSyncServ.Location = new System.Drawing.Point(4, 4);
            this.tabPageSyncServ.Name = "tabPageSyncServ";
            this.tabPageSyncServ.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSyncServ.Size = new System.Drawing.Size(770, 408);
            this.tabPageSyncServ.TabIndex = 0;
            this.tabPageSyncServ.Text = "서버";
            this.tabPageSyncServ.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(545, 69);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(205, 304);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(426, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "button1";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // buttonSyncServFileSend
            // 
            this.buttonSyncServFileSend.Location = new System.Drawing.Point(325, 25);
            this.buttonSyncServFileSend.Name = "buttonSyncServFileSend";
            this.buttonSyncServFileSend.Size = new System.Drawing.Size(95, 23);
            this.buttonSyncServFileSend.TabIndex = 13;
            this.buttonSyncServFileSend.Text = "파일전송";
            this.buttonSyncServFileSend.UseVisualStyleBackColor = true;
            this.buttonSyncServFileSend.Click += new System.EventHandler(this.buttonSyncServFileSend_Click);
            // 
            // listBoxSyncServ
            // 
            this.listBoxSyncServ.FormattingEnabled = true;
            this.listBoxSyncServ.ItemHeight = 12;
            this.listBoxSyncServ.Location = new System.Drawing.Point(321, 69);
            this.listBoxSyncServ.Name = "listBoxSyncServ";
            this.listBoxSyncServ.Size = new System.Drawing.Size(207, 304);
            this.listBoxSyncServ.TabIndex = 12;
            // 
            // textBoxSyncServPort
            // 
            this.textBoxSyncServPort.Location = new System.Drawing.Point(166, 27);
            this.textBoxSyncServPort.Name = "textBoxSyncServPort";
            this.textBoxSyncServPort.Size = new System.Drawing.Size(72, 21);
            this.textBoxSyncServPort.TabIndex = 11;
            // 
            // buttonSyncServOpen
            // 
            this.buttonSyncServOpen.Location = new System.Drawing.Point(244, 25);
            this.buttonSyncServOpen.Name = "buttonSyncServOpen";
            this.buttonSyncServOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonSyncServOpen.TabIndex = 10;
            this.buttonSyncServOpen.Text = "열기";
            this.buttonSyncServOpen.UseVisualStyleBackColor = true;
            this.buttonSyncServOpen.Click += new System.EventHandler(this.buttonSyncServOpen_Click);
            // 
            // labelSyncServ2
            // 
            this.labelSyncServ2.AutoSize = true;
            this.labelSyncServ2.Location = new System.Drawing.Point(59, 30);
            this.labelSyncServ2.Name = "labelSyncServ2";
            this.labelSyncServ2.Size = new System.Drawing.Size(53, 12);
            this.labelSyncServ2.TabIndex = 9;
            this.labelSyncServ2.Text = "127.0.0.1";
            // 
            // labelSyncServ1
            // 
            this.labelSyncServ1.AutoSize = true;
            this.labelSyncServ1.Location = new System.Drawing.Point(6, 30);
            this.labelSyncServ1.Name = "labelSyncServ1";
            this.labelSyncServ1.Size = new System.Drawing.Size(56, 12);
            this.labelSyncServ1.TabIndex = 8;
            this.labelSyncServ1.Text = "현재 IP : ";
            // 
            // buttonSyncServSend
            // 
            this.buttonSyncServSend.Location = new System.Drawing.Point(244, 376);
            this.buttonSyncServSend.Name = "buttonSyncServSend";
            this.buttonSyncServSend.Size = new System.Drawing.Size(71, 23);
            this.buttonSyncServSend.TabIndex = 2;
            this.buttonSyncServSend.Text = "전송";
            this.buttonSyncServSend.UseVisualStyleBackColor = true;
            this.buttonSyncServSend.Click += new System.EventHandler(this.buttonSyncServSend_Click);
            // 
            // textBoxSyncServSend
            // 
            this.textBoxSyncServSend.Location = new System.Drawing.Point(6, 376);
            this.textBoxSyncServSend.Name = "textBoxSyncServSend";
            this.textBoxSyncServSend.Size = new System.Drawing.Size(232, 21);
            this.textBoxSyncServSend.TabIndex = 1;
            // 
            // textBoxSyncServChatArea
            // 
            this.textBoxSyncServChatArea.Location = new System.Drawing.Point(6, 69);
            this.textBoxSyncServChatArea.Multiline = true;
            this.textBoxSyncServChatArea.Name = "textBoxSyncServChatArea";
            this.textBoxSyncServChatArea.ReadOnly = true;
            this.textBoxSyncServChatArea.Size = new System.Drawing.Size(309, 301);
            this.textBoxSyncServChatArea.TabIndex = 0;
            // 
            // tabPageSyncClient
            // 
            this.tabPageSyncClient.Controls.Add(this.button3);
            this.tabPageSyncClient.Controls.Add(this.buttonSyncClientFileSend);
            this.tabPageSyncClient.Controls.Add(this.listBoxSyncClient);
            this.tabPageSyncClient.Controls.Add(this.buttonSyncClientConnect);
            this.tabPageSyncClient.Controls.Add(this.textBoxSyncClientPort);
            this.tabPageSyncClient.Controls.Add(this.textBoxSyncClientIP);
            this.tabPageSyncClient.Controls.Add(this.labelSyncClient2);
            this.tabPageSyncClient.Controls.Add(this.labelSyncClient1);
            this.tabPageSyncClient.Controls.Add(this.buttonSyncClientSend);
            this.tabPageSyncClient.Controls.Add(this.textBoxSyncClientSend);
            this.tabPageSyncClient.Controls.Add(this.textBoxSyncClientChatArea);
            this.tabPageSyncClient.Location = new System.Drawing.Point(4, 4);
            this.tabPageSyncClient.Name = "tabPageSyncClient";
            this.tabPageSyncClient.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSyncClient.Size = new System.Drawing.Size(770, 408);
            this.tabPageSyncClient.TabIndex = 1;
            this.tabPageSyncClient.Text = "클라이언트";
            this.tabPageSyncClient.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(426, 23);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "button1";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // buttonSyncClientFileSend
            // 
            this.buttonSyncClientFileSend.Location = new System.Drawing.Point(325, 23);
            this.buttonSyncClientFileSend.Name = "buttonSyncClientFileSend";
            this.buttonSyncClientFileSend.Size = new System.Drawing.Size(95, 23);
            this.buttonSyncClientFileSend.TabIndex = 14;
            this.buttonSyncClientFileSend.Text = "파일전송";
            this.buttonSyncClientFileSend.UseVisualStyleBackColor = true;
            this.buttonSyncClientFileSend.Click += new System.EventHandler(this.buttonSyncClientFileSend_Click);
            // 
            // listBoxSyncClient
            // 
            this.listBoxSyncClient.FormattingEnabled = true;
            this.listBoxSyncClient.ItemHeight = 12;
            this.listBoxSyncClient.Location = new System.Drawing.Point(321, 69);
            this.listBoxSyncClient.Name = "listBoxSyncClient";
            this.listBoxSyncClient.Size = new System.Drawing.Size(207, 304);
            this.listBoxSyncClient.TabIndex = 13;
            // 
            // buttonSyncClientConnect
            // 
            this.buttonSyncClientConnect.Location = new System.Drawing.Point(244, 23);
            this.buttonSyncClientConnect.Name = "buttonSyncClientConnect";
            this.buttonSyncClientConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonSyncClientConnect.TabIndex = 10;
            this.buttonSyncClientConnect.Text = "연결";
            this.buttonSyncClientConnect.UseVisualStyleBackColor = true;
            this.buttonSyncClientConnect.Click += new System.EventHandler(this.buttonSyncClientConnect_Click);
            // 
            // textBoxSyncClientPort
            // 
            this.textBoxSyncClientPort.Location = new System.Drawing.Point(153, 39);
            this.textBoxSyncClientPort.Name = "textBoxSyncClientPort";
            this.textBoxSyncClientPort.Size = new System.Drawing.Size(85, 21);
            this.textBoxSyncClientPort.TabIndex = 9;
            // 
            // textBoxSyncClientIP
            // 
            this.textBoxSyncClientIP.Location = new System.Drawing.Point(153, 12);
            this.textBoxSyncClientIP.Name = "textBoxSyncClientIP";
            this.textBoxSyncClientIP.Size = new System.Drawing.Size(85, 21);
            this.textBoxSyncClientIP.TabIndex = 8;
            // 
            // labelSyncClient2
            // 
            this.labelSyncClient2.AutoSize = true;
            this.labelSyncClient2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelSyncClient2.Location = new System.Drawing.Point(65, 28);
            this.labelSyncClient2.Name = "labelSyncClient2";
            this.labelSyncClient2.Size = new System.Drawing.Size(53, 12);
            this.labelSyncClient2.TabIndex = 7;
            this.labelSyncClient2.Text = "127.0.0.1";
            // 
            // labelSyncClient1
            // 
            this.labelSyncClient1.AutoSize = true;
            this.labelSyncClient1.Location = new System.Drawing.Point(12, 28);
            this.labelSyncClient1.Name = "labelSyncClient1";
            this.labelSyncClient1.Size = new System.Drawing.Size(56, 12);
            this.labelSyncClient1.TabIndex = 6;
            this.labelSyncClient1.Text = "현재 IP : ";
            // 
            // buttonSyncClientSend
            // 
            this.buttonSyncClientSend.Location = new System.Drawing.Point(244, 379);
            this.buttonSyncClientSend.Name = "buttonSyncClientSend";
            this.buttonSyncClientSend.Size = new System.Drawing.Size(71, 23);
            this.buttonSyncClientSend.TabIndex = 5;
            this.buttonSyncClientSend.Text = "전송";
            this.buttonSyncClientSend.UseVisualStyleBackColor = true;
            this.buttonSyncClientSend.Click += new System.EventHandler(this.buttonSyncClientSend_Click);
            // 
            // textBoxSyncClientSend
            // 
            this.textBoxSyncClientSend.Location = new System.Drawing.Point(6, 379);
            this.textBoxSyncClientSend.Name = "textBoxSyncClientSend";
            this.textBoxSyncClientSend.Size = new System.Drawing.Size(232, 21);
            this.textBoxSyncClientSend.TabIndex = 4;
            // 
            // textBoxSyncClientChatArea
            // 
            this.textBoxSyncClientChatArea.Location = new System.Drawing.Point(6, 72);
            this.textBoxSyncClientChatArea.Multiline = true;
            this.textBoxSyncClientChatArea.Name = "textBoxSyncClientChatArea";
            this.textBoxSyncClientChatArea.ReadOnly = true;
            this.textBoxSyncClientChatArea.Size = new System.Drawing.Size(309, 301);
            this.textBoxSyncClientChatArea.TabIndex = 3;
            // 
            // tabPageAsync
            // 
            this.tabPageAsync.Controls.Add(this.tabControl1);
            this.tabPageAsync.Location = new System.Drawing.Point(22, 4);
            this.tabPageAsync.Name = "tabPageAsync";
            this.tabPageAsync.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAsync.Size = new System.Drawing.Size(784, 440);
            this.tabPageAsync.TabIndex = 1;
            this.tabPageAsync.Text = "비동기방식";
            this.tabPageAsync.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPageAsyncServ);
            this.tabControl1.Controls.Add(this.tabPageAsyncClient);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(781, 431);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageAsyncServ
            // 
            this.tabPageAsyncServ.Controls.Add(this.buttonAsyncServReFresh);
            this.tabPageAsyncServ.Controls.Add(this.pictureBoxAsyncServ);
            this.tabPageAsyncServ.Controls.Add(this.buttonAsyncServSendPicture);
            this.tabPageAsyncServ.Controls.Add(this.buttonAsyncServFileSend);
            this.tabPageAsyncServ.Controls.Add(this.listBoxAsyncServ);
            this.tabPageAsyncServ.Controls.Add(this.textBoxAsyncServPort);
            this.tabPageAsyncServ.Controls.Add(this.buttonAsyncServOpen);
            this.tabPageAsyncServ.Controls.Add(this.labelAsyncServ2);
            this.tabPageAsyncServ.Controls.Add(this.labelAsyncServ1);
            this.tabPageAsyncServ.Controls.Add(this.buttonAsyncServSend);
            this.tabPageAsyncServ.Controls.Add(this.textBoxAsyncServSend);
            this.tabPageAsyncServ.Controls.Add(this.textBoxAsyncServChatArea);
            this.tabPageAsyncServ.Location = new System.Drawing.Point(4, 4);
            this.tabPageAsyncServ.Name = "tabPageAsyncServ";
            this.tabPageAsyncServ.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAsyncServ.Size = new System.Drawing.Size(773, 405);
            this.tabPageAsyncServ.TabIndex = 0;
            this.tabPageAsyncServ.Text = "서버";
            this.tabPageAsyncServ.UseVisualStyleBackColor = true;
            // 
            // buttonAsyncServReFresh
            // 
            this.buttonAsyncServReFresh.Location = new System.Drawing.Point(538, 23);
            this.buttonAsyncServReFresh.Name = "buttonAsyncServReFresh";
            this.buttonAsyncServReFresh.Size = new System.Drawing.Size(75, 23);
            this.buttonAsyncServReFresh.TabIndex = 25;
            this.buttonAsyncServReFresh.Text = "새로고침";
            this.buttonAsyncServReFresh.UseVisualStyleBackColor = true;
            this.buttonAsyncServReFresh.Click += new System.EventHandler(this.buttonAsyncServReFresh_Click);
            // 
            // pictureBoxAsyncServ
            // 
            this.pictureBoxAsyncServ.Location = new System.Drawing.Point(538, 67);
            this.pictureBoxAsyncServ.Name = "pictureBoxAsyncServ";
            this.pictureBoxAsyncServ.Size = new System.Drawing.Size(229, 304);
            this.pictureBoxAsyncServ.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAsyncServ.TabIndex = 24;
            this.pictureBoxAsyncServ.TabStop = false;
            this.pictureBoxAsyncServ.WaitOnLoad = true;
            // 
            // buttonAsyncServSendPicture
            // 
            this.buttonAsyncServSendPicture.Location = new System.Drawing.Point(426, 23);
            this.buttonAsyncServSendPicture.Name = "buttonAsyncServSendPicture";
            this.buttonAsyncServSendPicture.Size = new System.Drawing.Size(102, 23);
            this.buttonAsyncServSendPicture.TabIndex = 22;
            this.buttonAsyncServSendPicture.Text = "사진 전송";
            this.buttonAsyncServSendPicture.UseVisualStyleBackColor = true;
            this.buttonAsyncServSendPicture.Click += new System.EventHandler(this.buttonAsyncServSendPicture_Click);
            // 
            // buttonAsyncServFileSend
            // 
            this.buttonAsyncServFileSend.Location = new System.Drawing.Point(325, 23);
            this.buttonAsyncServFileSend.Name = "buttonAsyncServFileSend";
            this.buttonAsyncServFileSend.Size = new System.Drawing.Size(95, 23);
            this.buttonAsyncServFileSend.TabIndex = 23;
            this.buttonAsyncServFileSend.Text = "파일 전송";
            this.buttonAsyncServFileSend.UseVisualStyleBackColor = true;
            this.buttonAsyncServFileSend.Click += new System.EventHandler(this.buttonAsyncServFileSend_Click);
            // 
            // listBoxAsyncServ
            // 
            this.listBoxAsyncServ.FormattingEnabled = true;
            this.listBoxAsyncServ.ItemHeight = 12;
            this.listBoxAsyncServ.Location = new System.Drawing.Point(325, 67);
            this.listBoxAsyncServ.Name = "listBoxAsyncServ";
            this.listBoxAsyncServ.Size = new System.Drawing.Size(207, 304);
            this.listBoxAsyncServ.TabIndex = 21;
            // 
            // textBoxAsyncServPort
            // 
            this.textBoxAsyncServPort.Location = new System.Drawing.Point(166, 25);
            this.textBoxAsyncServPort.Name = "textBoxAsyncServPort";
            this.textBoxAsyncServPort.Size = new System.Drawing.Size(72, 21);
            this.textBoxAsyncServPort.TabIndex = 20;
            // 
            // buttonAsyncServOpen
            // 
            this.buttonAsyncServOpen.Location = new System.Drawing.Point(244, 23);
            this.buttonAsyncServOpen.Name = "buttonAsyncServOpen";
            this.buttonAsyncServOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonAsyncServOpen.TabIndex = 19;
            this.buttonAsyncServOpen.Text = "열기";
            this.buttonAsyncServOpen.UseVisualStyleBackColor = true;
            this.buttonAsyncServOpen.Click += new System.EventHandler(this.buttonAsyncServOpen_Click);
            // 
            // labelAsyncServ2
            // 
            this.labelAsyncServ2.AutoSize = true;
            this.labelAsyncServ2.Location = new System.Drawing.Point(59, 28);
            this.labelAsyncServ2.Name = "labelAsyncServ2";
            this.labelAsyncServ2.Size = new System.Drawing.Size(53, 12);
            this.labelAsyncServ2.TabIndex = 18;
            this.labelAsyncServ2.Text = "127.0.0.1";
            // 
            // labelAsyncServ1
            // 
            this.labelAsyncServ1.AutoSize = true;
            this.labelAsyncServ1.Location = new System.Drawing.Point(4, 28);
            this.labelAsyncServ1.Name = "labelAsyncServ1";
            this.labelAsyncServ1.Size = new System.Drawing.Size(56, 12);
            this.labelAsyncServ1.TabIndex = 17;
            this.labelAsyncServ1.Text = "현재 IP : ";
            // 
            // buttonAsyncServSend
            // 
            this.buttonAsyncServSend.Location = new System.Drawing.Point(244, 374);
            this.buttonAsyncServSend.Name = "buttonAsyncServSend";
            this.buttonAsyncServSend.Size = new System.Drawing.Size(71, 23);
            this.buttonAsyncServSend.TabIndex = 16;
            this.buttonAsyncServSend.Text = "전송";
            this.buttonAsyncServSend.UseVisualStyleBackColor = true;
            this.buttonAsyncServSend.Click += new System.EventHandler(this.buttonAsyncServSend_Click);
            // 
            // textBoxAsyncServSend
            // 
            this.textBoxAsyncServSend.Location = new System.Drawing.Point(6, 374);
            this.textBoxAsyncServSend.Name = "textBoxAsyncServSend";
            this.textBoxAsyncServSend.Size = new System.Drawing.Size(232, 21);
            this.textBoxAsyncServSend.TabIndex = 15;
            // 
            // textBoxAsyncServChatArea
            // 
            this.textBoxAsyncServChatArea.Location = new System.Drawing.Point(6, 67);
            this.textBoxAsyncServChatArea.Multiline = true;
            this.textBoxAsyncServChatArea.Name = "textBoxAsyncServChatArea";
            this.textBoxAsyncServChatArea.ReadOnly = true;
            this.textBoxAsyncServChatArea.Size = new System.Drawing.Size(309, 301);
            this.textBoxAsyncServChatArea.TabIndex = 14;
            // 
            // tabPageAsyncClient
            // 
            this.tabPageAsyncClient.Controls.Add(this.buttonAsyncClientReFresh);
            this.tabPageAsyncClient.Controls.Add(this.pictureBoxAsyncClient);
            this.tabPageAsyncClient.Controls.Add(this.textBoxAsyncClientPort);
            this.tabPageAsyncClient.Controls.Add(this.textBoxAsyncClientIP);
            this.tabPageAsyncClient.Controls.Add(this.buttonAsyncClientSendPicture);
            this.tabPageAsyncClient.Controls.Add(this.buttonAsyncClientFileSend);
            this.tabPageAsyncClient.Controls.Add(this.listBoxAsyncClient);
            this.tabPageAsyncClient.Controls.Add(this.buttonAsyncClientConnect);
            this.tabPageAsyncClient.Controls.Add(this.labelAsyncClient2);
            this.tabPageAsyncClient.Controls.Add(this.labelAsyncClient1);
            this.tabPageAsyncClient.Controls.Add(this.buttonAsyncClientSend);
            this.tabPageAsyncClient.Controls.Add(this.textBoxAsyncClientSend);
            this.tabPageAsyncClient.Controls.Add(this.textBoxAsyncClientChatArea);
            this.tabPageAsyncClient.Location = new System.Drawing.Point(4, 4);
            this.tabPageAsyncClient.Name = "tabPageAsyncClient";
            this.tabPageAsyncClient.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAsyncClient.Size = new System.Drawing.Size(773, 405);
            this.tabPageAsyncClient.TabIndex = 1;
            this.tabPageAsyncClient.Text = "클라이언트";
            this.tabPageAsyncClient.UseVisualStyleBackColor = true;
            // 
            // buttonAsyncClientReFresh
            // 
            this.buttonAsyncClientReFresh.Location = new System.Drawing.Point(538, 21);
            this.buttonAsyncClientReFresh.Name = "buttonAsyncClientReFresh";
            this.buttonAsyncClientReFresh.Size = new System.Drawing.Size(75, 23);
            this.buttonAsyncClientReFresh.TabIndex = 37;
            this.buttonAsyncClientReFresh.Text = "새로고침";
            this.buttonAsyncClientReFresh.UseVisualStyleBackColor = true;
            this.buttonAsyncClientReFresh.Click += new System.EventHandler(this.buttonAsyncClientReFresh_Click);
            // 
            // pictureBoxAsyncClient
            // 
            this.pictureBoxAsyncClient.Location = new System.Drawing.Point(538, 65);
            this.pictureBoxAsyncClient.Name = "pictureBoxAsyncClient";
            this.pictureBoxAsyncClient.Size = new System.Drawing.Size(229, 304);
            this.pictureBoxAsyncClient.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAsyncClient.TabIndex = 36;
            this.pictureBoxAsyncClient.TabStop = false;
            this.pictureBoxAsyncClient.WaitOnLoad = true;
            // 
            // textBoxAsyncClientPort
            // 
            this.textBoxAsyncClientPort.Location = new System.Drawing.Point(153, 35);
            this.textBoxAsyncClientPort.Name = "textBoxAsyncClientPort";
            this.textBoxAsyncClientPort.Size = new System.Drawing.Size(85, 21);
            this.textBoxAsyncClientPort.TabIndex = 35;
            // 
            // textBoxAsyncClientIP
            // 
            this.textBoxAsyncClientIP.Location = new System.Drawing.Point(153, 8);
            this.textBoxAsyncClientIP.Name = "textBoxAsyncClientIP";
            this.textBoxAsyncClientIP.Size = new System.Drawing.Size(85, 21);
            this.textBoxAsyncClientIP.TabIndex = 34;
            // 
            // buttonAsyncClientSendPicture
            // 
            this.buttonAsyncClientSendPicture.Location = new System.Drawing.Point(426, 21);
            this.buttonAsyncClientSendPicture.Name = "buttonAsyncClientSendPicture";
            this.buttonAsyncClientSendPicture.Size = new System.Drawing.Size(102, 23);
            this.buttonAsyncClientSendPicture.TabIndex = 32;
            this.buttonAsyncClientSendPicture.Text = "사진 전송";
            this.buttonAsyncClientSendPicture.UseVisualStyleBackColor = true;
            this.buttonAsyncClientSendPicture.Click += new System.EventHandler(this.buttonAsyncClientSendPicture_Click);
            // 
            // buttonAsyncClientFileSend
            // 
            this.buttonAsyncClientFileSend.Location = new System.Drawing.Point(325, 21);
            this.buttonAsyncClientFileSend.Name = "buttonAsyncClientFileSend";
            this.buttonAsyncClientFileSend.Size = new System.Drawing.Size(95, 23);
            this.buttonAsyncClientFileSend.TabIndex = 33;
            this.buttonAsyncClientFileSend.Text = "파일 전송";
            this.buttonAsyncClientFileSend.UseVisualStyleBackColor = true;
            this.buttonAsyncClientFileSend.Click += new System.EventHandler(this.buttonAsyncClientFileSend_Click);
            // 
            // listBoxAsyncClient
            // 
            this.listBoxAsyncClient.FormattingEnabled = true;
            this.listBoxAsyncClient.ItemHeight = 12;
            this.listBoxAsyncClient.Location = new System.Drawing.Point(325, 65);
            this.listBoxAsyncClient.Name = "listBoxAsyncClient";
            this.listBoxAsyncClient.Size = new System.Drawing.Size(207, 304);
            this.listBoxAsyncClient.TabIndex = 31;
            // 
            // buttonAsyncClientConnect
            // 
            this.buttonAsyncClientConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonAsyncClientConnect.Location = new System.Drawing.Point(244, 21);
            this.buttonAsyncClientConnect.Name = "buttonAsyncClientConnect";
            this.buttonAsyncClientConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonAsyncClientConnect.TabIndex = 29;
            this.buttonAsyncClientConnect.Text = "연결";
            this.buttonAsyncClientConnect.UseVisualStyleBackColor = true;
            this.buttonAsyncClientConnect.Click += new System.EventHandler(this.buttonAsyncClientConnect_Click);
            // 
            // labelAsyncClient2
            // 
            this.labelAsyncClient2.AutoSize = true;
            this.labelAsyncClient2.Location = new System.Drawing.Point(59, 26);
            this.labelAsyncClient2.Name = "labelAsyncClient2";
            this.labelAsyncClient2.Size = new System.Drawing.Size(53, 12);
            this.labelAsyncClient2.TabIndex = 28;
            this.labelAsyncClient2.Text = "127.0.0.1";
            // 
            // labelAsyncClient1
            // 
            this.labelAsyncClient1.AutoSize = true;
            this.labelAsyncClient1.Location = new System.Drawing.Point(4, 26);
            this.labelAsyncClient1.Name = "labelAsyncClient1";
            this.labelAsyncClient1.Size = new System.Drawing.Size(56, 12);
            this.labelAsyncClient1.TabIndex = 27;
            this.labelAsyncClient1.Text = "현재 IP : ";
            // 
            // buttonAsyncClientSend
            // 
            this.buttonAsyncClientSend.Location = new System.Drawing.Point(244, 372);
            this.buttonAsyncClientSend.Name = "buttonAsyncClientSend";
            this.buttonAsyncClientSend.Size = new System.Drawing.Size(71, 23);
            this.buttonAsyncClientSend.TabIndex = 26;
            this.buttonAsyncClientSend.Text = "전송";
            this.buttonAsyncClientSend.UseVisualStyleBackColor = true;
            this.buttonAsyncClientSend.Click += new System.EventHandler(this.buttonAsyncClientSend_Click);
            // 
            // textBoxAsyncClientSend
            // 
            this.textBoxAsyncClientSend.Location = new System.Drawing.Point(6, 372);
            this.textBoxAsyncClientSend.Name = "textBoxAsyncClientSend";
            this.textBoxAsyncClientSend.Size = new System.Drawing.Size(232, 21);
            this.textBoxAsyncClientSend.TabIndex = 25;
            // 
            // textBoxAsyncClientChatArea
            // 
            this.textBoxAsyncClientChatArea.Location = new System.Drawing.Point(6, 65);
            this.textBoxAsyncClientChatArea.Multiline = true;
            this.textBoxAsyncClientChatArea.Name = "textBoxAsyncClientChatArea";
            this.textBoxAsyncClientChatArea.ReadOnly = true;
            this.textBoxAsyncClientChatArea.Size = new System.Drawing.Size(309, 301);
            this.textBoxAsyncClientChatArea.TabIndex = 24;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 477);
            this.Controls.Add(this.tabControlMain);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPagesync.ResumeLayout(false);
            this.tabControlSync.ResumeLayout(false);
            this.tabPageSyncServ.ResumeLayout(false);
            this.tabPageSyncServ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPageSyncClient.ResumeLayout(false);
            this.tabPageSyncClient.PerformLayout();
            this.tabPageAsync.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageAsyncServ.ResumeLayout(false);
            this.tabPageAsyncServ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAsyncServ)).EndInit();
            this.tabPageAsyncClient.ResumeLayout(false);
            this.tabPageAsyncClient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAsyncClient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPagesync;
        private System.Windows.Forms.TabPage tabPageAsync;
        private System.Windows.Forms.TabControl tabControlSync;
        private System.Windows.Forms.TabPage tabPageSyncServ;
        private System.Windows.Forms.TabPage tabPageSyncClient;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageAsyncServ;
        private System.Windows.Forms.TabPage tabPageAsyncClient;
        private System.Windows.Forms.Button buttonSyncServSend;
        private System.Windows.Forms.TextBox textBoxSyncServSend;
        private System.Windows.Forms.TextBox textBoxSyncServChatArea;
        private System.Windows.Forms.Label labelSyncClient1;
        private System.Windows.Forms.Button buttonSyncClientSend;
        private System.Windows.Forms.TextBox textBoxSyncClientSend;
        private System.Windows.Forms.TextBox textBoxSyncClientChatArea;
        private System.Windows.Forms.Label labelSyncClient2;
        private System.Windows.Forms.Label labelSyncServ2;
        private System.Windows.Forms.Label labelSyncServ1;
        private System.Windows.Forms.Button buttonSyncServOpen;
        private System.Windows.Forms.TextBox textBoxSyncServPort;
        private System.Windows.Forms.Button buttonSyncClientConnect;
        private System.Windows.Forms.TextBox textBoxSyncClientPort;
        private System.Windows.Forms.TextBox textBoxSyncClientIP;
        private System.Windows.Forms.ListBox listBoxSyncServ;
        private System.Windows.Forms.ListBox listBoxSyncClient;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonSyncServFileSend;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonSyncClientFileSend;
        private System.Windows.Forms.Button buttonAsyncServSendPicture;
        private System.Windows.Forms.Button buttonAsyncServFileSend;
        private System.Windows.Forms.ListBox listBoxAsyncServ;
        private System.Windows.Forms.TextBox textBoxAsyncServPort;
        private System.Windows.Forms.Button buttonAsyncServOpen;
        private System.Windows.Forms.Label labelAsyncServ2;
        private System.Windows.Forms.Label labelAsyncServ1;
        private System.Windows.Forms.Button buttonAsyncServSend;
        private System.Windows.Forms.TextBox textBoxAsyncServSend;
        private System.Windows.Forms.TextBox textBoxAsyncServChatArea;
        private System.Windows.Forms.Button buttonAsyncClientSendPicture;
        private System.Windows.Forms.Button buttonAsyncClientFileSend;
        private System.Windows.Forms.ListBox listBoxAsyncClient;
        private System.Windows.Forms.Button buttonAsyncClientConnect;
        private System.Windows.Forms.Label labelAsyncClient2;
        private System.Windows.Forms.Label labelAsyncClient1;
        private System.Windows.Forms.Button buttonAsyncClientSend;
        private System.Windows.Forms.TextBox textBoxAsyncClientSend;
        private System.Windows.Forms.TextBox textBoxAsyncClientChatArea;
        private System.Windows.Forms.TextBox textBoxAsyncClientPort;
        private System.Windows.Forms.TextBox textBoxAsyncClientIP;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBoxAsyncServ;
        private System.Windows.Forms.PictureBox pictureBoxAsyncClient;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonAsyncServReFresh;
        private System.Windows.Forms.Button buttonAsyncClientReFresh;
    }
}

