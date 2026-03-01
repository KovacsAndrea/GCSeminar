using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GCSeminar.Seminar1
{
    public partial class Aplicatia1 : BaseGraphicsForm
    {
        public Aplicatia1()
        {
            InitializeComponent();
            this.Text = "Seminar 1 - Aplicatia 1";
            this.NrPointsTextBox.Text = "10";
        }

        private void Aplicatia1_Load(object sender, EventArgs e)
        {
            SetupCanvas(this.pictureBox1);
            GenerateRandomPoints(ReadNrPoints());
            DrawDemo();
        }

        private int? ReadNrPoints() => int.TryParse(NrPointsTextBox.Text, out int nr) ? nr : (int?)null;

        private void DrawDemo()
        {
            g.Clear(Color.White);

            int minX = puncte.Min(p => p.X);
            int maxX = puncte.Max(p => p.X);
            int minY = puncte.Min(p => p.Y);
            int maxY = puncte.Max(p => p.Y);

            int width = maxX - minX;
            int height = maxY - minY;
            using (Pen thickPen = new Pen(Color.Red, 3))
            {
                g.DrawRectangle(thickPen, minX, minY, width, height);
            }

            foreach (var p in puncte)
            {
                DrawPoint(p, Brushes.CornflowerBlue);
            }

            RefreshCanvas();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GenerateRandomPoints(ReadNrPoints());
            DrawDemo();
        }
    }
}
