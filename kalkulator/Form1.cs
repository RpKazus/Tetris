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
        Class1 figure = new Class1();
        int defaultLoc = 0;
        public Form1()
        {
            InitializeComponent();
        }
        SolidBrush sb = new SolidBrush(Color.Gold);
        int r = 20;
        List<Point> Fallist = new List<Point>();
        bool[,] Matrix = new bool[18,24];
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
            bool boolFall = figure.SLT.Y + figure.r + 1 < panel1.Height;
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
            for (int y = 0; y < panel1.Height / r; y++ )
            {
                bool isFull = true;
                for (int x = 0; x < panel1.Width / r; x++)
                    if(isFull) isFull = Matrix[x, y];
                if (isFull)
                {
                    bool[,] TempMatrix = new bool[18, 24];

                    for (int x = 0; x < panel1.Width / r; x++)
                        Matrix[x, y] = false;
                    TempMatrix = Matrix;
                    for(int i = 0; i < Matrix.GetLength(1); i++)
                        for (int o = 0; o < Matrix.GetLength(0); o++)
                            if(y > i) TempMatrix[o, i + 1] = Matrix[o, i];
                    Matrix = TempMatrix;
                    Fallist.Clear();
                    for (int i = Matrix.GetLength(1) - 1; i > 0; i--)
                        for (int o = 0; o < Matrix.GetLength(0); o++)
                            if (Matrix[o, i]) Fallist.Add(new Point(o * r, i * r));  
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
            if (CanFall())
            {
                figure.step();
                //label
            }
            else
            {
                if (figure.location.Y == 0)
                {
                    timer1.Enabled = false;
                    MessageBox.Show("Вы проиграли!!");

                    this.Close();
                }
                foreach (Point p in figure.FillPoints)
                {
                    Matrix[p.X / r, p.Y / r] = true;
                }
                
                Fallist.AddRange(figure.FillPoints);
                //label2.Text = Convert.ToString(panel1.Height) + " " + Convert.ToString(figure.location.Y) + " " + Convert.ToString(Fallist[2].Y);
                figure = new Class1();
                if(defaultLoc < 320) defaultLoc += 40;
                else defaultLoc = 0;
                figure.location = new Point(defaultLoc, 320);
                CheckLine();
            }
            label1.Text = Convert.ToString(panel1.Height) + " " + Convert.ToString(figure.location.Y);
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
            }
        }
    }
}
