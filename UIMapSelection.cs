using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class UIMapSelection
    {


        Rectangle hoverCardFillRect = new Rectangle(40, 40, 10, 10);
        Rectangle hoverCardTopLeft = new Rectangle(30, 30, 10, 10);
        Rectangle hoverCardTopRight = new Rectangle(50, 30, 10, 10);
        Rectangle hoverCardBottomLeft = new Rectangle(30, 50, 10, 10);
        Rectangle hoverCardBottomRight = new Rectangle(50, 50, 10, 10);
        Rectangle hoverCardTopBorder = new Rectangle(40, 30, 10, 10);
        Rectangle hoverCardBottomBorder = new Rectangle(40, 50, 10, 10);
        Rectangle hoverCardLeftBorder = new Rectangle(30, 40, 10, 10);
        Rectangle hoverCardRightBorder = new Rectangle(50, 40, 10, 10);

        //add text selections

        //positions
        Vector2 uiMapSelectionPosition;
        Vector2 locationOffset = new Vector2(0, -50);

        public UIMapSelection(Vector2 position)
        {


            uiMapSelectionPosition = position + locationOffset;
        }
    }
}
