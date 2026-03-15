using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GCSeminar.Seminar2
{
    public partial class S2Aplicatia3 : BaseGraphicsForm
    {
        private PointF center;
        private double radius;
        private bool circleFound = false;

        public S2Aplicatia3()
        {
            InitializeComponent();
            this.Text = "Seminar 2 - Aplicatia 3";
            this.NrPointsTextBox.Text = "4";
        }

        private void S2Aplicatia3_Load(object sender, EventArgs e)
        {
            
            SetupCanvas(this.pictureBox1);
            GenerateRandomClusteredPoints(ReadNrPoints());
            FindMinimumEnclosingCircle();
            DrawDemo();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GenerateRandomClusteredPoints(ReadNrPoints());
            FindMinimumEnclosingCircle();
            DrawDemo();
        }


        private int? ReadNrPoints() =>
            int.TryParse(NrPointsTextBox.Text, out int nr) ? nr : (int?)null;

        private void FindMinimumEnclosingCircle()
        {
            circleFound = false;
            if (puncte == null || puncte.Count == 0)
                return;

            double minRadius = double.MaxValue;

            for (int i = 0; i < puncte.Count; i++)
            {
                for (int j = i + 1; j < puncte.Count; j++)
                {
                    Point A = puncte[i];
                    Point B = puncte[j];

                    PointF c = new PointF((A.X + B.X) / 2f, (A.Y + B.Y) / 2f);
                    double r = Distance(A, B) / 2.0;

                    if (AllPointsInside(c, r) && r < minRadius)
                    {
                        minRadius = r;
                        center = c;
                        radius = r;
                        circleFound = true;
                    }
                }
            }

            for (int i = 0; i < puncte.Count - 2; i++)
            {
                for (int j = i + 1; j < puncte.Count - 1; j++)
                {
                    for (int k = j + 1; k < puncte.Count; k++)
                    {
                        if (Circumcircle(puncte[i], puncte[j], puncte[k],
                                         out PointF c, out double r))
                        {
                            if (AllPointsInside(c, r) && r < minRadius)
                            {
                                minRadius = r;
                                center = c;
                                radius = r;
                                circleFound = true;
                            }
                        }
                    }
                }
            }
        }

        private bool AllPointsInside(PointF c, double r)
        {
            foreach (var p in puncte)
            {
                double dx = p.X - c.X;
                double dy = p.Y - c.Y;
                if (dx * dx + dy * dy > r * r + 0.0001)
                    return false;
            }
            return true;
        }

        private double Distance(Point a, Point b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private bool Circumcircle(Point A, Point B, Point C,
                                  out PointF center, out double radius)
        {
            center = new PointF();
            radius = 0;

            double d = 2 * (A.X * (B.Y - C.Y) +
                            B.X * (C.Y - A.Y) +
                            C.X * (A.Y - B.Y));

            if (Math.Abs(d) < 0.0001)
                return false; 

            double ux = ((A.X * A.X + A.Y * A.Y) * (B.Y - C.Y) +
                         (B.X * B.X + B.Y * B.Y) * (C.Y - A.Y) +
                         (C.X * C.X + C.Y * C.Y) * (A.Y - B.Y)) / d;

            double uy = ((A.X * A.X + A.Y * A.Y) * (C.X - B.X) +
                         (B.X * B.X + B.Y * B.Y) * (A.X - C.X) +
                         (C.X * C.X + C.Y * C.Y) * (B.X - A.X)) / d;

            center = new PointF((float)ux, (float)uy);
            radius = Math.Sqrt((A.X - ux) * (A.X - ux) +
                               (A.Y - uy) * (A.Y - uy));

            return true;
        }

        private void DrawDemo()
        {
            g.Clear(Color.White);

            foreach (var p in puncte)
                DrawPoint(p, Brushes.Green);

            if (circleFound)
            {
                g.DrawEllipse(Pens.Red,
                    center.X - (float)radius,
                    center.Y - (float)radius,
                    (float)(2 * radius),
                    (float)(2 * radius));
            }

            RefreshCanvas();
        }
    }
}