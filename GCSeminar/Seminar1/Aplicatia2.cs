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
    public partial class Aplicatia2 : BaseGraphicsForm
    {
        private List<Point> staticPoints; 
        private int nrStaticPoints = 50;
        public Aplicatia2()
        {
            InitializeComponent();
            this.Text = "Seminar 1 - Aplicatia 2";
            this.NrPointsTextBox.Text = "10";
        }

        private void Aplicatia2_Load(object sender, EventArgs e)
        {
            SetupCanvas(this.pictureBox1);
            GenerateStaticPoints();
            GenerateRandomPoints(ReadNrPoints());
            DrawDemo();
        }

        private int? ReadNrPoints() => int.TryParse(NrPointsTextBox.Text, out int nr) ? nr : (int?)null;

        private void GenerateStaticPoints()
        {
            if (staticPoints == null || staticPoints.Count == 0)
            {
                staticPoints = new List<Point>();
                for (int i = 0; i < nrStaticPoints; i++)
                {
                    int x = random.Next(0, displayTarget.Width);
                    int y = random.Next(0, displayTarget.Height);
                    staticPoints.Add(new Point(x, y));
                }
            }
        }
        private void DrawDemo()
        {
            g.Clear(Color.White);

            foreach (var p in staticPoints)
            {
                DrawPoint(p, Brushes.Green);
            }

            foreach (var p in puncte)
            {
                DrawPoint(p, Brushes.Orange);
            }

            foreach (var dynamicPoint in puncte)
            {
                Point closest = staticPoints
                    .OrderBy(p => DistanceSquared(dynamicPoint, p))
                    .First();
                g.DrawLine(Pens.Black, dynamicPoint, closest);
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
