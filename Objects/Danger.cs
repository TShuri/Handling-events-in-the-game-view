using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Handling.Objects
{
    class Danger : BaseObject
    {
        public int size;

        public Danger(float x, float y, float angle) : base(x, y, angle)
        {
            this.size = 5;
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.IndianRed), -2 * size, -2 * size, 3 * size, 3 * size);
            g.DrawEllipse(new Pen(Color.IndianRed, 2), -2 * size, -2 * size, 3 * size, 3 * size);
            //g.FillEllipse(new SolidBrush(Color.IndianRed), -1 * size, 1 * size, size, size);
            //g.DrawEllipse(new Pen(Color.IndianRed, 2), -1 * size, -1 * size, size, size);

            size += 1;
        }

        public void updateSize()
        {
            size = 5;
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-2 * size, -2 * size, 3 * size, 3 * size);
            return path;
        }
    }
}
