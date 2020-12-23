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
        //Leg Part
        LegSpriteData legPart;
        //Hands Part
        //Head Part
        HeadSpriteData headPart;
        //Hair Part
        HairSpriteData hairPart;
        //Face Part
        FaceSpriteData facePart;

        int torsosize = 50;
        int headsize = 50;
        int hairsize = 50;
        int facesize = 25;
        int legsize = 50;

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


            //hairs
            foreach (KeyValuePair<string, HairSpriteData> entry in datamanager.hairSpriteData)
            {
                candidates.Add(entry.Key);

            }
            //assign hair
            hairPart = datamanager.hairSpriteData[candidates[rando.Next(0, candidates.Count)].ToString()];
            candidates.Clear();


            //faces
            foreach (KeyValuePair<string, FaceSpriteData> entry in datamanager.faceSpriteData)
            {
                candidates.Add(entry.Key);

            }
            //assign face
            facePart = datamanager.faceSpriteData[candidates[rando.Next(0, candidates.Count)].ToString()];
            candidates.Clear();


            //legs
            foreach (KeyValuePair<string, LegSpriteData> entry in datamanager.legSpriteData)
            {
                candidates.Add(entry.Key);

            }
            //assign leg
            legPart = datamanager.legSpriteData[candidates[rando.Next(0, candidates.Count)].ToString()];
            candidates.Clear();



        }


        public void Draw(SpriteBatch spriteBatch, Vector2 location, Texture2D torso,Texture2D head,Texture2D hair,Texture2D face,Texture2D leg)
        {
            //Torso Part
            spriteBatch.Draw(torso, new Rectangle((int)location.X, (int)location.Y, torsosize, torsosize), new Rectangle((int)torsoPart.spriteLoc.X, (int)torsoPart.spriteLoc.Y, torsosize, torsosize), Color.White, 0, torsoPart.spriteCenter, SpriteEffects.None, 0.55f);
            //Head Part
            spriteBatch.Draw(head, new Rectangle((int)location.X, (int)location.Y, headsize, headsize), new Rectangle((int)headPart.spriteLoc.X, (int)headPart.spriteLoc.Y, headsize, headsize), Color.White, 0, headPart.spriteCenter, SpriteEffects.None, 0.56f);
            //Hair Part
            spriteBatch.Draw(hair, new Rectangle((int)location.X, (int)location.Y, hairsize, hairsize), new Rectangle((int)hairPart.spriteLoc.X, (int)hairPart.spriteLoc.Y, hairsize, hairsize), Color.White, 0, new Vector2(hairsize/2,hairsize/2), SpriteEffects.None, 0.57f);
            //Face Part
            spriteBatch.Draw(face, new Rectangle((int)location.X, (int)location.Y, facesize, facesize), new Rectangle((int)facePart.spriteLoc.X, (int)facePart.spriteLoc.Y, facesize, facesize), Color.White, 0, facePart.spriteCenter, SpriteEffects.None, 0.57f);

            //Legs
            spriteBatch.Draw(leg, new Rectangle((int)location.X, (int)location.Y, legsize, legsize), new Rectangle((int)legPart.spriteLoc.X, (int)legPart.spriteLoc.Y, legsize, legsize), Color.White, 0, legPart.spriteBodyConnector, SpriteEffects.None, 0.56f);
            spriteBatch.Draw(leg, new Rectangle((int)location.X, (int)location.Y, legsize, legsize), new Rectangle((int)legPart.spriteLoc.X, (int)legPart.spriteLoc.Y, legsize, legsize), Color.White, 0, legPart.spriteBodyConnector, SpriteEffects.None, 0.56f);

            //Hands Part
        }

    }
}
