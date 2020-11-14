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

        Vector2 backbuffersize;

        //Sprites
        Texture2D uiWindowSprites;
        Texture2D healtbarSprites;
        //hover cards
        List<UIHoverCard> hoverCards = new List<UIHoverCard>();
        //ui windows
        List<UIWindow> uiWindows = new List<UIWindow>();
        //character bubbles
        List<UICharacterBubble> uiCharacterBubbles = new List<UICharacterBubble>();

        public UI_Helper(GameMain thegame,SpriteFont bigfont, SpriteFont babyfont)
        {
            titleFont = bigfont;
            mainFont = babyfont;
            backbuffersize = thegame.backbufferJamz;

            LoadAllTextures(thegame);
            //the mark
            uiCharacterBubbles.Add(new UICharacterBubble(new Vector2(returnLocationBasedonBackBuffer(0.05f,true), backbuffersize.Y - 100 -  returnLocationBasedonBackBuffer(0.15f,false)),true));
            //party members
            for (int i =0; i < 5;++i)
            {
                uiCharacterBubbles.Add(new UICharacterBubble(new Vector2(returnLocationBasedonBackBuffer(0.05f, true) + 100 +  (i * 100), backbuffersize.Y - 100 - returnLocationBasedonBackBuffer(0.15f, false)),false));

            }

            //createHoverCard(new Vector2(200,200), "Horse Crimes", "you can dance if you want to you can leave your friends behind but if your friends don't dance and if they don't dance well they ain't friends of mine you can dance if you want to you can leave your friends behind but if your friends don't dance and if they don't dance well they ain't friends of mine you can dance if you want to you can leave your friends behind but if your friends don't dance and if they don't dance well they ain't friends of mine");
            //createUIWindow(new Rectangle(600, 200, 200, 300));
        }

        public void assignMarkToPartyWindow(String id)
        {
            uiCharacterBubbles[0].associatedCharacter = id;
        }

        int returnLocationBasedonBackBuffer(float percentage,Boolean isX)
        {
            if (isX==true)
            {
                return (int)(percentage * backbuffersize.X);
            }
            else
            {
                return (int)(percentage * backbuffersize.Y);
            }
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
            uiWindowSprites = thegame.Content.Load<Texture2D>("Sprites/UI/uiWindow");
            healtbarSprites = thegame.Content.Load<Texture2D>("Sprites/UI/uiHealthBars");

        }

        public Rectangle getUIPosition(Rectangle position, Vector2 offsetposition)
        {
            Rectangle posish = new Rectangle((int)(position.X + offsetposition.X), (int)(position.Y + offsetposition.Y), position.Width, position.Height);

            return posish;
        }

        public void Update(MouseHandler mouse)
        {
            if (mouse.isLeftClickDown == true)
            {
                hoverCards.Clear();

                for (int i = 0; i < uiWindows.Count;++i)
                {
                    if (mouse.leftMouseClickPosition.Intersects(uiWindows[i].publicxButtonPosition) == true)
                    {
                        uiWindows.RemoveAt(i);
                    }
                }

                for (int i = 0; i < uiCharacterBubbles.Count;++i)
                {
                    if (mouse.leftMouseClickPosition.Intersects(uiCharacterBubbles[i].bubblePosition) == true)
                    {
                        //do a thing here
                    }
                }

            }




        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            //Hovercards
            for (int i =0; i < hoverCards.Count;++i)
            {
                hoverCards[i].Draw(spriteBatch, uiWindowSprites, getUIPosition(hoverCards[i].hovercardPosition, offset),titleFont,mainFont);

            }
            //UI Windows
            for (int i = 0; i < uiWindows.Count;++i)
            {
                uiWindows[i].Draw(spriteBatch, uiWindowSprites, getUIPosition(uiWindows[i].uiWindowPosition, offset));

            }
            //character bubbles
            for (int i = 0; i < uiCharacterBubbles.Count;++i)
            {
                uiCharacterBubbles[i].Draw(spriteBatch, uiWindowSprites, healtbarSprites, getUIPosition(uiCharacterBubbles[i].bubblePosition, offset));
            }

        }

    }
}
