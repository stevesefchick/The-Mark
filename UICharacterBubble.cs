using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class UICharacterBubble
    {
        //has an active party member
        public Boolean isActive;
        public float HealthScale = 1;
        public float StaminaScale = 1;
        public float StressScale = 0;

        public Rectangle bubblePosition;
        int healthbaroffset = 90;
        int staminabaroffset = 115;
        int stressbaroffset = 140;

        //rects
        Rectangle bubbleSheet = new Rectangle(0, 50, 30, 30);
        Rectangle healthbarempty = new Rectangle(0, 0, 100, 25);
        Rectangle healthbarfull = new Rectangle(0, 25, 100, 25);
        Rectangle staminabarempty = new Rectangle(0, 50, 100, 25);
        Rectangle staminabarfull = new Rectangle(0, 75, 100, 25);
        Rectangle stressbarempty = new Rectangle(0, 100, 100, 25);
        Rectangle stressbarfull = new Rectangle(0, 125, 100, 25);


        public String associatedCharacter;

        public UICharacterBubble(Vector2 position,Boolean active)
        {
            bubblePosition = new Rectangle((int)position.X, (int)position.Y, 100, 100);
            isActive = active;

        }

        Rectangle getBarPosition(Rectangle offset,int thisoffset,float scale)
        {
            return new Rectangle(offset.X, offset.Y + thisoffset, (int)(100 * scale), 25);

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D bubblesprite, Texture2D healthbarSprite, Rectangle offsetposition)
        {

            if (isActive == true)
            {
                spriteBatch.Draw(bubblesprite, offsetposition, bubbleSheet, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                //health bar
                spriteBatch.Draw(healthbarSprite, getBarPosition(offsetposition,healthbaroffset,1), healthbarempty, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.81f);
                spriteBatch.Draw(healthbarSprite, getBarPosition(offsetposition,healthbaroffset,HealthScale), healthbarfull, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.82f);

                //stamina bar
                spriteBatch.Draw(healthbarSprite, getBarPosition(offsetposition,staminabaroffset,1), staminabarempty, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.81f);
                spriteBatch.Draw(healthbarSprite, getBarPosition(offsetposition,staminabaroffset,StaminaScale), staminabarfull, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.82f);

                //stress bar
                spriteBatch.Draw(healthbarSprite, getBarPosition(offsetposition, stressbaroffset,1), stressbarempty, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.81f);
                spriteBatch.Draw(healthbarSprite, getBarPosition(offsetposition, stressbaroffset,StressScale), stressbarfull, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.82f);


            }
            else if (isActive==false)
            {
                spriteBatch.Draw(bubblesprite, offsetposition, bubbleSheet, Color.White * 0.3f, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
            }
        }

    }
}
