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
            Vector2 posish = Vector2.Zero;

            posish = mousePosition - backbufferposition / 2 + cameraposition;

            return posish;
        }

        public void Draw(SpriteBatch spriteBatch,Vector2 cameraposition,Vector2 backbuffer)
        {
            spriteBatch.Draw(mouseTexture, getMousePosition(cameraposition,backbuffer), Color.White);

        }
    }
}
