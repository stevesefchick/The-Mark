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

        Rectangle hoverCardFillRect = new Rectangle(10, 10, 10, 10);
        Rectangle hoverCardTopLeft = new Rectangle(0, 0, 10, 10);
        Rectangle hoverCardTopRight = new Rectangle(20, 0, 10, 10);
        Rectangle hoverCardBottomLeft = new Rectangle(0, 20, 10, 10);
        Rectangle hoverCardBottomRight = new Rectangle(20, 20, 10, 10);
        Rectangle hoverCardTopBorder = new Rectangle(10, 0, 10, 10);
        Rectangle hoverCardBottomBorder = new Rectangle(10, 20, 10, 10);
        Rectangle hoverCardLeftBorder = new Rectangle(0, 10, 10, 10);
        Rectangle hoverCardRightBorder = new Rectangle(20, 10, 10, 10);

       

        public UIHoverCard(Rectangle location, String title, String body)
        {
            hovercardPosition = location;
            hovercardTitle = title;
            hovercardText = body;
        }

        int returnTitlePosition(int offsetWidth, SpriteFont font, String thetext)
        {
            return (int)((offsetWidth / 2) - (font.MeasureString(thetext).X / 2));
        }

        void DrawText(SpriteBatch spriteBatch,SpriteFont bigfont, SpriteFont littlefont, Rectangle offsetposition)
        {
            spriteBatch.DrawString(bigfont, hovercardTitle, new Vector2(offsetposition.X + returnTitlePosition(offsetposition.Width,bigfont,hovercardTitle), offsetposition.Y + 10), Color.Black,0,Vector2.Zero,1, SpriteEffects.None,0.82f);

            spriteBatch.DrawString(littlefont, hovercardText, new Vector2(offsetposition.X + 10, offsetposition.Y+50), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.82f);

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D hoverCardSprite, Rectangle offsetposition,SpriteFont bigfont,SpriteFont littlefont)
        {
            DrawText(spriteBatch, bigfont, littlefont, offsetposition);

            //draw fill
            spriteBatch.Draw(hoverCardSprite, offsetposition, hoverCardFillRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.81f);
            
            //draw top left
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X - 10, offsetposition.Y - 10, 10, 10), hoverCardTopLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

            //draw top right
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X + hovercardPosition.Width, offsetposition.Y - 10, 10, 10), hoverCardTopRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

            //draw bottom left
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X - 10, offsetposition.Y + hovercardPosition.Height, 10, 10), hoverCardBottomLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

            //draw bottom right
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X + hovercardPosition.Width, offsetposition.Y + hovercardPosition.Height, 10, 10), hoverCardBottomRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

            //draw top border
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X, offsetposition.Y - 10, hovercardPosition.Width, 10), hoverCardTopBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

            //draw bottom border
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X, offsetposition.Y + hovercardPosition.Height, hovercardPosition.Width, 10), hoverCardBottomBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

            //draw left border
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X - 10, offsetposition.Y, 10, hovercardPosition.Height), hoverCardLeftBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

            //draw right border
            spriteBatch.Draw(hoverCardSprite, new Rectangle(offsetposition.X + hovercardPosition.Width, offsetposition.Y, 10, hovercardPosition.Height), hoverCardRightBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
            

        }

    }
}
