using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Event_Handling.Objects
{
    class Black_area : BaseObject
    {
        public int height, width;
        public Black_area(float x, float y, float angle, int _h, int _w) : base(x, y, angle)
        {
            height = _h;
            width = _w;
        }

        public override void Render(Graphics g)
        {
            if (this.X == width)
            {
                this.X = width / -4;
            }

            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, width / 4, height);
            g.DrawRectangle(new Pen(Color.Black, 2), 0, 0, width / 4, height);
            this.X += 1;
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            //path.AddEllipse(0, 0, width, height);
            Rectangle pathRect = new Rectangle(0, 0, width / 4, height);
            path.AddRectangle(pathRect);
            return path;
        }

        public void ChangeFill(BaseObject obj)
        {
            obj.color = Color.White;
        }

        /*public void ReturnFill(BaseObject obj)
        {
            obj.color = obj.defaultColor;
        }*/
    }
}
