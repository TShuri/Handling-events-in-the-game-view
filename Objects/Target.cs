using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Handling.Objects
{
    class Target : BaseObject // Наследование от BaseObject
    {
        public int Time;
        public Target(float x, float y, float angle, int time) : base(x, y, angle)
        {
            this.Time = time;
            defaultColor = Color.GreenYellow;
            color = defaultColor;
        }

        public override void Render(Graphics g)
        {
            if (Time == 0)
            {
                this.updateTime(0, 0);
            }

            g.FillEllipse(new SolidBrush(color), -30, -30, 30, 30);
            g.DrawEllipse(new Pen(color, 2), -30, -30, 30, 30);

            if (color == defaultColor)
            {
                color = Color.Red;
            }
            g.DrawString(
                        Time.ToString(),
                        new Font("Verdana", 8), // шрифт и размер
                        new SolidBrush(color), // цвет шрифта
                        1, 1 // точка в которой нарисовать текст
            );

            CheckColor();
            Time -= 1;
        }

        public void updateTime(float playerX, float playerY)
        {
            Time = (int)Math.Sqrt((double)((this.X - playerX) * (this.X - playerX) 
                                            + (this.Y - playerY) * (this.Y - playerY))) / 2;
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-30, -30, 30, 30);
            return path;
        }
    }
}
