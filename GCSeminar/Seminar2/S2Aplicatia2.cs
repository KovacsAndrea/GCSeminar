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
    public partial class S2Aplicatia2 : BaseGraphicsForm
    {
        private Point A, B, C;
        private bool triangleFound = false;

        public S2Aplicatia2()
        {
            InitializeComponent();
            this.Text = "Seminar 2 - Aplicatia 2";
            this.NrPointsTextBox.Text = "4";
        }

        private void S2Aplicatia2_Load(object sender, EventArgs e)
        {
            SetupCanvas(this.pictureBox1);
            GenerateRandomPoints(ReadNrPoints());
            FindMinimumAreaTriangle();
            DrawDemo();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GenerateRandomPoints(ReadNrPoints());
            FindMinimumAreaTriangle();
            DrawDemo();
        }

        private int? ReadNrPoints() =>
            int.TryParse(NrPointsTextBox.Text, out int nr) ? nr : (int?)null;

        private void FindMinimumAreaTriangle()
        {
            triangleFound = false;

            if (puncte == null || puncte.Count < 3)
                return;

            double minArea = double.MaxValue;

            for (int i = 0; i < puncte.Count - 2; i++)
            {
                for (int j = i + 1; j < puncte.Count - 1; j++)
                {
                    for (int k = j + 1; k < puncte.Count; k++)
                    {
                        double area = TriangleArea(puncte[i], puncte[j], puncte[k]);

                        if (area > 0 && area < minArea)
                        {
                            minArea = area;
                            A = puncte[i];
                            B = puncte[j];
                            C = puncte[k];
                            triangleFound = true;
                        }
                    }
                }
            }
        }

        private double TriangleArea(Point p1, Point p2, Point p3)
        {
            return 0.5 * Math.Abs(
                p1.X * (p2.Y - p3.Y) +
                p2.X * (p3.Y - p1.Y) +
                p3.X * (p1.Y - p2.Y)
            );
        }

        private void DrawDemo()
        {
            g.Clear(Color.White);

            // desenăm toate punctele
            foreach (var p in puncte)
                DrawPoint(p, Brushes.Green);

            // desenăm triunghiul minim
            if (triangleFound)
            {
                g.DrawLine(Pens.Red, A, B);
                g.DrawLine(Pens.Red, B, C);
                g.DrawLine(Pens.Red, C, A);

                DrawPoint(A, Brushes.Blue, 14);
                DrawPoint(B, Brushes.Blue, 14);
                DrawPoint(C, Brushes.Blue, 14);
            }

            RefreshCanvas();
        }
    }
}
