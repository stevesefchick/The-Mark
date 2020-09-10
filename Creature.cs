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
        public enum AggressionType { Friendly, Neutral, Cautious, Aggressive}
        public enum ThisCreatureType { Birb}
        public enum ActiveTime { Daytime, Nighttime, Both}

        //properties
        public AggressionType thisAggressionType;
        public ThisCreatureType thisCreatureType;
        public ActiveTime thisCreatureActiveTime;
        public Boolean isUnique;
        public String uniqueCreatureName;

        //home
        public string placeIDHome;


        public Creature()
        {



        }

        public void Update()
        {


        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}
