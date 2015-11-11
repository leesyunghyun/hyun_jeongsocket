using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace Hyun_JeongSocket
{
    public partial class Form3 : Form //클라이언트 파일 전송 폼
    {
        public class AsyncObject //클라이언트에서 전송하는 데이터 형식
        {
            public Socket workSocket;
            public byte[] buffer;
            public Int32 size;
            public AsyncObject(Int32 bufferSize)
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

        public delegate void SetProgCallBack(int vv);
        public delegate void SetLabelCallBack(string str);
        public delegate void AsyncFormClose(string message);
        public delegate void FormFileDialog(string str);
        Socket socketFileSend = null;
        FileStream fsTrans = null;
        FileStream fsRecv = null;
        string recvFileExt; //받는 파일 확장자
        string type_of_form3; //클라이언트의 상태 (Accept or Connect)

        long Sendtotalbytes;

        string formType = "";

        public static event AsyncFormClose toform1;


        public Form3(string type, Socket socket)
        {
            InitializeComponent();
            formType = type;
            socketFileSend = socket;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Form1.toform3 += new Form1.AsyncFileTrans(SetFileTextBox);
            InitControl();
        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            FormFileDialog form = new FormFileDialog(SetDialogType);
            form(formType);
        }

        public void InitControl()
        {
            this.textBoxAsyncClientFileTrans.Text = "";
        }

        public void SetFileTextBox(string message, int flag, FileStream fsRecv)
        {
            switch (flag)
            {
                case 1: //파일크기, 이름 전송
                    textBoxAsyncClientFileTrans.Clear();
                    string[] splitMsg = message.Split('/');
                    string[] splitMsg2 = splitMsg[2].Split('.');
                    splitMsg2[0] += " 새로전송됨." + splitMsg2[1];
                    recvFileExt = splitMsg2[1];
                    this.fsRecv = fsRecv;
                    Thread prog1 = new Thread(new ParameterizedThreadStart(RecvProgIng));
                    prog1.Start(Convert.ToInt32(splitMsg[1]));
                    break;
                case 2: //파일데이터 전송
                    this.textBoxAsyncClientFileTrans.AppendText(message);
                    this.fsRecv = fsRecv;
                    break;
                case 3:
                    Thread prog = new Thread(SendProgIng);
                    prog.Start();
                    break;
                case 4:
                    MessageBox.Show("상대방이 파일저장을 취소하였습니다. 다시 시도해주세요.");
                    break;
            }
        }

        public void SetDialogType(string str)
        {
            switch (str)
            {
                case "FileTrans":
                    //파일송신 openfileDialog
                    listBoxClientFileForm.Items.Add("파일 선택중 . . .");

                    buttonAsyncClientFileSave.Enabled = false;

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        textBoxAsyncClientFileTrans.Clear();
                        fsTrans = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                        string[] fileName = fsTrans.Name.Split('\\');

                        socketFileSend.Send(Encoding.UTF8.GetBytes("FileSize" + "/" + fsTrans.Length + "/" + fileName[fileName.Length - 1] + "/"));
                        openFileDialog1.Dispose();
                    }
                    else
                    {
                        listBoxClientFileForm.Items.Add("파일 선택을 취소하셨습니다.");
                        openFileDialog1.Dispose();
                    }
                    type_of_form3 = "AcceptClientClose";
                    break;
                case "FileReceive":
                    textBoxAsyncClientFileTrans.Clear();
                    listBoxClientFileForm.Items.Add("파일 수신 대기중 . . .");
                    listBoxClientFileForm.Items.Add("잠시만 기다려주세요. . .");
                    buttonAsyncClientFileTrans.Enabled = false;
                    type_of_form3 = "ConnectClientClose";
                    break;
                case "ButtonFileTrans":
                    listBoxClientFileForm.Items.Add("파일 선택중 . . .");
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        textBoxAsyncClientFileTrans.Clear();
                        fsTrans = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                        string[] fileName = fsTrans.Name.Split('\\');

                        socketFileSend.Send(Encoding.UTF8.GetBytes("FileSize" + "/" + fsTrans.Length + "/" + fileName[fileName.Length - 1] + "/"));
                        openFileDialog1.Dispose();
                    }
                    else
                    {
                        listBoxClientFileForm.Items.Add("파일 선택을 취소하셨습니다.");
                        openFileDialog1.Dispose();
                    }
                    break;
                case "ButtonFileReceive":
                    saveFileDialog1.DefaultExt = recvFileExt;
                    saveFileDialog1.InitialDirectory = @"C:\";
                    saveFileDialog1.RestoreDirectory = true;
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {

                        //System.IO.BinaryWriter objWrite = new System.IO.BinaryWriter(File.Open(saveFileDialog1.FileName, FileMode.Create));
                        //objWrite.Write(message, 0, message.Length);
                        //objWrite.Close();

                        fsRecv = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite);
                        byte[] message = UnicodeEncoding.Unicode.GetBytes(textBoxAsyncClientFileTrans.Text);
                        string binarystr = string.Empty;
                        foreach (byte b in message)
                        {
                            string s = Convert.ToString(b, 2);
                            binarystr += s.PadLeft(8, '0');
                        }

                        fsRecv.Write(Encoding.Default.GetBytes(binarystr), 0, Encoding.Default.GetBytes(binarystr).Length);
                        fsRecv.Flush();
                        fsRecv.Close();
                        fsRecv.Dispose();
                        saveFileDialog1.Dispose();
                    }
                    else
                    {                        
                        saveFileDialog1.Dispose();
                    }
                    break;
                default:
                    break;
            }
        }

        public void SendProgIng()
        {
            AsyncObject ao = new AsyncObject(1);

            byte[] bytes = new byte[4096];
            int vv = 1;
            int nBytes = 0;

            Sendtotalbytes = 0L;

            //if (str[0] != "OK")
            //{
            //    MessageBox.Show("에러 : 파일정보 불량");
            //    this.listBoxClientFileForm.Items.Add("에러 : 파일정보 불량 ");
            //    return;
            //}
            //else

            this.listBoxClientFileForm.Items.Add("파일전송 시작");
            
            while ((nBytes = fsTrans.Read(bytes, 0, bytes.Length)) > 0)
            {
                if (socketFileSend != null && socketFileSend.Connected)
                {
                    socketFileSend.Send(bytes, nBytes, 0);
                }

                ao.buffer = bytes;
                ao.workSocket = socketFileSend;
                Sendtotalbytes += nBytes;
                vv = (int)(Sendtotalbytes * 100 / fsTrans.Length);
                SetClientProgBar(vv);
                SetClientLabel(vv + " %");
            }
            fsTrans.Close();
            fsTrans.Dispose();
            this.listBoxClientFileForm.Items.Add("파일전송 완료");
        }

        public void RecvProgIng(Object obj)
        {
            AsyncObject ao = new AsyncObject(1);

            byte[] bytes = new byte[4096];
            int vv = 1;
            int MaxSize = (Int32)obj;
            int nBytes = MaxSize;

            Sendtotalbytes = 0L;

            this.listBoxClientFileForm.Items.Add("파일수신 시작");
            Thread.Sleep(50);
            while (fsRecv.Length > 0)
            {
                Sendtotalbytes = fsRecv.Length;
                vv = (int)(Sendtotalbytes * 100 / MaxSize);
                SetClientProgBar(vv);
                SetClientLabel(vv + " %");
                if (fsRecv.Length >= MaxSize && vv >= 100)
                {
                    while (this.progressBarAsyncClientFileTrans.Value != vv)
                    {

                        SetClientProgBar(vv);
                        SetClientLabel(vv + " %");
                    }
                    fsRecv.Flush();
                    fsRecv.Close();
                    fsRecv.Dispose();
                    break;
                }
            }

            this.listBoxClientFileForm.Items.Add("파일수신 완료");
        }

        private void SetClientProgBar(int vv)
        {
            if (this.progressBarAsyncClientFileTrans.InvokeRequired)
            {
                SetProgCallBack dele = new SetProgCallBack(SetClientProgBar);
                if (this.IsHandleCreated)
                {
                    this.Invoke(dele, new object[] { vv });
                }
            }
            else
            {
                if (vv > 100)
                {
                    vv = 100;
                }
                this.progressBarAsyncClientFileTrans.Value = vv;
            }
        }

        private void SetClientLabel(string str)
        {
            if (this.labelAsyncClientFileTrans.InvokeRequired)
            {
                SetLabelCallBack dele = new SetLabelCallBack(SetClientLabel);
                if (this.IsHandleCreated)
                {
                    this.Invoke(dele, new object[] { str });
                }
            }
            else
                this.labelAsyncClientFileTrans.Text = str;
        }

        private void buttonAsyncClientFileSave_Click(object sender, EventArgs e)
        {
            FormFileDialog form = new FormFileDialog(SetDialogType);
            form("ButtonFileReceive");
        }

        private void buttonAsyncClientFileTrans_Click(object sender, EventArgs e)
        {
            FormFileDialog form = new FormFileDialog(SetDialogType);
            form("ButtonFileTrans");
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("파일 전송을 종료하시겠습니까?", "파일전송 종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Make your form dissappear of whatever you want
                    Form1.toform3 -= new Form1.AsyncFileTrans(SetFileTextBox);
                    toform1(type_of_form3);
                    this.Dispose(true);
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.toform3 -= new Form1.AsyncFileTrans(SetFileTextBox);
        }


    }
}
