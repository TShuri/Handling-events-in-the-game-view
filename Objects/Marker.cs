using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Event_Handling.Objects
{
    class Marker : BaseObject
    {
        public Marker(float x, float y, float angle) : base(x,y, angle)
        {
            defaultColor = Color.Red;
            color = defaultColor;
        }
        public override void Render(Graphics g) 
        {
            g.FillEllipse(new SolidBrush(color), -3, -3, 6, 6);
            g.DrawEllipse(new Pen(color, 2), -6, -6, 12, 12);
            g.DrawEllipse(new Pen(color, 2), -10, -10, 20, 20);

            CheckColor();
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-3, -3, 6, 6);
            return path;
        }
    }
}
