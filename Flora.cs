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
        //DarkPine,
        //SurfPalm,
        //bushes
        //FiremelonBush,
        //Bramblebush,
        Tishelbush,
        //fungus
        Bloomshroom,
        //Chillcap,
        //Stinkmoss,
        //flowers
        //BloodOrchid,
        //FlameLily,
        //algae
        //LakeWeed
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
        }


    }
}
