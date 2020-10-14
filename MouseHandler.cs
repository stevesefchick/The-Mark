using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace The_Mark
{
    class MouseHandler
    {
        public Vector2 mousePosition;
        Texture2D mouseTexture;
        MouseState mouseState;

        //font
        String mouseHoverFont = "hell ass";
        Vector2 fontPosition;
        int fontCenter;

        public MouseHandler(GameMain gamedeets)
        {
            mouseTexture = gamedeets.Content.Load<Texture2D>("Sprites/UI/mouseCursor");

        }

        public void Update(Vector2 cameraposition,Vector2 backbufferposition)
        {
            mouseState = Mouse.GetState();
            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;

            fontPosition = mousePosition - backbufferposition / 2 + cameraposition;

            if (mousePosition.Y>240)
            {
                fontPosition.Y -= 50;

            }
            else
            {
                fontPosition.Y += 50;
            }
        }

        public Vector2 getMousePosition(Vector2 cameraposition,Vector2 backbufferposition)
        {
            Vector2 posish = mousePosition - backbufferposition / 2 + cameraposition;

            return posish;
        }

        public Vector2 getTextOffset(Vector2 boyyyy, int size)
        {
            return new Vector2(boyyyy.X + size, boyyyy.Y + size);
        }

        Rectangle positionFromVectorToRect(Vector2 yeahboi)
        {
            Rectangle thisposishactually = new Rectangle((int)yeahboi.X, (int)yeahboi.Y,50,50);

            return thisposishactually;
        }

        public void Draw(SpriteBatch spriteBatch,Vector2 cameraposition,Vector2 backbuffer,SpriteFont font)
        {
            spriteBatch.DrawString(font, mouseHoverFont, fontPosition, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.73f);
            spriteBatch.DrawString(font, mouseHoverFont, getTextOffset(fontPosition,1), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.71f);
            spriteBatch.DrawString(font, mouseHoverFont, getTextOffset(fontPosition,2), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.71f);

            spriteBatch.Draw(mouseTexture, positionFromVectorToRect(getMousePosition(cameraposition,backbuffer)),null, Color.White,0,Vector2.Zero, SpriteEffects.None,0.8f);

        }
    }
}
