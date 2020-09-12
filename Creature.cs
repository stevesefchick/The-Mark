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
        public int creatureAge = 0;

        //home
        public string placeIDHome;


        public Creature(ThisCreatureType theType,DataManager dataManager,Random rando)
        {
            AssignProperties(theType, dataManager,rando);

            if (rando.Next(1,50)==1)
            {
                AssignUniqueCreatureProperties(rando);
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

        void AssignUniqueCreatureProperties(Random rando)
        {
            isUnique = true;
            uniqueCreatureName = generateCreatureName(rando);
        }

        string generateCreatureName(Random rando)
        {
            string thename = "";


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
