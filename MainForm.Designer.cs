namespace DivaSongIndexer
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            folderBrowserDialog = new FolderBrowserDialog();
            label1 = new Label();
            modsDirectoryTextBox = new TextBox();
            modsDirectoryBrowseButton = new Button();
            createSongIndexButton = new Button();
            loadingSpinnerPictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)loadingSpinnerPictureBox).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 18);
            label1.Name = "label1";
            label1.Size = new Size(107, 15);
            label1.TabIndex = 1;
            label1.Text = "Select Mods Folder";
            // 
            // modsDirectoryTextBox
            // 
            modsDirectoryTextBox.BorderStyle = BorderStyle.FixedSingle;
            modsDirectoryTextBox.Location = new Point(23, 41);
            modsDirectoryTextBox.Name = "modsDirectoryTextBox";
            modsDirectoryTextBox.Size = new Size(410, 23);
            modsDirectoryTextBox.TabIndex = 2;
            // 
            // modsDirectoryBrowseButton
            // 
            modsDirectoryBrowseButton.Location = new Point(440, 40);
            modsDirectoryBrowseButton.Name = "modsDirectoryBrowseButton";
            modsDirectoryBrowseButton.Size = new Size(100, 26);
            modsDirectoryBrowseButton.TabIndex = 3;
            modsDirectoryBrowseButton.Text = "Browse";
            modsDirectoryBrowseButton.UseVisualStyleBackColor = true;
            modsDirectoryBrowseButton.Click += modsDirectoryBrowseButton_Click;
            // 
            // createSongIndexButton
            // 
            createSongIndexButton.Location = new Point(212, 84);
            createSongIndexButton.Name = "createSongIndexButton";
            createSongIndexButton.Size = new Size(150, 26);
            createSongIndexButton.TabIndex = 7;
            createSongIndexButton.Text = "Create Song Index";
            createSongIndexButton.UseVisualStyleBackColor = true;
            createSongIndexButton.Click += createSongIndexButton_Click;
            // 
            // loadingSpinnerPictureBox
            // 
            loadingSpinnerPictureBox.Image = Properties.Resources.LoadingSpinner;
            loadingSpinnerPictureBox.Location = new Point(365, 85);
            loadingSpinnerPictureBox.Name = "loadingSpinnerPictureBox";
            loadingSpinnerPictureBox.Size = new Size(25, 25);
            loadingSpinnerPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            loadingSpinnerPictureBox.TabIndex = 8;
            loadingSpinnerPictureBox.TabStop = false;
            loadingSpinnerPictureBox.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(559, 131);
            Controls.Add(loadingSpinnerPictureBox);
            Controls.Add(createSongIndexButton);
            Controls.Add(modsDirectoryBrowseButton);
            Controls.Add(modsDirectoryTextBox);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DivaSongIndexer";
            ((System.ComponentModel.ISupportInitialize)loadingSpinnerPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private FolderBrowserDialog folderBrowserDialog;
        private Label label1;
        private TextBox modsDirectoryTextBox;
        private Button modsDirectoryBrowseButton;
        private Button createSongIndexButton;
        private PictureBox loadingSpinnerPictureBox;
    }
}
