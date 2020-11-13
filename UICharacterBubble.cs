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
        //has an active party member
        Boolean isActive;

        public String associatedCharacter;

        public UICharacterBubble(Vector2 position,Boolean active)
        {
            bubblePosition = new Rectangle((int)position.X, (int)position.Y, 100, 100);
            isActive = active;

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D bubblesprite, Rectangle offsetposition)
        {

            if (isActive == true)
            {
                spriteBatch.Draw(bubblesprite, offsetposition, bubbleSheet, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
            }
            else if (isActive==false)
            {
                spriteBatch.Draw(bubblesprite, offsetposition, bubbleSheet, Color.White * 0.3f, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
            }
        }

    }
}
