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


        public UIEventWindow(Vector2 position, UIEventWindowType windowtype)
        {
            uiEventWindowPosition = new Rectangle((int)position.X, (int)position.Y, 100, 100);
            if (windowtype == UIEventWindowType.WorldEvent)
            {
                //determine position offset
                //scale window based on options
                //populate options
            }
            thisEventWindowType = windowtype;


        }

        public void Draw(SpriteBatch spriteBatch, Texture2D uiSprite, SpriteFont font, Rectangle offset)
        {
            //draw sprites


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
