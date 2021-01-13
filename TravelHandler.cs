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
        public List<TravelRoute> travelPath = new List<TravelRoute>();


        public TravelHandler()
        {


        }

        public void clearTravelPath()
        {
            travelPath.Clear();
        }

        public void createTravelPath(Point destination,Random rando)
        {
            Point up = new Point(0, -1);
            Point down = new Point(0, 1);
            Point right = new Point(1, 0);
            Point left = new Point(-1, 0);

            Point start = currentGridLocation;
            travelPath.Add(new TravelRoute(start));

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

                travelPath.Add(new TravelRoute(start));
            }

            determineTravelRouteSpriteMap();

        }

        void determineTravelRouteSpriteMap()
        {
            for (int i =0;i < travelPath.Count;++i)
            {
                if (i==0)
                {
                    if (travelPath[i+1].routeLocation.X < travelPath[i].routeLocation.X)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(1, 0);
                    }
                    else if (travelPath[i + 1].routeLocation.X > travelPath[i].routeLocation.X)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(0, 0);
                    }
                    else if (travelPath[i + 1].routeLocation.Y < travelPath[i].routeLocation.Y)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(1, 1);
                    }
                    else if (travelPath[i + 1].routeLocation.Y > travelPath[i].routeLocation.Y)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(0, 1);
                    }
                }
                else if (i==travelPath.Count-1)
                {
                    if (travelPath[i - 1].routeLocation.X < travelPath[i].routeLocation.X)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(1, 0);
                    }
                    else if (travelPath[i - 1].routeLocation.X > travelPath[i].routeLocation.X)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(0, 0);
                    }
                    else if (travelPath[i - 1].routeLocation.Y < travelPath[i].routeLocation.Y)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(1, 1);
                    }
                    else if (travelPath[i - 1].routeLocation.Y > travelPath[i].routeLocation.Y)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(0, 1);
                    }
                }
                else
                {
                    if ((travelPath[i - 1].routeLocation.X < travelPath[i].routeLocation.X &&
    travelPath[i + 1].routeLocation.X > travelPath[i].routeLocation.X) ||
    (travelPath[i - 1].routeLocation.X > travelPath[i].routeLocation.X &&
    travelPath[i + 1].routeLocation.X < travelPath[i].routeLocation.X))

                    {
                        travelPath[i].routeSpriteSheet = new Vector2(4, 0);
                    }
                    else if ((travelPath[i - 1].routeLocation.Y < travelPath[i].routeLocation.Y &&
travelPath[i + 1].routeLocation.Y > travelPath[i].routeLocation.Y) ||
(travelPath[i - 1].routeLocation.Y > travelPath[i].routeLocation.Y &&
travelPath[i + 1].routeLocation.Y < travelPath[i].routeLocation.Y))

                    {
                        travelPath[i].routeSpriteSheet = new Vector2(4, 1);
                    }

                    //TODO 2,0
                    //TODO 2,1
                    //TODO 3,0
                    //TODO 3,1
                }

            }

        }

        public void TravelToDestination(Point start, Point destination, WorldMap world,Random rando)
        {
            createTravelCoords(start, destination);
            createDestinationStrings(world);
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

        public void DrawPathOnMap(SpriteBatch spriteBatch, Texture2D pathTexture)
        {
            for (int i =0; i < travelPath.Count;++i)
            {
                spriteBatch.Draw(pathTexture, new Rectangle(travelPath[i].routeLocation.X * 64, travelPath[i].routeLocation.Y * 64, 64, 64), new Rectangle((int)(travelPath[i].routeSpriteSheet.X*64), (int)(travelPath[i].routeSpriteSheet.Y * 64), 64, 64), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.71f);

            }

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
