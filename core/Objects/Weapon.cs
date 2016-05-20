﻿
using System;

namespace GameCore {
    public abstract class WeaponBase: GameObject {
        protected virtual CoordPoint Origin { get; set; }
        protected Ship Owner { get; set; }
        protected virtual Size Size { get; set; }

        protected internal override Bounds Bounds {
            get {
                return new Bounds(Owner.Location - Origin, Owner.Location - Origin + new CoordPoint(Size.Width, Size.Height));
            }
        }
        protected internal override string ContentString { get { return "weapon"; } }
        protected internal override float Rotation { get { return rotation + Owner.Rotation; } }

        internal override bool IsMinimapVisible { get { return false; } }

        public WeaponBase(Ship owner) : base(owner.CurrentSystem) {
            Owner = owner;
            Size = new Size(10, 20);
            Origin = new CoordPoint(0, 0);
        }

        protected internal override void Step() { }
        // symmetry y/n
        // rotate y/n
        // firemode single/auto
        // load number/inf
        float rotation = 0f;
    }
    public class DefaultCannon: WeaponBase {
        public DefaultCannon(Ship owner) : base(owner) {
        }
    }
}