﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class UIWindow
    {
        Boolean isForeground;
        public Rectangle uiWindowPosition;
        public Rectangle xButtonPosition;

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


        public UIWindow(Rectangle location)
        {
            uiWindowPosition = location;
            xButtonPosition = new Rectangle((int)(location.Width - 40), 0, 20, 20);
            isForeground = false;
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

        Rectangle returnActualXButtonPosition(Rectangle offset, Rectangle xbutton)
        {
            return new Rectangle(offset.X + xbutton.X, offset.Y + xbutton.Y, 40, 40);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D uiWindowSprite, Rectangle offsetposition)
        {
            //X Button
             spriteBatch.Draw(uiWindowSprite, returnActualXButtonPosition(offsetposition, xButtonPosition), uiWindowxButton, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.815f);


            if (isForeground == true)
            {
                //draw fill
                spriteBatch.Draw(uiWindowSprite, offsetposition, uiWindowForegroundFill, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.81f);

                //draw top left
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X - 10, offsetposition.Y - 10, 10, 10), uiWindowForegroundTopLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw top right
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X + uiWindowPosition.Width, offsetposition.Y - 10, 10, 10), uiWindowForegroundTopRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw bottom left
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X - 10, offsetposition.Y + uiWindowPosition.Height, 10, 10), uiWindowForegroundBottomLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw bottom right
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X + uiWindowPosition.Width, offsetposition.Y + uiWindowPosition.Height, 10, 10), uiWindowForegroundBottomRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw top border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X, offsetposition.Y - 10, uiWindowPosition.Width, 10), uiWindowForegroundTopBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw bottom border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X, offsetposition.Y + uiWindowPosition.Height, uiWindowPosition.Width, 10), uiWindowForegroundBottomBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw left border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X - 10, offsetposition.Y, 10, uiWindowPosition.Height), uiWindowForegroundLeftBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw right border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X + uiWindowPosition.Width, offsetposition.Y, 10, uiWindowPosition.Height), uiWindowForegroundRightBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
            }
            else if (isForeground == false)
            {
                //draw fill
                spriteBatch.Draw(uiWindowSprite, offsetposition, uiWindowBackgroundFill, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.81f);

                //draw top left
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X - 10, offsetposition.Y - 10, 10, 10), uiWindowBackgroundTopLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw top right
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X + uiWindowPosition.Width, offsetposition.Y - 10, 10, 10), uiWindowBackgroundTopRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw bottom left
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X - 10, offsetposition.Y + uiWindowPosition.Height, 10, 10), uiWindowBackgroundBottomLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw bottom right
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X + uiWindowPosition.Width, offsetposition.Y + uiWindowPosition.Height, 10, 10), uiWindowBackgroundBottomRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw top border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X, offsetposition.Y - 10, uiWindowPosition.Width, 10), uiWindowBackgroundTopBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw bottom border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X, offsetposition.Y + uiWindowPosition.Height, uiWindowPosition.Width, 10), uiWindowBackgroundBottomBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw left border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X - 10, offsetposition.Y, 10, uiWindowPosition.Height), uiWindowBackgroundLeftBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

                //draw right border
                spriteBatch.Draw(uiWindowSprite, new Rectangle(offsetposition.X + uiWindowPosition.Width, offsetposition.Y, 10, uiWindowPosition.Height), uiWindowBackgroundRightBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
            }
        }

    }
}
