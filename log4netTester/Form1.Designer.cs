namespace log4netTester
{
    partial class Form1
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
            this.buttonGenerateError = new System.Windows.Forms.Button();
            this.buttonGenerateInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonGenerateError
            // 
            this.buttonGenerateError.Location = new System.Drawing.Point(104, 87);
            this.buttonGenerateError.Name = "buttonGenerateError";
            this.buttonGenerateError.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerateError.TabIndex = 39;
            this.buttonGenerateError.Tag = "";
            this.buttonGenerateError.Text = "Ops Error";
            this.buttonGenerateError.UseVisualStyleBackColor = true;
            this.buttonGenerateError.Click += new System.EventHandler(this.buttonGenerateError_Click);
            // 
            // buttonGenerateInfo
            // 
            this.buttonGenerateInfo.Location = new System.Drawing.Point(205, 87);
            this.buttonGenerateInfo.Name = "buttonGenerateInfo";
            this.buttonGenerateInfo.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerateInfo.TabIndex = 40;
            this.buttonGenerateInfo.Tag = "";
            this.buttonGenerateInfo.Text = "Ops Info";
            this.buttonGenerateInfo.UseVisualStyleBackColor = true;
            this.buttonGenerateInfo.Click += new System.EventHandler(this.buttonGenerateInfo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 289);
            this.Controls.Add(this.buttonGenerateInfo);
            this.Controls.Add(this.buttonGenerateError);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerateError;
        private System.Windows.Forms.Button buttonGenerateInfo;
    }
}

