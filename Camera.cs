using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace The_Mark
{
    class Camera
    {
		public Vector2 cameraPosition;
        public float cameraZoom;

		public Camera()
        {
            //cameraPosition = Vector2.Zero;
            cameraPosition = new Vector2(1600, 1600);
            cameraZoom = 1;
        }

		public void Update(Boolean up,Boolean down,Boolean left, Boolean right,Boolean pagedown,Boolean pageup)
		{
            if (up==true)
            {
                cameraPosition.Y -= 1;
            }
            if (down==true)
            {
                cameraPosition.Y += 1;
            }
            if (left==true)
            {
                cameraPosition.X -= 1;
            }
            if (right==true)
            {
                cameraPosition.X += 1;
            }
            if (pagedown==true)
            {
                cameraZoom -= 0.01f;
            }
            if (pageup==true)
            {
                cameraZoom += 0.01f;
            }
		}

        public Matrix get_transformation(GraphicsDeviceManager graphicsDevice)
        {
            Matrix _transform =      
              Matrix.CreateTranslation(new Vector3(-cameraPosition.X, -cameraPosition.Y, 0)) *
                                         Matrix.CreateRotationZ(0) *
                                         Matrix.CreateScale(new Vector3(cameraZoom, cameraZoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(graphicsDevice.PreferredBackBufferWidth * 0.5f, graphicsDevice.PreferredBackBufferHeight * 0.5f, 0));
            return _transform;
        }
    }
}
