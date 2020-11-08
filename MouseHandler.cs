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

        public Boolean isLeftClickDown;
        public Rectangle leftMouseClickPosition;

        //font
        public String mouseHoverFont = "";
        public String placeText = "";
        public String roadText = "";
        public String waterText = "";
        public String terrainText = "";
        Vector2 fontPosition;

        public MouseHandler(GameMain gamedeets)
        {
            mouseTexture = gamedeets.Content.Load<Texture2D>("Sprites/UI/mouseCursor");

        }

        public void Update(Vector2 cameraposition,Vector2 backbufferposition,SpriteFont spritefont)
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

            fontPosition.X -= spritefont.MeasureString(mouseHoverFont).X / 2;

            mouseHoverFont = prioritizeText();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                isLeftClickDown = true;
                //leftMouseClickPosition = new Rectangle((int)mousePosition.X,(int)mousePosition.Y,50,50);
                leftMouseClickPosition = new Rectangle((int)getMousePosition(cameraposition, backbufferposition).X, (int)getMousePosition(cameraposition, backbufferposition).Y,50,50);
            }
            else
            {
                isLeftClickDown = false;
            }
        }

        String prioritizeText()
        {
            if (placeText!="")
            {
                return placeText;
            }
            else if (roadText!="")
            {
                return roadText;
            }
            else if (waterText!="")
            {
                return waterText;
            }
            else if (terrainText!="")
            {
                return terrainText;
            }
            else
            {
                return "";
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

            spriteBatch.Draw(mouseTexture, positionFromVectorToRect(getMousePosition(cameraposition,backbuffer)),null, Color.White,0,Vector2.Zero, SpriteEffects.None,0.9f);

        }
    }
}
