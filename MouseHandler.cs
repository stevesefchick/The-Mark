﻿using Microsoft.Xna.Framework;
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
        public Boolean isRightClickDown;
        public Rectangle rightMouseClickPosition;

        //font
        public String mouseHoverFont = "";
        public String placeText = "";
        public String roadText = "";
        public String waterText = "";
        public String terrainText = "";

        public MouseHandler(GameMain gamedeets)
        {
            mouseTexture = gamedeets.Content.Load<Texture2D>("Sprites/UI/mouseCursor");

        }

        Vector2 getMouseFontPosition(SpriteFont spritefont, Vector2 backbufferposition, Vector2 cameraposition, int offset)
        {
            Vector2 position = mousePosition - backbufferposition / 2 + cameraposition;

            if (mousePosition.Y > 240)
            {
                position.Y -= 50;

            }
            else
            {
                position.Y += 50;
            }

            position.X -= spritefont.MeasureString(mouseHoverFont).X / 2;

            position.X += offset;
            position.Y += offset;

            return position;
        }

        public void Update(Vector2 cameraposition,Vector2 backbufferposition,SpriteFont spritefont)
        {
            mouseState = Mouse.GetState();

            //mouse moved
            if (mousePosition.X != mouseState.X ||
                mousePosition.Y != mouseState.Y)
            {
                mousePosition.X = mouseState.X;
                mousePosition.Y = mouseState.Y;
                mouseHoverFont = prioritizeText();

            }

            //left mouse pressed
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                isLeftClickDown = true;
                leftMouseClickPosition = new Rectangle((int)getMousePosition(cameraposition, backbufferposition).X, (int)getMousePosition(cameraposition, backbufferposition).Y,1,1);
            }
            else
            {
                isLeftClickDown = false;
            }

            //right mouse pressed
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                isRightClickDown = true;
                rightMouseClickPosition = new Rectangle((int)getMousePosition(cameraposition, backbufferposition).X, (int)getMousePosition(cameraposition, backbufferposition).Y, 1, 1);
            }
            else
            {
                isRightClickDown = false;
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


        Rectangle positionFromVectorToRect(Vector2 yeahboi)
        {
            Rectangle thisposishactually = new Rectangle((int)yeahboi.X, (int)yeahboi.Y,50,50);

            return thisposishactually;
        }

        public void Draw(SpriteBatch spriteBatch,Vector2 cameraposition,Vector2 backbuffer,SpriteFont font, Boolean dontShowText)
        {
            if (dontShowText == false)
            {
                spriteBatch.DrawString(font, mouseHoverFont, getMouseFontPosition(font,backbuffer,cameraposition,0), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.73f);
                spriteBatch.DrawString(font, mouseHoverFont, getMouseFontPosition(font, backbuffer, cameraposition, 1), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.71f);
                spriteBatch.DrawString(font, mouseHoverFont, getMouseFontPosition(font, backbuffer, cameraposition, 2), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.71f);

            }

            spriteBatch.Draw(mouseTexture, positionFromVectorToRect(getMousePosition(cameraposition,backbuffer)),null, Color.White,0,Vector2.Zero, SpriteEffects.None,0.9f);

        }
    }
}
