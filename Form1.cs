using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
namespace Hyun_JeongSocket
{
    public partial class Form1 : Form
    {
        IPHostEntry ipHost; // 현재 ip를 얻기 위한 객체

        //파일전송
        public delegate void AsyncFileFormLoad(string type, string title);
        public delegate void AsyncSaveDialogLoad();
        public delegate void AsyncFileTrans(string message, int flag, FileStream FileRecv);
        public delegate void AsyncFormClosingSend(string message);
        Form2 FileServTrans;
        Form2 FileServReceive;
        Form3 FileClientTrans;
        Form3 FileClientReceive;
        public static event AsyncFileTrans toform2;
        public static event AsyncFileTrans toform3;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            listBoxAsyncServ.ScrollAlwaysVisible = true;
            listBoxAsyncClient.ScrollAlwaysVisible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ipHost = Dns.GetHostByName(Dns.GetHostName());
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //동기 서버
            labelSyncServ2.Text = ipHost.AddressList[0].ToString();

            //동기 클라이언트
            labelSyncClient2.Text = ipHost.AddressList[0].ToString();

            //비동기 서버
            labelAsyncServ2.Text = ipHost.AddressList[0].ToString();
            buttonAsyncServSend.Enabled = false;
            buttonAsyncServFileSend.Enabled = false;
            buttonAsyncServSendPicture.Enabled = false;

            //비동기 클라이언트
            labelAsyncClient2.Text = ipHost.AddressList[0].ToString();
            buttonAsyncClientSend.Enabled = false;
            buttonAsyncClientFileSend.Enabled = false;
            buttonAsyncClientSendPicture.Enabled = false;

            //지워야할 것들
            textBoxSyncServPort.Text = "8888";
            textBoxSyncClientIP.Text = labelSyncClient2.Text;
            textBoxSyncClientPort.Text = "8888";
            textBoxAsyncServPort.Text = "8888";
            textBoxAsyncClientIP.Text = labelAsyncClient2.Text;
            textBoxAsyncClientPort.Text = "8888";
            //지워야할 것들
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //동기 클라이언트 메모리 해제

            if (socketConSyncClient != null)
            {
                //
                ////스트림 메모리 해제
                //
                if (streamWriteSyncServer != null)
                {
                    streamWriteSyncServer.Close();
                }

                if (streamReaderSyncServer != null)
                {
                    streamReaderSyncServer.Close();
                }

                if (streamNetSyncServer != null)
                {
                    streamNetSyncServer.Close();
                }

                socketConSyncClient.Close();
            }


            //동기 서버 메모리 해제

            if (socketSyncServerMain != null)
            {
                //
                ////스트림 메모리 해제
                //
                if (streamWriteSyncServer != null)
                {
                    streamWriteSyncServer.Close();
                }

                if (streamReaderSyncServer != null)
                {
                    streamReaderSyncServer.Close();
                }

                if (streamNetSyncServer != null)
                {
                    streamNetSyncServer.Close();
                }

                //
                ////스레드 인터럽트
                //

                threadAcceptSyncServer.Interrupt();

                //
                ////소켓 메모리 해제
                //

                if (socketAcceptSyncServer != null)
                {
                    socketAcceptSyncServer.Disconnect(true);
                }
                socketSyncServerMain.Close(5);
            }


            //비동기 서버 메모리 해제

            //비동기 클라이언트 메모리 해제


        }

        //
        //동기 서버
        //

        Socket socketSyncServerMain = null;
        Socket socketAcceptSyncServer = null;
        Thread threadAcceptSyncServer = null;
        NetworkStream streamNetSyncServer = null;
        StreamWriter streamWriteSyncServer = null;
        StreamReader streamReaderSyncServer = null;
        //byte[] byMessageSyncServer; 파일전송


        private void buttonSyncServOpen_Click(object sender, EventArgs e)
        {
            socketSyncServerMain = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress addrServer = IPAddress.Parse(labelSyncServ2.Text);
            IPEndPoint ipEPServer = new IPEndPoint(addrServer, Convert.ToInt32(textBoxSyncServPort.Text));
            socketSyncServerMain.Bind(ipEPServer);
            socketSyncServerMain.Listen(10);

            listBoxSyncServ.Items.Add("동기 서버 Open");
            listBoxSyncServ.Items.Add("클라이언트 접속 대기중. . .");

            threadAcceptSyncServer = new Thread(thSyncServerAccept);
            threadAcceptSyncServer.Start();
        }

        private void buttonSyncServSend_Click(object sender, EventArgs e)
        {
            if (textBoxSyncServSend.Text == "")
            {
                return;
            }

            if (socketAcceptSyncServer.Connected)
            {
                streamWriteSyncServer.WriteLine("서버(동기) : " + textBoxSyncServSend.Text);
                streamWriteSyncServer.Flush();
                textBoxSyncServChatArea.AppendText("서버(동기) : " + textBoxSyncServSend.Text + "\r\n");
                textBoxSyncServSend.Text = "";
            }
            else
            {
                MessageBox.Show("연결이 끊어져있습니다.");
            }
        }

        private void thSyncServerAccept()
        {
            while (true)
            {
                try
                {
                    socketAcceptSyncServer = socketSyncServerMain.Accept();
                }
                catch
                {
                    MessageBox.Show("서버(동기) Accept 구간이 종료되었습니다.");
                    break;
                }

                streamNetSyncServer = new NetworkStream(socketAcceptSyncServer);
                streamWriteSyncServer = new StreamWriter(streamNetSyncServer, Encoding.UTF8);
                streamReaderSyncServer = new StreamReader(streamNetSyncServer, Encoding.UTF8);
                IPEndPoint ipEPAcceptClient = (IPEndPoint)socketAcceptSyncServer.RemoteEndPoint;

                listBoxSyncServ.Items.Add(ipEPAcceptClient.Address + " 접속.");
                //int nCountSyncServer = Convert.ToInt32()

                Thread threadRecvSyncServer = new Thread(thRecvSyncServer);
                threadRecvSyncServer.Start();
            }
        }

        private void thRecvSyncServer()
        {
            while (true)
            {
                string strMsgSyncServer = "";
                try
                {
                    strMsgSyncServer = streamReaderSyncServer.ReadLine();
                }
                catch
                {
                    if (!socketAcceptSyncServer.Connected)
                    {
                        MessageBox.Show("연결이 끊어졌습니다. 서버(동기)를 종료합니다.");
                        break;
                    }
                    else
                        continue;
                }
                string[] strSplitMsg = null;

                if (strMsgSyncServer != null)
                {
                    strSplitMsg = strMsgSyncServer.Trim().Split('/');
                }
                else
                {
                    if (socketAcceptSyncServer.Connected)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                switch (strSplitMsg[0])
                {
                    case "SyncFileTrans":
                        listBoxSyncServ.Items.Add("상대방이 파일전송을 희망합니다.");
                        if (MessageBox.Show("상대방이 파일전송을 희망합니다. 수락하시겠습니까 ?", "파일전송", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            listBoxSyncServ.Items.Add("파일전송을 진행합니다.");
                            streamWriteSyncServer.WriteLine("SyncFileTransYes/");
                            streamWriteSyncServer.Flush();
                            //파일전송
                        }
                        else
                        {
                            listBoxSyncServ.Items.Add("파일전송을 거절하였습니다.");
                            streamWriteSyncServer.WriteLine("SyncFileTransNo/");
                            streamWriteSyncServer.Flush();
                            //거절의사 전송
                        }
                        break;
                    case "SyncFileTransYes":
                        listBoxSyncServ.Items.Add("상대방이 파일전송을 허락하였습니다.");
                        break;
                    case "SyncFileTransNo":
                        listBoxSyncServ.Items.Add("상대방이 파일전송을 거절하였습니다.");
                        break;
                    default:
                        textBoxSyncServChatArea.AppendText(strMsgSyncServer + "\r\n");
                        break;
                }


            }
        }

        private void buttonSyncServFileSend_Click(object sender, EventArgs e)
        {
            streamWriteSyncServer.WriteLine("SyncFileTrans/");
            streamWriteSyncServer.Flush();
            listBoxSyncServ.Items.Add("파일전송을 요청하였습니다.");
        }




        //
        //동기 클라이언트
        //


        Socket socketConSyncClient = null;
        NetworkStream streamNetSyncClient = null;
        StreamWriter streamWriteSyncClient = null;
        StreamReader streamReaderSyncClient = null;

        private void buttonSyncClientConnect_Click(object sender, EventArgs e)
        {
            IPEndPoint ipEPSyncClient = new IPEndPoint(IPAddress.Parse(textBoxSyncClientIP.Text), Convert.ToInt32(textBoxSyncClientPort.Text));
            socketConSyncClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketConSyncClient.Connect(ipEPSyncClient);
            listBoxSyncClient.Items.Add("서버에 연결중 . . .");
            if (socketConSyncClient.Connected)
            {
                listBoxSyncClient.Items.Add("서버에 연결되었습니다.");
                streamNetSyncClient = new NetworkStream(socketConSyncClient);
                streamWriteSyncClient = new StreamWriter(streamNetSyncClient, Encoding.UTF8);
                streamReaderSyncClient = new StreamReader(streamNetSyncClient, Encoding.UTF8);

                Thread threadRecvSyncClient = new Thread(thRecvSyncClient);
                threadRecvSyncClient.Start();

            }
            else
            {
                listBoxSyncClient.Items.Add("서버가 닫혀있음.");
            }
        }

        private void buttonSyncClientSend_Click(object sender, EventArgs e)
        {
            if (socketConSyncClient.Connected)
            {
                streamWriteSyncClient.WriteLine("클라이언트(동기) : " + textBoxSyncClientSend.Text);
                streamWriteSyncClient.Flush();
                textBoxSyncClientChatArea.AppendText("클라이언트(동기) : " + textBoxSyncClientSend.Text + "\r\n");
                textBoxSyncClientSend.Text = "";
            }
            else
            {
                MessageBox.Show("연결이 끊어져있습니다.");
            }
        }

        private void thRecvSyncClient()
        {
            while (true)
            {
                string strMsgSyncClient = "";
                try
                {
                    strMsgSyncClient = streamReaderSyncClient.ReadLine();
                }
                catch
                {
                    if (!socketConSyncClient.Connected)
                    {
                        MessageBox.Show("연결이 끊어졌습니다. 클라이언트(동기)를 종료합니다.");
                        break;
                    }
                    else
                        continue;
                }
                string[] strSplitMsg = null;

                if (strMsgSyncClient != null)
                {
                    strSplitMsg = strMsgSyncClient.Trim().Split('/');
                }
                else
                {
                    if (socketConSyncClient.Connected)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                switch (strSplitMsg[0])
                {
                    case "SyncFileTrans":
                        listBoxSyncClient.Items.Add("상대방이 파일전송을 희망합니다.");
                        if (MessageBox.Show("상대방이 파일전송을 희망합니다. 수락하시겠습니까 ?", "파일전송", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            listBoxSyncClient.Items.Add("파일전송을 진행합니다.");
                            streamWriteSyncClient.WriteLine("SyncFileTransYes/");
                            streamWriteSyncClient.Flush();
                            //파일전송
                        }
                        else
                        {
                            listBoxSyncClient.Items.Add("파일전송을 거절하였습니다.");
                            streamWriteSyncClient.WriteLine("SyncFileTransNo/");
                            streamWriteSyncClient.Flush();
                            //거절의사 전송
                        }
                        break;
                    case "SyncFileTransYes":
                        listBoxSyncClient.Items.Add("상대방이 파일전송을 허락하였습니다.");
                        break;
                    case "SyncFileTransNo":
                        listBoxSyncClient.Items.Add("상대방이 파일전송을 거절하였습니다.");
                        break;
                    default:
                        textBoxSyncClientChatArea.AppendText(strMsgSyncClient + "\r\n");
                        break;
                }

            }
        }

        private void buttonSyncClientFileSend_Click(object sender, EventArgs e)
        {
            streamWriteSyncClient.WriteLine("SyncFileTrans/");
            streamWriteSyncClient.Flush();
            listBoxSyncClient.Items.Add("파일전송을 요청하였습니다.");
        }


        //
        //비동기 서버
        //

        public class AsyncServObject //서버에서 전송하는 데이터 형식
        {
            public Socket workSocket;
            public byte[] buffer;
            public Int32 size;
            public AsyncServObject(Int32 bufferSize)
            {
                this.size = bufferSize;
                this.buffer = new byte[size];
            }
            public void inibuffer()
            {
                this.buffer = null;
                this.buffer = new byte[size];
            }
        }

        Socket socketAyncServerMain = null;
        Socket fileWorkServAccpetSocket = null;
        Socket fileWorkServConnectSocket = null;
        Socket ImageAsyncServAcceptSocket = null;
        Socket ImageAsyncServConnectSocket = null;

        AsyncCallback asyncCbServSend = null;
        AsyncCallback asyncCbServrecv = null;
        AsyncCallback asyncCbServAccept = null;
        AsyncCallback asyncCbServFileAccept = null;
        AsyncCallback asyncCbServFileReceive = null;
        AsyncCallback asyncCbServFileSend = null;
        AsyncCallback asyncCbServFileConnect = null;
        AsyncCallback asyncCbServImageAccept = null;
        AsyncCallback asyncCbServImageReceive = null;
        AsyncCallback asyncCbServImageSend = null;
        AsyncCallback asyncCbServImageConnect = null;

        MemoryStream msPictureAsyncServ;

        byte[] btAsyncServerPicture;

        public static FileStream asyncStreamServfileRecv = null;

        private void buttonAsyncServOpen_Click(object sender, EventArgs e)
        {
            try
            {
                asyncCbServSend = new AsyncCallback(AsyncHandleServSend);
                asyncCbServrecv = new AsyncCallback(AsyncHandleServReceive);
                asyncCbServAccept = new AsyncCallback(AsyncHandleServAccept);

                socketAyncServerMain = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketAyncServerMain.Bind(new IPEndPoint(IPAddress.Parse(labelAsyncServ2.Text), Convert.ToInt32(textBoxAsyncServPort.Text)));
                socketAyncServerMain.Listen(5);

                listBoxAsyncServ.Items.Add("서버(비동기)가 열렸습니다.");
                listBoxAsyncServ.Items.Add("클라이언트가 접속대기중 . . .");
                listBoxAsyncServ.SelectedIndex = listBoxAsyncServ.Items.Count - 1;
                buttonAsyncServOpen.Enabled = false;

                socketAyncServerMain.BeginAccept(asyncCbServAccept, null);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }

        }

        private void AsyncHandleServAccept(IAsyncResult ar)
        {
            socketAyncServerMain = socketAyncServerMain.EndAccept(ar);

            IPEndPoint ipEPAcceptClient = (IPEndPoint)socketAyncServerMain.RemoteEndPoint;
            listBoxAsyncServ.Items.Add(ipEPAcceptClient.Address + " 접속.");
            listBoxAsyncServ.SelectedIndex = listBoxAsyncServ.Items.Count - 1;
            buttonAsyncServSend.Enabled = true;
            buttonAsyncServFileSend.Enabled = true;
            buttonAsyncServSendPicture.Enabled = true;

            AsyncServObject ao = new AsyncServObject(4096);
            ao.workSocket = socketAyncServerMain;

            socketAyncServerMain.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServrecv, ao);
        }

        private void AsyncHandleServReceive(IAsyncResult ar)
        {
            AsyncServObject ao = (AsyncServObject)ar.AsyncState;
            Int32 recvBytes = ao.workSocket.EndReceive(ar);

            if (recvBytes > 0)
            {
                String strmsgAsyncServ = Encoding.UTF8.GetString(ao.buffer);
                string[] strmsgSplit = strmsgAsyncServ.Trim().Split('/');
                switch (strmsgSplit[0])
                {
                    case "AsyncFileTrans":
                        listBoxAsyncServ.Items.Add("상대방이 파일전송을 희망합니다.");
                        buttonAsyncServFileSend.Enabled = false;
                        if (MessageBox.Show("상대방이 파일전송을 희망합니다. 수락하시겠습니까 ?", "파일전송", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            ao.inibuffer();
                            ao.buffer = Encoding.UTF8.GetBytes("AsyncFileTransYes/");
                            socketAyncServerMain.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServSend, ao);
                            //파일전송
                            if (fileWorkServConnectSocket == null || !fileWorkServConnectSocket.Connected)
                            {
                                IPEndPoint ipEPAcceptClient = (IPEndPoint)socketAyncServerMain.RemoteEndPoint;

                                asyncCbServFileSend = new AsyncCallback(AsyncHandleServFileSend);
                                asyncCbServFileConnect = new AsyncCallback(AsyncHandleServFileConnect);
                                asyncCbServFileReceive = new AsyncCallback(AsyncHandleServFileRecv);
                                fileWorkServConnectSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                fileWorkServConnectSocket.BeginConnect(new IPEndPoint(ipEPAcceptClient.Address, 8889), asyncCbServFileConnect, fileWorkServConnectSocket);
                            }
                            AsyncFileFormLoad AsyncFileReceive = new AsyncFileFormLoad(FileFormLoad);
                            this.Invoke(AsyncFileReceive, "FileReceive", "Server");
                        }
                        else
                        {
                            buttonAsyncServFileSend.Enabled = true;
                            ao.inibuffer();
                            ao.buffer = Encoding.UTF8.GetBytes("AsyncFileTransNo/");
                            socketAyncServerMain.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServSend, ao);
                            //거절의사 전송
                        }
                        break;
                    case "AsyncFileTransYes":
                        listBoxAsyncServ.Items.Add("상대방이 파일전송을 허락하였습니다.");
                        AsyncFileFormLoad AsyncFileTrans = new AsyncFileFormLoad(FileFormLoad);
                        this.Invoke(AsyncFileTrans, "FileTrans", "Server");

                        break;
                    case "AsyncFileTransNo":
                        listBoxAsyncServ.Items.Add("상대방이 파일전송을 거절하였습니다.");
                        buttonAsyncServFileSend.Enabled = true;

                        break;
                    case "ImageSend":
                        //클라이언트가 나에게 사진전송을 허락할것인지 묻는 메시지
                        listBoxAsyncServ.Items.Add("상대방이 사진을 공유하고 싶어합니다.");
                        buttonAsyncServSendPicture.Enabled = false;
                        if (MessageBox.Show("상대방이 사진을 공유하고 싶어합니다. 수락하시겠습니까 ?", "사진전송", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            btAsyncServerPicture = new byte[Convert.ToInt32(strmsgSplit[1])];
                            ao.inibuffer();
                            ao.buffer = Encoding.UTF8.GetBytes("ImageOK/");
                            socketAyncServerMain.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServSend, ao);
                            //파일전송
                            if (ImageAsyncServConnectSocket == null || !ImageAsyncServConnectSocket.Connected)
                            {
                                IPEndPoint ipEPAcceptClient = (IPEndPoint)socketAyncServerMain.RemoteEndPoint;

                                asyncCbServImageSend = new AsyncCallback(AsyncHandleServImageSend);
                                asyncCbServImageConnect = new AsyncCallback(AsyncHandleServImageConnect);
                                asyncCbServImageReceive = new AsyncCallback(AsyncHandleServImageReceive);
                                ImageAsyncServConnectSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                ImageAsyncServConnectSocket.BeginConnect(new IPEndPoint(ipEPAcceptClient.Address, 7879), asyncCbServImageConnect, ImageAsyncServConnectSocket);
                            }

                            if (msPictureAsyncServ != null)
                            {
                                msPictureAsyncServ.Flush();
                                msPictureAsyncServ.Close();
                                msPictureAsyncServ.Dispose();
                                msPictureAsyncServ = null;
                            }
                            msPictureAsyncServ = new MemoryStream();
                        }
                        else
                        {
                            ao.inibuffer();
                            ao.buffer = Encoding.UTF8.GetBytes("ImageNO/");
                            socketAyncServerMain.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServSend, ao);
                            //거절의사 전송
                            buttonAsyncServSendPicture.Enabled = true;
                        }
                        break;
                    case "ImageOK":
                        //클라이언트가 사진전송을 허락하여 사진전송
                        listBoxAsyncServ.Items.Add("상대방이 사진공유를 허락하였습니다.");
                        AsyncServSendImage();
                        break;
                    case "ImageNO":
                        //클라이언트가 사진전송을 거절하여 사진거절
                        listBoxAsyncServ.Items.Add("상대방이 사진공유를 거절하였습니다.");
                        buttonAsyncServSendPicture.Enabled = true;
                        break;
                    default:
                        textBoxAsyncServChatArea.AppendText(Encoding.UTF8.GetString(ao.buffer));
                        textBoxAsyncServChatArea.AppendText("\r\n");
                        break;
                }
            }
            listBoxAsyncServ.SelectedIndex = listBoxAsyncServ.Items.Count - 1;
            Thread.Sleep(10);
            ao.inibuffer();
            ao.workSocket.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServrecv, ao);
        }

        private void buttonAsyncServSend_Click(object sender, EventArgs e)
        {
            AsyncServObject ao = new AsyncServObject(1);

            ao.buffer = Encoding.UTF8.GetBytes("서버(비동기) : " + textBoxAsyncServSend.Text);

            textBoxAsyncServSend.Text = "";
            ao.workSocket = socketAyncServerMain;
            socketAyncServerMain.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServSend, ao);
        }

        private void AsyncHandleServSend(IAsyncResult ar)
        {
            AsyncServObject ao = (AsyncServObject)ar.AsyncState;

            Int32 sentbyte = ao.workSocket.EndSend(ar);

            if (sentbyte > 0)
            {
                string strmsgAsyncServ = Encoding.UTF8.GetString(ao.buffer);
                string[] strmsgSplit = strmsgAsyncServ.Trim().Split('/');
                switch (strmsgSplit[0])
                {
                    case "AsyncFileTrans":
                        listBoxAsyncServ.Items.Add("파일전송을 요청하였습니다.");
                        break;
                    case "AsyncFileTransYes":
                        listBoxAsyncServ.Items.Add("파일전송을 진행합니다.");
                        break;
                    case "AsyncFileTransNo":
                        listBoxAsyncServ.Items.Add("파일전송을 거절하였습니다.");
                        break;
                    case "ImageSend":
                        listBoxAsyncServ.Items.Add("사진공유를 요청하였습니다.");
                        break;
                    case "ImageOK":
                        listBoxAsyncServ.Items.Add("사진공유를 진행합니다.");
                        break;
                    case "ImageNO":
                        listBoxAsyncServ.Items.Add("사진공유를 거절하였습니다.");
                        break;
                    default:
                        textBoxAsyncServChatArea.AppendText(Encoding.UTF8.GetString(ao.buffer) + "\r\n");
                        break;
                }
            }
            listBoxAsyncServ.SelectedIndex = listBoxAsyncServ.Items.Count - 1;
        }

        private void AsyncHandleServFileAccept(IAsyncResult ar)
        {
            fileWorkServAccpetSocket = fileWorkServAccpetSocket.EndAccept(ar);

            IPEndPoint ipEPAcceptClient = (IPEndPoint)fileWorkServAccpetSocket.RemoteEndPoint;

            if (fileWorkServAccpetSocket.Connected)
                listBoxAsyncServ.Items.Add(ipEPAcceptClient.Address + "에서 파일서버 접속함.");

            listBoxAsyncServ.SelectedIndex = listBoxAsyncServ.Items.Count - 1;
            AsyncServObject ao = new AsyncServObject(4096);
            ao.workSocket = fileWorkServAccpetSocket;

            fileWorkServAccpetSocket.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServFileReceive, ao);
        }

        private void AsyncHandleServFileConnect(IAsyncResult ar)
        {
            Socket sock = (Socket)ar.AsyncState;
            try
            {
                sock.EndConnect(ar);

                if (sock.Connected)
                {
                    AsyncServObject ao = new AsyncServObject(4096);
                    ao.workSocket = sock;
                    listBoxAsyncClient.Items.Add("파일서버 접속 완료");
                    listBoxAsyncClient.SelectedIndex = listBoxAsyncClient.Items.Count - 1;
                    sock.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServFileReceive, ao);
                }
                else
                {
                    sock.Close();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void AsyncHandleServFileRecv(IAsyncResult ar)
        {
            AsyncServObject ao = (AsyncServObject)ar.AsyncState;
            Int32 recvBytes = ao.workSocket.EndReceive(ar);

            if (recvBytes > 0)
            {
                string message = Encoding.UTF8.GetString(ao.buffer);
                string[] splitMsg = message.Trim().Split('/');
                switch (splitMsg[0])
                {
                    case "FileSize":
                        //splitmsg[1] = "파일크기";
                        //splitmsg[2] = "파일이름";
                        string[] splitMsg2 = splitMsg[2].Split('.');
                        splitMsg2[0] += " 새로전송됨." + splitMsg2[1];

                        saveFileDialog1.DefaultExt = splitMsg2[1];
                        saveFileDialog1.InitialDirectory = @"C:\";
                        saveFileDialog1.RestoreDirectory = true;

                        AsyncFileFormLoad AsyncServerSaveDialog = new AsyncFileFormLoad(FileFormLoad);
                        this.Invoke(AsyncServerSaveDialog, "SaveDialogServer", message);

                        //ao.workSocket.Send(Encoding.UTF8.GetBytes("OK/"));
                        break;
                    case "FileFormAcceptClose":
                        if (FileServTrans.Visible && !FileServTrans.IsDisposed)
                        {
                            Form1.toform2 -= new Form1.AsyncFileTrans(FileServTrans.SetFileTextBox);
                            FileServTrans.Dispose();
                            MessageBox.Show("상대방이 파일전송 폼을 종료하여 자동 종료되었습니다.");
                            buttonAsyncServFileSend.Enabled = true;
                        }
                        break;
                    case "FileFormConnectClose":
                        if (FileServReceive.Visible && !FileServReceive.IsDisposed)
                        {
                            Form1.toform2 -= new Form1.AsyncFileTrans(FileServReceive.SetFileTextBox);
                            FileServReceive.Dispose();
                            MessageBox.Show("상대방이 파일전송 폼을 종료하여 자동 종료되었습니다.");
                            buttonAsyncServFileSend.Enabled = true;
                        }
                        break;
                    case "OK":
                        toform2("OK", 3, null);
                        break;
                    case "NO":
                        toform2("NO", 4, null);
                        break;
                    default:
                        if (asyncStreamServfileRecv != null)
                            asyncStreamServfileRecv.Write(ao.buffer, 0, ao.buffer.Length);
                        toform2(Encoding.UTF8.GetString(ao.buffer), 2, asyncStreamServfileRecv);
                        break;


                }
            }

            ao.inibuffer();
            ao.workSocket.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServFileReceive, ao);
        }

        private void AsyncHandleServFileSend(IAsyncResult ar)
        {
            AsyncServObject ao = (AsyncServObject)ar.AsyncState;

            Int32 sentbyte = ao.workSocket.EndSend(ar);

            if (sentbyte > 0)
            {
                //toform2(Encoding.UTF8.GetString(ao.buffer));
            }
        }

        private void buttonAsyncServFileSend_Click(object sender, EventArgs e)
        {
            AsyncServObject ao = new AsyncServObject(1);
            ao.buffer = Encoding.UTF8.GetBytes("AsyncFileTrans/");
            ao.workSocket = socketAyncServerMain;

            if (fileWorkServAccpetSocket != null)
            {
                if (!fileWorkServAccpetSocket.IsBound)
                {
                    asyncCbServFileAccept = new AsyncCallback(AsyncHandleServFileAccept);
                    asyncCbServFileSend = new AsyncCallback(AsyncHandleServFileSend);
                    asyncCbServFileReceive = new AsyncCallback(AsyncHandleServFileRecv);

                    fileWorkServAccpetSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    fileWorkServAccpetSocket.Bind(new IPEndPoint(IPAddress.Parse(labelAsyncServ2.Text), 8890));
                    fileWorkServAccpetSocket.Listen(5);
                    fileWorkServAccpetSocket.BeginAccept(asyncCbServFileAccept, null);
                }
            }
            else
            {
                asyncCbServFileAccept = new AsyncCallback(AsyncHandleServFileAccept);
                asyncCbServFileSend = new AsyncCallback(AsyncHandleServFileSend);
                asyncCbServFileReceive = new AsyncCallback(AsyncHandleServFileRecv);

                fileWorkServAccpetSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                fileWorkServAccpetSocket.Bind(new IPEndPoint(IPAddress.Parse(labelAsyncServ2.Text), 8890));
                fileWorkServAccpetSocket.Listen(5);
                fileWorkServAccpetSocket.BeginAccept(asyncCbServFileAccept, null);
            }
            buttonAsyncServFileSend.Enabled = false;
            socketAyncServerMain.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServSend, ao);
        }

        private void buttonAsyncServSendPicture_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "이미지|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.psd";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxAsyncServ.Image = Bitmap.FromFile(openFileDialog1.FileName);
                if (msPictureAsyncServ != null)
                {
                    msPictureAsyncServ.Close();
                    msPictureAsyncServ.Dispose();
                }
                msPictureAsyncServ = new MemoryStream();
                pictureBoxAsyncServ.Image.Save(msPictureAsyncServ, System.Drawing.Imaging.ImageFormat.Png);


                AsyncServObject ao = new AsyncServObject(1);
                ao.buffer = Encoding.UTF8.GetBytes("ImageSend/" + msPictureAsyncServ.Length + "/");

                ao.workSocket = socketAyncServerMain;

                if (ImageAsyncServAcceptSocket != null)
                {
                    if (!ImageAsyncServAcceptSocket.IsBound)
                    {
                        asyncCbServImageAccept = new AsyncCallback(AsyncHandleServImageAccept);
                        asyncCbServImageSend = new AsyncCallback(AsyncHandleServImageSend);
                        asyncCbServImageReceive = new AsyncCallback(AsyncHandleServImageReceive);

                        ImageAsyncServAcceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        ImageAsyncServAcceptSocket.Bind(new IPEndPoint(IPAddress.Parse(labelAsyncServ2.Text), 7878));
                        ImageAsyncServAcceptSocket.Listen(5);
                        ImageAsyncServAcceptSocket.BeginAccept(asyncCbServImageAccept, null);
                    }
                }
                else
                {
                    asyncCbServImageAccept = new AsyncCallback(AsyncHandleServImageAccept);
                    asyncCbServImageSend = new AsyncCallback(AsyncHandleServImageSend);
                    asyncCbServImageReceive = new AsyncCallback(AsyncHandleServImageReceive);

                    ImageAsyncServAcceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ImageAsyncServAcceptSocket.Bind(new IPEndPoint(IPAddress.Parse(labelAsyncServ2.Text), 7878));
                    ImageAsyncServAcceptSocket.Listen(5);
                    ImageAsyncServAcceptSocket.BeginAccept(asyncCbServImageAccept, null);
                }
                socketAyncServerMain.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServSend, ao);
                buttonAsyncServSendPicture.Enabled = false;
                openFileDialog1.Dispose();
            }
            else
            {
                openFileDialog1.Dispose();
            }
        }

        private void AsyncHandleServImageAccept(IAsyncResult ar)
        {
            ImageAsyncServAcceptSocket = ImageAsyncServAcceptSocket.EndAccept(ar);

            IPEndPoint ipEPAcceptClient = (IPEndPoint)ImageAsyncServAcceptSocket.RemoteEndPoint;

            if (ImageAsyncServAcceptSocket.Connected)
                listBoxAsyncServ.Items.Add(ipEPAcceptClient.Address + "에서 이미지서버 접속함.");

            listBoxAsyncServ.SelectedIndex = listBoxAsyncServ.Items.Count - 1;
            AsyncServObject ao = new AsyncServObject(4096);
            ao.workSocket = ImageAsyncServAcceptSocket;

            ImageAsyncServAcceptSocket.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServImageReceive, ao);
        }

        private void AsyncHandleServImageConnect(IAsyncResult ar)
        {
            Socket sock = (Socket)ar.AsyncState;
            try
            {
                sock.EndConnect(ar);

                if (sock.Connected)
                {
                    AsyncServObject ao = new AsyncServObject(4096);
                    ao.workSocket = sock;
                    listBoxAsyncServ.Items.Add("이미지서버 접속 완료");
                    listBoxAsyncServ.SelectedIndex = listBoxAsyncServ.Items.Count - 1;
                    sock.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServImageReceive, ao);
                }
                else
                {
                    sock.Close();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void AsyncHandleServImageSend(IAsyncResult ar)
        {
            AsyncServObject ao = (AsyncServObject)ar.AsyncState;
            Int32 sentbyte = ao.workSocket.EndSend(ar);

            if (sentbyte > 0)
            {
                buttonAsyncServSendPicture.Enabled = true;
            }
        }

        int countserv = 0;

        private void AsyncHandleServImageReceive(IAsyncResult ar)
        {
            AsyncServObject ao = (AsyncServObject)ar.AsyncState;
            Int32 recvBytes = ao.workSocket.EndReceive(ar);

            if (recvBytes > 0)
            {
                string message = Encoding.UTF8.GetString(ao.buffer);
                string[] splitMsg = message.Trim().Split('/');
                switch (splitMsg[0])
                {
                    case "ImageInfo":
                        //클라이언트가 사진전송을 허락하여 사진전송

                        break;
                    default:

                        if (msPictureAsyncServ.Length == btAsyncServerPicture.Length)
                        {
                            break;
                        }

                        if (countserv != btAsyncServerPicture.Length / 4096)
                        {
                            countserv++;
                            msPictureAsyncServ.Write(ao.buffer, 0, ao.buffer.Length);
                        }
                        else
                        {
                            msPictureAsyncServ.Write(ao.buffer, 0, btAsyncServerPicture.Length % 4096);
                            countserv = 0;
                        }
                        //pictureBoxAsyncServ.Image = Bitmap.FromStream(msPictureAsyncServ);
                        if (msPictureAsyncServ.Length >= btAsyncServerPicture.Length)
                        {
                            pictureBoxAsyncServ.Image = Bitmap.FromStream(msPictureAsyncServ);

                            buttonAsyncServSendPicture.Enabled = true;
                        }
                        break;
                }
            }

            ao.inibuffer();
            ao.workSocket.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbServImageReceive, ao);
        }

        public void AsyncServSendImage()
        {
            byte[] btImage = msPictureAsyncServ.ToArray();
            AsyncServObject ao2 = new AsyncServObject(1);

            ao2.buffer = btImage;
            Thread.Sleep(100);
            ImageAsyncServAcceptSocket.Send(ao2.buffer);
            //if (ImageAsyncServAcceptSocket.Connected)
            //{
            //    ao2.workSocket = ImageAsyncServAcceptSocket;
            //    ImageAsyncServAcceptSocket.BeginSend(ao2.buffer, 0, ao2.buffer.Length, SocketFlags.None, asyncCbServImageSend, ao2);
            //}
            //else
            //{
            //    listBoxAsyncServ.Items.Add("재시도중...");
            //    AsyncServSendImage();
            //}
            buttonAsyncServSendPicture.Enabled = true;
        }

        //
        //비동기 클라이언트
        //

        public class AsyncClientObject //클라이언트에서 전송하는 데이터 형식
        {
            public Socket workSocket;
            public byte[] buffer;
            public Int32 size;
            public AsyncClientObject(Int32 bufferSize)
            {
                this.size = bufferSize;
                this.buffer = new byte[size];
            }
            public void inibuffer()
            {
                this.buffer = null;
                this.buffer = new byte[size];
            }
        }

        Socket socketConAsyncClient = null;
        Socket fileWorkClientConnectSocket = null;
        Socket fileWorkClientAcceptSocket = null;
        Socket ImageAsyncClientAccepttSocket = null;
        Socket ImageAsyncClientConnectSocket = null;

        AsyncCallback asyncCbClientSend = null;
        AsyncCallback asyncCbClientrecv = null;
        AsyncCallback asyncCbClientConnect = null;
        AsyncCallback asyncCbClientFileConnect = null;
        AsyncCallback asyncCbClientFileAccept = null;
        AsyncCallback asyncCbClientFileSend = null;
        AsyncCallback asyncCbClientFileRecv = null;
        AsyncCallback asyncCbClientImageAccept = null;
        AsyncCallback asyncCbClientImageReceive = null;
        AsyncCallback asyncCbClientImageSend = null;
        AsyncCallback asyncCbClientImageConnect = null;

        public static FileStream asyncStreamClientFileRecv = null;

        MemoryStream msPictureAsyncClient;
        byte[] btAsyncClientPicture;

        private void buttonAsyncClientConnect_Click(object sender, EventArgs e)
        {
            try
            {
                socketConAsyncClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipAddr = IPAddress.Parse(textBoxAsyncClientIP.Text);
                IPEndPoint ipep = new IPEndPoint(ipAddr, Convert.ToInt32(textBoxAsyncClientPort.Text));

                asyncCbClientConnect = new AsyncCallback(AsyncHandleClientConnect);
                asyncCbClientrecv = new AsyncCallback(AsyncHandleClientReceive);
                asyncCbClientSend = new AsyncCallback(AsyncHandleClientSend);

                socketConAsyncClient.BeginConnect(ipep, asyncCbClientConnect, socketConAsyncClient);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void AsyncHandleClientConnect(IAsyncResult ar)
        {
            Socket sock = (Socket)ar.AsyncState;
            try
            {
                sock.EndConnect(ar);

                if (sock.Connected)
                {
                    AsyncClientObject ao = new AsyncClientObject(4096);
                    ao.workSocket = sock;
                    sock.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientrecv, ao);
                    listBoxAsyncClient.Items.Add("서버에 연결되었습니다.");

                    buttonAsyncClientConnect.Enabled = false;
                    buttonAsyncClientFileSend.Enabled = true;
                    buttonAsyncClientSend.Enabled = true;
                    buttonAsyncClientSendPicture.Enabled = true;
                }
                else
                {
                    listBoxAsyncClient.Items.Add("연결에 실패하였습니다.");
                    sock.Close();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            listBoxAsyncClient.SelectedIndex = listBoxAsyncClient.Items.Count - 1;
        }

        private void AsyncHandleClientReceive(IAsyncResult ar)
        {
            AsyncClientObject ao = (AsyncClientObject)ar.AsyncState;
            Int32 recvBytes = ao.workSocket.EndReceive(ar);
            if (recvBytes > 0)
            {
                string strmsgAsyncClient = Encoding.UTF8.GetString(ao.buffer);
                string[] strmsgSplit = strmsgAsyncClient.Trim().Split('/');
                switch (strmsgSplit[0])
                {
                    case "AsyncFileTrans":
                        listBoxAsyncClient.Items.Add("상대방이 파일전송을 희망합니다.");
                        buttonAsyncClientFileSend.Enabled = false;

                        if (MessageBox.Show("상대방이 파일전송을 희망합니다. 수락하시겠습니까 ?", "파일전송", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            ao.inibuffer();
                            ao.buffer = Encoding.UTF8.GetBytes("AsyncFileTransYes/");
                            socketConAsyncClient.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientSend, ao);
                            //파일전송
                            if (fileWorkClientConnectSocket == null || !fileWorkClientConnectSocket.Connected)
                            {
                                asyncCbClientFileConnect = new AsyncCallback(AsyncHandleClientFileConnect);
                                asyncCbClientFileSend = new AsyncCallback(AsyncHandleClientFileSend);
                                asyncCbClientFileRecv = new AsyncCallback(AsyncHandleClientFileRecv);

                                fileWorkClientConnectSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                fileWorkClientConnectSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(textBoxAsyncClientIP.Text), 8890), asyncCbClientFileConnect, fileWorkClientConnectSocket);
                            }

                            AsyncFileFormLoad AsyncFileReceive = new AsyncFileFormLoad(FileFormLoad);
                            this.Invoke(AsyncFileReceive, "FileReceive", "Client");
                        }
                        else
                        {
                            buttonAsyncClientFileSend.Enabled = true;
                            ao.inibuffer();
                            ao.buffer = Encoding.UTF8.GetBytes("AsyncFileTransNo/");
                            socketConAsyncClient.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientSend, ao);
                            //거절의사 전송
                        }
                        break;
                    case "AsyncFileTransYes":
                        listBoxAsyncClient.Items.Add("상대방이 파일전송을 허락하였습니다.");
                        AsyncFileFormLoad AsyncFileTrans = new AsyncFileFormLoad(FileFormLoad);
                        this.Invoke(AsyncFileTrans, "FileTrans", "Client");
                        break;
                    case "AsyncFileTransNo":
                        listBoxAsyncClient.Items.Add("상대방이 파일전송을 거절하였습니다.");
                        buttonAsyncClientFileSend.Enabled = true;
                        break;
                    case "ImageSend":
                        //서버 나에게 사진전송을 허락할것인지 묻는 메시지
                        listBoxAsyncClient.Items.Add("상대방이 사진을 공유하고 싶어합니다.");
                        buttonAsyncClientSendPicture.Enabled = false;
                        if (MessageBox.Show("상대방이 사진을 공유하고 싶어합니다. 수락하시겠습니까 ?", "사진전송", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            btAsyncClientPicture = new byte[Convert.ToInt32(strmsgSplit[1])];
                            ao.inibuffer();
                            ao.buffer = Encoding.UTF8.GetBytes("ImageOK/");
                            socketConAsyncClient.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientSend, ao);

                            if (ImageAsyncClientConnectSocket == null || !ImageAsyncClientConnectSocket.Connected)
                            {
                                asyncCbClientImageSend = new AsyncCallback(AsyncHandleClientImageSend);
                                asyncCbClientImageConnect = new AsyncCallback(AsyncHandleClientImageConnect);
                                asyncCbClientImageReceive = new AsyncCallback(AsyncHandleClientImageReceive);

                                ImageAsyncClientConnectSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                ImageAsyncClientConnectSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(textBoxAsyncClientIP.Text), 7878), asyncCbClientImageConnect, ImageAsyncClientConnectSocket);
                            }
                            if (msPictureAsyncClient != null)
                            {
                                msPictureAsyncClient.Flush();
                                msPictureAsyncClient.Close();
                                msPictureAsyncClient.Dispose();
                                msPictureAsyncClient = null;
                            }
                            msPictureAsyncClient = new MemoryStream();
                        }
                        else
                        {
                            ao.inibuffer();
                            ao.buffer = Encoding.UTF8.GetBytes("ImageNO/");
                            socketConAsyncClient.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientSend, ao);
                            //거절의사 전송
                            buttonAsyncClientSendPicture.Enabled = true;
                        }
                        break;
                    case "ImageOK":
                        //서버가 사진전송을 허락하여 사진전송
                        listBoxAsyncClient.Items.Add("상대방이 사진공유를 허락하였습니다.");

                        AsyncClientSendImage();
                        break;
                    case "ImageNO":
                        //서버가 사진전송을 거절하여 사진거절
                        listBoxAsyncClient.Items.Add("상대방이 사진공유를 거절하였습니다.");
                        buttonAsyncClientSendPicture.Enabled = true;
                        break;
                    default:
                        textBoxAsyncClientChatArea.AppendText(Encoding.UTF8.GetString(ao.buffer));
                        textBoxAsyncClientChatArea.AppendText("\r\n");
                        break;
                }

            }
            listBoxAsyncClient.SelectedIndex = listBoxAsyncClient.Items.Count - 1;
            Thread.Sleep(10);
            ao.inibuffer();
            ao.workSocket.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientrecv, ao);
        }

        private void buttonAsyncClientSend_Click(object sender, EventArgs e)
        {
            AsyncClientObject ao = new AsyncClientObject(1);

            ao.buffer = Encoding.UTF8.GetBytes("클라이언트(비동기) : " + textBoxAsyncClientSend.Text);

            textBoxAsyncClientSend.Text = "";
            ao.workSocket = socketConAsyncClient;
            socketConAsyncClient.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientSend, ao);
        }

        private void AsyncHandleClientSend(IAsyncResult ar)
        {
            AsyncClientObject ao = (AsyncClientObject)ar.AsyncState;

            Int32 sentbyte = ao.workSocket.EndSend(ar);

            if (sentbyte > 0)
            {
                string strmsgAsyncClient = Encoding.UTF8.GetString(ao.buffer);
                string[] strmsgSplit = strmsgAsyncClient.Trim().Split('/');
                switch (strmsgSplit[0])
                {
                    case "AsyncFileTrans":
                        listBoxAsyncClient.Items.Add("파일전송을 요청하였습니다.");
                        break;
                    case "AsyncFileTransYes":
                        listBoxAsyncClient.Items.Add("파일전송을 진행합니다.");
                        break;
                    case "AsyncFileTransNo":
                        listBoxAsyncClient.Items.Add("파일전송을 거절합니다.");
                        break;
                    case "ImageSend":
                        listBoxAsyncClient.Items.Add("사진공유를 요청하였습니다.");
                        break;
                    case "ImageOK":
                        listBoxAsyncClient.Items.Add("사진공유를 진행합니다.");
                        break;
                    case "ImageNO":
                        listBoxAsyncClient.Items.Add("사진공유를 거절하였습니다.");
                        break;
                    default:
                        textBoxAsyncClientChatArea.AppendText(Encoding.UTF8.GetString(ao.buffer) + "\r\n");
                        break;
                }
            }
        }

        private void AsyncHandleClientFileAccept(IAsyncResult ar)
        {
            fileWorkClientAcceptSocket = fileWorkClientAcceptSocket.EndAccept(ar);

            IPEndPoint ipEPAcceptClient = (IPEndPoint)fileWorkClientAcceptSocket.RemoteEndPoint;
            listBoxAsyncClient.Items.Add(ipEPAcceptClient.Address + "에서 파일서버 접속함.");
            listBoxAsyncClient.SelectedIndex = listBoxAsyncClient.Items.Count - 1;
            AsyncClientObject ao = new AsyncClientObject(4096);
            ao.workSocket = fileWorkClientAcceptSocket;

            fileWorkClientAcceptSocket.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientFileRecv, ao);
        }

        private void AsyncHandleClientFileConnect(IAsyncResult ar)
        {
            Socket sock = (Socket)ar.AsyncState;
            try
            {
                sock.EndConnect(ar);

                if (sock.Connected)
                {
                    AsyncClientObject ao = new AsyncClientObject(4096);
                    ao.workSocket = sock;
                    listBoxAsyncClient.Items.Add("파일서버 접속 완료");
                    listBoxAsyncClient.SelectedIndex = listBoxAsyncClient.Items.Count - 1;
                    sock.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientFileRecv, ao);
                }
                else
                {
                    sock.Close();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void AsyncHandleClientFileRecv(IAsyncResult ar)
        {
            AsyncClientObject ao = (AsyncClientObject)ar.AsyncState;
            Int32 recvBytes = ao.workSocket.EndReceive(ar);

            if (recvBytes > 0)
            {

                string message = Encoding.UTF8.GetString(ao.buffer);
                string[] splitMsg = message.Trim().Split('/');
                switch (splitMsg[0])
                {
                    case "FileSize":
                        //splitmsg[1] = "파일크기";
                        //splitmsg[2] = "파일이름";
                        string[] splitMsg2 = splitMsg[2].Split('.');
                        splitMsg2[0] += " 새로전송됨." + splitMsg2[1];
                        saveFileDialog1.DefaultExt = splitMsg2[1];
                        saveFileDialog1.InitialDirectory = @"C:\";
                        saveFileDialog1.RestoreDirectory = true;

                        AsyncFileFormLoad AsyncClientSaveDialog = new AsyncFileFormLoad(FileFormLoad);
                        this.Invoke(AsyncClientSaveDialog, "SaveDialogClient", message);
                        break;
                    case "FileFormAcceptClose":
                        if (FileClientTrans.Visible && !FileClientTrans.IsDisposed)
                        {
                            Form1.toform3 -= new Form1.AsyncFileTrans(FileClientTrans.SetFileTextBox);
                            FileClientTrans.Dispose();
                            MessageBox.Show("상대방이 파일전송 폼을 종료하여 자동 종료되었습니다.");
                            buttonAsyncClientFileSend.Enabled = true;
                        }
                        break;
                    case "FileFormConnectClose":
                        if (FileClientReceive.Visible && !FileClientReceive.IsDisposed)
                        {
                            Form1.toform3 -= new Form1.AsyncFileTrans(FileClientReceive.SetFileTextBox);
                            FileClientReceive.Dispose();
                            MessageBox.Show("상대방이 파일전송 폼을 종료하여 자동 종료되었습니다.");
                            buttonAsyncClientFileSend.Enabled = true;
                        }
                        break;
                    case "OK":
                        toform3("OK", 3, null);
                        break;
                    case "NO":
                        toform3("NO", 4, null);
                        break;
                    default:
                        if (asyncStreamClientFileRecv != null)
                            asyncStreamClientFileRecv.Write(ao.buffer, 0, ao.buffer.Length);
                        toform3(Encoding.UTF8.GetString(ao.buffer), 2, asyncStreamClientFileRecv);
                        break;

                }
            }

            ao.inibuffer();
            ao.workSocket.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientFileRecv, ao);
        }

        private void AsyncHandleClientFileSend(IAsyncResult ar)
        {
            AsyncClientObject ao = (AsyncClientObject)ar.AsyncState;

            Int32 sentbyte = ao.workSocket.EndSend(ar);

            if (sentbyte > 0)
            {
                //toform2(Encoding.UTF8.GetString(ao.buffer));
            }
        }

        private void buttonAsyncClientFileSend_Click(object sender, EventArgs e)
        {
            AsyncClientObject ao = new AsyncClientObject(1);
            ao.buffer = Encoding.UTF8.GetBytes("AsyncFileTrans/");
            ao.workSocket = socketConAsyncClient;

            if (fileWorkClientAcceptSocket != null)
            {
                if (!fileWorkClientAcceptSocket.IsBound)
                {
                    asyncCbClientFileAccept = new AsyncCallback(AsyncHandleClientFileAccept);
                    asyncCbClientFileSend = new AsyncCallback(AsyncHandleClientFileSend);
                    asyncCbClientFileRecv = new AsyncCallback(AsyncHandleClientFileRecv);

                    fileWorkClientAcceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    fileWorkClientAcceptSocket.Bind(new IPEndPoint(IPAddress.Parse(labelAsyncClient2.Text), 8889));
                    fileWorkClientAcceptSocket.Listen(5);
                    fileWorkClientAcceptSocket.BeginAccept(asyncCbClientFileAccept, null);
                }

            }
            else
            {
                asyncCbClientFileAccept = new AsyncCallback(AsyncHandleClientFileAccept);
                asyncCbClientFileSend = new AsyncCallback(AsyncHandleClientFileSend);
                asyncCbClientFileRecv = new AsyncCallback(AsyncHandleClientFileRecv);

                fileWorkClientAcceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                fileWorkClientAcceptSocket.Bind(new IPEndPoint(IPAddress.Parse(labelAsyncClient2.Text), 8889));
                fileWorkClientAcceptSocket.Listen(5);
                fileWorkClientAcceptSocket.BeginAccept(asyncCbClientFileAccept, null);
            }
            buttonAsyncClientFileSend.Enabled = false;
            socketConAsyncClient.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientSend, ao);
        }

        private void buttonAsyncClientSendPicture_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "이미지|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.psd";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxAsyncClient.Image = Bitmap.FromFile(openFileDialog1.FileName);

                if (msPictureAsyncClient != null)
                {
                    msPictureAsyncClient.Close();
                    msPictureAsyncClient.Dispose();
                }
                msPictureAsyncClient = new MemoryStream();
                pictureBoxAsyncClient.Image.Save(msPictureAsyncClient, System.Drawing.Imaging.ImageFormat.Png);

                AsyncClientObject ao = new AsyncClientObject(1);
                ao.buffer = Encoding.UTF8.GetBytes("ImageSend/" + msPictureAsyncClient.Length + "/");
                ao.workSocket = socketConAsyncClient;

                if (ImageAsyncClientAccepttSocket != null)
                {
                    if (!ImageAsyncClientAccepttSocket.IsBound)
                    {
                        asyncCbClientImageAccept = new AsyncCallback(AsyncHandleClientImageAccept);
                        asyncCbClientImageSend = new AsyncCallback(AsyncHandleClientImageSend);
                        asyncCbClientImageReceive = new AsyncCallback(AsyncHandleClientImageReceive);

                        ImageAsyncClientAccepttSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        ImageAsyncClientAccepttSocket.Bind(new IPEndPoint(IPAddress.Parse(labelAsyncClient2.Text), 7879));
                        ImageAsyncClientAccepttSocket.Listen(5);
                        ImageAsyncClientAccepttSocket.BeginAccept(asyncCbClientImageAccept, null);
                    }
                }
                else
                {
                    asyncCbClientImageAccept = new AsyncCallback(AsyncHandleClientImageAccept);
                    asyncCbClientImageSend = new AsyncCallback(AsyncHandleClientImageSend);
                    asyncCbClientImageReceive = new AsyncCallback(AsyncHandleClientImageReceive);

                    ImageAsyncClientAccepttSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ImageAsyncClientAccepttSocket.Bind(new IPEndPoint(IPAddress.Parse(labelAsyncClient2.Text), 7879));
                    ImageAsyncClientAccepttSocket.Listen(5);
                    ImageAsyncClientAccepttSocket.BeginAccept(asyncCbClientImageAccept, null);
                }

                socketConAsyncClient.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientSend, ao);
                openFileDialog1.Dispose();
            }
            else
            {
                openFileDialog1.Dispose();
            }

        }

        private void AsyncHandleClientImageAccept(IAsyncResult ar)
        {
            ImageAsyncClientAccepttSocket = ImageAsyncClientAccepttSocket.EndAccept(ar);

            IPEndPoint ipEPAcceptClient = (IPEndPoint)ImageAsyncClientAccepttSocket.RemoteEndPoint;

            if (ImageAsyncClientAccepttSocket.Connected)
                listBoxAsyncClient.Items.Add(ipEPAcceptClient.Address + "에서 이미지서버 접속함.");

            listBoxAsyncClient.SelectedIndex = listBoxAsyncClient.Items.Count - 1;
            AsyncClientObject ao = new AsyncClientObject(4096);
            ao.workSocket = ImageAsyncClientAccepttSocket;

            ImageAsyncClientAccepttSocket.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientImageReceive, ao);
        }

        private void AsyncHandleClientImageConnect(IAsyncResult ar)
        {
            Socket sock = (Socket)ar.AsyncState;
            try
            {
                sock.EndConnect(ar);

                if (sock.Connected)
                {
                    AsyncClientObject ao = new AsyncClientObject(4096);
                    ao.workSocket = sock;
                    listBoxAsyncClient.Items.Add("이미지서버 접속 완료");
                    listBoxAsyncClient.SelectedIndex = listBoxAsyncClient.Items.Count - 1;
                    sock.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientImageReceive, ao);
                }
                else
                {
                    sock.Close();
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void AsyncHandleClientImageSend(IAsyncResult ar)
        {
            AsyncClientObject ao = (AsyncClientObject)ar.AsyncState;
            Int32 sentbyte = ao.workSocket.EndSend(ar);
            if (sentbyte > 0)
            {
                buttonAsyncClientSendPicture.Enabled = true;
            }
        }

        int count = 0;

        private void AsyncHandleClientImageReceive(IAsyncResult ar)
        {
            AsyncClientObject ao = (AsyncClientObject)ar.AsyncState;
            Int32 recvBytes = ao.workSocket.EndReceive(ar);

            if (recvBytes > 0)
            {
                string message = Encoding.UTF8.GetString(ao.buffer);
                string[] splitMsg = message.Trim().Split('/');
                switch (splitMsg[0])
                {
                    case "ImageInfo":
                        break;
                    default:
                        if (msPictureAsyncClient.Length == btAsyncClientPicture.Length)
                        {
                            break;
                        }

                        if (count != btAsyncClientPicture.Length / 4096)
                        {
                            msPictureAsyncClient.Write(ao.buffer, 0, ao.buffer.Length);
                            count++;
                        }
                        else
                        {
                            msPictureAsyncClient.Write(ao.buffer, 0, btAsyncClientPicture.Length % 4096);
                        }

                        //pictureBoxAsyncClient.Image = Bitmap.FromStream(msPictureAsyncClient);
                        if (msPictureAsyncClient.Length >= btAsyncClientPicture.Length)
                        {
                            pictureBoxAsyncClient.Image = Bitmap.FromStream(msPictureAsyncClient);

                            buttonAsyncClientSendPicture.Enabled = true;
                            count = 0;
                        }
                        break;
                }
            }

            ao.inibuffer();
            ao.workSocket.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, asyncCbClientImageReceive, ao);
        }

        public void AsyncClientSendImage()
        {
            byte[] btImage = msPictureAsyncClient.ToArray();
            AsyncClientObject ao2 = new AsyncClientObject(1);

            ao2.workSocket = ImageAsyncClientAccepttSocket;

            ao2.buffer = btImage;
            Thread.Sleep(100);
            ImageAsyncClientAccepttSocket.Send(ao2.buffer);
            buttonAsyncClientSendPicture.Enabled = true;
            //if(ImageAsyncClientAccepttSocket.Connected)
            //{
            //    ImageAsyncClientAccepttSocket.BeginSend(ao2.buffer, 0, ao2.buffer.Length, SocketFlags.None, asyncCbClientImageSend, ao2);
            //}
            //else
            //{
            //    listBoxAsyncClient.Items.Add("재시도중. . .");
            //    AsyncClientSendImage();
            //}


        }


        //기타 델리게이트 함수들

        private void FileFormLoad(string type, string title)
        {
            Thread.Sleep(100);
            switch (type)
            {
                case "FileTrans":
                    if (title == "Server")
                    {
                        if (fileWorkServAccpetSocket.Connected)
                        {
                            FileServTrans = new Form2("FileTrans", fileWorkServAccpetSocket);

                            int CenterX = this.Location.X + (this.Width / 2); //부모 폼 중심에 자식 폼을 띄우기 위한 로직
                            int CenterY = this.Location.Y + (this.Height / 2);
                            int fileformX = CenterX - (FileServTrans.Width / 2);
                            int fileformY = CenterY - (FileServTrans.Height / 2);

                            FileServTrans.Location = new Point(fileformX, fileformY);
                            FileServTrans.Text = title;
                            FileServTrans.Show();
                            Form2.toform1 += new Form2.AsyncFormClose(AsyncFormClose);
                        }
                    }
                    else
                    {
                        if (fileWorkClientAcceptSocket.Connected)
                        {
                            FileClientTrans = new Form3("FileTrans", fileWorkClientAcceptSocket);

                            int CenterX = this.Location.X + (this.Width / 2); //부모 폼 중심에 자식 폼을 띄우기 위한 로직
                            int CenterY = this.Location.Y + (this.Height / 2);
                            int fileformX = CenterX - (FileClientTrans.Width / 2);
                            int fileformY = CenterY - (FileClientTrans.Height / 2);

                            FileClientTrans.Location = new Point(fileformX, fileformY);
                            FileClientTrans.Text = title;
                            FileClientTrans.Show();
                            Form3.toform1 += new Form3.AsyncFormClose(AsyncFormClose);
                        }
                    }

                    break;
                case "FileReceive":
                    if (title == "Server")
                    {
                        if (fileWorkServConnectSocket.Connected)
                        {
                            FileServReceive = new Form2("FileReceive", fileWorkServConnectSocket);

                            int CenterX = this.Location.X + (this.Width / 2); //부모 폼 중심에 자식 폼을 띄우기 위한 로직
                            int CenterY = this.Location.Y + (this.Height / 2);
                            int fileformX = CenterX - (FileServReceive.Width / 2);
                            int fileformY = CenterY - (FileServReceive.Height / 2);

                            FileServReceive.Location = new Point(fileformX, fileformY);
                            FileServReceive.Text = title;
                            FileServReceive.Show();
                            Form2.toform1 += new Form2.AsyncFormClose(AsyncFormClose);
                        }
                    }
                    else
                    {
                        if (fileWorkClientConnectSocket.Connected)
                        {
                            FileClientReceive = new Form3("FileReceive", fileWorkClientConnectSocket);

                            int CenterX = this.Location.X + (this.Width / 2); //부모 폼 중심에 자식 폼을 띄우기 위한 로직
                            int CenterY = this.Location.Y + (this.Height / 2);
                            int fileformX = CenterX - (FileClientReceive.Width / 2);
                            int fileformY = CenterY - (FileClientReceive.Height / 2);

                            FileClientReceive.Location = new Point(fileformX, fileformY);
                            FileClientReceive.Text = title;
                            FileClientReceive.Show();
                            Form3.toform1 += new Form3.AsyncFormClose(AsyncFormClose);
                        }
                    }
                    break;
                case "SaveDialogServer":
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        asyncStreamServfileRecv = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite);
                        Thread.Sleep(100);
                        toform2(title, 1, asyncStreamServfileRecv);
                        fileWorkServConnectSocket.Send(Encoding.UTF8.GetBytes("OK/"));
                        saveFileDialog1.Dispose();
                    }
                    else
                    {
                        //파일취소햇다는 문구 보내기
                        saveFileDialog1.Dispose();
                        fileWorkServConnectSocket.Send(Encoding.UTF8.GetBytes("NO/"));
                    }
                    break;
                case "SaveDialogClient":
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        asyncStreamClientFileRecv = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite);
                        Thread.Sleep(100);
                        toform3(title, 1, asyncStreamClientFileRecv);
                        fileWorkClientConnectSocket.Send(Encoding.UTF8.GetBytes("OK/"));
                        saveFileDialog1.Dispose();
                    }
                    else
                    {
                        saveFileDialog1.Dispose();
                        fileWorkClientConnectSocket.Send(Encoding.UTF8.GetBytes("NO/"));
                    }
                    break;
                default:
                    break;
            }

        }
        public void AsyncFormClose(string message)
        {
            switch (message)
            {
                case "AcceptClientClose":
                    buttonAsyncClientFileSend.Enabled = true;
                    Form3.toform1 -= new Form3.AsyncFormClose(AsyncFormClose);
                    fileWorkClientAcceptSocket.Send(Encoding.UTF8.GetBytes("FileFormConnectClose/"));
                    break;
                case "ConnectClientClose":
                    buttonAsyncClientFileSend.Enabled = true;
                    Form3.toform1 -= new Form3.AsyncFormClose(AsyncFormClose);
                    fileWorkClientConnectSocket.Send(Encoding.UTF8.GetBytes("FileFormAcceptClose/"));
                    break;
                case "AcceptServerClose":
                    buttonAsyncServFileSend.Enabled = true;
                    Form2.toform1 -= new Form2.AsyncFormClose(AsyncFormClose);
                    fileWorkServAccpetSocket.Send(Encoding.UTF8.GetBytes("FileFormConnectClose/"));
                    break;
                case "ConnectServerClose":
                    buttonAsyncServFileSend.Enabled = true;
                    Form2.toform1 -= new Form2.AsyncFormClose(AsyncFormClose);
                    fileWorkServConnectSocket.Send(Encoding.UTF8.GetBytes("FileFormAcceptClose/"));
                    break;
            }
        }

        private void buttonAsyncServReFresh_Click(object sender, EventArgs e)
        {
            pictureBoxAsyncServ.Image = Bitmap.FromStream(msPictureAsyncServ);
            pictureBoxAsyncServ.Invalidate();

            if (msPictureAsyncServ == null || btAsyncServerPicture == null)
            {
                listBoxAsyncServ.Items.Add("에러, 사진 정보가 없습니다.");
                listBoxAsyncServ.SelectedIndex = listBoxAsyncServ.Items.Count - 1;
                return;
            }

            listBoxAsyncServ.Items.Add("--로그정보 출력 시작--");
            listBoxAsyncServ.Items.Add("msPictureAsyncServ.Length");
            listBoxAsyncServ.Items.Add("=" + msPictureAsyncServ.Length);
            listBoxAsyncServ.Items.Add("btAsyncServerPicture.Length");
            listBoxAsyncServ.Items.Add("=" + btAsyncServerPicture.Length);
            listBoxAsyncServ.Items.Add("--로그정보 출력 끝--");

            listBoxAsyncServ.SelectedIndex = listBoxAsyncServ.Items.Count - 1;
        }

        private void buttonAsyncClientReFresh_Click(object sender, EventArgs e)
        {
            pictureBoxAsyncClient.Image = Bitmap.FromStream(msPictureAsyncClient);
            pictureBoxAsyncClient.Invalidate();

            if (msPictureAsyncClient == null || btAsyncClientPicture == null)
            {
                listBoxAsyncClient.Items.Add("에러, 사진 정보가 없습니다.");
                listBoxAsyncClient.SelectedIndex = listBoxAsyncClient.Items.Count - 1;
                return;
            }

            listBoxAsyncClient.Items.Add("--로그정보 출력 시작--");
            listBoxAsyncClient.Items.Add("msPictureAsyncClient.Length");
            listBoxAsyncClient.Items.Add("=" + msPictureAsyncClient.Length);
            listBoxAsyncClient.Items.Add("btAsyncClientPicture.Length");
            listBoxAsyncClient.Items.Add("=" + btAsyncClientPicture.Length);
            listBoxAsyncClient.Items.Add("--로그정보 출력 끝--");
            listBoxAsyncClient.SelectedIndex = listBoxAsyncClient.Items.Count - 1;
        }
    }

}
