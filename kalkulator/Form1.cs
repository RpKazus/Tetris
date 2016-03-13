using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        int Score = 0;
        Figure figure = new Line();
        int defaultLoc = 0;
        public Form1()
        {
            InitializeComponent();
        }
        SolidBrush sb = new SolidBrush(Color.Gold);
        int r = 20;
        List<Point> Fallist = new List<Point>();
        public void DrawPoint(Graphics gr)
        {
            foreach (Point paint in Fallist)
            {
                gr.FillRectangle(sb, paint.X, paint.Y, r, r);
                gr.DrawRectangle(new Pen(Color.Black, 2), paint.X, paint.Y, r, r);
            }
        }
        public bool CanFall()
        {
            bool boolFall = figure.SNT.Y + figure.r + 1 < panel1.Height;
                foreach (Point falling in figure.FillPoints)
                {
                    foreach (Point falled in Fallist)
                    {
                        if (falled.Equals(new Point(falling.X, falling.Y + 20)))
                        {

                            return false;
                        }
                    }
                }
            return boolFall;
        }
        public bool CanLeft()
        {
            bool boolMove = figure.SLT.X -1 > 0;
            foreach (Point falling in figure.FillPoints)
            {
                foreach (Point falled in Fallist)
                {
                    if (falled.Equals(new Point(falling.X - r, falling.Y)))
                        return false;
                }
            }
            return boolMove;
        }
        public bool CanRight()
        {
            bool boolMove = figure.SLT.X + r + r + 1 < panel1.Width;
            foreach (Point falling in figure.FillPoints)
            {
                foreach (Point falled in Fallist)
                {
                    if (falled.Equals(new Point(falling.X + (r * 3), falling.Y)))
                        return false;
                }
            }
            return boolMove;
        }
        public void CheckLine()
        {
            for (int y = 0; y < panel1.Height; y += r)
            {
                List<Point> Liest = new List<Point>();
                foreach (Point pp in Fallist)
                    if (pp.Y == y) Liest.Add(pp);
                if (Liest.Count >= 18)
                {
                    foreach (Point pp in Liest)
                        Fallist.Remove(pp);
                    for (int i = 0; i < Fallist.Count; i++)
                        if (Fallist[i].Y < y) Fallist[i] = new Point(Fallist[i].X, Fallist[i].Y + r);
                    Score += 100;
                }
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            figure.DrowFigure(e.Graphics);
            DrawPoint(e.Graphics);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ScoreList.Text = Score.ToString();
            if (CanFall())
                figure.step();
            else
            {
                if (figure.location.Y == 0)
                {
                    timer1.Enabled = false;
                    MessageBox.Show("Вы проиграли!!");

                    this.Close();
                }                
                Fallist.AddRange(figure.FillPoints);
                //label2.Text = Convert.ToString(panel1.Height) + " " + Convert.ToString(figure.location.Y) + " " + Convert.ToString(Fallist[2].Y);
                figure = new Line();
                if(defaultLoc < 320) defaultLoc += 40;
                else defaultLoc = 0;
                figure.location = new Point(defaultLoc, 320);
                CheckLine();
            }
            panel1.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (CanLeft())
                    figure.location = new Point(figure.location.X - r, figure.location.Y);
                    panel1.Invalidate();
                    break;
                case Keys.D:
                    if (CanRight())
                    figure.location = new Point(figure.location.X + r, figure.location.Y);
                    panel1.Invalidate();
                    break;
                case Keys.S:
                    while(CanFall())
                        figure.step();
                    break;
                case Keys.Space:
                    if (CanFall()) figure.Rotate();                       
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
