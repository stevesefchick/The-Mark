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
        public enum ThisCreatureType { Birb, FlapFlap, Leggy, Stinkhorn}
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
        public Terrain.TerrainType terrainTypeHome;


        public Creature(ThisCreatureType theType,DataManager dataManager,Random rando)
        {
            AssignProperties(theType, dataManager,rando);
            

            if (rando.Next(1,50)==1)
            {
                AssignUniqueCreatureProperties(rando,dataManager);
            }

        }
        public Creature()
        {

        }

        //get loot drops when killed
        public List<Item> GetCreatureLoot(DataManager dataManager,Random rando)
        {
            List<Item> possibleLoot = new List<Item>();
            List<Item> actualLoot = new List<Item>();

            if (thisCreatureType == ThisCreatureType.Birb)
            {
                possibleLoot.Add(dataManager.itemLootData["Birb Feather"]);
                possibleLoot.Add(dataManager.itemLootData["Birb Talon"]);
                possibleLoot.Add(dataManager.itemLootData["Birb Wing"]);

            }

            for (int i = 0; i < possibleLoot.Count;++i)
            {
                int rand = rando.Next(1, 101);

                if (rand <= possibleLoot[i].dropRate)
                {
                    actualLoot.Add(possibleLoot[i]);
                }

            }


            return possibleLoot;
        }

        protected void AssignProperties(ThisCreatureType thisType, DataManager dataManager,Random rando)
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
            else if (thisType == ThisCreatureType.Birb)
            {
                thisAggressionType = AggressionType.Cautious;
                thisCreatureType = ThisCreatureType.Birb;
                thisCreatureActiveTime = ActiveTime.Daytime;

            }
            else if (thisType == ThisCreatureType.Leggy)
            {
                thisAggressionType = AggressionType.Aggressive;
                thisCreatureType = ThisCreatureType.Leggy;
                thisCreatureActiveTime = ActiveTime.Nighttime;

            }
            else if (thisType == ThisCreatureType.Stinkhorn)
            {
                thisAggressionType = AggressionType.Neutral;
                thisCreatureType = ThisCreatureType.Stinkhorn;
                thisCreatureActiveTime = ActiveTime.Daytime;

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
                thischoice = rando.Next(1, 6);

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
                else if (thischoice == 2)
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
                else if (thischoice == 3)
                {
                    List<string> candidateflightname = new List<string>();
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
                        if (entry.Value.creaturenametype == "creatureflyingname")
                        {
                            candidateflightname.Add(entry.Key);
                        }
                    }
                    thename = candidatesbitename[rando.Next(0, candidatesbitename.Count)].ToString() + candidateflightname[rando.Next(0, candidateflightname.Count)].ToString();

                }
                //type 4 - scratchname + flying
                else if (thischoice == 4)
                {
                    List<string> candidateflightname = new List<string>();
                    List<string> candidatesscratchname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturescratchname")
                        {
                            candidatesscratchname.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creatureflyingname")
                        {
                            candidateflightname.Add(entry.Key);
                        }
                    }
                    thename = candidatesscratchname[rando.Next(0, candidatesscratchname.Count)].ToString() + candidateflightname[rando.Next(0, candidateflightname.Count)].ToString();

                }
                //type 2 - name + title "blank, the blank"
                else if (thischoice == 5)
                {
                    List<string> candidatestitlename = new List<string>();
                    List<string> candidatesflapflapname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "flapflapname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesflapflapname.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturetitle")
                        {
                            candidatestitlename.Add(entry.Key);
                        }
                    }
                    thename = candidatesflapflapname[rando.Next(0, candidatesflapflapname.Count)].ToString() + " " + candidatestitlename[rando.Next(0, candidatestitlename.Count)].ToString();

                }




            }
            #endregion

            //birb
            #region birb
            if (thisCreatureType == ThisCreatureType.Birb)
            {
                thischoice = rando.Next(1, 5);

                //type 1 - name + the birb
                if (thischoice == 1)
                {
                    string ending = " the Birb";


                    List<string> candidatesbirbname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "birbname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesbirbname.Add(entry.Key);
                        }
                    }

                    thename = candidatesbirbname[rando.Next(0, candidatesbirbname.Count)].ToString() + ending;

                }
                //type 2 - name + title
                else if (thischoice == 2)
                {
                    List<string> candidatestitlename = new List<string>();
                    List<string> candidatesbirbname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "birbname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesbirbname.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturetitle")
                        {
                            candidatestitlename.Add(entry.Key);
                        }
                    }
                    thename = candidatesbirbname[rando.Next(0, candidatesbirbname.Count)].ToString() + " " + candidatestitlename[rando.Next(0, candidatestitlename.Count)].ToString();

                }
                //type 3 - yelling + flying
                else if (thischoice == 3)
                {
                    List<string> candidateflightname = new List<string>();
                    List<string> candidatesyellingname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creatureyellingname")
                        {
                            candidatesyellingname.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creatureflyingname")
                        {
                            candidateflightname.Add(entry.Key);
                        }
                    }
                    thename = candidatesyellingname[rando.Next(0, candidatesyellingname.Count)].ToString() + candidateflightname[rando.Next(0, candidateflightname.Count)].ToString();

                }
                //type 4 - flying + cute
                else if (thischoice == 4)
                {
                    List<string> candidateflightname = new List<string>();
                    List<string> candidatescutename = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturecutename")
                        {
                            candidatescutename.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creatureflyingname")
                        {
                            candidateflightname.Add(entry.Key);
                        }
                    }
                    thename = candidateflightname[rando.Next(0, candidateflightname.Count)].ToString() + candidatescutename[rando.Next(0, candidatescutename.Count)].ToString();

                }
            }
            #endregion

            //leggy
            #region leggy
            if (thisCreatureType == ThisCreatureType.Leggy)
            {
                thischoice = rando.Next(1, 4);
                //type 1 - name + the Leggy
                if (thischoice == 1)
                {
                    string ending = " the Leggy";


                    List<string> candidatesleggyname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "leggyname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesleggyname.Add(entry.Key);
                        }
                    }

                    thename = candidatesleggyname[rando.Next(0, candidatesleggyname.Count)].ToString() + ending;

                }
                //type 2 - name + title
                else if (thischoice == 2)
                {
                    List<string> candidatestitlename = new List<string>();
                    List<string> candidatesleggyname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "leggyname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesleggyname.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturetitle")
                        {
                            candidatestitlename.Add(entry.Key);
                        }
                    }
                    thename = candidatesleggyname[rando.Next(0, candidatesleggyname.Count)].ToString() + " " + candidatestitlename[rando.Next(0, candidatestitlename.Count)].ToString();

                }
                //type 3 - insect + bite
                else if (thischoice == 3)
                {
                    List<string> candidatesbitename = new List<string>();
                    List<string> candidatesinsectname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturebitename")
                        {
                            candidatesbitename.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creatureinsectname")
                        {
                            candidatesinsectname.Add(entry.Key);
                        }
                    }
                    thename = candidatesinsectname[rando.Next(0, candidatesinsectname.Count)].ToString() + candidatesbitename[rando.Next(0, candidatesbitename.Count)].ToString();

                }

            }
            #endregion

            //leggy
            #region stinkhorn
            if (thisCreatureType == ThisCreatureType.Stinkhorn)
            {
                thischoice = rando.Next(1, 4);
                //type 1 - name + the Stinkhorn
                if (thischoice == 1)
                {
                    string ending = " the Stinkhorn";


                    List<string> candidatesstinkhornname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "stinkhornname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesstinkhornname.Add(entry.Key);
                        }
                    }

                    thename = candidatesstinkhornname[rando.Next(0, candidatesstinkhornname.Count)].ToString() + ending;

                }
                //type 2 - name + title
                else if (thischoice == 2)
                {
                    List<string> candidatestitlename = new List<string>();
                    List<string> candidatesstinkhornname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "stinkhornname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesstinkhornname.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturetitle")
                        {
                            candidatestitlename.Add(entry.Key);
                        }
                    }
                    thename = candidatesstinkhornname[rando.Next(0, candidatesstinkhornname.Count)].ToString() + " " + candidatestitlename[rando.Next(0, candidatestitlename.Count)].ToString();

                }
                //type 3 = cute + ground
                else if (thischoice == 3)
                {
                    List<string> candidatescutename = new List<string>();
                    List<string> candidatesgroundname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturecutename")
                        {
                            candidatescutename.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturegroundname")
                        {
                            candidatesgroundname.Add(entry.Key);
                        }
                    }
                    thename = candidatescutename[rando.Next(0, candidatescutename.Count)].ToString() + candidatesgroundname[rando.Next(0, candidatesgroundname.Count)].ToString();

                }

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
