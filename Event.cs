using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Event
    {
        //enums
        public enum EventType { Passive, Character, World }
        //passive builders
        public enum PassiveEventBase { Find, Pick, Say, Eat, Injure, Joke, Sing}
        public enum PassiveEventSuccess { Success, Fail}

        //requirements
        Boolean requiresSkill = false;
        PersonSkill.SkillType requiredSkill;
        PersonSkill.SkillRanking requiredRanking;
        Boolean requiresTrait = false;
        Person.TraitType requiredTraitType;

        //properties
        EventType thisEventType;
        PassiveEventBase thisEventBaseType;
        String thisEventText;
        int eventChance;
        List<Person> eligiblePeople = new List<Person>();
        Person associatedPerson;

        public void GetRandomAssociatedPerson(Random rando)
        {
            associatedPerson = eligiblePeople[rando.Next(0, eligiblePeople.Count)];
            eligiblePeople.Clear();
        }

        public void UpdateText()
        {
            String newtext = thisEventText.Replace("%char%", associatedPerson.personFirstName + " " + associatedPerson.personLastName);
            thisEventText = newtext;
        }

        Boolean CheckForTraits(Person person)
        {
            if (requiresTrait == true)
            {
                for (int i=0;i<person.personTraits.Count;++i)
                {
                    if (requiredTraitType == person.personTraits[i])
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }


            return false;
        }

        Boolean isSkillRankingBetterThan(PersonSkill.SkillRanking requirement, PersonSkill.SkillRanking personskillranking)
        {
            if (requirement == PersonSkill.SkillRanking.Apprentice)
            {
                return true;
            }
            else if (requirement == PersonSkill.SkillRanking.Novice
                && (personskillranking == PersonSkill.SkillRanking.Novice || personskillranking == PersonSkill.SkillRanking.Master || personskillranking == PersonSkill.SkillRanking.Professional))
            {
                return true;
            }
            else if (requirement == PersonSkill.SkillRanking.Professional
    && (personskillranking == PersonSkill.SkillRanking.Master || personskillranking == PersonSkill.SkillRanking.Professional))
            {
                return true;
            }
            else if (requirement == PersonSkill.SkillRanking.Master && personskillranking == PersonSkill.SkillRanking.Master)
            {
                return true;
            }
            else
            { 
                return false; 
            }    
        }

        Boolean CheckForSkills(Person person)
        {
            if (requiresSkill == true)
            {
                for (int i =0;i < person.personSkills.Count;++i)
                {
                    if (requiredSkill == person.personSkills[i].skillType &&
                        isSkillRankingBetterThan(requiredRanking, person.personSkills[i].skillRanking) == true)
                    {

                        return true;
                    }

                }
            }
            else
            {
                return true;
            }

            return false;
        }

        public Boolean IsPersonEligible(Person person)
        {
            if (CheckForTraits(person) == true && CheckForSkills(person)==true)
            {
                eligiblePeople.Add(person);
            }
            return true;
        }

        public Boolean IsEligibleExists()
        {
            if (eligiblePeople.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public EventType GetEventType()
        {
            return thisEventType;
        }

        public int ReturnEventChance()
        {
            return eventChance;
        }

        public String ReturnEventText()
        {
            return thisEventText;
        }

        //passive event builder
        public Event(String thisEventTypeString, String thisEventBaseString, int percentChance, String eventText, 
            String requiredSkillString, String requireSkillRankingString, String requiredTraitString)
        {
            //properties
            thisEventType = (EventType) Enum.Parse(typeof(EventType), thisEventTypeString);
            thisEventBaseType = (PassiveEventBase) Enum.Parse(typeof(PassiveEventBase), thisEventBaseString);
            eventChance = percentChance;
            thisEventText = eventText;

            //requirements
            if (requiredSkillString != "")
            {
                requiresSkill = true;
                requiredSkill = (PersonSkill.SkillType)Enum.Parse(typeof(PersonSkill.SkillType), requiredSkillString);
            }
            if (requireSkillRankingString != "")
            {
                requiredRanking = (PersonSkill.SkillRanking)Enum.Parse(typeof(PersonSkill.SkillRanking), requireSkillRankingString);
            }
            if (requiredTraitString != "")
            {
                requiresTrait = true;
                requiredTraitType = (Person.TraitType)Enum.Parse(typeof(Person.TraitType), requiredTraitString);
            }

        }
    }
}
