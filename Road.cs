using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Road
    {
        //position info
        Vector2 startingPosition;
        Vector2 endingPosition;

        //road name
        public string roadName = "Road";
        Vector2 displayLoc = Vector2.Zero;

        //sprites
        Texture2D roadSprite;

        //road chunks
        public List<RoadChunk> roadChunks = new List<RoadChunk>();

        Boolean isColliding;

        public Road(Vector2 start, Vector2 end, GameMain gamedeets, Random rando)
        {
            startingPosition = start;
            endingPosition = end;

            //createRoadChunks();
            getRoadName(gamedeets.dataManager, rando);
            LoadRoadTexture(gamedeets,rando);

        }

        public Road(GameMain gamedeets, Random rando)
        {


            getRoadName(gamedeets.dataManager, rando);
            LoadRoadTexture(gamedeets, rando);
        }

        /*
        void createRoadChunks()
        {
            Vector2 currentPosition = startingPosition;
            Vector2 distance = (endingPosition - startingPosition);
            Vector2 chunkamount = distance / 100; //distance / size

            Vector2 edge = endingPosition - startingPosition;
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            while (MathHelper.Distance(currentPosition.X,endingPosition.X) > 5 && MathHelper.Distance(currentPosition.Y,endingPosition.Y) > 5)
            {
                roadChunks.Add(new RoadChunk(currentPosition, angle));

                currentPosition += chunkamount;
            }

        }
        */

        void LoadRoadTexture(GameMain gamedeets, Random rando)
        {
            roadSprite = gamedeets.Content.Load<Texture2D>("Sprites/Road/road_test");

        }

        Boolean checkForCollision(Vector2 mouse)
        {
            Boolean collided = false;
            Rectangle mouseRect = new Rectangle((int)mouse.X, (int)mouse.Y, 32, 32);

            for (int i = 0; i <roadChunks.Count;++i)
            {
                Rectangle thisChunk = new Rectangle((int)(roadChunks[i].rect.X), (int)(roadChunks[i].rect.Y), (int)roadChunks[i].rect.Width, (int)roadChunks[i].rect.Height);

                if (thisChunk.Intersects(mouseRect))
                {
                    collided = true;
                    displayLoc = new Vector2(roadChunks[i].rect.X-25, roadChunks[i].rect.Y - 50);
                    break;
                }

            }

            return collided;
        }

        void getRoadName(DataManager datamanager, Random rando)
        {
            int roadnametype = rando.Next(1, 3);
            //debug
            roadnametype = 1;

            if (roadnametype == 1)
            {
                List<string> candidatesnoun = new List<string>();
                List<string> candidatesend = new List<string>();
                foreach (KeyValuePair<string, RoadNameData> entry in datamanager.roadNames)
                {
                    if (entry.Value.roadnametype == "roadnoun")
                    {
                        candidatesnoun.Add(entry.Key);
                    }
                }
                foreach (KeyValuePair<string, RoadNameData> entry in datamanager.roadNames)
                {
                    if (entry.Value.roadnametype == "roadend")
                    {
                        candidatesend.Add(entry.Key);
                    }
                }
                roadName = candidatesnoun[rando.Next(0, candidatesnoun.Count)].ToString() + " " + candidatesend[rando.Next(0, candidatesend.Count)].ToString();
            }
        }

        public void Update(GameMain gamedeets)
        {
            isColliding = checkForCollision(gamedeets.mouse.getMousePosition(gamedeets.camera.cameraPosition, gamedeets.backbufferJamz));


        }

        private void DrawFont(SpriteBatch spriteBatch, SpriteFont displayfont)
        {
            spriteBatch.DrawString(displayfont, roadName, new Vector2((int)(displayLoc.X+ 1), (int)(displayLoc.Y +1)), Color.Black);
            spriteBatch.DrawString(displayfont, roadName, new Vector2((int)(displayLoc.X + 2), (int)(displayLoc.Y +2)), Color.Black);
            spriteBatch.DrawString(displayfont, roadName, displayLoc, Color.White);

        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont displayfont,Boolean dontdrawfont,Texture2D roadTiles)
        {
            DrawRoadChunks(spriteBatch,roadTiles);
            //DrawLine(spriteBatch, startingPosition, endingPosition);

            if (isColliding == true && dontdrawfont == false)
            {
                DrawFont(spriteBatch, displayfont);
            }
        }

        
        void DrawRoadChunks(SpriteBatch spriteBatch,Texture2D roadTiles)
        {
            for (int i = 0; i < roadChunks.Count;++i)
            {
                    spriteBatch.Draw(roadTiles, roadChunks[i].rect, roadChunks[i].tile, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                
            }

        }
        


    }



    class RoadChunk
    {
        public Rectangle rect;
        public Rectangle tile;

        public RoadChunk(Vector2 thispos)
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
