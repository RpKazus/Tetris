using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class Square : Figure
    {
        Color c = Color.Gold;
        public override List<Point> FillPoints
        {
            get
            {
                List<Point> result = new List<Point>();
                result.Add(location);
                result.Add(new Point(location.X, location.Y + r));
                result.Add(new Point(location.X + r, location.Y + r));
                result.Add(new Point(location.X + r, location.Y));
                return result;
            }
        }
    }
}
