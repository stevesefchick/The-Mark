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
            


        }




    }
}
