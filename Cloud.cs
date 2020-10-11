using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Cloud
    {
        public Rectangle position;
        public Rectangle shadowposition;
        Vector2 drift = new Vector2(-0.5f, -0.5f);

        Rectangle rect;
        Rectangle shadowrect;

        public Cloud(Random rando)
        {
            rect = new Rectangle(rando.Next(0, 3) * 128, 0, 128, 128);
            shadowrect = new Rectangle(rect.X, 128, 128, 128);
            position.X = rando.Next(2000, 7000);
            position.Y = rando.Next(3500, 5500);
            position.Width = 512;
            position.Height = 512;

            shadowposition.Width = 512;
            shadowposition.Height = 512;
        }

        public void Update()
        {
            position.X += (int)drift.X;
            position.Y += (int)drift.Y;

            shadowposition.X = position.X;
            shadowposition.Y = position.Y + 300;
        }

        public void Draw(SpriteBatch spriteBatch,Texture2D cloudSprite)
        {
            spriteBatch.Draw(cloudSprite, position, rect, Color.White,0,Vector2.Zero, SpriteEffects.None,0.65f);

            spriteBatch.Draw(cloudSprite, shadowposition, shadowrect, Color.White,0,Vector2.Zero, SpriteEffects.None,0.6f);
        }
    }
}
