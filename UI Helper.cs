using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class UI_Helper
    {
        //fonts
        SpriteFont titleFont;
        SpriteFont mainFont;


        //foreground window sprites

        //background window sprites

        //hover card sprites
        Texture2D hoverCardSprite;
        List<UIHoverCard> hoverCards = new List<UIHoverCard>();

        public UI_Helper(GameMain thegame,SpriteFont bigfont, SpriteFont babyfont)
        {
            titleFont = bigfont;
            mainFont = babyfont;
            LoadAllTextures(thegame);
            createHoverCard(new Rectangle(200, 200, 300, 400), "title", "body");
        }

        public void createHoverCard(Rectangle position, String title, String body)
        {
            hoverCards.Add(new UIHoverCard(position, title, body));
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
                hoverCards.Clear();
            }


        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            for (int i =0; i < hoverCards.Count;++i)
            {
                hoverCards[i].Draw(spriteBatch, hoverCardSprite, getUIPosition(hoverCards[i].hovercardPosition, offset),titleFont,mainFont);

            }

        }

    }
}
