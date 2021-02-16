using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class UIEventWindow
    {
        //enum
        public enum UIEventWindowType { WorldEvent }

        //properties
        UIEventWindowType thisEventWindowType;
        public Rectangle uiEventWindowPosition;
        String titleText;
        Vector2 titleTextOffset;
        List<String> descriptionTextLines = new List<String>();
        List<EventOption> eventOptions = new List<EventOption>();
        //collisions
        public Dictionary<Rectangle, EventOption> eventOptionCollisions = new Dictionary<Rectangle, EventOption>();
        

        //rects
        Rectangle uiWindowForegroundFill = new Rectangle(40, 10, 10, 10);
        Rectangle uiWindowForegroundTopLeft = new Rectangle(30, 0, 10, 10);
        Rectangle uiWindowForegroundTopRight = new Rectangle(50, 0, 10, 10);
        Rectangle uiWindowForegroundBottomLeft = new Rectangle(30, 20, 10, 10);
        Rectangle uiWindowForegroundBottomRight = new Rectangle(50, 20, 10, 10);
        Rectangle uiWindowForegroundTopBorder = new Rectangle(40, 0, 10, 10);
        Rectangle uiWindowForegroundBottomBorder = new Rectangle(40, 20, 10, 10);
        Rectangle uiWindowForegroundLeftBorder = new Rectangle(30, 10, 10, 10);
        Rectangle uiWindowForegroundRightBorder = new Rectangle(50, 10, 10, 10);

        //const
        const int maxLengthSize = 250;

        public UIEventWindow(Vector2 position, UIEventWindowType windowtype, WorldEvent worldevent,SpriteFont font)
        {
            uiEventWindowPosition = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            if (windowtype == UIEventWindowType.WorldEvent)
            {
                titleText = worldevent.ReturnEventTitleText();
                eventOptions = worldevent.GetEventOptions();
                uiEventWindowPosition.Width= getCardWidth(font);
                descriptionTextLines = getEventWindowDescriptionLines(worldevent.ReturnEventDescriptionText(), font);
                uiEventWindowPosition.Height = GetCardHeight(descriptionTextLines.Count, worldevent.GetCountofEventOptions());
                PopulateEventOptionCollisions();
            }
            thisEventWindowType = windowtype;


        }

        void PopulateEventOptionCollisions()
        {
            int starting = 50 + descriptionTextLines.Count * 25;

            for (int i =0; i < eventOptions.Count;++i)
            {
                eventOptionCollisions.Add(new Rectangle(uiEventWindowPosition.X, uiEventWindowPosition.Y + starting + (i * 25), uiEventWindowPosition.Width, 25), eventOptions[i]);
            }

        }

        List<String> getEventWindowDescriptionLines(String eventdescription, SpriteFont font)
        {
            List<String> list = new List<String>();
            String basetext = eventdescription;
            int maxlength = eventdescription.Length;
            int currentpointer = 0;
            int lastline = 0;

            while (currentpointer < maxlength)
            {
                if ((basetext.Substring(currentpointer, 1) == " " &&
                    font.MeasureString(basetext.Substring(lastline, currentpointer - lastline)).X > (uiEventWindowPosition.Width - 50)) ||
                    currentpointer == (maxlength - 1))
                {


                    //end
                    if (currentpointer == maxlength - 1)
                    {
                        list.Add(basetext.Substring(lastline));
                    }
                    else
                    {
                        list.Add(basetext.Substring(lastline, currentpointer - lastline));
                        lastline = currentpointer + 1;
                    }
                }


                currentpointer += 1;
            }

            return list;
        }

        int GetCardHeight(int countofdesclines, int countofoptions)
        {
            return ((countofdesclines * 25) + (countofoptions * 25) + 50);

        }
        int getCardWidth(SpriteFont font)
        {
            int buffer = 125;

            if ((int)font.MeasureString(titleText).X < 100)
            {
                buffer += 75;
            }

            int length = ((int)font.MeasureString(titleText).X + buffer);
            if (length > maxLengthSize)
            {
                length = maxLengthSize;
            }

            titleTextOffset = new Vector2(length / 2, 0);

            return length;
        }


        public void Draw(SpriteBatch spriteBatch, Texture2D uiSprite, SpriteFont font, Rectangle offset)
        {
            //draw title
            spriteBatch.DrawString(font, titleText, new Vector2(offset.X + (uiEventWindowPosition.Width / 2), offset.Y), Color.White,0,titleTextOffset,1, SpriteEffects.None,0.87f);
            //draw description
            for (int i=0; i<descriptionTextLines.Count;++i)
            {
                spriteBatch.DrawString(font, descriptionTextLines[i], new Vector2(offset.X, offset.Y + 25 +  (i * 25)), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.87f);
            }
            //draw options
            int starting = 50 + descriptionTextLines.Count * 25;
            // for (int i = 0; i < eventOptionTextLines.Count; ++i)
            for (int i = 0; i < eventOptions.Count; ++i)
            {
                Color optionColor = Color.White;
                if (eventOptions[i].IsAvailable() == false)
                {
                    optionColor = Color.Gray;
                }
                    spriteBatch.DrawString(font, eventOptions[i].ReturnOptionName(), new Vector2(offset.X, offset.Y + starting + (i * 25)), optionColor, 0, Vector2.Zero, 1, SpriteEffects.None, 0.87f);
                
            }


            //draw fill
            spriteBatch.Draw(uiSprite, offset, uiWindowForegroundFill, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.86f);

            //draw top left
            spriteBatch.Draw(uiSprite, new Rectangle(offset.X - 20, offset.Y - 20, 20, 20), uiWindowForegroundTopLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw top right
            spriteBatch.Draw(uiSprite, new Rectangle(offset.X + uiEventWindowPosition.Width, offset.Y - 20, 20, 20), uiWindowForegroundTopRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw bottom left
            spriteBatch.Draw(uiSprite, new Rectangle(offset.X - 20, offset.Y + uiEventWindowPosition.Height, 20, 20), uiWindowForegroundBottomLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw bottom right
            spriteBatch.Draw(uiSprite, new Rectangle(offset.X + uiEventWindowPosition.Width, offset.Y + uiEventWindowPosition.Height, 20, 20), uiWindowForegroundBottomRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw top border
            spriteBatch.Draw(uiSprite, new Rectangle(offset.X, offset.Y - 20, uiEventWindowPosition.Width, 20), uiWindowForegroundTopBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw bottom border
            spriteBatch.Draw(uiSprite, new Rectangle(offset.X, offset.Y + uiEventWindowPosition.Height, uiEventWindowPosition.Width, 20), uiWindowForegroundBottomBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw left border
            spriteBatch.Draw(uiSprite, new Rectangle(offset.X - 20, offset.Y, 20, uiEventWindowPosition.Height), uiWindowForegroundLeftBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);

            //draw right border
            spriteBatch.Draw(uiSprite, new Rectangle(offset.X + offset.Width, offset.Y, 20, uiEventWindowPosition.Height), uiWindowForegroundRightBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.85f);



        }

    }
}
