using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Mark
{
    class MouseHandler
    {
        public Vector2 mousePosition;
        Texture2D mouseTexture;
        MouseState mouseState;

        public MouseHandler(GameMain gamedeets)
        {
            mouseTexture = gamedeets.Content.Load<Texture2D>("Sprites/UI/mouseCursor");

        }

        public void Update()
        {
            mouseState = Mouse.GetState();
            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;
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

        public void Draw(SpriteBatch spriteBatch,Vector2 cameraposition,Vector2 backbuffer)
        {
            
            spriteBatch.Draw(mouseTexture, positionFromVectorToRect(getMousePosition(cameraposition,backbuffer)),null, Color.White,0,Vector2.Zero, SpriteEffects.None,0.8f);

        }
    }
}
