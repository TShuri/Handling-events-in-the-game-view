﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
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
        public Color defaultColor;
        public Color color;

        public Action<BaseObject, BaseObject> OnOverlap;

        public BaseObject(float x, float y, float angle) // Конструктор    
        {
            X = x;
            Y = y;
            Angle = angle;
        }

        public Matrix GetTransform()
        {
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);
            return matrix;
        }

        public virtual void Render(Graphics g)
        {
            // Пока пусто
        }

        public virtual GraphicsPath GetGraphicsPath()
        {
            return new GraphicsPath();
        }

        public virtual bool Overlaps(BaseObject obj, Graphics g)
        {
            var path1 = this.GetGraphicsPath();
            var path2 = obj.GetGraphicsPath();

            path1.Transform(this.GetTransform());
            path2.Transform(obj.GetTransform());

            var region = new Region(path1);
            region.Intersect(path2);
            return !region.IsEmpty(g);
        }

        public virtual void Overlap(BaseObject obj)
        {
            if (this.OnOverlap != null)
            {
                this.OnOverlap(this, obj);
            }
        }

        public void CheckColor()
        {
            if (color != defaultColor)
            {
                color = defaultColor;
            }
        }
    }
}
