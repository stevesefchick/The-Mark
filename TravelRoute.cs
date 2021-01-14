using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class TravelRoute
    {
        //display and loc
        public Point routeLocation;
        public Vector2 routeSpriteSheet;

        //estimations-tbd
        int estimate;

        public TravelRoute(Point loc)
        {
            routeLocation = loc;
        }

        public void AssignEstimate(int value)
        {
            estimate = value;
        }

    }
}
