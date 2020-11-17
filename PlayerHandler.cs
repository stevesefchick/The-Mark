using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class PlayerHandler
    {
        public Person theMark;

        public List<Person> partyMembers = new List<Person>();

        public PlayerHandler()
        {


        }

        public void CreateTheMark(WorldMap world, Random rando)
        {
            theMark = selectTheMark(world, rando);
        }

        public void AddPartyMembers(Person person,WorldMap world)
        {
            partyMembers.Add(person);

            for (int i =0; i < world.people.Count;++i)
            {
                if (world.people[i] == person)
                {
                    world.people.RemoveAt(i);
                    break;
                }
            }

        }

        public (int,int,float) returnHealthValuesForMark()
        {
            return (theMark.maxHealth, theMark.currentHealth, (float)theMark.currentHealth/(float)theMark.maxHealth);
        }

        public (int, int, float) returnStaminaValuesForMark()
        {
            return (theMark.maxStamina, theMark.currentStamina, (float)theMark.currentStamina / (float)theMark.maxStamina);
        }

        public (int, int, float) returnStressValuesForMark()
        {
            return (theMark.maxStress, theMark.currentStress, (float)theMark.currentStress / (float)theMark.maxStress);
        }

        public (int, int, float) returnHealthValuesForPartyMember(int member)
        {
            return (partyMembers[member].maxHealth, partyMembers[member].currentHealth, (float)partyMembers[member].currentHealth / (float)partyMembers[member].maxHealth);
        }

        public (int, int, float) returnStaminaValuesForPartyMember(int member)
        {
            return (partyMembers[member].maxStamina, partyMembers[member].currentStamina, (float)partyMembers[member].currentStamina / (float)partyMembers[member].maxStamina);
        }

        public (int, int, float) returnStressValuesForPartyMember(int member)
        {
            return (partyMembers[member].maxStress, partyMembers[member].currentStress, (float)partyMembers[member].currentStress / (float)partyMembers[member].maxStress);
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
