using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GCSeminar.Seminar3
{
    public partial class S3Aplicatia1 : BaseGraphicsForm
    {
        private List<Tuple<Point, Point>> bestMatching;
        private double bestCost;

        public S3Aplicatia1()
        {
            InitializeComponent();
            this.Text = "Seminar 3 - Aplicatia 1 (Problema de cuplaj)";
            NrPointsTextBox.Text = "4";
        }

        private void S3Aplicatia1_Load(object sender, EventArgs e)
        {
            SetupCanvas(pictureBox1);

            GenerateRandomPoints(ReadNrPoints());
            SolveMatching();

            DrawDemo();
        }



        private int? ReadNrPoints()
        {
            if (int.TryParse(NrPointsTextBox.Text, out int nr) && nr > 0)
                return nr * 2;

            return null;
        }

        private void SolveMatching()
        {
            bestCost = double.MaxValue;
            bestMatching = new List<Tuple<Point, Point>>();

            bool[] used = new bool[puncte.Count];

            Backtrack(new List<Tuple<Point, Point>>(), used, 0);
        }

        private void Backtrack(List<Tuple<Point, Point>> current, bool[] used, double cost)
        {
            if (cost >= bestCost)
                return;

            int first = -1;

            for (int i = 0; i < puncte.Count; i++)
            {
                if (!used[i])
                {
                    first = i;
                    break;
                }
            }

            if (first == -1)
            {
                bestCost = cost;
                bestMatching = new List<Tuple<Point, Point>>(current);
                return;
            }

            used[first] = true;

            for (int j = first + 1; j < puncte.Count; j++)
            {
                if (!used[j])
                {
                    used[j] = true;

                    Point a = puncte[first];
                    Point b = puncte[j];

                    double d = Math.Sqrt(DistanceSquared(a, b));

                    current.Add(Tuple.Create(a, b));

                    Backtrack(current, used, cost + d);

                    current.RemoveAt(current.Count - 1);

                    used[j] = false;
                }
            }

            used[first] = false;
        }

        private void DrawDemo()
        {
            g.Clear(Color.White);

            foreach (var p in puncte)
                DrawPoint(p, Brushes.Green);

            if (bestMatching != null)
            {
                foreach (var pair in bestMatching)
                {
                    g.DrawLine(new Pen(Color.Red, 2), pair.Item1, pair.Item2);
                }
            }

            RefreshCanvas();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GenerateRandomPoints(ReadNrPoints());
            SolveMatching();
            DrawDemo();
        }
    }
}