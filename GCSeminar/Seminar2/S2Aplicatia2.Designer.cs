namespace GCSeminar.Seminar2
{
    partial class S2Aplicatia2
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
            pictureBox1.Size = new Size(801, 453);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // NrPointsTextBox
            // 
            NrPointsTextBox.Location = new Point(807, 27);
            NrPointsTextBox.Name = "NrPointsTextBox";
            NrPointsTextBox.Size = new Size(127, 23);
            NrPointsTextBox.TabIndex = 4;
            // 
            // NrPointsLabel
            // 
            NrPointsLabel.AutoSize = true;
            NrPointsLabel.Location = new Point(807, 9);
            NrPointsLabel.Name = "NrPointsLabel";
            NrPointsLabel.Size = new Size(110, 15);
            NrPointsLabel.TabIndex = 5;
            NrPointsLabel.Text = "Numarul de puncte";
            // 
            // S2Aplicatia2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(956, 450);
            Controls.Add(NrPointsTextBox);
            Controls.Add(NrPointsLabel);
            Controls.Add(pictureBox1);
            Name = "S2Aplicatia2";
            Text = "S2Aplicatia2";
            Load += S2Aplicatia2_Load;
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