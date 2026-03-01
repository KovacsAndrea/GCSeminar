using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GCSeminar.Seminar2
{
    public partial class S2Aplicatia1 : BaseGraphicsForm
    {
        private Point q;
        private bool qGenerated = false;

        public S2Aplicatia1()
        {
            InitializeComponent();
            this.Text = "Seminar 2 - Aplicatia 1";
            this.DistanceTextBox.Text = "200";
            this.NrPointsTextBox.Text = "10";
        }

        private void S2Aplicatia1_Load(object sender, EventArgs e)
        {
            SetupCanvas(this.pictureBox1);
            GenerateRandomPoints(ReadNrPoints());
            GenerateQ();
            DrawDemo();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GenerateRandomPoints(ReadNrPoints());
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

        private int? ReadNrPoints() =>
            int.TryParse(NrPointsTextBox.Text, out int nr) ? nr : (int?)null;

        private double? ReadDistance()
        {
            if (double.TryParse(DistanceTextBox.Text, out double d) && d > 0)
                return d;

            return 200;
        }

        private void DrawDemo()
        {
            g.Clear(Color.White);

            var d = ReadDistance();
            double dSquared = d.HasValue ? d.Value * d.Value : 0;

            foreach (var p in puncte)
            {
                if (d.HasValue && DistanceSquared(q, p) <= dSquared)
                    DrawPoint(p, Brushes.Red);
                else
                    DrawPoint(p, Brushes.Green);
            }

            DrawPoint(q, Brushes.Blue, 14);
            if (d.HasValue)
            {
                g.DrawEllipse(
                    Pens.Black,
                    q.X - (float)d.Value,
                    q.Y - (float)d.Value,
                    (float)(2 * d.Value),
                    (float)(2 * d.Value)
                );
            }

            RefreshCanvas();
        }
    }
}
