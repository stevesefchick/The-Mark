using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Creature
    {
        //uniqueID
        public string creatureID;

        //enums
        public enum AggressionType { Friendly, Neutral, Cautious, Defensive, Aggressive}
        public enum ThisCreatureType { Birb, FlapFlap}
        public enum ActiveTime { Daytime, Nighttime, Both}

        //properties
        public AggressionType thisAggressionType;
        public ThisCreatureType thisCreatureType;
        public ActiveTime thisCreatureActiveTime;
        public Boolean isUnique;
        public String uniqueCreatureName;
        public int creatureAge = 0;

        //home
        public string placeIDHome;


        public Creature(ThisCreatureType theType,DataManager dataManager,Random rando)
        {
            AssignProperties(theType, dataManager,rando);

            if (rando.Next(1,50)==1)
            {
                AssignUniqueCreatureProperties(rando,dataManager);
            }

        }

        void AssignProperties(ThisCreatureType thisType, DataManager dataManager,Random rando)
        {
            //global stuff
            isUnique = false;
            uniqueCreatureName = "";
            creatureAge = 0;
            creatureID = dataManager.getRandomID(rando);

            if (thisType == ThisCreatureType.FlapFlap)
            {
                thisAggressionType = AggressionType.Neutral;
                thisCreatureType = ThisCreatureType.FlapFlap;
                thisCreatureActiveTime = ActiveTime.Nighttime;

            }
        }

        void AssignUniqueCreatureProperties(Random rando, DataManager dataManager)
        {
            isUnique = true;
            uniqueCreatureName = generateCreatureName(rando,dataManager);
        }

        string generateCreatureName(Random rando, DataManager datamanager)
        {
            string thename = "";
            int thischoice;

            //flap flap
            #region flap flap
            if (thisCreatureType == ThisCreatureType.FlapFlap)
            {
                thischoice = rando.Next(1, 3);

                //type 1 - name + the Flap Flap"
                if (thischoice==1)
                {
                        string ending = " the FlapFlap";


                        List<string> candidatesflapflapname = new List<string>();
                        foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                        {
                            if (entry.Value.creaturenametype == "flapflapname" || entry.Value.creaturenametype == "normalcreaturename")
                            {
                            candidatesflapflapname.Add(entry.Key);
                            }
                        }

                        thename = candidatesflapflapname[rando.Next(0, candidatesflapflapname.Count)].ToString() + ending;
                    
                }

                //type 3 - bitename + scratchname
                if (thischoice == 2)
                {
                    List<string> candidatesscratchname = new List<string>();
                    List<string> candidatesbitename = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturebitename")
                        {
                            candidatesbitename.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturescratchname")
                        {
                            candidatesscratchname.Add(entry.Key);
                        }
                    }
                    thename = candidatesbitename[rando.Next(0, candidatesbitename.Count)].ToString() + candidatesscratchname[rando.Next(0, candidatesscratchname.Count)].ToString();

                }

                //type 5 - bitename + flying
                //type 2 - name + title "blank, the blank"
                //type 4 - scratchname + flying

            }
            #endregion

            //capitalize first letter
            thename = thename.Substring(0, 1).ToUpper() + thename.Substring(1);

            return thename;
        }

        public void Update()
        {


        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}
