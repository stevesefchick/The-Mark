using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class PlayerHandler
    {
        Person theMark;

        public List<Person> partyMembers = new List<Person>();

        public PlayerHandler()
        {


        }

        public void CreateTheMark(WorldMap world, Random rando)
        {
            theMark = selectTheMark(world, rando);
        }

        public (int,int,float) returnHealthValuesForMark()
        {
            return (theMark.maxHealth, theMark.currentHealth, theMark.currentHealth/theMark.maxHealth);
        }

        public (int, int, float) returnStaminaValuesForMark()
        {
            return (theMark.maxStamina, theMark.currentStamina, theMark.currentStamina / theMark.maxStamina);
        }

        public (int, int, float) returnStressValuesForMark()
        {
            return (theMark.maxStress, theMark.currentStress, theMark.currentStress / theMark.maxStress);
        }

        Person selectTheMark(WorldMap world,Random rando)
        {
            Person thisPerson = null;

            while (thisPerson == null)
            {
                int person = rando.Next(0, world.people.Count);

                if (world.people[person].personAge >= 18)
                {
                    thisPerson = world.people[person];
                    world.people.RemoveAt(person);
                }

            }

            return thisPerson;
        }
    }
}
