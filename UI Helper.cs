using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class UI_Helper
    {
        //foreground window sprites

        //background window sprites

        //hover card sprites
        Texture2D hoverCardSprite;
        List<Rectangle> hoverCardLocations = new List<Rectangle>();
        Rectangle hoverCardFillRect = new Rectangle(10, 10, 10, 10);
        Rectangle hoverCardTopLeft = new Rectangle(0, 0, 10, 10);
        Rectangle hoverCardTopRight = new Rectangle(20, 0, 10, 10);
        Rectangle hoverCardBottomLeft = new Rectangle(0, 20, 10, 10);
        Rectangle hoverCardBottomRight = new Rectangle(20, 20, 10, 10);
        Rectangle hoverCardTopBorder = new Rectangle(10, 0, 10, 10);
        Rectangle hoverCardBottomBorder = new Rectangle(10, 20, 10, 10);
        Rectangle hoverCardLeftBorder = new Rectangle(0, 10, 10, 10);
        Rectangle hoverCardRightBorder = new Rectangle(20, 10, 10, 10);

        public UI_Helper(GameMain thegame)
        {
            LoadAllTextures(thegame);
            hoverCardLocations.Add(new Rectangle(200, 200, 300, 400));
        }

        void LoadAllTextures(GameMain thegame)
        {
            hoverCardSprite = thegame.Content.Load<Texture2D>("Sprites/UI/uiWindow");

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i =0; i < hoverCardLocations.Count;++i)
            {
                //draw fill
                spriteBatch.Draw(hoverCardSprite, hoverCardLocations[i], hoverCardFillRect, Color.White);

                //draw top left
                spriteBatch.Draw(hoverCardSprite, new Rectangle(hoverCardLocations[i].X-10, hoverCardLocations[i].Y-10,10,10), hoverCardTopLeft, Color.White);

                //draw top right
                spriteBatch.Draw(hoverCardSprite, new Rectangle(hoverCardLocations[i].X + hoverCardLocations[i].Width, hoverCardLocations[i].Y - 10, 10, 10), hoverCardTopRight, Color.White);

                //draw bottom left
                spriteBatch.Draw(hoverCardSprite, new Rectangle(hoverCardLocations[i].X - 10, hoverCardLocations[i].Y + hoverCardLocations[i].Height, 10, 10), hoverCardBottomLeft, Color.White);

                //draw bottom right
                spriteBatch.Draw(hoverCardSprite, new Rectangle(hoverCardLocations[i].X + hoverCardLocations[i].Width, hoverCardLocations[i].Y + hoverCardLocations[i].Height, 10, 10), hoverCardBottomRight, Color.White);

                //draw top border
                spriteBatch.Draw(hoverCardSprite, new Rectangle(hoverCardLocations[i].X, hoverCardLocations[i].Y - 10, hoverCardLocations[i].Width, 10), hoverCardTopBorder, Color.White);

                //draw bottom border
                spriteBatch.Draw(hoverCardSprite, new Rectangle(hoverCardLocations[i].X, hoverCardLocations[i].Y + hoverCardLocations[i].Height, hoverCardLocations[i].Width, 10), hoverCardBottomBorder, Color.White);

                //draw left border
                spriteBatch.Draw(hoverCardSprite, new Rectangle(hoverCardLocations[i].X - 10, hoverCardLocations[i].Y, 10, hoverCardLocations[i].Height), hoverCardLeftBorder, Color.White);

                //draw right border
                spriteBatch.Draw(hoverCardSprite, new Rectangle(hoverCardLocations[i].X + hoverCardLocations[i].Width, hoverCardLocations[i].Y, 10, hoverCardLocations[i].Height), hoverCardRightBorder, Color.White);

            }

        }

    }
}
