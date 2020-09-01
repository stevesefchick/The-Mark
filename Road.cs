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


        //sprites
        Texture2D roadSprite;

        //road chunks
        List<RoadChunk> roadChunks = new List<RoadChunk>();

        public Road(Vector2 start, Vector2 end, GameMain gamedeets, Random rando)
        {
            startingPosition = start;
            endingPosition = end;

            createRoadChunks();
            LoadRoadTexture(gamedeets,rando);

        }

        void createRoadChunks()
        {
            Vector2 currentPosition = startingPosition;
            Vector2 distance = (endingPosition - startingPosition);
            Vector2 chunkamount = distance / 10; //distance / size

            Vector2 edge = endingPosition - startingPosition;
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            while (MathHelper.Distance(currentPosition.X,endingPosition.X) < 5 && MathHelper.Distance(currentPosition.Y,endingPosition.Y) < 5)
            {
                roadChunks.Add(new RoadChunk(currentPosition, angle));

                currentPosition += chunkamount;
            }

        }

        void LoadRoadTexture(GameMain gamedeets, Random rando)
        {
            roadSprite = gamedeets.Content.Load<Texture2D>("Sprites/Road/road_test");

        }

        public void Update()
        {


        }


        public void Draw(SpriteBatch spriteBatch)
        {
            //DrawLine(spriteBatch, startingPosition, endingPosition);
            DrawRoadChunks(startingPosition, endingPosition, spriteBatch);
        }

        void DrawRoadChunks(Vector2 start, Vector2 end, SpriteBatch spriteBatch)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(roadSprite, new Rectangle((int)start.X, (int)start.Y, 8, 8), null, Color.White, angle, new Vector2(0, 0), SpriteEffects.None, 0);


        }


        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(roadSprite,
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    8), //width of line
                null,
                Color.White, 
                angle,
                new Vector2(0, 0), 
                SpriteEffects.None,
                0);

        }

    }



    class RoadChunk
    {
        public Vector2 position;
        public float angle;

        public RoadChunk(Vector2 thispos, float thisang)
        {
            position = thispos;
            angle = thisang;
        }


    }
}
