using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace The_Mark
{
    class Road
    {
        //position info
        Vector2 startingPosition;
        Vector2 endingPosition;


        //sprites
        Texture2D roadSprite;

        public Road(Vector2 start, Vector2 end, GameMain gamedeets, Random rando)
        {
            startingPosition = start;
            endingPosition = end;

            LoadRoadTexture(gamedeets,rando);

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
            DrawLine(spriteBatch, startingPosition, endingPosition);

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
                    5), //width of line
                null,
                Color.White, 
                angle,     
                new Vector2(0, 0), 
                SpriteEffects.None,
                0);

        }

    }
}
