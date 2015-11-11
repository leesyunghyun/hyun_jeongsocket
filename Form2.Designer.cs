namespace Hyun_JeongSocket
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
            this.textBoxAsyncServFileTrans = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonAsyncServFileTrans = new System.Windows.Forms.Button();
            this.buttonAsyncServFileSave = new System.Windows.Forms.Button();
            this.progressBarAsyncServFileTrans = new System.Windows.Forms.ProgressBar();
            this.labelAsyncServFileTrans = new System.Windows.Forms.Label();
            this.listBoxServFileForm = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textBoxAsyncServFileTrans
            // 
            this.textBoxAsyncServFileTrans.Location = new System.Drawing.Point(12, 12);
            this.textBoxAsyncServFileTrans.Multiline = true;
            this.textBoxAsyncServFileTrans.Name = "textBoxAsyncServFileTrans";
            this.textBoxAsyncServFileTrans.ReadOnly = true;
            this.textBoxAsyncServFileTrans.Size = new System.Drawing.Size(312, 253);
            this.textBoxAsyncServFileTrans.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonAsyncServFileTrans
            // 
            this.buttonAsyncServFileTrans.Location = new System.Drawing.Point(337, 12);
            this.buttonAsyncServFileTrans.Name = "buttonAsyncServFileTrans";
            this.buttonAsyncServFileTrans.Size = new System.Drawing.Size(75, 23);
            this.buttonAsyncServFileTrans.TabIndex = 1;
            this.buttonAsyncServFileTrans.Text = "전송";
            this.buttonAsyncServFileTrans.UseVisualStyleBackColor = true;
            this.buttonAsyncServFileTrans.Click += new System.EventHandler(this.buttonAsyncFileTrans_Click);
            // 
            // buttonAsyncServFileSave
            // 
            this.buttonAsyncServFileSave.Location = new System.Drawing.Point(427, 12);
            this.buttonAsyncServFileSave.Name = "buttonAsyncServFileSave";
            this.buttonAsyncServFileSave.Size = new System.Drawing.Size(75, 23);
            this.buttonAsyncServFileSave.TabIndex = 2;
            this.buttonAsyncServFileSave.Text = "저장";
            this.buttonAsyncServFileSave.UseVisualStyleBackColor = true;
            this.buttonAsyncServFileSave.Click += new System.EventHandler(this.buttonAsyncFileSave_Click);
            // 
            // progressBarAsyncServFileTrans
            // 
            this.progressBarAsyncServFileTrans.Location = new System.Drawing.Point(12, 271);
            this.progressBarAsyncServFileTrans.Name = "progressBarAsyncServFileTrans";
            this.progressBarAsyncServFileTrans.Size = new System.Drawing.Size(312, 23);
            this.progressBarAsyncServFileTrans.TabIndex = 3;
            // 
            // labelAsyncServFileTrans
            // 
            this.labelAsyncServFileTrans.Location = new System.Drawing.Point(333, 273);
            this.labelAsyncServFileTrans.Name = "labelAsyncServFileTrans";
            this.labelAsyncServFileTrans.Size = new System.Drawing.Size(85, 23);
            this.labelAsyncServFileTrans.TabIndex = 4;
            this.labelAsyncServFileTrans.Text = "진행률 : 0 %";
            this.labelAsyncServFileTrans.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxServFileForm
            // 
            this.listBoxServFileForm.FormattingEnabled = true;
            this.listBoxServFileForm.ItemHeight = 12;
            this.listBoxServFileForm.Location = new System.Drawing.Point(337, 41);
            this.listBoxServFileForm.Name = "listBoxServFileForm";
            this.listBoxServFileForm.Size = new System.Drawing.Size(165, 220);
            this.listBoxServFileForm.TabIndex = 5;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 308);
            this.Controls.Add(this.listBoxServFileForm);
            this.Controls.Add(this.labelAsyncServFileTrans);
            this.Controls.Add(this.progressBarAsyncServFileTrans);
            this.Controls.Add(this.buttonAsyncServFileSave);
            this.Controls.Add(this.buttonAsyncServFileTrans);
            this.Controls.Add(this.textBoxAsyncServFileTrans);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form2";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.Form2_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAsyncServFileTrans;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonAsyncServFileTrans;
        private System.Windows.Forms.Button buttonAsyncServFileSave;
        private System.Windows.Forms.ProgressBar progressBarAsyncServFileTrans;
        private System.Windows.Forms.Label labelAsyncServFileTrans;
        private System.Windows.Forms.ListBox listBoxServFileForm;
    }
}