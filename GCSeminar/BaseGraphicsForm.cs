using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GCSeminar
{
    public partial class BaseGraphicsForm : Form
    {
        protected Graphics g;
        protected Bitmap b;
        protected PictureBox displayTarget;
        protected List<Point> puncte;
        protected Random random = new Random();

        public BaseGraphicsForm()
        {
            InitializeComponent();
        }

        protected void SetupCanvas(PictureBox pb)
        {
            if (pb == null) return;
            displayTarget = pb;
            g?.Dispose();
            b?.Dispose();
            b = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(b);
            pb.Image = b;
            g.Clear(Color.White);
        }

        protected void RefreshCanvas()
        {
            displayTarget?.Refresh();
        }

        protected void GenerateRandomPoints(int? updatedNrPoints = null)
        {
            int currentNrPoints;
            if (updatedNrPoints.HasValue && updatedNrPoints > 0)
            {
                currentNrPoints = updatedNrPoints.Value; 
            }else
            {
                currentNrPoints = 10;
            }

            puncte = new List<Point>();
            for (int i = 0; i < currentNrPoints; i++)
            {
                int x = random.Next(0, displayTarget.Width);
                int y = random.Next(0, displayTarget.Height);
                puncte.Add(new Point(x, y));
            }
        }

        protected void DrawPoint(Point p, Brush brush, int size = 12)
        {
            int halfSize = size / 2;
            g.FillEllipse(brush, p.X - halfSize, p.Y - halfSize, size, size);
        }

        protected double DistanceSquared(Point a, Point b)
        {
            int dx = a.X - b.X;
            int dy = a.Y - b.Y;
            return dx * dx + dy * dy;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            g?.Dispose();
            b?.Dispose();
            base.OnFormClosing(e);
        }

        private void BaseGraphicsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
