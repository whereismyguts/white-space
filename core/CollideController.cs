﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameCore {/*
    internal class CollideController {


        internal static void Step(List<Ship> ships, IEnumerable<GameObject> spaceBodies) {



            var objects = MainCore.Instance.Objects;

            foreach(GameObject o1 in objects)
                if(!o1.TemporaryNoclip)
                    foreach(GameObject o2 in objects)
                        if(o1 != o2)
                            if((o1 is ProjectileBase || o1 is Ship) && (o2 is SpaceBody || o2 is Ship)) {

                                Ship s = o2 as Ship;
                                ProjectileBase b = o1 as ProjectileBase;
                                if(s != null && b != null) {
                                    if(b.Owner == s)
                                        continue;
                                    if(Vector2.Distance(s.Hull.Location, b.Location) <= s.Hull.Size.X / 2) {
                                        if(b.Impact())
                                            s.GetDamage(b.Damage, b.Owner == null ? "nothing" : b.Owner.Name);

                                    }
                                }
                                else

                                if(o2.ObjectBounds.Intersect(o1.ObjectBounds)) {

                                    var alfa = (o1.Location - o2.Location).AngleTo(o1.Velosity);
                                    o1.Velosity = o1.Velosity.GetRotated(alfa) * 0.5f;
                                    o1.TemporaryNoclip = true;
                                }
                            }
            //         foreach(Body obj in CurrentSystem.Objects)
            //            if(CoordPoint.Distance(obj.Position, Position) <= obj.Radius) {
            //                Dead("impact with '" + obj.Name + "'");
            //}


            //        foreach(Ship ship in MainCore.Instance.Ships.Where(s => s != owner)) {
            //                if(CoordPoint.Distance(ship.Position, Position) <= ship.ObjectBounds.Width / 2) {
            //                    CurrentSystem.Add(new Explosion(CurrentSystem, Position));
            //                    ship.GetDamage(1, owner);
            //                    ToRemove = true;
            //                    return;
            //                }
            //}
            //}
        }
    }
    */
}