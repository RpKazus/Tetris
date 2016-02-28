using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class Class1
    {

        public int r = 20;
        Color c = Color.Gold;
        public Point location = new Point(0,0);
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

        public List<Point> FillPoints
        {
            get
            {
                List<Point> result = new List<Point>();
                result.Add(location);
                result.Add(new Point(location.X , location.Y + r));
                result.Add(new Point(location.X +  r, location.Y + r));
                result.Add(new Point(location.X +  r, location.Y));
                return result;
            }
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
