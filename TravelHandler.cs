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


        public TravelHandler()
        {


        }

        public void createTravelCoords(Point start, Point destination, WorldMap world)
        {
            travelStartingLocation = start;
            destinationLocation = destination;
            displayDestination = true;
            createDestinationStrings(world);

        }

        void createDestinationStrings(WorldMap world)
        {
            currentLocationText = world.returnCurrentLocNameDescription(destinationLocation).Item1;
            currentLocationDescription = world.returnCurrentLocNameDescription(destinationLocation).Item2;
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
                spriteBatch.DrawString(worldfont, currentDestinationText, new Vector2(baseposition.X + 150, baseposition.Y + 100), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
                spriteBatch.DrawString(worldfont, currentDestinationText, new Vector2(baseposition.X + 151, baseposition.Y + 101), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                spriteBatch.DrawString(worldfont, currentDestinationText, new Vector2(baseposition.X + 152, baseposition.Y + 102), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                //description
                spriteBatch.DrawString(worldfont, currentDestinationDescription, new Vector2(baseposition.X + 175, baseposition.Y + 125), Color.LightGray, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
                spriteBatch.DrawString(worldfont, currentDestinationDescription, new Vector2(baseposition.X + 176, baseposition.Y + 126), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                spriteBatch.DrawString(worldfont, currentDestinationDescription, new Vector2(baseposition.X + 177, baseposition.Y + 127), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);



            }
        }
    }
}
