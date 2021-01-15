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
        Vector2 direction;

        public TravelRoute(Point loc)
        {
            routeLocation = loc;
        }

        public void AssignEstimate(int value)
        {
            estimate = value;
        }
        public void AssignNextDirection(Vector2 next)
        {
            direction = next;
        }

        public Vector2 getDirection()
        {
            return direction;
        }

        public int GetTravelEstimate()
        {
            return estimate;
        }

    }
}
