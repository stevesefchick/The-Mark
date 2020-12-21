using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class PersonSpriteConstruct
    {
        //Torso Part
        TorsoSpriteData torsoPart;
        //Feet Part
        //Hands Part
        //Head Part
        //Hair Part
        //Face Part

        int torsosize = 50;

        public PersonSpriteConstruct(DataManager datamanager,Random rando)
        {
            torsoPart = datamanager.torsoSpriteData["Standard1"];
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 location, Texture2D torso)
        {
            //Torso Part
            spriteBatch.Draw(torso, new Rectangle((int)location.X, (int)location.Y, torsosize, torsosize), new Rectangle((int)torsoPart.spriteLoc.X, (int)torsoPart.spriteLoc.Y, torsosize, torsosize), Color.White, 0, torsoPart.spriteCenter, SpriteEffects.None, 1);

            //Feet Part
            //Hands Part
            //Head Part
            //Hair Part
            //Face Part


        }

    }
}
