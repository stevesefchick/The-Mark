using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace The_Mark
{
    class UIHoverCard
    {
        public Rectangle hovercardPosition;
        String hovercardTitle;
        String hovercardText;
        List<String> hovercardTextLines = new List<String>();

        Rectangle hoverCardFillRect = new Rectangle(10, 10, 10, 10);
        Rectangle hoverCardTopLeft = new Rectangle(0, 0, 10, 10);
        Rectangle hoverCardTopRight = new Rectangle(20, 0, 10, 10);
        Rectangle hoverCardBottomLeft = new Rectangle(0, 20, 10, 10);
        Rectangle hoverCardBottomRight = new Rectangle(20, 20, 10, 10);
        Rectangle hoverCardTopBorder = new Rectangle(10, 0, 10, 10);
        Rectangle hoverCardBottomBorder = new Rectangle(10, 20, 10, 10);
        Rectangle hoverCardLeftBorder = new Rectangle(0, 10, 10, 10);
        Rectangle hoverCardRightBorder = new Rectangle(20, 10, 10, 10);

       

        public UIHoverCard(Vector2 location, String title, String body, SpriteFont littlefont,SpriteFont titlefont)
        {
            hovercardTitle = title;
            hovercardText = body;
            hovercardPosition = new Rectangle((int)location.X, (int)location.Y, getCardWidth(titlefont, title), 300);
            hovercardTextLines = getHovercardLines(hovercardText,littlefont);
            hovercardPosition.Height = getCardHeight(hovercardTextLines.Count);
        }

        int getCardWidth(SpriteFont titlefont, String title)
        {
            int buffer = 125;

            return ((int)titlefont.MeasureString(title).X + buffer);
        }

        int getCardHeight(int lines)
        {
            int buffer = 140;

            return (buffer + lines * 25);

        }

        public List<String> getHovercardLines(String hovercardtext, SpriteFont littlefont)
        {
            List<String> list = new List<String>();
            String basetext = hovercardtext;
            int maxlength = basetext.Length;
            int currentpointer = 0;
            int lastline = 0;

            while (currentpointer < maxlength)
            {
                if ((basetext.Substring(currentpointer, 1) == " " &&
                    littlefont.MeasureString(basetext.Substring(lastline,currentpointer-lastline)).X > (hovercardPosition.Width-70)) ||
                    currentpointer == (maxlength-1))
                {


                    //end
                    if (currentpointer == maxlength - 1)
                    {
                        list.Add(basetext.Substring(lastline));
                    }
                    else
                    {
                        list.Add(basetext.Substring(lastline, currentpointer - lastline));
                        lastline = currentpointer+1;
                    }
                }


                currentpointer += 1;
            }
            
            return list;
        }

        int returnTitlePosition(int offsetWidth, SpriteFont font, String thetext)
        {
            return (int)((offsetWidth / 2) - (font.MeasureString(thetext).X / 2));
        }

        void DrawText(SpriteBatch spriteBatch,SpriteFont bigfont, SpriteFont littlefont, Rectangle offsetposition)
        {
            spriteBatch.DrawString(bigfont, hovercardTitle, new Vector2(offsetposition.X + returnTitlePosition(offsetposition.Width,bigfont,hovercardTitle), offsetposition.Y + 10), Color.Black,0,Vector2.Zero,1, SpriteEffects.None,0.87f);

            //spriteBatch.DrawString(littlefont, hovercardText, new Vector2(offsetposition.X + 10, offsetposition.Y+50), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.82f);
            for (int i =0; i < hovercardTextLines.Count;++i)
            {
                spriteBatch.DrawString(littlefont, hovercardTextLines[i], new Vector2(offsetposition.X + 10, offsetposition.Y + 75 + (25*i)), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.87f);

            }

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D hoverCardSprite, Rectangle offsetposition,SpriteFont bigfont,SpriteFont littlefont)
        {
            DrawText(spriteBatch, bigfont, littlefont, offsetposition);

            //draw fill
            spriteBatch.Draw(hoverCardSprite, offsetposition, hoverCardFillRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.86f);
            
            //draw top left
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X - 20, offsetposition.Y - 20, 20, 20), hoverCardTopLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw top right
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X + hovercardPosition.Width, offsetposition.Y - 20, 20, 20), hoverCardTopRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw bottom left
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X - 20, offsetposition.Y + hovercardPosition.Height, 20, 20), hoverCardBottomLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw bottom right
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X + hovercardPosition.Width, offsetposition.Y + hovercardPosition.Height, 20, 20), hoverCardBottomRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw top border
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X, offsetposition.Y - 20, hovercardPosition.Width, 20), hoverCardTopBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw bottom border
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X, offsetposition.Y + hovercardPosition.Height, hovercardPosition.Width, 20), hoverCardBottomBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw left border
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X - 20, offsetposition.Y, 20, hovercardPosition.Height), hoverCardLeftBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw right border
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X + hovercardPosition.Width, offsetposition.Y, 20, hovercardPosition.Height), hoverCardRightBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);
            

        }

    }
}
