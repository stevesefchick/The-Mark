using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Water
    {
        //enums
        public enum WaterType { Lake, River }
        public WaterType thisWaterType;

        //water name
        public string riverName = "Water";
        Vector2 displayLoc = Vector2.Zero;

        //water chunks
        public List<WaterChunk> waterChunks = new List<WaterChunk>();

        //colliding
        Boolean isColliding;


        public Water(WaterType gettinwet, Game gamedeets, Random rando)
        {
            thisWaterType = gettinwet;
            getWaterName(gamedeets, rando);

        }


        public void Update(GameMain gamedeets)
        {
            isColliding = checkForCollision(gamedeets.mouse.getMousePosition(gamedeets.camera.cameraPosition, gamedeets.backbufferJamz));


        }

        void getWaterName(Game gamedeets, Random rando)
        {
            if (thisWaterType == WaterType.Lake)
            {
                //variation 1 - Noun + Lakename (Lake, Pond)
                //variation 2 - Lake + Noun
                //variation 3 - Noun + Noun + Lakename
               //variation 4 - Adj + Noun + Lakename

                riverName = "Lake";
            }
            else if (thisWaterType == WaterType.River)
            {
                //variation 1 - Noun + Rivername (River, Brook, Creek)
                //variation 2 - Adj + Noun + Rivername


                riverName = "River";
            }


        }
        
        Boolean checkForCollision(Vector2 mouse)
        {
            Boolean collided = false;
            Rectangle mouseRect = new Rectangle((int)mouse.X, (int)mouse.Y, 32, 32);

            for (int i = 0; i < waterChunks.Count; ++i)
            {
                Rectangle thisChunk = new Rectangle((int)(waterChunks[i].rect.X), (int)(waterChunks[i].rect.Y), (int)waterChunks[i].rect.Width, (int)waterChunks[i].rect.Height);

                if (thisChunk.Intersects(mouseRect))
                {
                    collided = true;
                    displayLoc = new Vector2(waterChunks[i].rect.X - 25, waterChunks[i].rect.Y - 50);
                    break;
                }

            }

            return collided;
        }


        public void Draw(SpriteBatch spritebatch, Texture2D waterTiles, SpriteFont displayFont, Boolean dontdrawwatername)
        {
            DrawWaterChunks(spritebatch, waterTiles);

            if (isColliding == true && dontdrawwatername == false)
            {
                DrawFont(spritebatch, displayFont);
            }
        }

        private void DrawFont(SpriteBatch spriteBatch, SpriteFont displayfont)
        {
            spriteBatch.DrawString(displayfont, riverName, new Vector2((int)(displayLoc.X + 1), (int)(displayLoc.Y + 1)), Color.Black);
            spriteBatch.DrawString(displayfont, riverName, new Vector2((int)(displayLoc.X + 2), (int)(displayLoc.Y + 2)), Color.Black);
            spriteBatch.DrawString(displayfont, riverName, displayLoc, Color.White);

        }

        void DrawWaterChunks(SpriteBatch spriteBatch, Texture2D waterTiles)
        {
            for (int i = 0; i < waterChunks.Count; ++i)
            {
                spriteBatch.Draw(waterTiles, waterChunks[i].rect, waterChunks[i].tile, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

            }



        }
    }


    class WaterChunk
    {
        public Rectangle rect;
        public Rectangle tile;

        public WaterChunk(Vector2 thispos)
        {
            rect = new Rectangle((int)thispos.X, (int)thispos.Y, 64, 64);
            tile = new Rectangle(0, 192, 64, 64);
        }

        public void AssignTile(Rectangle tilevec)
        {
            tile = tilevec;
        }
    }
}
