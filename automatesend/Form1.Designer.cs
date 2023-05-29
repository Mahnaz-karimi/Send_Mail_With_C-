namespace automatesend
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
            btnSendMail = new Button();
            label1 = new Label();
            btnmergeFile = new Button();
            SuspendLayout();
            // 
            // btnSendMail
            // 
            btnSendMail.Location = new Point(451, 105);
            btnSendMail.Margin = new Padding(3, 4, 3, 4);
            btnSendMail.Name = "btnSendMail";
            btnSendMail.Size = new Size(86, 31);
            btnSendMail.TabIndex = 0;
            btnSendMail.Text = "SendMail";
            btnSendMail.UseVisualStyleBackColor = true;
            btnSendMail.Click += btnSendMail_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(471, 81);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // btnmergeFile
            // 
            btnmergeFile.Location = new Point(448, 229);
            btnmergeFile.Name = "btnmergeFile";
            btnmergeFile.Size = new Size(94, 29);
            btnmergeFile.TabIndex = 2;
            btnmergeFile.Text = "MergeFile";
            btnmergeFile.UseVisualStyleBackColor = true;
            btnmergeFile.Click += btnmergeFile_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(btnmergeFile);
            Controls.Add(label1);
            Controls.Add(btnSendMail);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Button btnSendMail;
        private Label label1;
        private Button btnmergeFile;
    }
}