﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using GameCore;

namespace MonoGameDirectX {
    public class Control : InteractiveObject {
        public Color BorderColor { get; internal set; }
        public Color FillColor { get; internal set; }
        public Rectangle Rectangle { get; set; }

        public Control(Rectangle rect) {
            Rectangle = rect;
            FillColor = Color.White;
            BorderColor = Color.Brown;
        }

        public override bool Contains(object position) {
            return Rectangle.Contains((Point)position);
        }
    }
    public class Label: Control {
        public string Text { get; set; }
        public Microsoft.Xna.Framework.Color TextColor { get; internal set; }

        public Label(int x, int y, int w, int h, string text) : base(new Rectangle(x, y, w, h)) {
            Text = text;
            TextColor = Color.Black;
        }
    }
    public class Button: Label {
        public event EventHandler ButtonClick;
        
        protected override void HandleMouseClick() {
            ButtonClick?.Invoke(this, EventArgs.Empty);
            base.HandleMouseClick();
        }
        public Button(int x, int y, int w, int h, string text) : base (x, y, w, h, text) { 

        }

        protected override void HighligtedChanged() {
            if(IsSelected)
                return;
            FillColor = IsHighlited ? Color.Red : Color.White;
            BorderColor = IsHighlited ? Color.Blue : Color.Black;
        }
        protected override void SelectedChanged() {
            FillColor = IsSelected ? Color.Green : Color.White;
            BorderColor = IsHighlited ? Color.Yellow : Color.Black;
        }
    }
}
