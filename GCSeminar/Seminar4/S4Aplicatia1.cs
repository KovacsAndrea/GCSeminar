using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GCSeminar.Seminar4
{
    public partial class S4Aplicatia1 : BaseGraphicsForm
    {
        private List<Point> hull = new List<Point>();

        public S4Aplicatia1()
        {
            InitializeComponent();
            this.Text = "Seminar 4 - Invelitoare Convexa";
            NrPointsTextBox.Text = "10";
        }

        private void S4Aplicatia1_Load(object sender, EventArgs e)
        {
            SetupCanvas(pictureBox1);

            GenerateRandomPoints(ReadNrPoints());
            ComputeConvexHull();

            DrawDemo();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GenerateRandomPoints(ReadNrPoints());
            ComputeConvexHull();
            DrawDemo();
        }

        private int? ReadNrPoints() =>
            int.TryParse(NrPointsTextBox.Text, out int nr) ? nr : (int?)null;

        private void ComputeConvexHull()
        {
            hull.Clear();

            if (puncte.Count < 3)
                return;

            var pts = puncte.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();

            List<Point> lower = new List<Point>();

            foreach (var p in pts)
            {
                while (lower.Count >= 2 &&
                       Cross(lower[lower.Count - 2],
                             lower[lower.Count - 1],
                             p) <= 0)
                {
                    lower.RemoveAt(lower.Count - 1);
                }

                lower.Add(p);
            }

            List<Point> upper = new List<Point>();

            for (int i = pts.Count - 1; i >= 0; i--)
            {
                var p = pts[i];

                while (upper.Count >= 2 &&
                       Cross(upper[upper.Count - 2],
                             upper[upper.Count - 1],
                             p) <= 0)
                {
                    upper.RemoveAt(upper.Count - 1);
                }

                upper.Add(p);
            }

            lower.RemoveAt(lower.Count - 1);
            upper.RemoveAt(upper.Count - 1);

            hull = lower.Concat(upper).ToList();
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