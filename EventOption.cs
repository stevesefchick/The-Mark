using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class EventOption
    {
        //enums
        public enum EventOptionType { Fight, Run, Approach }

        //requirements
        Boolean requiresSkill = false;
        PersonSkill.SkillType requiredSkill;
        PersonSkill.SkillRanking requiredRanking;
        Boolean requiresTrait = false;
        Person.TraitType requiredTraitType;
        Boolean avoidsTrait = false;
        Person.TraitType avoidedTraitType;

        //properties
        EventOptionType thisOptionType;
        String optionTextDescription;
        Boolean isAvailable = false;

        public void SetAvailability(Boolean availability)
        {
            isAvailable = availability;
        }

        public String ReturnOptionName()
        {
            return optionTextDescription;
        }

        #region trait and skill methods

        public Boolean DoesRequireSkill()
        {
            return requiresSkill;
        }
        public Boolean DoesRequireTrait()
        {
            if (requiresTrait ==false && avoidsTrait == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public PersonSkill.SkillType ReturnRequiredSkillType()
        {
            return requiredSkill;
        }

        public Person.TraitType ReturnRequiredTraitType()
        {
            return requiredTraitType;
        }
        public Person.TraitType ReturnAvoidedTraitType()
        {
            return avoidedTraitType;
        }

        public PersonSkill.SkillRanking ReturnRequiredSkillRanking()
        {
            return requiredRanking;
        }

        #endregion 

        public EventOption(String optionTypeString, String optionTextDescriptionString,String optionRequiredSkillString, String optionRequiredSkillRankingString,
            String optionRequiredTraitString, String optionAvoidedTraitString)
        {
            thisOptionType = (EventOptionType)Enum.Parse(typeof(EventOptionType), optionTypeString);
            optionTextDescription = optionTextDescriptionString;

            if (optionRequiredSkillString != "")
            {
                requiresSkill = true;
                requiredSkill = (PersonSkill.SkillType)Enum.Parse(typeof(PersonSkill.SkillType), optionRequiredSkillString);
                requiredRanking = (PersonSkill.SkillRanking)Enum.Parse(typeof(PersonSkill.SkillRanking), optionRequiredSkillRankingString);
            }

            if (optionRequiredTraitString!="")
            {
                requiresTrait = true;
                requiredTraitType = (Person.TraitType)Enum.Parse(typeof(Person.TraitType), optionRequiredTraitString);
            }

            if (optionAvoidedTraitString!="")
            {
                avoidsTrait = true;
                avoidedTraitType = (Person.TraitType)Enum.Parse(typeof(Person.TraitType), optionAvoidedTraitString);

            }


        }
    }
}
