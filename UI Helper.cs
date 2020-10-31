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

                //draw top border

                //draw top right

                //draw bottom left

                //draw bottom right

                //draw left border

                //draw right border
            }

        }

    }
}
