using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace The_Mark
{
    class TerrainDoodad
    {
        //enum
        public enum TerrainDoodadType { NormalTree1, Hill1 }
        public TerrainDoodadType thisDoodadType;

        public Rectangle Location;
        public Rectangle rect;
        float doodadDepth;

        public TerrainDoodad(TerrainDoodadType thisType, Vector2 thisLoc,Random rando)
        {
            thisDoodadType = thisType;
            if (thisDoodadType == TerrainDoodadType.NormalTree1)
            {
                Location = new Rectangle((int)thisLoc.X, (int)thisLoc.Y, 16, 32);
                rect = new Rectangle(rando.Next(0,3)*16, 0, 16, 32);
            }
            else if (thisDoodadType == TerrainDoodadType.Hill1)
            {
                Location = new Rectangle((int)thisLoc.X, (int)thisLoc.Y, 64, 64);
                rect = new Rectangle(rando.Next(0,2)*64, 0, 64, 64);
            }

            doodadDepth = 0.3f + (thisLoc.Y * 0.00001f);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D thisTexture)
        {
            spriteBatch.Draw(thisTexture, Location, rect,Color.White,0,new Vector2(8,32), SpriteEffects.None, doodadDepth);
        }

    }
}
