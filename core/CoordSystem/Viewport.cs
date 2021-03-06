﻿using Microsoft.Xna.Framework;
using System;

namespace GameCore {
    public class Viewport {
        float pxlHeight;
        float pxlWidth;
        float zoomTarget = 128f;
        float zoomStep = 12.8f;
        float scale = 128f;
        int lockTime = 0;
        Vector2 centerPoint = new Vector2();

        public delegate void ViewportChangedEventHandler(ViewportChangedEventArgs args);
        public event ViewportChangedEventHandler Changed;

        public Bounds Bounds {
            get {
                return new Bounds(Centerpoint - new Vector2(pxlWidth * scale / 2, pxlHeight * scale / 2), Centerpoint + new Vector2(pxlWidth * scale / 2, pxlHeight * scale / 2));
            }
        }
        public Vector2 Centerpoint {
            get {
                return centerPoint;
            }
            set {
                if(centerPoint == value)
                    return;
                if(!SmoothUpdate || Vector2.Subtract(centerPoint, value).Length() < 1) {
                    centerPoint = value;
                    return;
                }
                SmoothScroll(value);
            }
        }
        public float MiniMapScale {
            get {
                return 30f;
            }
        }
        public float PxlHeight {
            get {
                return pxlHeight;
            }
            set {
                if(pxlHeight == value)
                    return;
                pxlHeight = value;
            }
        }
        public float PxlWidth {
            get {

                return pxlWidth;
            }
            set {
                if(pxlWidth == value)
                    return;
                pxlWidth = value;
            }
        }
        public float Scale {
            get {
                return scale;
            }
            set {
                if(scale == value)
                    return;
                var d = value - scale;
                if(SmoothUpdate && Math.Abs(d) > 0.1)
                    d /= 50;
                scale += d;
            }
        }

        public Vector2 Location { get; set; }
        public Vector2 PxlSize { get { return new Vector2(pxlWidth, pxlHeight); } }
        public Rectangle Rectangle { get { return new Rectangle(Location.ToPoint(), PxlSize.ToPoint()); } }
        public bool SmoothUpdate { get; set; } = true;

        public Viewport(Vector2 location, Vector2 size) {
            this.pxlWidth = size.X;
            this.pxlHeight = size.Y;
            Location = location;
        }

        public Vector2 Screen2WorldPoint(float x, float y) {
            float pixelFactorX = PxlWidth > 0 ? Bounds.Width / PxlWidth : 0;
            float pixelFactorY = PxlHeight > 0 ? Bounds.Height / pxlHeight : 0;
            return new Vector2(x * pixelFactorX + Bounds.LeftTop.X, y * pixelFactorY + Bounds.LeftTop.Y) - Location;
        }

        public Vector2 Screen2WorldPoint(Vector2 scrPoint) {
            return Screen2WorldPoint(scrPoint.X, scrPoint.Y);
        }
        public Bounds ScreenToWorldBounds(Bounds scrBounds) {
            return new Bounds(Screen2WorldPoint(scrBounds.LeftTop), Screen2WorldPoint(scrBounds.RightBottom));
        }
        public Bounds World2ScreenBounds(Bounds scrBounds) {
            return new Bounds(World2ScreenPoint(scrBounds.LeftTop), World2ScreenPoint(scrBounds.RightBottom));
        }
        public Vector2 World2ScreenPoint(float x, float y) {
            float unitFactorX = Bounds.Width > 0 ? PxlWidth / Bounds.Width : 0;
            float unitFactorY = Bounds.Height > 0 ? PxlHeight / Bounds.Height : 0;
            return new Vector2((x - Bounds.LeftTop.X) * unitFactorX, (y - Bounds.LeftTop.Y) * unitFactorY) + Location;
        }

        public Vector2 World2ScreenPoint(Vector2 wrlPoint) {
            return World2ScreenPoint(wrlPoint.X, wrlPoint.Y);
        }
        public override string ToString() {
            return string.Format("Bounds: {0}:{1} | Size: {2}x{3} | Centerpoint: {4}", Bounds.LeftTop, Bounds.RightBottom, pxlWidth, pxlHeight, Centerpoint);
        }
        public void ZoomIn() {
            ChangeZoom(scale + scale / 10, scale / 50f);
        }
        public void ZoomOut() {
            ChangeZoom(scale - scale / 10, -scale / 50f);

        }
        public void Update() {

            //if(Math.Abs(scale - target) > 0.1f)
            //    scale += step;

            if(scale < 0)
                scale = 0;
        }

        void RaiseChanged() {
            if(Changed != null)
                Changed(new ViewportChangedEventArgs(this));
        }
        void SmoothScroll(Vector2 value) {
            //  var step = (centerPoint - value).Length / 100f;
            centerPoint = (Vector2.Subtract(value, centerPoint)).UnaryVector()
                * (Math.Abs(value.Substract(centerPoint).Length()) / 50)
                + centerPoint;
        }
        void ChangeZoom(float target, float step) {
            this.zoomTarget = target;
            this.zoomStep = Math.Sign(step) * Math.Max(0.1f, Math.Abs(step));
        }
        void ChangeZoom(float delta) {
            if(lockTime == 0) {
                scale += delta;
                lockTime = 5;
            }
            else
                lockTime--;
        }

        public void SetWorldBounds(float left, float top, float right, float bottom, int clipOffset = 0) {
            //  Scale = 1;
            // Centerpoint = Vector2.Zero;
            // return;
            Vector2 lt = new Vector2(left - clipOffset, top - clipOffset);
            Vector2 rb = new Vector2(right + clipOffset, bottom + clipOffset);
            Centerpoint = lt + (rb - lt) / 2;

            var scale1 = Math.Abs(((Centerpoint - lt) * 2).X / pxlWidth);
            var scale2 = Math.Abs(((Centerpoint - lt) * 2).Y / pxlHeight);
            var scale3 = Math.Abs(((rb - Centerpoint) * 2).X / pxlWidth);
            var scale4 = Math.Abs(((rb - Centerpoint) * 2).Y / pxlHeight);

            Scale = Math.Max(Math.Max(scale1, scale2), Math.Max(scale3, scale4));
            //  scale  == (rb-cp)*2/(pxlWidth , pxlHeight)
        }
    }

    public class ViewportChangedEventArgs: EventArgs {
        Viewport view;
        public Viewport View { get { return view; } }
        public ViewportChangedEventArgs(Viewport view) {
            this.view = view;
        }
    }
}
