using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class UIMapSelection
    {


        Rectangle uiMapFillRect = new Rectangle(40, 40, 10, 10);
        Rectangle uiMapTopLeft = new Rectangle(30, 30, 10, 10);
        Rectangle uiMapTopRight = new Rectangle(50, 30, 10, 10);
        Rectangle uiMapBottomLeft = new Rectangle(30, 50, 10, 10);
        Rectangle uiMapBottomRight = new Rectangle(50, 50, 10, 10);
        Rectangle uiMapTopBorder = new Rectangle(40, 30, 10, 10);
        Rectangle uiMapBottomBorder = new Rectangle(40, 50, 10, 10);
        Rectangle uiMapLeftBorder = new Rectangle(30, 40, 10, 10);
        Rectangle uiMapRightBorder = new Rectangle(50, 40, 10, 10);

        //add text selections

        //positions
        Rectangle uiMapSelectionPosition;
        Vector2 locationOffset = new Vector2(0, -50);

        public UIMapSelection(Rectangle position)
        {


            uiMapSelectionPosition = new Rectangle((int)(position.X + locationOffset.X), (int)(position.Y + locationOffset.Y), position.Width, position.Height);
        }


        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D uiWindowSprite)
        {
            //draw fill
            spriteBatch.Draw(uiWindowSprite, uiMapSelectionPosition, uiMapFillRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.72f);
            //draw top left
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X - 20, uiMapSelectionPosition.Y - 20, 20, 20), uiMapTopLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw top right
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X + uiMapSelectionPosition.Width, uiMapSelectionPosition.Y - 20, 20, 20), uiMapTopRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw bottom left
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X - 20, uiMapSelectionPosition.Y + uiMapSelectionPosition.Height, 20, 20), uiMapBottomLeft, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw bottom right
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X + uiMapSelectionPosition.Width, uiMapSelectionPosition.Y + uiMapSelectionPosition.Height, 20, 20), uiMapBottomRight, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw top border
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X, uiMapSelectionPosition.Y - 20, uiMapSelectionPosition.Width, 20), uiMapTopBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw bottom border
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X, uiMapSelectionPosition.Y + uiMapSelectionPosition.Height, uiMapSelectionPosition.Width, 20), uiMapBottomBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw left border
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X - 20, uiMapSelectionPosition.Y, 20, uiMapSelectionPosition.Height), uiMapLeftBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);
            //draw right border
            spriteBatch.Draw(uiWindowSprite, new Rectangle(uiMapSelectionPosition.X + uiMapSelectionPosition.Width, uiMapSelectionPosition.Y, 20, uiMapSelectionPosition.Height), uiMapRightBorder, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.73f);

        }
    }
}
