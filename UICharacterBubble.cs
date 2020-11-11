using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class UICharacterBubble
    {
        public Rectangle bubblePosition;
        //rects
        Rectangle bubbleSheet = new Rectangle(0, 50, 30, 30);

        public String associatedCharacter;

        public UICharacterBubble(Vector2 position)
        {
            bubblePosition = new Rectangle((int)position.X, (int)position.Y, 100, 100);

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D bubblesprite, Rectangle offsetposition)
        {
            spriteBatch.Draw(bubblesprite, offsetposition, bubbleSheet, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);

        }

    }
}
