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
            folderBrowserDialog = new FolderBrowserDialog();
            label1 = new Label();
            modsDirectoryTextBox = new TextBox();
            modsDirectoryBrowseButton = new Button();
            outputDirectoryBrowseButton = new Button();
            outputDirectoryTextBox = new TextBox();
            label2 = new Label();
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
            label1.Size = new Size(122, 15);
            label1.TabIndex = 1;
            label1.Text = "Select Mods Directory";
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
            modsDirectoryBrowseButton.Click += modDirectoryBrowseButton_Click;
            // 
            // outputDirectoryBrowseButton
            // 
            outputDirectoryBrowseButton.Location = new Point(440, 98);
            outputDirectoryBrowseButton.Name = "outputDirectoryBrowseButton";
            outputDirectoryBrowseButton.Size = new Size(100, 26);
            outputDirectoryBrowseButton.TabIndex = 6;
            outputDirectoryBrowseButton.Text = "Browse";
            outputDirectoryBrowseButton.UseVisualStyleBackColor = true;
            outputDirectoryBrowseButton.Click += outputDirectoryBrowseButton_Click;
            // 
            // outputDirectoryTextBox
            // 
            outputDirectoryTextBox.BorderStyle = BorderStyle.FixedSingle;
            outputDirectoryTextBox.Location = new Point(23, 99);
            outputDirectoryTextBox.Name = "outputDirectoryTextBox";
            outputDirectoryTextBox.Size = new Size(410, 23);
            outputDirectoryTextBox.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 76);
            label2.Name = "label2";
            label2.Size = new Size(130, 15);
            label2.TabIndex = 4;
            label2.Text = "Select Output Directory";
            // 
            // createSongIndexButton
            // 
            createSongIndexButton.Location = new Point(212, 144);
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
            loadingSpinnerPictureBox.Location = new Point(365, 145);
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
            ClientSize = new Size(559, 191);
            Controls.Add(loadingSpinnerPictureBox);
            Controls.Add(createSongIndexButton);
            Controls.Add(outputDirectoryBrowseButton);
            Controls.Add(outputDirectoryTextBox);
            Controls.Add(label2);
            Controls.Add(modsDirectoryBrowseButton);
            Controls.Add(modsDirectoryTextBox);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainForm";
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
        private Button outputDirectoryBrowseButton;
        private TextBox outputDirectoryTextBox;
        private Label label2;
        private Button createSongIndexButton;
        private PictureBox loadingSpinnerPictureBox;
    }
}
