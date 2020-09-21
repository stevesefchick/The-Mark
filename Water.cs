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


        public Water(WaterType gettinwet, DataManager datamanager, Random rando)
        {
            thisWaterType = gettinwet;
            getWaterName(datamanager, rando);

        }


        public void Update(GameMain gamedeets)
        {
            isColliding = checkForCollision(gamedeets.mouse.getMousePosition(gamedeets.camera.cameraPosition, gamedeets.backbufferJamz));


        }

        void getWaterName(DataManager datamanager, Random rando)
        {
            #region Lake
            if (thisWaterType == WaterType.Lake)
            {
                int waternametype = rando.Next(1, 5);

                //variation 1 - Noun + Lakename 
                if (waternametype == 1)
                {
                    List<string> candidatesnoun = new List<string>();
                    List<string> candidateslakename = new List<string>();
                    foreach (KeyValuePair<string, WaterNameData> entry in datamanager.waterNames)
                    {
                        if (entry.Value.waternametype == "waternoun")
                        {
                            candidatesnoun.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, WaterNameData> entry in datamanager.waterNames)
                    {
                        if (entry.Value.waternametype == "waterlakename")
                        {
                            candidateslakename.Add(entry.Key);
                        }
                    }
                    riverName = candidatesnoun[rando.Next(0, candidatesnoun.Count)].ToString() + " " + candidateslakename[rando.Next(0, candidateslakename.Count)].ToString();
                }
                //variation 2 - Lake + Noun
                else if (waternametype == 2)
                {
                    List<string> candidatesnoun = new List<string>();
                    foreach (KeyValuePair<string, WaterNameData> entry in datamanager.waterNames)
                    {
                        if (entry.Value.waternametype == "waternoun")
                        {
                            candidatesnoun.Add(entry.Key);
                        }
                    }
                    riverName = "Lake " + candidatesnoun[rando.Next(0, candidatesnoun.Count)].ToString();
                }
                //variation 3 - Noun + Noun + Lakename
                else if (waternametype == 3)
                {
                    List<string> candidatesnoun = new List<string>();
                    List<string> candidatesnoun2 = new List<string>();
                    List<string> candidateslakename = new List<string>();

                    foreach (KeyValuePair<string, WaterNameData> entry in datamanager.waterNames)
                    {
                        if (entry.Value.waternametype == "waternoun")
                        {
                            candidatesnoun.Add(entry.Key);
                            candidatesnoun2.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, WaterNameData> entry in datamanager.waterNames)
                    {
                        if (entry.Value.waternametype == "waterlakename")
                        {
                            candidateslakename.Add(entry.Key);
                        }
                    }

                    riverName = candidatesnoun[rando.Next(0, candidatesnoun.Count)].ToString() + " " + candidatesnoun2[rando.Next(0, candidatesnoun2.Count)].ToString() + " " + candidateslakename[rando.Next(0, candidateslakename.Count)].ToString();
                }
                //variation 4 - Adj + Noun + Lakename
                else if (waternametype == 4)
                {
                    List<string> candidatesnoun = new List<string>();
                    List<string> candidatesadj = new List<string>();
                    List<string> candidateslakename = new List<string>();
                    foreach (KeyValuePair<string, WaterNameData> entry in datamanager.waterNames)
                    {
                        if (entry.Value.waternametype == "waternoun")
                        {
                            candidatesnoun.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, WaterNameData> entry in datamanager.waterNames)
                    {
                        if (entry.Value.waternametype == "wateradj")
                        {
                            candidatesadj.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, WaterNameData> entry in datamanager.waterNames)
                    {
                        if (entry.Value.waternametype == "waterlakename")
                        {
                            candidateslakename.Add(entry.Key);
                        }
                    }
                    riverName = candidatesadj[rando.Next(0, candidatesadj.Count)].ToString() + " " + candidatesnoun[rando.Next(0, candidatesnoun.Count)].ToString() + " " + candidateslakename[rando.Next(0, candidateslakename.Count)].ToString();
                }


            }
            #endregion

            #region River
            else if (thisWaterType == WaterType.River)
            {
                int waternametype = rando.Next(1, 3);

                //variation 1 - Noun + Rivername (River, Brook, Creek)
                if (waternametype == 1)
                {
                    List<string> candidatesnoun = new List<string>();
                    List<string> candidatesrivername = new List<string>();
                    foreach (KeyValuePair<string, WaterNameData> entry in datamanager.waterNames)
                    {
                        if (entry.Value.waternametype == "waternoun")
                        {
                            candidatesnoun.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, WaterNameData> entry in datamanager.waterNames)
                    {
                        if (entry.Value.waternametype == "waterrivername")
                        {
                            candidatesrivername.Add(entry.Key);
                        }
                    }
                    riverName = candidatesnoun[rando.Next(0, candidatesnoun.Count)].ToString() + " " + candidatesrivername[rando.Next(0, candidatesrivername.Count)].ToString();
                }
                //variation 2 - Adj + Noun + Rivername
                else if (waternametype == 2)
                {
                    List<string> candidatesnoun = new List<string>();
                    List<string> candidatesadj = new List<string>();
                    List<string> candidatesrivername = new List<string>();
                    foreach (KeyValuePair<string, WaterNameData> entry in datamanager.waterNames)
                    {
                        if (entry.Value.waternametype == "waternoun")
                        {
                            candidatesnoun.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, WaterNameData> entry in datamanager.waterNames)
                    {
                        if (entry.Value.waternametype == "wateradj")
                        {
                            candidatesadj.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, WaterNameData> entry in datamanager.waterNames)
                    {
                        if (entry.Value.waternametype == "waterrivername")
                        {
                            candidatesrivername.Add(entry.Key);
                        }
                    }
                    riverName = candidatesadj[rando.Next(0, candidatesadj.Count)].ToString() + " " + candidatesnoun[rando.Next(0, candidatesnoun.Count)].ToString() + " " + candidatesrivername[rando.Next(0, candidatesrivername.Count)].ToString();
                }




            }
            #endregion

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
