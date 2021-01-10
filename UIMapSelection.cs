using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class UIMapSelection
    {
        //enum
        public enum MapSelectionType { TravelLoc}

        //type
        MapSelectionType thisMapSelectionType;

        //rect
        Rectangle uiMapFillRect = new Rectangle(40, 40, 10, 10);
        Rectangle uiMapTopLeft = new Rectangle(30, 30, 10, 10);
        Rectangle uiMapTopRight = new Rectangle(50, 30, 10, 10);
        Rectangle uiMapBottomLeft = new Rectangle(30, 50, 10, 10);
        Rectangle uiMapBottomRight = new Rectangle(50, 50, 10, 10);
        Rectangle uiMapTopBorder = new Rectangle(40, 30, 10, 10);
        Rectangle uiMapBottomBorder = new Rectangle(40, 50, 10, 10);
        Rectangle uiMapLeftBorder = new Rectangle(30, 40, 10, 10);
        Rectangle uiMapRightBorder = new Rectangle(50, 40, 10, 10);

        //add text selections
        public Rectangle travelCollision;
        public Rectangle infoCollision;

        //positions
        Rectangle uiMapSelectionPosition;
        Vector2 locationOffset = new Vector2(0, -50);

        public UIMapSelection(Vector2 position, MapSelectionType thisType)
        {
            Vector2 size = Vector2.Zero;
            if (thisType == MapSelectionType.TravelLoc)
            {
                size = new Vector2(100, 100);
                travelCollision = new Rectangle((int)(position.X + locationOffset.X), (int)(position.Y + locationOffset.Y), 100, 25);
                infoCollision = new Rectangle((int)(position.X + locationOffset.X), (int)(position.Y + 25 + locationOffset.Y), 100, 25);
            }
            thisMapSelectionType = thisType;

            uiMapSelectionPosition = new Rectangle((int)(position.X + locationOffset.X), (int)(position.Y + locationOffset.Y), (int)size.X, (int)size.Y);

            

        }

        public void Update()
        {


        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D uiWindowSprite)
        {
            if (thisMapSelectionType == MapSelectionType.TravelLoc)
            {
                //text
                spriteBatch.DrawString(font, "Travel", new Vector2(uiMapSelectionPosition.X + (uiMapSelectionPosition.Height * 0.5f), uiMapSelectionPosition.Y), Color.White, 0, new Vector2(font.MeasureString("Travel").X / 2, 0), 1, SpriteEffects.None, 0.75f);
                spriteBatch.DrawString(font, "Travel", new Vector2(uiMapSelectionPosition.X + 1 + (uiMapSelectionPosition.Height * 0.5f), uiMapSelectionPosition.Y + 1), Color.Black, 0, new Vector2(font.MeasureString("Travel").X / 2, 0), 1, SpriteEffects.None, 0.745f);
                spriteBatch.DrawString(font, "Travel", new Vector2(uiMapSelectionPosition.X + 2 + (uiMapSelectionPosition.Height * 0.5f), uiMapSelectionPosition.Y + 2), Color.Black, 0, new Vector2(font.MeasureString("Travel").X / 2, 0), 1, SpriteEffects.None, 0.745f);


                spriteBatch.DrawString(font, "Info", new Vector2(uiMapSelectionPosition.X + (uiMapSelectionPosition.Height * 0.5f), uiMapSelectionPosition.Y + 25), Color.White, 0, new Vector2(font.MeasureString("Info").X / 2, 0), 1, SpriteEffects.None, 0.75f);
                spriteBatch.DrawString(font, "Info", new Vector2(uiMapSelectionPosition.X + 1 + (uiMapSelectionPosition.Height * 0.5f), uiMapSelectionPosition.Y + 26), Color.Black, 0, new Vector2(font.MeasureString("Info").X / 2, 0), 1, SpriteEffects.None, 0.745f);
                spriteBatch.DrawString(font, "Info", new Vector2(uiMapSelectionPosition.X + 2 + (uiMapSelectionPosition.Height * 0.5f), uiMapSelectionPosition.Y + 27), Color.Black, 0, new Vector2(font.MeasureString("Info").X / 2, 0), 1, SpriteEffects.None, 0.745f);
            }

            //draw fill
            spriteBatch.Draw(uiWindowSprite, uiMapSelectionPosition, uiMapFillRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.72f);
            //draw top left
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X - 20, uiMapSelectionPosition.Y - 20, 20, 20), uiMapTopLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw top right
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X + uiMapSelectionPosition.Width, uiMapSelectionPosition.Y - 20, 20, 20), uiMapTopRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw bottom left
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X - 20, uiMapSelectionPosition.Y + uiMapSelectionPosition.Height, 20, 20), uiMapBottomLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw bottom right
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X + uiMapSelectionPosition.Width, uiMapSelectionPosition.Y + uiMapSelectionPosition.Height, 20, 20), uiMapBottomRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw top border
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X, uiMapSelectionPosition.Y - 20, uiMapSelectionPosition.Width, 20), uiMapTopBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw bottom border
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X, uiMapSelectionPosition.Y + uiMapSelectionPosition.Height, uiMapSelectionPosition.Width, 20), uiMapBottomBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw left border
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X - 20, uiMapSelectionPosition.Y, 20, uiMapSelectionPosition.Height), uiMapLeftBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw right border
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X + uiMapSelectionPosition.Width, uiMapSelectionPosition.Y, 20, uiMapSelectionPosition.Height), uiMapRightBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);

        }
    }
}
