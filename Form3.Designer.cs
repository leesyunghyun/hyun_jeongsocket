namespace Hyun_JeongSocket
{
    partial class Form3
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
            this.listBoxClientFileForm = new System.Windows.Forms.ListBox();
            this.labelAsyncClientFileTrans = new System.Windows.Forms.Label();
            this.progressBarAsyncClientFileTrans = new System.Windows.Forms.ProgressBar();
            this.buttonAsyncClientFileSave = new System.Windows.Forms.Button();
            this.buttonAsyncClientFileTrans = new System.Windows.Forms.Button();
            this.textBoxAsyncClientFileTrans = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // listBoxClientFileForm
            // 
            this.listBoxClientFileForm.FormattingEnabled = true;
            this.listBoxClientFileForm.ItemHeight = 12;
            this.listBoxClientFileForm.Location = new System.Drawing.Point(332, 41);
            this.listBoxClientFileForm.Name = "listBoxClientFileForm";
            this.listBoxClientFileForm.Size = new System.Drawing.Size(165, 220);
            this.listBoxClientFileForm.TabIndex = 11;
            // 
            // labelAsyncClientFileTrans
            // 
            this.labelAsyncClientFileTrans.Location = new System.Drawing.Point(328, 273);
            this.labelAsyncClientFileTrans.Name = "labelAsyncClientFileTrans";
            this.labelAsyncClientFileTrans.Size = new System.Drawing.Size(85, 23);
            this.labelAsyncClientFileTrans.TabIndex = 10;
            this.labelAsyncClientFileTrans.Text = "진행률 : 0 %";
            this.labelAsyncClientFileTrans.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarAsyncClientFileTrans
            // 
            this.progressBarAsyncClientFileTrans.Location = new System.Drawing.Point(7, 271);
            this.progressBarAsyncClientFileTrans.Name = "progressBarAsyncClientFileTrans";
            this.progressBarAsyncClientFileTrans.Size = new System.Drawing.Size(312, 23);
            this.progressBarAsyncClientFileTrans.TabIndex = 9;
            // 
            // buttonAsyncClientFileSave
            // 
            this.buttonAsyncClientFileSave.Location = new System.Drawing.Point(422, 12);
            this.buttonAsyncClientFileSave.Name = "buttonAsyncClientFileSave";
            this.buttonAsyncClientFileSave.Size = new System.Drawing.Size(75, 23);
            this.buttonAsyncClientFileSave.TabIndex = 8;
            this.buttonAsyncClientFileSave.Text = "저장";
            this.buttonAsyncClientFileSave.UseVisualStyleBackColor = true;
            this.buttonAsyncClientFileSave.Click += new System.EventHandler(this.buttonAsyncClientFileSave_Click);
            // 
            // buttonAsyncClientFileTrans
            // 
            this.buttonAsyncClientFileTrans.Location = new System.Drawing.Point(332, 12);
            this.buttonAsyncClientFileTrans.Name = "buttonAsyncClientFileTrans";
            this.buttonAsyncClientFileTrans.Size = new System.Drawing.Size(75, 23);
            this.buttonAsyncClientFileTrans.TabIndex = 7;
            this.buttonAsyncClientFileTrans.Text = "전송";
            this.buttonAsyncClientFileTrans.UseVisualStyleBackColor = true;
            this.buttonAsyncClientFileTrans.Click += new System.EventHandler(this.buttonAsyncClientFileTrans_Click);
            // 
            // textBoxAsyncClientFileTrans
            // 
            this.textBoxAsyncClientFileTrans.Location = new System.Drawing.Point(7, 12);
            this.textBoxAsyncClientFileTrans.Multiline = true;
            this.textBoxAsyncClientFileTrans.Name = "textBoxAsyncClientFileTrans";
            this.textBoxAsyncClientFileTrans.ReadOnly = true;
            this.textBoxAsyncClientFileTrans.Size = new System.Drawing.Size(312, 253);
            this.textBoxAsyncClientFileTrans.TabIndex = 6;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 308);
            this.Controls.Add(this.listBoxClientFileForm);
            this.Controls.Add(this.labelAsyncClientFileTrans);
            this.Controls.Add(this.progressBarAsyncClientFileTrans);
            this.Controls.Add(this.buttonAsyncClientFileSave);
            this.Controls.Add(this.buttonAsyncClientFileTrans);
            this.Controls.Add(this.textBoxAsyncClientFileTrans);
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form3_FormClosed);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.Shown += new System.EventHandler(this.Form3_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxClientFileForm;
        private System.Windows.Forms.Label labelAsyncClientFileTrans;
        private System.Windows.Forms.ProgressBar progressBarAsyncClientFileTrans;
        private System.Windows.Forms.Button buttonAsyncClientFileSave;
        private System.Windows.Forms.Button buttonAsyncClientFileTrans;
        private System.Windows.Forms.TextBox textBoxAsyncClientFileTrans;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}