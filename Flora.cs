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
        DarkPine,
        SurfPalm,
        //bushes
        FiremelonBush,
        Bramblebush,
        Tishelbush,
        //fungus
        Bloomshroom,
        Chillcap,
        Stinkmoss,
        //flowers
        BloodOrchid,
        FlameLily,
        //algae
        LakeWeed
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

        public FloraSpecies ReturnSpecies()
        {
            return thisFloraSpecies;
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

        }


    }
}
