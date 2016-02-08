using System;
using System.Collections.Generic;
using System.Linq;

namespace Core {
    public class CoordPoint {
        float x;
        float y;
        public static float Distance(CoordPoint p1, CoordPoint p2) {
            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
        public static CoordPoint operator +(CoordPoint p1, CoordPoint p2) {
            return new CoordPoint(p1.X + p2.X, p1.Y + p2.Y);
        }
        public static CoordPoint operator -(CoordPoint p1, CoordPoint p2) {
            return new CoordPoint(p1.X - p2.X, p1.Y - p2.Y);
        }
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        public CoordPoint(float X, float Y) {
            x = X;
            y = Y;
        }
        public CoordPoint() {
            x = 0;
            y = 0;
        }
        public override string ToString() {
            return string.Format("({0}:{1})", x, y);
        }
    }
}
