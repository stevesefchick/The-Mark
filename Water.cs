using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Water
    {
        //water name
        public string roadName = "River";
        Vector2 displayLoc = Vector2.Zero;

        //sprites
        Texture2D waterSprite;


        //water chunks
        public List<WaterChunk> waterChunks = new List<WaterChunk>();

        //colliding
        Boolean isColliding;


        public Water()
        {



        }


        public void Update()
        {


        }


        public void Draw(SpriteBatch spritebatch)
        {


        }
    }


    class WaterChunk
    {
        public Rectangle rect;
        public Rectangle tile;

        public WaterChunk(Vector2 thispos)
        {
            rect = new Rectangle((int)thispos.X, (int)thispos.Y, 64, 64);
            tile = new Rectangle(0, 0, 64, 64);
        }

        public void AssignTile(Rectangle tilevec)
        {
            tile = tilevec;
        }
    }
}
