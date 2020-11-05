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

        //Sprites
        Texture2D hoverCardSprite;
        //hover cards
        List<UIHoverCard> hoverCards = new List<UIHoverCard>();
        //ui windows
        List<UIWindow> uiWindows = new List<UIWindow>();

        public UI_Helper(GameMain thegame,SpriteFont bigfont, SpriteFont babyfont)
        {
            titleFont = bigfont;
            mainFont = babyfont;
            LoadAllTextures(thegame);
            createHoverCard(new Vector2(200,200), "Horse Crimes", "you can dance if you want to you can leave your friends behind but if your friends don't dance and if they don't dance well they ain't friends of mine you can dance if you want to you can leave your friends behind but if your friends don't dance and if they don't dance well they ain't friends of mine you can dance if you want to you can leave your friends behind but if your friends don't dance and if they don't dance well they ain't friends of mine");
            createUIWindow(new Rectangle(600, 200, 200, 300));
        }

        public void createHoverCard(Vector2 position, String title, String body)
        {
            hoverCards.Add(new UIHoverCard(position, title, body,mainFont,titleFont));
        }

        public void createUIWindow(Rectangle position)
        {
            uiWindows.Add(new UIWindow(position));
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
            //Hovercards
            for (int i =0; i < hoverCards.Count;++i)
            {
                hoverCards[i].Draw(spriteBatch, hoverCardSprite, getUIPosition(hoverCards[i].hovercardPosition, offset),titleFont,mainFont);

            }
            //UI Windows
            for (int i = 0; i < uiWindows.Count;++i)
            {
                uiWindows[i].Draw(spriteBatch, hoverCardSprite, getUIPosition(uiWindows[i].uiWindowPosition, offset));

            }

        }

    }
}
