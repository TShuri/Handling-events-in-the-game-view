using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Handling.Objects
{
    class BaseObject
    {
        public float X;
        public float Y;
        public float Angle;

        public BaseObject(float x, float y, float angle) // Конструктор    
        {
            X = x;
            Y = y;
            Angle = angle;
        }

        public virtual void Render(Graphics g)
        {
            // Пока пусто
        }
    }
}
