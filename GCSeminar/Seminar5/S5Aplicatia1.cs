using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GCSeminar.Seminar5
{
    public partial class S5Aplicatia1 : BaseGraphicsForm
    {
        private List<Point> hull = new List<Point>();

        public S5Aplicatia1()
        {
            InitializeComponent();
            this.Text = "Seminar 5 - Graham Scan";
            NrPointsTextBox.Text = "10";
        }

        private void S5Aplicatia1_Load(object sender, EventArgs e)
        {
            SetupCanvas(pictureBox1);

            GenerateRandomClusteredPoints(ReadNrPoints());
            ComputeHull();

            DrawDemo();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GenerateRandomClusteredPoints(ReadNrPoints());
            ComputeHull();
            DrawDemo();
        }

        private int? ReadNrPoints() =>
            int.TryParse(NrPointsTextBox.Text, out int nr) ? nr : (int?)null;

        private void ComputeHull()
        {
            hull.Clear();

            int n = puncte.Count;
            if (n < 3)
                return;

            int leftmost = 0;
            for (int i = 1; i < n; i++)
            {
                if (puncte[i].X < puncte[leftmost].X)
                    leftmost = i;
            }

            int p = leftmost;

            do
            {
                hull.Add(puncte[p]);

                int q = (p + 1) % n;

                for (int i = 0; i < n; i++)
                {
                    if (Cross(puncte[p], puncte[i], puncte[q]) > 0)
                        q = i;
                }

                p = q;

            } while (p != leftmost);
        }

        private long Cross(Point O, Point A, Point B)
        {
            return (long)(A.X - O.X) * (B.Y - O.Y) -
                   (long)(A.Y - O.Y) * (B.X - O.X);
        }

        private void DrawDemo()
        {
            g.Clear(Color.White);

            foreach (var p in puncte)
                DrawPoint(p, Brushes.Green);

            if (hull.Count > 1)
            {
                for (int i = 0; i < hull.Count; i++)
                {
                    Point a = hull[i];
                    Point b = hull[(i + 1) % hull.Count];

                    g.DrawLine(new Pen(Color.Red, 2), a, b);
                }
            }

            RefreshCanvas();
        }
    }
}