using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GCSeminar.Seminar3
{
    public partial class S3Aplicatia2 : BaseGraphicsForm
    {
        private List<Segment> segmente = new List<Segment>();
        private List<PointF> intersectii = new List<PointF>();

        public S3Aplicatia2()
        {
            InitializeComponent();
            this.Text = "Seminar 3 - Aplicatia 2 (Intersectii segmente - Sweep Line)";
            NrPointsTextBox.Text = "4";
        }

        private void S3Aplicatia2_Load(object sender, EventArgs e)
        {
            SetupCanvas(pictureBox1);

            GenerateRandomPoints(ReadNrPoints());
            BuildSegments();
            FindIntersectionsSweep();
            DrawDemo();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GenerateRandomPoints(ReadNrPoints());
            BuildSegments();
            FindIntersectionsSweep();
            DrawDemo();
        }

        private int? ReadNrPoints()
        {
            if (int.TryParse(NrPointsTextBox.Text, out int nr) && nr > 0)
                return nr * 2;

            return null;
        }

        private void BuildSegments()
        {
            segmente.Clear();

            for (int i = 0; i < puncte.Count; i += 2)
            {
                segmente.Add(new Segment(puncte[i], puncte[i + 1]));
            }
        }

        private void FindIntersectionsSweep()
        {
            intersectii.Clear();

            List<EventPoint> events = new List<EventPoint>();

            foreach (var s in segmente)
            {
                events.Add(new EventPoint(s.A.X, s.A, s, true));
                events.Add(new EventPoint(s.B.X, s.B, s, false));
            }

            events = events.OrderBy(e => e.X).ToList();

            List<Segment> activeSegments = new List<Segment>();

            foreach (var ev in events)
            {
                if (ev.IsStart)
                {
                    activeSegments.Add(ev.Segment);

                    foreach (var other in activeSegments)
                    {
                        if (other == ev.Segment)
                            continue;

                        if (SegmentIntersection(
                            ev.Segment.A,
                            ev.Segment.B,
                            other.A,
                            other.B,
                            out PointF inter))
                        {
                            intersectii.Add(inter);
                        }
                    }
                }
                else
                {
                    activeSegments.Remove(ev.Segment);
                }
            }
        }

        private bool SegmentIntersection(Point A, Point B, Point C, Point D, out PointF intersection)
        {
            intersection = new PointF();

            float a1 = B.Y - A.Y;
            float b1 = A.X - B.X;
            float c1 = a1 * A.X + b1 * A.Y;

            float a2 = D.Y - C.Y;
            float b2 = C.X - D.X;
            float c2 = a2 * C.X + b2 * C.Y;

            float det = a1 * b2 - a2 * b1;

            if (Math.Abs(det) < 0.0001)
                return false;

            float x = (b2 * c1 - b1 * c2) / det;
            float y = (a1 * c2 - a2 * c1) / det;

            if (PointOnSegment(x, y, A, B) && PointOnSegment(x, y, C, D))
            {
                intersection = new PointF(x, y);
                return true;
            }

            return false;
        }

        private bool PointOnSegment(float x, float y, Point A, Point B)
        {
            return x >= Math.Min(A.X, B.X) - 0.01 &&
                   x <= Math.Max(A.X, B.X) + 0.01 &&
                   y >= Math.Min(A.Y, B.Y) - 0.01 &&
                   y <= Math.Max(A.Y, B.Y) + 0.01;
        }

        private void DrawDemo()
        {
            g.Clear(Color.White);

            foreach (var s in segmente)
            {
                g.DrawLine(Pens.Blue, s.A, s.B);
                DrawPoint(s.A, Brushes.Green);
                DrawPoint(s.B, Brushes.Green);
            }

            foreach (var p in intersectii)
            {
                g.FillEllipse(Brushes.Red, p.X - 6, p.Y - 6, 12, 12);
            }

            RefreshCanvas();
        }
    }

    public class Segment
    {
        public Point A;
        public Point B;

        public Segment(Point a, Point b)
        {
            if (a.X < b.X)
            {
                A = a;
                B = b;
            }
            else
            {
                A = b;
                B = a;
            }
        }
    }

    public class EventPoint
    {
        public float X;
        public Point P;
        public Segment Segment;
        public bool IsStart;

        public EventPoint(float x, Point p, Segment s, bool start)
        {
            X = x;
            P = p;
            Segment = s;
            IsStart = start;
        }
    }
}