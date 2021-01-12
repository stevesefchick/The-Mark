using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class TravelHandler
    {
        //location and travel
        public Point currentGridLocation;
        public String currentLocationText;
        public String currentLocationDescription;
        public Point destinationLocation;
        public Boolean displayDestination = false;
        public String currentDestinationText;
        public String currentDestinationDescription;
        public Point travelStartingLocation;

        //paths
        public List<Point> travelPath = new List<Point>();


        public TravelHandler()
        {


        }

        public void createTravelPath(Point destination,Random rando)
        {
            Point up = new Point(0, -1);
            Point down = new Point(0, 1);
            Point right = new Point(1, 0);
            Point left = new Point(-1, 0);

            Point start = currentGridLocation;
            travelPath.Add(start);

            while (start != destination)
            {
                
                List<Point> possibleDirections = new List<Point>();
                if (destination.X > start.X)
                {
                    possibleDirections.Add(right);
                }
                if (destination.X < start.X)
                {
                    possibleDirections.Add(left);
                }
                if (destination.Y > start.Y)
                {
                    possibleDirections.Add(down);
                }
                if (destination.Y < start.Y)
                {
                    possibleDirections.Add(up);
                }

                start += possibleDirections[rando.Next(0, possibleDirections.Count)];

                travelPath.Add(start);
            }


        }

        public void TravelToDestination(Point start, Point destination, WorldMap world,Random rando)
        {
            createTravelCoords(start, destination);
            createDestinationStrings(world);

            createTravelPath(destination, rando);
        }

        void createTravelCoords(Point start, Point destination)
        {
            travelStartingLocation = start;
            destinationLocation = destination;
            displayDestination = true;


        }

        void createDestinationStrings(WorldMap world)
        {
            currentDestinationText = world.returnCurrentLocNameDescription(destinationLocation).Item1;
            currentDestinationDescription = world.returnCurrentLocNameDescription(destinationLocation).Item2;
        }


        public void DrawLocationUI(SpriteBatch spriteBatch, SpriteFont worldfont, Vector2 baseposition, Texture2D currentLocIcon, Texture2D destinationIcon)
        {
            spriteBatch.Draw(currentLocIcon, new Rectangle((int)baseposition.X - 40, (int)baseposition.Y + 30, 50, 50), new Rectangle(0, 0, 100, 100), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.81f);

            //location
            spriteBatch.DrawString(worldfont, currentLocationText, new Vector2(baseposition.X + 10, baseposition.Y + 30), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
            spriteBatch.DrawString(worldfont, currentLocationText, new Vector2(baseposition.X + 11, baseposition.Y + 31), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
            spriteBatch.DrawString(worldfont, currentLocationText, new Vector2(baseposition.X + 12, baseposition.Y + 32), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
            //description
            spriteBatch.DrawString(worldfont, currentLocationDescription, new Vector2(baseposition.X + 35, baseposition.Y + 55), Color.LightGray, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
            spriteBatch.DrawString(worldfont, currentLocationDescription, new Vector2(baseposition.X + 36, baseposition.Y + 56), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
            spriteBatch.DrawString(worldfont, currentLocationDescription, new Vector2(baseposition.X + 37, baseposition.Y + 57), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);

            if (displayDestination == true)
            {
                spriteBatch.Draw(destinationIcon, new Rectangle((int)baseposition.X, (int)baseposition.Y + 100, 50, 50), new Rectangle(0, 0, 100, 100), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.81f);

                //location
                spriteBatch.DrawString(worldfont, currentDestinationText, new Vector2(baseposition.X + 50, baseposition.Y + 100), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
                spriteBatch.DrawString(worldfont, currentDestinationText, new Vector2(baseposition.X + 51, baseposition.Y + 101), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                spriteBatch.DrawString(worldfont, currentDestinationText, new Vector2(baseposition.X + 52, baseposition.Y + 102), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                //description
                spriteBatch.DrawString(worldfont, currentDestinationDescription, new Vector2(baseposition.X + 75, baseposition.Y + 125), Color.LightGray, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
                spriteBatch.DrawString(worldfont, currentDestinationDescription, new Vector2(baseposition.X + 76, baseposition.Y + 126), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                spriteBatch.DrawString(worldfont, currentDestinationDescription, new Vector2(baseposition.X + 77, baseposition.Y + 127), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);



            }
        }
    }
}
