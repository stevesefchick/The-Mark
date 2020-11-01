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

        public Rectangle getUIPosition(Rectangle position, Vector2 offsetposition)
        {
            Rectangle posish = new Rectangle((int)(position.X + offsetposition.X), (int)(position.Y + offsetposition.Y), position.Width, position.Height);

            return posish;
        }

        public void Update(Boolean isLeftClick)
        {
            if (isLeftClick==true)
            {
                hoverCardLocations.Clear();
            }


        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            for (int i =0; i < hoverCardLocations.Count;++i)
            {
                //draw fill
                spriteBatch.Draw(hoverCardSprite, getUIPosition(hoverCardLocations[i],offset), hoverCardFillRect, Color.White,0,Vector2.Zero, SpriteEffects.None,0.81f);

                //draw top left
                spriteBatch.Draw(hoverCardSprite, getUIPosition(new Rectangle(hoverCardLocations[i].X-10, hoverCardLocations[i].Y-10,10,10),offset), hoverCardTopLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw top right
                spriteBatch.Draw(hoverCardSprite, getUIPosition(new Rectangle(hoverCardLocations[i].X + hoverCardLocations[i].Width, hoverCardLocations[i].Y - 10, 10, 10),offset), hoverCardTopRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw bottom left
                spriteBatch.Draw(hoverCardSprite, getUIPosition(new Rectangle(hoverCardLocations[i].X - 10, hoverCardLocations[i].Y + hoverCardLocations[i].Height, 10, 10),offset), hoverCardBottomLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw bottom right
                spriteBatch.Draw(hoverCardSprite, getUIPosition(new Rectangle(hoverCardLocations[i].X + hoverCardLocations[i].Width, hoverCardLocations[i].Y + hoverCardLocations[i].Height, 10, 10),offset), hoverCardBottomRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw top border
                spriteBatch.Draw(hoverCardSprite, getUIPosition(new Rectangle(hoverCardLocations[i].X, hoverCardLocations[i].Y - 10, hoverCardLocations[i].Width, 10),offset), hoverCardTopBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw bottom border
                spriteBatch.Draw(hoverCardSprite, getUIPosition(new Rectangle(hoverCardLocations[i].X, hoverCardLocations[i].Y + hoverCardLocations[i].Height, hoverCardLocations[i].Width, 10),offset), hoverCardBottomBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw left border
                spriteBatch.Draw(hoverCardSprite, getUIPosition(new Rectangle(hoverCardLocations[i].X - 10, hoverCardLocations[i].Y, 10, hoverCardLocations[i].Height),offset), hoverCardLeftBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw right border
                spriteBatch.Draw(hoverCardSprite, getUIPosition(new Rectangle(hoverCardLocations[i].X + hoverCardLocations[i].Width, hoverCardLocations[i].Y, 10, hoverCardLocations[i].Height),offset), hoverCardRightBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

            }

        }

    }
}
