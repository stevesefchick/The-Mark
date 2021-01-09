using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace The_Mark
{
    class Camera
    {
		public Vector2 cameraPosition;
        public float cameraZoom;

        Vector2 cameraDestination;

		public Camera()
        {
            //cameraPosition = Vector2.Zero;
            cameraPosition = new Vector2(1600, 1600);
            cameraZoom = 1;
        }

        public void InstantCenterOnLocation(Vector2 gridLocation)
        {
            cameraPosition = new Vector2(gridLocation.X * 64, gridLocation.Y * 64);
        }

        public void CreateDestination(Vector2 dest)
        {
            if (cameraDestination == Vector2.Zero)
            {
                cameraDestination = dest;
            }
        }

		public void Update(Boolean up,Boolean down,Boolean left, Boolean right,Boolean pagedown,Boolean pageup)
		{
            if (cameraDestination != Vector2.Zero)
            {
                Vector2 distance = new Vector2((cameraDestination.X - cameraPosition.X)/10, (cameraDestination.Y - cameraPosition.Y)/10);

                cameraPosition += distance;

                if ((distance.X > -1 && distance.X < 1) && (distance.Y > -1 &&distance.Y < 1))
                {
                    cameraDestination = Vector2.Zero;
                }

            }
            else
            {
                if (up == true)
                {
                    cameraPosition.Y -= 3;
                }
                if (down == true)
                {
                    cameraPosition.Y += 3;
                }
                if (left == true)
                {
                    cameraPosition.X -= 3;
                }
                if (right == true)
                {
                    cameraPosition.X += 3;
                }
                if (pagedown == true)
                {
                    cameraZoom -= 0.01f;
                }
                if (pageup == true)
                {
                    cameraZoom += 0.01f;
                }
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
