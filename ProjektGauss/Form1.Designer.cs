namespace ProjektGauss
{
    partial class ProjectGauss
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonFile = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.radioButtonAsembler = new System.Windows.Forms.RadioButton();
            this.radioButtonCpp = new System.Windows.Forms.RadioButton();
            this.trackBarThreads = new System.Windows.Forms.TrackBar();
            this.labelThreads = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.labelPath = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreads)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelTitle.Location = new System.Drawing.Point(55, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(681, 31);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Program do obróbki dźwięku za pomocą filtracji Gaussa";
            // 
            // buttonFile
            // 
            this.buttonFile.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonFile.Location = new System.Drawing.Point(12, 399);
            this.buttonFile.Name = "buttonFile";
            this.buttonFile.Size = new System.Drawing.Size(385, 39);
            this.buttonFile.TabIndex = 1;
            this.buttonFile.Text = "Wybierz plik";
            this.buttonFile.UseVisualStyleBackColor = true;
            this.buttonFile.Click += new System.EventHandler(this.buttonFile_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRun.Location = new System.Drawing.Point(403, 399);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(385, 39);
            this.buttonRun.TabIndex = 2;
            this.buttonRun.Text = "Uruchom program";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // radioButtonAsembler
            // 
            this.radioButtonAsembler.AutoSize = true;
            this.radioButtonAsembler.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButtonAsembler.Location = new System.Drawing.Point(55, 63);
            this.radioButtonAsembler.Name = "radioButtonAsembler";
            this.radioButtonAsembler.Size = new System.Drawing.Size(112, 32);
            this.radioButtonAsembler.TabIndex = 3;
            this.radioButtonAsembler.TabStop = true;
            this.radioButtonAsembler.Text = "Asembler";
            this.radioButtonAsembler.UseVisualStyleBackColor = true;
            // 
            // radioButtonCpp
            // 
            this.radioButtonCpp.AutoSize = true;
            this.radioButtonCpp.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButtonCpp.Location = new System.Drawing.Point(55, 103);
            this.radioButtonCpp.Name = "radioButtonCpp";
            this.radioButtonCpp.Size = new System.Drawing.Size(70, 32);
            this.radioButtonCpp.TabIndex = 4;
            this.radioButtonCpp.TabStop = true;
            this.radioButtonCpp.Text = "C++";
            this.radioButtonCpp.UseVisualStyleBackColor = true;
            // 
            // trackBarThreads
            // 
            this.trackBarThreads.LargeChange = 1;
            this.trackBarThreads.Location = new System.Drawing.Point(55, 186);
            this.trackBarThreads.Maximum = 64;
            this.trackBarThreads.Minimum = 1;
            this.trackBarThreads.Name = "trackBarThreads";
            this.trackBarThreads.Size = new System.Drawing.Size(681, 45);
            this.trackBarThreads.TabIndex = 5;
            this.trackBarThreads.Value = 1;
            this.trackBarThreads.Scroll += new System.EventHandler(this.trackBarThreads_Scroll);
            // 
            // labelThreads
            // 
            this.labelThreads.AutoSize = true;
            this.labelThreads.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelThreads.Location = new System.Drawing.Point(55, 155);
            this.labelThreads.Name = "labelThreads";
            this.labelThreads.Size = new System.Drawing.Size(143, 28);
            this.labelThreads.TabIndex = 6;
            this.labelThreads.Text = "Ilość wątków: 1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "1";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(717, 216);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(19, 15);
            this.label64.TabIndex = 8;
            this.label64.Text = "64";
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelPath.Location = new System.Drawing.Point(55, 244);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(162, 28);
            this.labelPath.TabIndex = 9;
            this.labelPath.Text = "Ścieżka do pliku: ";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelMessage.Location = new System.Drawing.Point(55, 307);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(0, 28);
            this.labelMessage.TabIndex = 10;
            // 
            // ProjectGauss
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.label64);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelThreads);
            this.Controls.Add(this.trackBarThreads);
            this.Controls.Add(this.radioButtonCpp);
            this.Controls.Add(this.radioButtonAsembler);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.buttonFile);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ProjectGauss";
            this.Text = "ProjectGauss";
            this.Load += new System.EventHandler(this.ProjectGauss_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelTitle;
        private Button buttonFile;
        private Button buttonRun;
        private RadioButton radioButtonAsembler;
        private RadioButton radioButtonCpp;
        private TrackBar trackBarThreads;
        private Label labelThreads;
        private Label label1;
        private Label label64;
        private Label labelPath;
        private Label labelMessage;
    }
}