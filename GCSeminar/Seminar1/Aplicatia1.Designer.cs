namespace GCSeminar.Seminar1
{
    partial class Aplicatia1
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
            pictureBox1 = new PictureBox();
            NrPointsTextBox = new TextBox();
            NrPointsLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 452);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // NrPointsTextBox
            // 
            NrPointsTextBox.Location = new Point(806, 24);
            NrPointsTextBox.Name = "NrPointsTextBox";
            NrPointsTextBox.Size = new Size(127, 23);
            NrPointsTextBox.TabIndex = 1;
            // 
            // NrPointsLabel
            // 
            NrPointsLabel.AutoSize = true;
            NrPointsLabel.Location = new Point(806, 6);
            NrPointsLabel.Name = "NrPointsLabel";
            NrPointsLabel.Size = new Size(110, 15);
            NrPointsLabel.TabIndex = 2;
            NrPointsLabel.Text = "Numarul de puncte";
            // 
            // Aplicatia1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(945, 450);
            Controls.Add(NrPointsLabel);
            Controls.Add(NrPointsTextBox);
            Controls.Add(pictureBox1);
            Name = "Aplicatia1";
            Text = "Aplicatia1";
            Load += Aplicatia1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox NrPointsTextBox;
        private Label NrPointsLabel;
    }
}