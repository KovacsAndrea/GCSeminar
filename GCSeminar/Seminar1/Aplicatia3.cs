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
    public partial class Aplicatia3 : BaseGraphicsForm
    {
        private Point q;
        private bool qGenerated = false;

        public Aplicatia3()
        {
            InitializeComponent();
            this.Text = "Seminar 1 - Aplicatia 3";
            this.NrPointsTextBox.Text = "10";
        }

        private void Aplicatia3_Load(object sender, EventArgs e)
        {
            SetupCanvas(this.pictureBox1);
            GenerateRandomPoints(ReadNrPoints());
            GenerateQ();
            DrawDemo();
        }

        private void GenerateQ()
        {
            if (!qGenerated)
            {
                q = new Point(
                    random.Next(0, displayTarget.Width),
                    random.Next(0, displayTarget.Height)
                );
                qGenerated = true;
            }
        }

        private int? ReadNrPoints() => int.TryParse(NrPointsTextBox.Text, out int nr) ? nr : (int?)null;

        private void DrawDemo()
        {
            g.Clear(Color.White);
            foreach (var p in puncte)
            {
                DrawPoint(p, Brushes.Green);
            }
            DrawPoint(q, Brushes.Blue, 14);
            double minDistSquared = puncte.Min(p => DistanceSquared(q, p));
            double radius = Math.Sqrt(minDistSquared);
            g.DrawEllipse(Pens.Red, q.X - (float)radius, q.Y - (float)radius,
                          (float)(2 * radius), (float)(2 * radius));

            RefreshCanvas();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GenerateRandomPoints(ReadNrPoints());
            DrawDemo();
        }
    }
}
