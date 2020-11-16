using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class UI_Helper
    {
        //enums
        public enum UIWindowCreationTypes { CharacterStatWindow }


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
            uiCharacterBubbles.Add(new UICharacterBubble(new Vector2(returnLocationBasedonBackBuffer(0.05f,true,0,0), backbuffersize.Y - 100 -  returnLocationBasedonBackBuffer(0.15f,false,0,0)),true));
            //party members
            for (int i =0; i < 5;++i)
            {
                uiCharacterBubbles.Add(new UICharacterBubble(new Vector2(returnLocationBasedonBackBuffer(0.05f, true,0,0) + 100 +  (i * 100), backbuffersize.Y - 100 - returnLocationBasedonBackBuffer(0.15f, false,0,0)),false));

            }

            //createHoverCard(new Vector2(200,200), "Horse Crimes", "you can dance if you want to you can leave your friends behind but if your friends don't dance and if they don't dance well they ain't friends of mine you can dance if you want to you can leave your friends behind but if your friends don't dance and if they don't dance well they ain't friends of mine you can dance if you want to you can leave your friends behind but if your friends don't dance and if they don't dance well they ain't friends of mine");
            //createUIWindow(new Rectangle(600, 200, 200, 300));
        }

        public void assignMarkToPartyWindow(String id)
        {
            uiCharacterBubbles[0].associatedCharacter = id;
        }

        int returnLocationBasedonBackBuffer(float percentage,Boolean isX, int XOffset, int YOffset)
        {
            if (isX==true)
            {
                return (int)((percentage * backbuffersize.X) + XOffset);
            }
            else
            {
                return (int)((percentage * backbuffersize.Y) + YOffset);
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

        public void createUIWindow(UIWindowCreationTypes creationType, Boolean isMark,int associatedChar)
        {
            if (creationType == UIWindowCreationTypes.CharacterStatWindow)
            {
                UIWindow newwindow = new UIWindow(new Rectangle(returnLocationBasedonBackBuffer(0.5f,true,-250,0), returnLocationBasedonBackBuffer(0.25f,false,0,-100), 500, 400));
                newwindow.AssignTitle("Character Screen", titleFont);
                uiWindows.Add(newwindow);
            }
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

        public void Update(MouseHandler mouse, PlayerHandler party,Vector2 offset)
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
                    if (mouse.leftMouseClickPosition.Intersects(getUIPosition(uiCharacterBubbles[i].bubblePosition, offset)) == true &&
                        uiWindows.Count==0)
                    {
                        if (i == 0)
                        {
                            createUIWindow(UIWindowCreationTypes.CharacterStatWindow, true, 0);
                        }
                        else
                        {
                            createUIWindow(UIWindowCreationTypes.CharacterStatWindow, false, i);
                        }
                    }

                    if (i==0)
                    {
                        uiCharacterBubbles[i].HealthScale = party.returnHealthValuesForMark().Item3;
                        uiCharacterBubbles[i].StaminaScale = party.returnStaminaValuesForMark().Item3;
                        uiCharacterBubbles[i].StressScale = party.returnStressValuesForMark().Item3;
                    }
                    else if (uiCharacterBubbles[i].isActive==true)
                    {
                        uiCharacterBubbles[i].HealthScale = party.returnHealthValuesForPartyMember(i).Item3;
                        uiCharacterBubbles[i].StaminaScale = party.returnStaminaValuesForPartyMember(i).Item3;
                        uiCharacterBubbles[i].StressScale = party.returnStressValuesForPartyMember(i).Item3;
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
                uiWindows[i].Draw(spriteBatch, uiWindowSprites, getUIPosition(uiWindows[i].uiWindowPosition, offset),titleFont);

            }
            //character bubbles
            for (int i = 0; i < uiCharacterBubbles.Count;++i)
            {
                uiCharacterBubbles[i].Draw(spriteBatch, uiWindowSprites, healtbarSprites, getUIPosition(uiCharacterBubbles[i].bubblePosition, offset));
            }

        }

    }
}
