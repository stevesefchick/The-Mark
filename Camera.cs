using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace The_Mark
{
    class Camera
    {
		public Vector2 cameraPosition;

		public Camera()
        {
			cameraPosition = Vector2.Zero;
        }

		public void Update(Boolean up,Boolean down,Boolean left, Boolean right)
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
		}

        public Matrix get_transformation(GraphicsDeviceManager graphicsDevice)
        {
            Matrix _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-cameraPosition.X, -cameraPosition.Y, 0)) *
                                         Matrix.CreateRotationZ(0) *
                                         Matrix.CreateScale(new Vector3(1, 1, 1)) *
                                         Matrix.CreateTranslation(new Vector3(graphicsDevice.PreferredBackBufferWidth * 0.5f, graphicsDevice.PreferredBackBufferHeight * 0.5f, 0));
            return _transform;
        }
    }
}
