using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Flora
    {
        //enums
        public enum FloraType { Tree,Bush,Fungus,Flower,Algae }
        public enum FloraSpecies { 
        //trees
        MoonTree,
        Darkpine,
        SurfPalm,
        //bushes
        FiremelonBush,
        Bramblebush,
        Tishelbush,
        //fungus
        Bloomshroom,
        Chillcap,
        Dingushroom,
        //flowers
        FlameLily,
        FloatingAqualily,
        Dandytiger,
        //algae
        LakeWeed,
        Stinkmoss,

        }

        //properties
        FloraType thisFloraType;
        FloraSpecies thisFloraSpecies;
        Point gridLocation;
        List<String> monthsAvailable = new List<String>();
        List<int> hoursAvailable = new List<int>();



        public Flora(FloraSpecies thisSpecies, Point location)
        {
            AssignProperties(thisSpecies, location);

        }

        public List<ConsumableItem> GetLootItemDrops(Random rando, DataManager dataManager)
        {
            List<ConsumableItem> possibleLoot = new List<ConsumableItem>();
            List<ConsumableItem> actualLoot = new List<ConsumableItem>();

            if (thisFloraSpecies == FloraSpecies.MoonTree)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Moon Fruit"]);
            }
            else if (thisFloraSpecies == FloraSpecies.Tishelbush)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Tishelberry"]);
            }
            else if (thisFloraSpecies == FloraSpecies.Bloomshroom)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Bloomshroom Dust"]);
            }
            else if (thisFloraSpecies == FloraSpecies.LakeWeed)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Lake Weed Clump"]);
            }
            else if (thisFloraSpecies == FloraSpecies.SurfPalm)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Surf Coconut"]);
            }
            else if (thisFloraSpecies == FloraSpecies.Darkpine)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Darkpine Cone"]);
            }
            else if (thisFloraSpecies == FloraSpecies.FiremelonBush)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Firemelon"]);
            }
            else if (thisFloraSpecies == FloraSpecies.Chillcap)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Chillcap"]);
            }
            else if (thisFloraSpecies == FloraSpecies.Stinkmoss)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Clump of Stinkmoss"]);
            }
            else if (thisFloraSpecies == FloraSpecies.Bramblebush)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Brambleberry"]);
            }
            else if (thisFloraSpecies == FloraSpecies.FlameLily)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Flame Lily Petal"]);
            }
            else if (thisFloraSpecies == FloraSpecies.FloatingAqualily)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Aqua Lily Petal"]);
            }
            else if (thisFloraSpecies == FloraSpecies.Dandytiger)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Dandytiger Puff"]);
            }
            else if (thisFloraSpecies == FloraSpecies.Dingushroom)
            {
                possibleLoot.Add(dataManager.itemConsumableData["Dingushroom Cap"]);
            }
            for (int i = 0; i < possibleLoot.Count; ++i)
            {
                int rand = rando.Next(1, 101);

                if (rand <= possibleLoot[i].dropRate)
                {
                    actualLoot.Add(possibleLoot[i]);
                }

            }


            return possibleLoot;
        }

        public FloraSpecies ReturnSpecies()
        {
            return thisFloraSpecies;
        }

        public Boolean IsAtLocation(Point loc)
        {
            if (gridLocation == loc)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        void AssignProperties(FloraSpecies thisSpecies, Point location)
        {
            gridLocation = location;

            if (thisSpecies == FloraSpecies.MoonTree)
            {
                thisFloraType = FloraType.Tree;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Flame");
                monthsAvailable.Add("Frost");
                monthsAvailable.Add("Storm");
                monthsAvailable.Add("Earth");
                monthsAvailable.Add("Spirit");
                monthsAvailable.Add("Dew");
                monthsAvailable.Add("Light");

                hoursAvailable.Add(20);
                hoursAvailable.Add(21);
                hoursAvailable.Add(22);
                hoursAvailable.Add(23);
                hoursAvailable.Add(0);
                hoursAvailable.Add(1);
                hoursAvailable.Add(2);
                hoursAvailable.Add(3);
            }
            else if (thisSpecies == FloraSpecies.Tishelbush)
            {
                thisFloraType = FloraType.Bush;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Flame");
                monthsAvailable.Add("Storm");
                monthsAvailable.Add("Earth");
                monthsAvailable.Add("Dew");
                monthsAvailable.Add("Light");

                for (int i =0;i<24;++i)
                {
                    hoursAvailable.Add(i);
                }

            }
            else if (thisSpecies == FloraSpecies.Bloomshroom)
            {
                thisFloraType = FloraType.Fungus;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Storm");
                monthsAvailable.Add("Earth");
                monthsAvailable.Add("Dew");

                for (int i = 6; i < 18; ++i)
                {
                    hoursAvailable.Add(i);
                }

            }
            else if (thisSpecies == FloraSpecies.LakeWeed)
            {
                thisFloraType = FloraType.Algae;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Flame");
                monthsAvailable.Add("Frost");
                monthsAvailable.Add("Storm");
                monthsAvailable.Add("Earth");
                monthsAvailable.Add("Spirit");
                monthsAvailable.Add("Dew");
                monthsAvailable.Add("Light");

                for (int i = 0; i < 24; ++i)
                {
                    hoursAvailable.Add(i);
                }

            }
            else if (thisSpecies == FloraSpecies.SurfPalm)
            {
                thisFloraType = FloraType.Tree;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Flame");
                monthsAvailable.Add("Storm");
                monthsAvailable.Add("Spirit");
                monthsAvailable.Add("Light");

                for (int i = 0; i < 24; ++i)
                {
                    hoursAvailable.Add(i);
                }

            }
            else if (thisSpecies == FloraSpecies.Darkpine)
            {
                thisFloraType = FloraType.Tree;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Frost");
                monthsAvailable.Add("Storm");
                monthsAvailable.Add("Earth");
                monthsAvailable.Add("Spirit");

                hoursAvailable.Add(20);
                hoursAvailable.Add(21);
                hoursAvailable.Add(22);
                hoursAvailable.Add(23);
                hoursAvailable.Add(0);
                hoursAvailable.Add(1);
                hoursAvailable.Add(2);
                hoursAvailable.Add(3);

            }
            else if (thisSpecies == FloraSpecies.FiremelonBush)
            {
                thisFloraType = FloraType.Bush;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Flame");
                monthsAvailable.Add("Earth");
                monthsAvailable.Add("Spirit");
                monthsAvailable.Add("Dew");
                monthsAvailable.Add("Light");

                for (int i = 10; i < 20; ++i)
                {
                    hoursAvailable.Add(i);
                }

            }
            else if (thisSpecies == FloraSpecies.Chillcap)
            {
                thisFloraType = FloraType.Fungus;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Frost");
                monthsAvailable.Add("Dew");

                for (int i = 0; i < 24; ++i)
                {
                    hoursAvailable.Add(i);
                }

            }
            else if (thisSpecies == FloraSpecies.Stinkmoss)
            {
                thisFloraType = FloraType.Algae;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Earth");
                monthsAvailable.Add("Spirit");
                monthsAvailable.Add("Dew");
                monthsAvailable.Add("Light");

                for (int i = 10; i < 20; ++i)
                {
                    hoursAvailable.Add(i);
                }

            }
            else if (thisSpecies == FloraSpecies.Bramblebush)
            {
                thisFloraType = FloraType.Bush;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Flame");
                monthsAvailable.Add("Frost");
                monthsAvailable.Add("Storm");
                monthsAvailable.Add("Earth");
                monthsAvailable.Add("Spirit");
                monthsAvailable.Add("Dew");
                monthsAvailable.Add("Light");

                for (int i = 4; i < 12; ++i)
                {
                    hoursAvailable.Add(i);
                }

            }
            else if (thisSpecies == FloraSpecies.FlameLily)
            {
                thisFloraType = FloraType.Flower;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Flame");
                monthsAvailable.Add("Light");

                for (int i = 7; i < 17; ++i)
                {
                    hoursAvailable.Add(i);
                }

            }
            else if (thisSpecies == FloraSpecies.FloatingAqualily)
            {
                thisFloraType = FloraType.Flower;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Flame");
                monthsAvailable.Add("Spirit");
                monthsAvailable.Add("Dew");
                monthsAvailable.Add("Light");

                for (int i = 7; i < 17; ++i)
                {
                    hoursAvailable.Add(i);
                }

            }
            else if (thisSpecies == FloraSpecies.Dandytiger)
            {
                thisFloraType = FloraType.Flower;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Earth");
                monthsAvailable.Add("Spirit");
                monthsAvailable.Add("Dew");
                monthsAvailable.Add("Light");

                for (int i = 0; i < 24; ++i)
                {
                    hoursAvailable.Add(i);
                }

            }
            else if (thisSpecies == FloraSpecies.Dingushroom)
            {
                thisFloraType = FloraType.Fungus;
                thisFloraSpecies = thisSpecies;

                monthsAvailable.Add("Earth");
                monthsAvailable.Add("Spirit");
                monthsAvailable.Add("Dew");

                for (int i = 16; i < 22; ++i)
                {
                    hoursAvailable.Add(i);
                }

            }
        }


    }
}
