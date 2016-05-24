﻿using System;
using System.Collections.Generic;

namespace GameCore {
    public class Bounds {

        public CoordPoint Center {
            get {
                return (LeftTop + RightBottom) / 2f;
            }
        }
        public float Height {
            get {
                return Math.Abs(RightBottom.Y - LeftTop.Y);
            }
        }
        public float Width {
            get {
                return Math.Abs(RightBottom.X - LeftTop.X);
            }
        }

        public Bounds() { }

        public Bounds(CoordPoint lt, CoordPoint rb) {
            LeftTop = lt;
            RightBottom = rb;
        }

        public static Bounds operator -(Bounds p1, Bounds p2) {
            return new Bounds(p1.LeftTop - p2.LeftTop, p1.RightBottom - p2.RightBottom);
        }
        public static Bounds operator -(Bounds p1, CoordPoint p2) {
            return new Bounds(p1.LeftTop - p2, p1.RightBottom - p2);
        }
        public static Bounds operator *(Bounds p1, float k) {
            return new Bounds(p1.LeftTop * k, p1.RightBottom * k);
        }
        public static Bounds operator /(Bounds p1, float k) {
            return new Bounds(p1.LeftTop / k, p1.RightBottom / k);
        }
        public static Bounds operator +(Bounds p1, Bounds p2) {
            return new Bounds(p1.LeftTop + p2.LeftTop, p1.RightBottom + p2.RightBottom);
        }
        public static Bounds operator +(Bounds p1, CoordPoint p2) {
            return new Bounds(p1.LeftTop + p2, p1.RightBottom + p2);
        }

        //public  bool isIntersect(Bounds obj) {
        //    return (Math.Abs(LeftTop.X - obj.LeftTop.X) * 2 < (Width + obj.Width)) &&
        //    (Math.Abs(LeftTop.Y - obj.LeftTop.Y) * 2 < (Height + obj.Height));
        //}
        public bool Contains(CoordPoint p) {
            return this.LeftTop.X <= p.X && LeftTop.Y <= p.Y && RightBottom.X >= p.X && RightBottom.Y > p.Y;
        }
        public IEnumerable<CoordPoint> GetPoints() {
            yield return this.LeftTop;
            yield return this.LeftTop + new CoordPoint(this.Width, 0);
            yield return this.RightBottom;
            yield return this.RightBottom + new CoordPoint(0, this.Height);
        }
        public bool Intersect(Bounds bounds) {
            foreach(CoordPoint p in bounds.GetPoints())
                if(bounds.Contains(p))
                    return true;
            return false;
        }
        public override string ToString() {
            return LeftTop + " : " + RightBottom;
        }
        public CoordPoint LeftTop = new CoordPoint();
        public CoordPoint RightBottom = new CoordPoint();

    }
}