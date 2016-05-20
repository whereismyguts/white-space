﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace GameCore {
    public class StarSystem {
        Body star;

        public CoordPoint MapLocation { get; }
        public List<Body> Objects {
            get {
                List<Body> objs = new List<Body>();
                objs.AddRange(planets);
                objs.AddRange(stations);
                objs.Add(star);
                return objs;
            }
        }
        public Body Star { get { return star; } }

        public StarSystem(int planetsNumber) {
            planets = new List<Body>();
            //TODO Data Driven Factory
            star = new Body(new CoordPoint(0, 0), 55000, "planet1", this);
            planets.Add(new Planet(new CoordPoint(96000, 96000), 150, RndService.GetPeriod(), "planet2", true, this));
            planets.Add(new Planet(new CoordPoint(81000, 81000), 100, RndService.GetPeriod(), "planet3", false, this));
            planets.Add(new Planet(new CoordPoint(100000, 100000), 200, RndService.GetPeriod(), "planet4", true, this));

        }
        List<Body> planets = new List<Body>();
        List<Body> stations = new List<Body>();
    }
}

