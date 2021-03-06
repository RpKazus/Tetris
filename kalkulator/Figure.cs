﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    abstract class Figure
    {
        //Poles
        public int r = 20;
        Color c = Color.Gold;
        public int rotation = 0;
        public Point location = new Point(0,0);
        //Constructors
        public Figure()
        {
        }
        //Methods
        public void DrowFigure(Graphics gr)
        {
            Pen p = new Pen(Color.Black,2);
            SolidBrush b = new SolidBrush(c);
            foreach (Point point in FillPoints)
            {
                 gr.FillRectangle(b, point.X, point.Y, r, r);
                 gr.DrawRectangle(p,point.X,point.Y,r,r);
            }
        }
        public void Rotate()
        {
            if (rotation >= 0 && rotation <= 2)
                rotation++;
            else if (rotation == 2) rotation = 0;
        }
        public abstract List<Point> FillPoints
        {
            get;
        }

        public void step()
        {
            location = new Point(location.X,location.Y + r);
        }

        public Point SLT
        {
            get
            {
                /*foreach (Point np in result)
                {
                   
                }*/
                Point result = new Point(location.X,location.Y + r);
                return result;
            }
        }
        public Point SNT
        {
            get
            {
                Point result = location;
                foreach (Point pp in FillPoints)
                {
                    if (result.Y < pp.Y)
                        result = pp;
                }
                result = new Point(result.X,result.Y);
                return result;
            }
        }
        public Point SPT
        {
            get
            {
                Point result = location;
                foreach (Point pp in FillPoints)
                {
                    if (result.X < pp.X)
                        result = pp;
                }
                return result;
            }
        }
    }
}
