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
        int hourAvailabilityStart;
        int hourAvailabilityEnd;


        public Flora()
        {


        }

        void AssignProperties()
        {


        }


    }
}
