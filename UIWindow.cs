using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class UIWindow
    {
        //enums
        public enum UIWindowAlignmentType { Normal, Left, Right, Center }

        Boolean isForeground;
        public Rectangle uiWindowPosition;
        Rectangle xButtonPosition;
        public Rectangle publicxButtonPosition;

        //foreground rects
        Rectangle uiWindowForegroundFill = new Rectangle(40, 10, 10, 10);
        Rectangle uiWindowForegroundTopLeft = new Rectangle(30, 0, 10, 10);
        Rectangle uiWindowForegroundTopRight = new Rectangle(50, 0, 10, 10);
        Rectangle uiWindowForegroundBottomLeft = new Rectangle(30, 20, 10, 10);
        Rectangle uiWindowForegroundBottomRight = new Rectangle(50, 20, 10, 10);
        Rectangle uiWindowForegroundTopBorder = new Rectangle(40, 0, 10, 10);
        Rectangle uiWindowForegroundBottomBorder = new Rectangle(40, 20, 10, 10);
        Rectangle uiWindowForegroundLeftBorder = new Rectangle(30, 10, 10, 10);
        Rectangle uiWindowForegroundRightBorder = new Rectangle(50, 10, 10, 10);

        //background rects
        Rectangle uiWindowBackgroundFill = new Rectangle(70, 10, 10, 10);
        Rectangle uiWindowBackgroundTopLeft = new Rectangle(60, 0, 10, 10);
        Rectangle uiWindowBackgroundTopRight = new Rectangle(80, 0, 10, 10);
        Rectangle uiWindowBackgroundBottomLeft = new Rectangle(60, 20, 10, 10);
        Rectangle uiWindowBackgroundBottomRight = new Rectangle(80, 20, 10, 10);
        Rectangle uiWindowBackgroundTopBorder = new Rectangle(70, 0, 10, 10);
        Rectangle uiWindowBackgroundBottomBorder = new Rectangle(70, 20, 10, 10);
        Rectangle uiWindowBackgroundLeftBorder = new Rectangle(60, 10, 10, 10);
        Rectangle uiWindowBackgroundRightBorder = new Rectangle(80, 10, 10, 10);

        //X sprite rect
        Rectangle uiWindowxButton= new Rectangle(0, 30, 20, 20);

        //CONTENT
        String title;
        Vector2 titleposition;

        List<String> textBodies = new List<String>();
        List<Vector2> textBodiesLocations = new List<Vector2>();
        List<Color> textBodiesColors = new List<Color>();

        public UIWindow(Rectangle location)
        {
            uiWindowPosition = location;
            xButtonPosition = new Rectangle((int)(location.Width - 40), -10, 20, 20);
            isForeground = false;
        }

        public void AssignTitle(String titlevalue,SpriteFont font)
        {
            title = titlevalue;
            int length = (int)font.MeasureString(title).X;
            titleposition = new Vector2(uiWindowPosition.Width / 2 - length / 2, -10);
        }

        public void AssignTextBody(String textvalue, Vector2 textlocation, Color textcolor, UIWindowAlignmentType alignment, SpriteFont mainfont)
        {
            textBodies.Add(textvalue);
            textBodiesColors.Add(textcolor);

            if (alignment == UIWindowAlignmentType.Normal)
            {
                textBodiesLocations.Add(textlocation);
            }
            else if (alignment == UIWindowAlignmentType.Left)
            {
                Vector2 location = new Vector2(0, textlocation.Y);
                textBodiesLocations.Add(location);
            }
            else if (alignment == UIWindowAlignmentType.Right)
            {
                Vector2 location = new Vector2(uiWindowPosition.Width - mainfont.MeasureString(textvalue).X - 20, textlocation.Y);
                textBodiesLocations.Add(location);
            }
            else if (alignment == UIWindowAlignmentType.Center)
            {
                Vector2 location = new Vector2((uiWindowPosition.Width/2) - (mainfont.MeasureString(textvalue).X /2), textlocation.Y);
                textBodiesLocations.Add(location);
            }
        }


        public void switchForegroundBackground(Boolean changetoForeground)
        {
            if (changetoForeground == true)
            {
                isForeground = true;
            }
            else
            {
                isForeground = false;
            }
        }

        public Rectangle returnActualXButtonPosition(Rectangle offset, Rectangle xbutton)
        {
            publicxButtonPosition = new Rectangle(offset.X + xbutton.X, offset.Y + xbutton.Y, 40, 40);
            return publicxButtonPosition;
        }

        public Vector2 returnTextPosition(Vector2 text, Rectangle offset, int shadowvalue)
        {
            Vector2 plusshadow = new Vector2(shadowvalue, shadowvalue);


            return new Vector2(text.X + offset.X+ plusshadow.X, text.Y + offset.Y + plusshadow.Y);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D uiWindowSprite, Rectangle offsetposition,SpriteFont titlefont, SpriteFont normalfont)
        {
            //X Button
             spriteBatch.Draw(uiWindowSprite, returnActualXButtonPosition(offsetposition, xButtonPosition), uiWindowxButton, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.845f);

            //title
            spriteBatch.DrawString(titlefont, title, returnTextPosition(titleposition, offsetposition,0), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.845f);
            spriteBatch.DrawString(titlefont, title, returnTextPosition(titleposition, offsetposition, 1), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.844f);
            spriteBatch.DrawString(titlefont, title, returnTextPosition(titleposition, offsetposition, 2), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.844f);


            for (int i =0; i < textBodies.Count;++i)
            {
                spriteBatch.DrawString(normalfont, textBodies[i], returnTextPosition(textBodiesLocations[i], offsetposition, 0), textBodiesColors[i], 0, Vector2.Zero, 1, SpriteEffects.None, 0.845f);
                spriteBatch.DrawString(normalfont, textBodies[i], returnTextPosition(textBodiesLocations[i], offsetposition, 1), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.844f);
                spriteBatch.DrawString(normalfont, textBodies[i], returnTextPosition(textBodiesLocations[i], offsetposition, 2), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.844f);
            }

            if (isForeground == true)
            {
                //draw fill
                spriteBatch.Draw(uiWindowSprite, offsetposition, uiWindowForegroundFill, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.842f);

                //draw top left
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X - 20, offsetposition.Y - 20, 20, 20), uiWindowForegroundTopLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw top right
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X + uiWindowPosition.Width, offsetposition.Y - 20, 20, 20), uiWindowForegroundTopRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw bottom left
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X - 20, offsetposition.Y + uiWindowPosition.Height, 20, 20), uiWindowForegroundBottomLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw bottom right
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X + uiWindowPosition.Width, offsetposition.Y + uiWindowPosition.Height, 20, 20), uiWindowForegroundBottomRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw top border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X, offsetposition.Y - 20, uiWindowPosition.Width, 20), uiWindowForegroundTopBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw bottom border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X, offsetposition.Y + uiWindowPosition.Height, uiWindowPosition.Width, 20), uiWindowForegroundBottomBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw left border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X - 20, offsetposition.Y, 20, uiWindowPosition.Height), uiWindowForegroundLeftBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw right border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X + uiWindowPosition.Width, offsetposition.Y, 20, uiWindowPosition.Height), uiWindowForegroundRightBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);
            }
            else if (isForeground == false)
            {
                //draw fill
                spriteBatch.Draw(uiWindowSprite, offsetposition, uiWindowBackgroundFill, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.842f);

                //draw top left
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X - 20, offsetposition.Y - 20, 20, 20), uiWindowBackgroundTopLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw top right
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X + uiWindowPosition.Width, offsetposition.Y - 20, 20, 20), uiWindowBackgroundTopRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw bottom left
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X - 20, offsetposition.Y + uiWindowPosition.Height, 20, 20), uiWindowBackgroundBottomLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw bottom right
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X + uiWindowPosition.Width, offsetposition.Y + uiWindowPosition.Height, 20, 20), uiWindowBackgroundBottomRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw top border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X, offsetposition.Y - 20, uiWindowPosition.Width, 20), uiWindowBackgroundTopBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw bottom border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X, offsetposition.Y + uiWindowPosition.Height, uiWindowPosition.Width, 20), uiWindowBackgroundBottomBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw left border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X - 20, offsetposition.Y, 20, uiWindowPosition.Height), uiWindowBackgroundLeftBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);

                //draw right border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X + uiWindowPosition.Width, offsetposition.Y, 20, uiWindowPosition.Height), uiWindowBackgroundRightBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.84f);
            }
        }

    }
}
