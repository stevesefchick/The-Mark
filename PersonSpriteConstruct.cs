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
        HeadSpriteData headPart;
        //Hair Part
        //Face Part

        int torsosize = 50;
        int headsize = 50;

        public PersonSpriteConstruct(DataManager datamanager,Random rando)
        {
            //torsos
            List<string> candidates = new List<string>();
            foreach (KeyValuePair<string, TorsoSpriteData> entry in datamanager.torsoSpriteData)
            {
                candidates.Add(entry.Key);

            }
            //assign torso
            torsoPart = datamanager.torsoSpriteData[candidates[rando.Next(0, candidates.Count)].ToString()];
            candidates.Clear();

            //heads
            foreach (KeyValuePair<string, HeadSpriteData> entry in datamanager.headSpriteData)
            {
                candidates.Add(entry.Key);

            }
            //assign head
            headPart = datamanager.headSpriteData[candidates[rando.Next(0, candidates.Count)].ToString()];
            candidates.Clear();

        }


        public void Draw(SpriteBatch spriteBatch, Vector2 location, Texture2D torso,Texture2D head)
        {
            //TODO: Get depth draworder

            //Torso Part
            spriteBatch.Draw(torso, new Rectangle((int)location.X, (int)location.Y, torsosize, torsosize), new Rectangle((int)torsoPart.spriteLoc.X, (int)torsoPart.spriteLoc.Y, torsosize, torsosize), Color.White, 0, torsoPart.spriteCenter, SpriteEffects.None, 1);

            //Feet Part
            //Hands Part
            //Head Part
            spriteBatch.Draw(head, new Rectangle((int)location.X, (int)location.Y, headsize, headsize), new Rectangle((int)headPart.spriteLoc.X, (int)headPart.spriteLoc.Y, headsize, headsize), Color.White, 0, headPart.spriteCenter, SpriteEffects.None, 1);

            //Hair Part
            //Face Part


        }

    }
}
