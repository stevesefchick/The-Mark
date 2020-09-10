using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


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

        //home
        public string placeIDHome;


        public Creature(ThisCreatureType theType)
        {
            AssignProperties(theType);


        }

        void AssignProperties(ThisCreatureType thisType)
        {
            if (thisType == ThisCreatureType.FlapFlap)
            {
                thisAggressionType = AggressionType.Neutral;
                thisCreatureType = ThisCreatureType.FlapFlap;
                thisCreatureActiveTime = ActiveTime.Nighttime;
            }
        }

        public void Update()
        {


        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}
