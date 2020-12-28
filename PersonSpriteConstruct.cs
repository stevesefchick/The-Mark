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
        //Arm Part
        ArmSpriteData armPart;
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
        int armsize = 50;

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


            //arms
            foreach (KeyValuePair<string, ArmSpriteData> entry in datamanager.armSpriteData)
            {
                candidates.Add(entry.Key);

            }
            //assign arm
            armPart = datamanager.armSpriteData[candidates[rando.Next(0, candidates.Count)].ToString()];
            candidates.Clear();
        }


        public void DrawFullCharacter(SpriteBatch spriteBatch, Vector2 location, Texture2D torso,Texture2D head,Texture2D hair,Texture2D face,Texture2D leg,Texture2D arm, float depth)
        {
            //Torso Part
            spriteBatch.Draw(torso, new Rectangle((int)location.X, (int)location.Y, torsosize, torsosize), new Rectangle((int)torsoPart.spriteLoc.X, (int)torsoPart.spriteLoc.Y, torsosize, torsosize), Color.Black, 0, torsoPart.spriteCenter, SpriteEffects.None, depth);
            //Head Part
            spriteBatch.Draw(head, new Rectangle((int)(location.X + torsoPart.spriteHeadConnector.X), (int)(location.Y + torsoPart.spriteHeadConnector.Y), headsize, headsize), new Rectangle((int)headPart.spriteLoc.X, (int)headPart.spriteLoc.Y, headsize, headsize), Color.Black, 0, headPart.spriteBodyConnector, SpriteEffects.None, depth +0.01f);
            //Hair Part
            spriteBatch.Draw(hair, new Rectangle((int)(location.X + torsoPart.spriteHeadConnector.X + headPart.spriteHairConnector.X), (int)(location.Y + torsoPart.spriteHeadConnector.Y + headPart.spriteHairConnector.Y), hairsize, hairsize), new Rectangle((int)hairPart.spriteLoc.X, (int)hairPart.spriteLoc.Y, hairsize, hairsize), Color.Black, 0, hairPart.spriteHeadConnector, SpriteEffects.None, depth + 0.02f);
            //Face Part
            //spriteBatch.Draw(face, new Rectangle((int)(location.X + torsoPart.spriteHeadConnector.X), (int)(location.Y + torsoPart.spriteHeadConnector.Y), facesize, facesize), new Rectangle((int)facePart.spriteLoc.X, (int)facePart.spriteLoc.Y, facesize, facesize), Color.Black, 0, facePart.spriteCenter, SpriteEffects.None, depth + 0.03f);

            //Left Leg
            spriteBatch.Draw(leg, new Rectangle((int)(location.X + torsoPart.leftLegConnector.X), (int)(location.Y + torsoPart.leftLegConnector.Y), legsize, legsize), new Rectangle((int)legPart.spriteLoc.X, (int)legPart.spriteLoc.Y, legsize, legsize), Color.Black, 0, legPart.spriteBodyConnector, SpriteEffects.FlipHorizontally, depth + 0.01f);
            //Right Leg
            spriteBatch.Draw(leg, new Rectangle((int)(location.X + torsoPart.rightLegConnector.X), (int)(location.Y + torsoPart.rightLegConnector.Y), legsize, legsize), new Rectangle((int)legPart.spriteLoc.X, (int)legPart.spriteLoc.Y, legsize, legsize), Color.Black, 0, legPart.spriteBodyConnector, SpriteEffects.None, depth + 0.01f);

            //Left Arm
            spriteBatch.Draw(arm, new Rectangle((int)(location.X + torsoPart.leftArmConnector.X), (int)(location.Y + torsoPart.leftArmConnector.Y), armsize, armsize), new Rectangle((int)armPart.spriteLoc.X, (int)armPart.spriteLoc.Y, armsize, armsize), Color.Black, 0, armPart.spriteBodyConnector, SpriteEffects.FlipHorizontally, depth + 0.01f);
            //Right Arm
            spriteBatch.Draw(arm, new Rectangle((int)(location.X + torsoPart.rightArmConnector.X), (int)(location.Y + torsoPart.rightArmConnector.Y), armsize, armsize), new Rectangle((int)armPart.spriteLoc.X, (int)armPart.spriteLoc.Y, armsize, armsize), Color.Black, 0, armPart.spriteBodyConnector, SpriteEffects.None, depth + 0.01f);

        }

    }
}
