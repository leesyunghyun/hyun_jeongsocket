using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;


namespace Hyun_JeongSocket
{
    public partial class Form2 : Form //서버 파일 전송 폼
    {
        public class AsyncObject //서버 전송하는 데이터 형식
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
        public delegate void FormFileDialog(string str);
        public delegate void AsyncFormClose(string message);

        Socket socketFileSend = null;
        FileStream fsTrans = null;
        FileStream fsRecv = null;
        string recvFileExt; // 받은 파일 확장자
        string type_of_form2; //서버폼의 상태 (Accept or Connect)

        long Sendtotalbytes;

        string formType = "";

        public static event AsyncFormClose toform1;

        public Form2(string type, Socket socket)
        {
            InitializeComponent();
            formType = type;
            socketFileSend = socket;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1.toform2 += new Form1.AsyncFileTrans(SetFileTextBox);
            InitControl();
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            FormFileDialog form = new FormFileDialog(SetDialogType);
            form(formType);
        }

        public void InitControl()
        {
            this.textBoxAsyncServFileTrans.Text = "";
        }

        public void SetFileTextBox(string message, int flag, FileStream fsRecv)
        {
            switch (flag)
            {
                case 1: //파일크기, 이름 전송
                    textBoxAsyncServFileTrans.Clear();
                    string[] splitMsg = message.Split('/');
                    string[] splitMsg2 = splitMsg[2].Split('.');
                    splitMsg2[0] += " 새로전송됨." + splitMsg2[1];
                    this.fsRecv = fsRecv;
                    Thread prog1 = new Thread(new ParameterizedThreadStart(RecvProgIng));
                    prog1.Start(Convert.ToInt32(splitMsg[1]));
                    break;
                case 2: //파일데이터 전송
                    this.textBoxAsyncServFileTrans.AppendText(message);
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
                    listBoxServFileForm.Items.Add("파일 선택중 . . .");

                    buttonAsyncServFileSave.Enabled = false;

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        textBoxAsyncServFileTrans.Clear();
                        fsTrans = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                        string[] fileName = fsTrans.Name.Split('\\');

                        socketFileSend.Send(Encoding.UTF8.GetBytes("FileSize" + "/" + fsTrans.Length + "/" + fileName[fileName.Length - 1] + "/"));
                        openFileDialog1.Dispose();
                    }
                    else
                    {
                        listBoxServFileForm.Items.Add("파일 선택을 취소하셨습니다.");
                        openFileDialog1.Dispose();
                    }
                    type_of_form2 = "AcceptServerClose";
                    break;
                case "FileReceive":
                    textBoxAsyncServFileTrans.Clear();
                    listBoxServFileForm.Items.Add("파일 수신 대기중 . . .");
                    listBoxServFileForm.Items.Add("잠시만 기다려주세요. . .");
                    buttonAsyncServFileTrans.Enabled = false;
                    type_of_form2 = "ConnectServerClose";
                    break;
                case "ButtonFileTrans":
                    listBoxServFileForm.Items.Add("파일 선택중 . . .");
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        textBoxAsyncServFileTrans.Clear();
                        fsTrans = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                        string[] fileName = fsTrans.Name.Split('\\');

                        socketFileSend.Send(Encoding.UTF8.GetBytes("FileSize" + "/" + fsTrans.Length + "/" + fileName[fileName.Length - 1] + "/"));
                        openFileDialog1.Dispose();
                    }
                    else
                    {
                        listBoxServFileForm.Items.Add("파일 선택을 취소하셨습니다.");
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
                        byte[] message = Encoding.UTF8.GetBytes(textBoxAsyncServFileTrans.Text);

                        fsRecv.Write(message, 0, message.Length);
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
            byte[] revbytes = new byte[8];
            int vv = 1;
            int nBytes = 0;

            Sendtotalbytes = 0L;

            //string[] str = Encoding.UTF8.GetString(revbytes).Split('/');

            //if (str[0] != "OK")
            //{
            //    MessageBox.Show("에러 : 파일정보 불량");
            //    this.listBoxServFileForm.Items.Add("에러 : 파일정보 불량 ");
            //    return;
            //}
            //else
            this.listBoxServFileForm.Items.Add("파일전송 시작");

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
                SetServProgBar(vv);
                SetServLabel(vv + " %");
            }
            fsTrans.Close();
            fsTrans.Dispose();
            this.listBoxServFileForm.Items.Add("파일전송 완료");
        }

        public void RecvProgIng(Object obj)
        {
            AsyncObject ao = new AsyncObject(1);

            byte[] bytes = new byte[4096];
            int vv = 1;
            int MaxSize = (Int32)obj;
            int nBytes = MaxSize;

            Sendtotalbytes = 0L;

            this.listBoxServFileForm.Items.Add("파일수신 시작");
            Thread.Sleep(50);
            while (fsRecv.Length > 0)
            {
                Sendtotalbytes = fsRecv.Length;
                vv = (int)(Sendtotalbytes * 100 / MaxSize);
                SetServProgBar(vv);
                SetServLabel(vv + " %");
               
                if (fsRecv.Length >= MaxSize && vv >= 100)
                {
                    while (this.progressBarAsyncServFileTrans.Value != vv)
                    {
                        SetServProgBar(vv);
                        SetServLabel(vv + " %");
                    }

                    fsRecv.Flush();
                    fsRecv.Close();
                    fsRecv.Dispose();
                    break;
                }
            }
            this.listBoxServFileForm.Items.Add("파일수신 완료");
        }

        private void SetServProgBar(int vv)
        {
            if (this.progressBarAsyncServFileTrans.InvokeRequired)
            {
                SetProgCallBack dele = new SetProgCallBack(SetServProgBar);
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
                this.progressBarAsyncServFileTrans.Value = vv;
            }
                
        }

        private void SetServLabel(string str)
        {
            if (this.labelAsyncServFileTrans.InvokeRequired)
            {
                SetLabelCallBack dele = new SetLabelCallBack(SetServLabel);
                if (this.IsHandleCreated)
                {
                    this.Invoke(dele, new object[] { str });
                }

            }
            else
                this.labelAsyncServFileTrans.Text = str;
        }

        private void buttonAsyncFileTrans_Click(object sender, EventArgs e)
        {
            FormFileDialog form = new FormFileDialog(SetDialogType);
            form("ButtonFileTrans");
        }

        private void buttonAsyncFileSave_Click(object sender, EventArgs e)
        {
            FormFileDialog form = new FormFileDialog(SetDialogType);
            form("ButtonFileReceive");
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("파일 전송을 종료하시겠습니까?", "파일전송 종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Make your form dissappear of whatever you want
                    Form1.toform2 -= new Form1.AsyncFileTrans(SetFileTextBox);
                    toform1(type_of_form2);
                    this.Dispose(true);
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
        }


    }
}
