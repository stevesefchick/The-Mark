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
        PersonSkill.SkillType requiredSkill;
        PersonSkill.SkillRanking requiredRanking;
        Person.TraitType requiredTraitType;

        //properties
        EventType thisEventType;
        PassiveEventBase thisEventBaseType;
        String thisEventText;
        int eventChance;

        public EventType GetEventType()
        {
            return thisEventType;
        }

        public int ReturnEventChance()
        {
            return eventChance;
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
                requiredSkill = (PersonSkill.SkillType)Enum.Parse(typeof(PersonSkill.SkillType), requiredSkillString);
            }
            if (requireSkillRankingString != "")
            {
                requiredRanking = (PersonSkill.SkillRanking)Enum.Parse(typeof(PersonSkill.SkillRanking), requireSkillRankingString);
            }
            if (requiredTraitString != "")
            {
                requiredTraitType = (Person.TraitType)Enum.Parse(typeof(Person.TraitType), requiredTraitString);
            }

        }
    }
}
