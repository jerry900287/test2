namespace ImageEncoder
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.generateButton = new System.Windows.Forms.Button();
            this.systemLabel = new System.Windows.Forms.Label();
            this.secretPictureBox = new System.Windows.Forms.PictureBox();
            this.sourcePictureBox = new System.Windows.Forms.PictureBox();
            this.loadSourceButton = new System.Windows.Forms.Button();
            this.loadDefaultButton = new System.Windows.Forms.Button();
            this.embedImageButton = new System.Windows.Forms.Button();
            this.embededPictureBox = new System.Windows.Forms.PictureBox();
            this.saveEmbededImageButton = new System.Windows.Forms.Button();
            this.decodeImageButton = new System.Windows.Forms.Button();
            this.decodePictureBox = new System.Windows.Forms.PictureBox();
            this.loadEmbededImageButton = new System.Windows.Forms.Button();
            this.saveDecodedImageButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.secretPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.embededPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.decodePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(12, 24);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(170, 36);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username:";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Font = new System.Drawing.Font("Arial", 12F);
            this.usernameTextBox.Location = new System.Drawing.Point(188, 21);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(216, 44);
            this.usernameTextBox.TabIndex = 1;
            // 
            // generateButton
            // 
            this.generateButton.Font = new System.Drawing.Font("Arial", 12F);
            this.generateButton.Location = new System.Drawing.Point(419, 12);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(280, 60);
            this.generateButton.TabIndex = 2;
            this.generateButton.Text = "Generate Image";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // systemLabel
            // 
            this.systemLabel.AutoSize = true;
            this.systemLabel.Font = new System.Drawing.Font("Arial", 10F);
            this.systemLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.systemLabel.Location = new System.Drawing.Point(12, 88);
            this.systemLabel.Name = "systemLabel";
            this.systemLabel.Size = new System.Drawing.Size(153, 32);
            this.systemLabel.TabIndex = 3;
            this.systemLabel.Text = "SystemText";
            // 
            // secretPictureBox
            // 
            this.secretPictureBox.Location = new System.Drawing.Point(725, 4);
            this.secretPictureBox.Name = "secretPictureBox";
            this.secretPictureBox.Size = new System.Drawing.Size(128, 128);
            this.secretPictureBox.TabIndex = 4;
            this.secretPictureBox.TabStop = false;
            // 
            // sourcePictureBox
            // 
            this.sourcePictureBox.Location = new System.Drawing.Point(30, 181);
            this.sourcePictureBox.Name = "sourcePictureBox";
            this.sourcePictureBox.Size = new System.Drawing.Size(256, 256);
            this.sourcePictureBox.TabIndex = 5;
            this.sourcePictureBox.TabStop = false;
            // 
            // loadSourceButton
            // 
            this.loadSourceButton.Font = new System.Drawing.Font("Arial", 12F);
            this.loadSourceButton.Location = new System.Drawing.Point(20, 519);
            this.loadSourceButton.Name = "loadSourceButton";
            this.loadSourceButton.Size = new System.Drawing.Size(280, 60);
            this.loadSourceButton.TabIndex = 6;
            this.loadSourceButton.Text = "Load Source";
            this.loadSourceButton.UseVisualStyleBackColor = true;
            this.loadSourceButton.Click += new System.EventHandler(this.loadSourceButton_Click);
            // 
            // loadDefaultButton
            // 
            this.loadDefaultButton.Font = new System.Drawing.Font("Arial", 12F);
            this.loadDefaultButton.Location = new System.Drawing.Point(20, 453);
            this.loadDefaultButton.Name = "loadDefaultButton";
            this.loadDefaultButton.Size = new System.Drawing.Size(280, 60);
            this.loadDefaultButton.TabIndex = 7;
            this.loadDefaultButton.Text = "Load Default";
            this.loadDefaultButton.UseVisualStyleBackColor = true;
            this.loadDefaultButton.Click += new System.EventHandler(this.loadDefaultButton_Click);
            // 
            // embedImageButton
            // 
            this.embedImageButton.Font = new System.Drawing.Font("Arial", 12F);
            this.embedImageButton.Location = new System.Drawing.Point(314, 266);
            this.embedImageButton.Name = "embedImageButton";
            this.embedImageButton.Size = new System.Drawing.Size(160, 100);
            this.embedImageButton.TabIndex = 8;
            this.embedImageButton.Text = "Embed\r\nImage";
            this.embedImageButton.UseVisualStyleBackColor = true;
            this.embedImageButton.Click += new System.EventHandler(this.embedImageButton_Click);
            // 
            // embededPictureBox
            // 
            this.embededPictureBox.Location = new System.Drawing.Point(495, 181);
            this.embededPictureBox.Name = "embededPictureBox";
            this.embededPictureBox.Size = new System.Drawing.Size(256, 256);
            this.embededPictureBox.TabIndex = 9;
            this.embededPictureBox.TabStop = false;
            // 
            // saveEmbededImageButton
            // 
            this.saveEmbededImageButton.Font = new System.Drawing.Font("Arial", 12F);
            this.saveEmbededImageButton.Location = new System.Drawing.Point(481, 519);
            this.saveEmbededImageButton.Name = "saveEmbededImageButton";
            this.saveEmbededImageButton.Size = new System.Drawing.Size(280, 60);
            this.saveEmbededImageButton.TabIndex = 10;
            this.saveEmbededImageButton.Text = "Save Image";
            this.saveEmbededImageButton.UseVisualStyleBackColor = true;
            this.saveEmbededImageButton.Click += new System.EventHandler(this.saveEmbededImageButton_Click);
            // 
            // decodeImageButton
            // 
            this.decodeImageButton.Font = new System.Drawing.Font("Arial", 12F);
            this.decodeImageButton.Location = new System.Drawing.Point(779, 266);
            this.decodeImageButton.Name = "decodeImageButton";
            this.decodeImageButton.Size = new System.Drawing.Size(160, 100);
            this.decodeImageButton.TabIndex = 11;
            this.decodeImageButton.Text = "Decode\r\nImage";
            this.decodeImageButton.UseVisualStyleBackColor = true;
            this.decodeImageButton.Click += new System.EventHandler(this.decodeImageButton_Click);
            // 
            // decodePictureBox
            // 
            this.decodePictureBox.Location = new System.Drawing.Point(965, 252);
            this.decodePictureBox.Name = "decodePictureBox";
            this.decodePictureBox.Size = new System.Drawing.Size(128, 128);
            this.decodePictureBox.TabIndex = 12;
            this.decodePictureBox.TabStop = false;
            // 
            // loadEmbededImageButton
            // 
            this.loadEmbededImageButton.Font = new System.Drawing.Font("Arial", 12F);
            this.loadEmbededImageButton.Location = new System.Drawing.Point(481, 453);
            this.loadEmbededImageButton.Name = "loadEmbededImageButton";
            this.loadEmbededImageButton.Size = new System.Drawing.Size(280, 60);
            this.loadEmbededImageButton.TabIndex = 13;
            this.loadEmbededImageButton.Text = "Load Image";
            this.loadEmbededImageButton.UseVisualStyleBackColor = true;
            this.loadEmbededImageButton.Click += new System.EventHandler(this.loadEmbededImageButton_Click);
            // 
            // saveDecodedImageButton
            // 
            this.saveDecodedImageButton.Font = new System.Drawing.Font("Arial", 12F);
            this.saveDecodedImageButton.Location = new System.Drawing.Point(882, 453);
            this.saveDecodedImageButton.Name = "saveDecodedImageButton";
            this.saveDecodedImageButton.Size = new System.Drawing.Size(280, 60);
            this.saveDecodedImageButton.TabIndex = 14;
            this.saveDecodedImageButton.Text = "Save Decode";
            this.saveDecodedImageButton.UseVisualStyleBackColor = true;
            this.saveDecodedImageButton.Click += new System.EventHandler(this.saveDecodedImageButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 729);
            this.Controls.Add(this.saveDecodedImageButton);
            this.Controls.Add(this.loadEmbededImageButton);
            this.Controls.Add(this.decodePictureBox);
            this.Controls.Add(this.decodeImageButton);
            this.Controls.Add(this.saveEmbededImageButton);
            this.Controls.Add(this.embededPictureBox);
            this.Controls.Add(this.embedImageButton);
            this.Controls.Add(this.loadDefaultButton);
            this.Controls.Add(this.loadSourceButton);
            this.Controls.Add(this.sourcePictureBox);
            this.Controls.Add(this.secretPictureBox);
            this.Controls.Add(this.systemLabel);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.usernameLabel);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.secretPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.embededPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.decodePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Label systemLabel;
        private System.Windows.Forms.PictureBox secretPictureBox;
        private System.Windows.Forms.PictureBox sourcePictureBox;
        private System.Windows.Forms.Button loadSourceButton;
        private System.Windows.Forms.Button loadDefaultButton;
        private System.Windows.Forms.Button embedImageButton;
        private System.Windows.Forms.PictureBox embededPictureBox;
        private System.Windows.Forms.Button saveEmbededImageButton;
        private System.Windows.Forms.Button decodeImageButton;
        private System.Windows.Forms.PictureBox decodePictureBox;
        private System.Windows.Forms.Button loadEmbededImageButton;
        private System.Windows.Forms.Button saveDecodedImageButton;
    }
}

