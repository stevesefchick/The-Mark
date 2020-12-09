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
        public enum ThisCreatureType { Birb, FlapFlap, Leggy, Stinkhorn, Krab, Doggo, Swampus, RatRat, Hoars, Furball}
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
            else if (thisCreatureType == ThisCreatureType.FlapFlap)
            {
                possibleLoot.Add(dataManager.itemLootData["FlapFlap Wing"]);
                possibleLoot.Add(dataManager.itemLootData["FlapFlap Blood"]);
            }
            else if (thisCreatureType == ThisCreatureType.Leggy)
            {
                possibleLoot.Add(dataManager.itemLootData["Leggy Leg"]);
                possibleLoot.Add(dataManager.itemLootData["Leggy Silk"]);
                possibleLoot.Add(dataManager.itemLootData["Tuft of Leggy Hair"]);
            }
            else if (thisCreatureType == ThisCreatureType.Stinkhorn)
            {
                possibleLoot.Add(dataManager.itemLootData["Stinkhorn Horn"]);
                possibleLoot.Add(dataManager.itemLootData["Stinkhorn Fluff"]);
            }
            else if (thisCreatureType == ThisCreatureType.Krab)
            {
                possibleLoot.Add(dataManager.itemLootData["Krab Claw"]);
                possibleLoot.Add(dataManager.itemLootData["Krab Meat"]);
            }
            else if (thisCreatureType == ThisCreatureType.Doggo)
            {
                possibleLoot.Add(dataManager.itemLootData["Doggo Paw"]);
                possibleLoot.Add(dataManager.itemLootData["Doggo Fang"]);
            }
            else if (thisCreatureType == ThisCreatureType.Swampus)
            {
                possibleLoot.Add(dataManager.itemLootData["Swampus Tail"]);
                possibleLoot.Add(dataManager.itemLootData["Swampus Eye"]);
            }
            else if (thisCreatureType == ThisCreatureType.RatRat)
            {
                possibleLoot.Add(dataManager.itemLootData["RatRat Tail"]);
                possibleLoot.Add(dataManager.itemLootData["RatRat Meat"]);
            }
            else if (thisCreatureType == ThisCreatureType.Hoars)
            {
                possibleLoot.Add(dataManager.itemLootData["Hoars Meat"]);
                possibleLoot.Add(dataManager.itemLootData["Hoars Blood"]);
            }
            else if (thisCreatureType == ThisCreatureType.Furball)
            {
                possibleLoot.Add(dataManager.itemLootData["Furball Fluff"]);
                possibleLoot.Add(dataManager.itemLootData["Furball Fang"]);
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
            else if (thisType == ThisCreatureType.Krab)
            {
                thisAggressionType = AggressionType.Defensive;
                thisCreatureType = ThisCreatureType.Krab;
                thisCreatureActiveTime = ActiveTime.Daytime;
            }
            else if (thisType == ThisCreatureType.Doggo)
            {
                thisAggressionType = AggressionType.Friendly;
                thisCreatureType = ThisCreatureType.Doggo;
                thisCreatureActiveTime = ActiveTime.Both;
            }
            else if (thisType == ThisCreatureType.Swampus)
            {
                thisAggressionType = AggressionType.Neutral;
                thisCreatureType = ThisCreatureType.Swampus;
                thisCreatureActiveTime = ActiveTime.Both;
            }
            else if (thisType == ThisCreatureType.RatRat)
            {
                thisAggressionType = AggressionType.Neutral;
                thisCreatureType = ThisCreatureType.RatRat;
                thisCreatureActiveTime = ActiveTime.Nighttime;
            }
            else if (thisType == ThisCreatureType.Hoars)
            {
                thisAggressionType = AggressionType.Aggressive;
                thisCreatureType = ThisCreatureType.Hoars;
                thisCreatureActiveTime = ActiveTime.Nighttime;
            }
            else if (thisType == ThisCreatureType.Furball)
            {
                thisAggressionType = AggressionType.Aggressive;
                thisCreatureType = ThisCreatureType.Furball;
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

            //krab
            #region krab
            if (thisCreatureType == ThisCreatureType.Krab)
            {
                thischoice = rando.Next(1, 4);
                //type 1 - name + the Krab
                if (thischoice == 1)
                {
                    string ending = " the Krab";


                    List<string> candidateskrabname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "krabname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidateskrabname.Add(entry.Key);
                        }
                    }

                    thename = candidateskrabname[rando.Next(0, candidateskrabname.Count)].ToString() + ending;

                }
                //type 2 - name + title
                else if (thischoice == 2)
                {
                    List<string> candidatestitlename = new List<string>();
                    List<string> candidateskrabname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "krabname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidateskrabname.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturetitle")
                        {
                            candidatestitlename.Add(entry.Key);
                        }
                    }
                    thename = candidateskrabname[rando.Next(0, candidateskrabname.Count)].ToString() + " " + candidatestitlename[rando.Next(0, candidatestitlename.Count)].ToString();

                }
                //type 3 = water + scratch
                else if (thischoice == 3)
                {
                    List<string> candidateswatername = new List<string>();
                    List<string> candidatesscratchname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturewatername")
                        {
                            candidateswatername.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturescratchname")
                        {
                            candidatesscratchname.Add(entry.Key);
                        }
                    }
                    thename = candidateswatername[rando.Next(0, candidateswatername.Count)].ToString() + candidatesscratchname[rando.Next(0, candidatesscratchname.Count)].ToString();

                }
            }

            #endregion

            //doggo
            #region doggo
            if (thisCreatureType == ThisCreatureType.Doggo)
            {
                thischoice = rando.Next(1, 5);
                //type 1 - name + the Doggo
                if (thischoice == 1)
                {
                    string ending = " the Doggo";


                    List<string> candidatesdoggoname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "doggoname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesdoggoname.Add(entry.Key);
                        }
                    }

                    thename = candidatesdoggoname[rando.Next(0, candidatesdoggoname.Count)].ToString() + ending;

                }
                //type 2 - name + title
                else if (thischoice == 2)
                {
                    List<string> candidatestitlename = new List<string>();
                    List<string> candidatesdoggoname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "doggoname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesdoggoname.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturetitle")
                        {
                            candidatestitlename.Add(entry.Key);
                        }
                    }
                    thename = candidatesdoggoname[rando.Next(0, candidatesdoggoname.Count)].ToString() + " " + candidatestitlename[rando.Next(0, candidatestitlename.Count)].ToString();

                }
                //type 3 = cute + bite
                else if (thischoice == 3)
                {
                    List<string> candidatescutename = new List<string>();
                    List<string> candidatesbitename = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturecutename")
                        {
                            candidatescutename.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturebitename")
                        {
                            candidatesbitename.Add(entry.Key);
                        }
                    }
                    thename = candidatescutename[rando.Next(0, candidatescutename.Count)].ToString() + candidatesbitename[rando.Next(0, candidatesbitename.Count)].ToString();

                }
                //type 4 = yelling + cute
                else if (thischoice == 4)
                {
                    List<string> candidatescutename = new List<string>();
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
                        if (entry.Value.creaturenametype == "creaturecutename")
                        {
                            candidatescutename.Add(entry.Key);
                        }
                    }
                    thename = candidatesyellingname[rando.Next(0, candidatesyellingname.Count)].ToString() + candidatescutename[rando.Next(0, candidatescutename.Count)].ToString();

                }


            }
            #endregion

            //swampus
            #region swampus
            if (thisCreatureType == ThisCreatureType.Swampus)
            {
                thischoice = rando.Next(1, 3);
                //type 1 - name + the Swampus
                if (thischoice == 1)
                {
                    string ending = " the Swampus";


                    List<string> candidatesswampusname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "swampusname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesswampusname.Add(entry.Key);
                        }
                    }

                    thename = candidatesswampusname[rando.Next(0, candidatesswampusname.Count)].ToString() + ending;

                }
                //type 2 - name + title
                else if (thischoice == 2)
                {
                    List<string> candidatestitlename = new List<string>();
                    List<string> candidatesswampusname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "swampusname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesswampusname.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturetitle")
                        {
                            candidatestitlename.Add(entry.Key);
                        }
                    }
                    thename = candidatesswampusname[rando.Next(0, candidatesswampusname.Count)].ToString() + " " + candidatestitlename[rando.Next(0, candidatestitlename.Count)].ToString();

                }
            }
            #endregion

            //ratrat
            #region ratrat
            if (thisCreatureType == ThisCreatureType.RatRat)
            {
                thischoice = rando.Next(1, 3);
                //type 1 - name + the RatRat
                if (thischoice == 1)
                {
                    string ending = " the RatRat";


                    List<string> candidatesratratname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "ratratname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesratratname.Add(entry.Key);
                        }
                    }

                    thename = candidatesratratname[rando.Next(0, candidatesratratname.Count)].ToString() + ending;

                }
                //type 2 - name + title
                else if (thischoice == 2)
                {
                    List<string> candidatestitlename = new List<string>();
                    List<string> candidatesratratname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "ratratname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesratratname.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturetitle")
                        {
                            candidatestitlename.Add(entry.Key);
                        }
                    }
                    thename = candidatesratratname[rando.Next(0, candidatesratratname.Count)].ToString() + " " + candidatestitlename[rando.Next(0, candidatestitlename.Count)].ToString();

                }
            }
            #endregion

            //hoars
            #region hoars
            if (thisCreatureType == ThisCreatureType.Hoars)
            {
                thischoice = rando.Next(1, 3);
                //type 1 - name + the Hoars
                if (thischoice == 1)
                {
                    string ending = " the Hoars";


                    List<string> candidateshoarsname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "hoarsname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidateshoarsname.Add(entry.Key);
                        }
                    }

                    thename = candidateshoarsname[rando.Next(0, candidateshoarsname.Count)].ToString() + ending;

                }
                //type 2 - name + title
                else if (thischoice == 2)
                {
                    List<string> candidatestitlename = new List<string>();
                    List<string> candidateshoarsname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "hoarsname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidateshoarsname.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturetitle")
                        {
                            candidatestitlename.Add(entry.Key);
                        }
                    }
                    thename = candidateshoarsname[rando.Next(0, candidateshoarsname.Count)].ToString() + " " + candidatestitlename[rando.Next(0, candidatestitlename.Count)].ToString();

                }
            }



            #endregion


            //furball
            #region furball
            if (thisCreatureType == ThisCreatureType.Furball)
            {
                thischoice = rando.Next(1, 3);
                //type 1 - name + the Furball
                if (thischoice == 1)
                {
                    string ending = " the Furball";


                    List<string> candidatesfurballname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "furballname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesfurballname.Add(entry.Key);
                        }
                    }

                    thename = candidatesfurballname[rando.Next(0, candidatesfurballname.Count)].ToString() + ending;

                }
                //type 2 - name + title
                else if (thischoice == 2)
                {
                    List<string> candidatestitlename = new List<string>();
                    List<string> candidatesfurballname = new List<string>();
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "furballname" || entry.Value.creaturenametype == "normalcreaturename")
                        {
                            candidatesfurballname.Add(entry.Key);
                        }
                    }
                    foreach (KeyValuePair<string, CreatureNameData> entry in datamanager.creatureNames)
                    {
                        if (entry.Value.creaturenametype == "creaturetitle")
                        {
                            candidatestitlename.Add(entry.Key);
                        }
                    }
                    thename = candidatesfurballname[rando.Next(0, candidatesfurballname.Count)].ToString() + " " + candidatestitlename[rando.Next(0, candidatestitlename.Count)].ToString();

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
