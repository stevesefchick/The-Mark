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
        List<EventOptionOutcomes> eventOptionOutcomes = new List<EventOptionOutcomes>();



        public void SetAvailability(Boolean availability)
        {
            isAvailable = availability;
        }

        public Boolean IsAvailable()
        {
            return isAvailable;
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

        #region perform next steps based on selection
        EventOptionOutcomes CheckForSuccessOrFail(PlayerHandler player)
        {
            Boolean isSuccess = false;

            //do checks here
            isSuccess = true;


            for (int i =0;i<eventOptionOutcomes.Count;++i)
            {
                if (eventOptionOutcomes[i].thisOptionOutcome == EventOptionOutcomes.OptionOutcome.Success
                    && isSuccess==true)
                {
                    return eventOptionOutcomes[i];
                }
                else if (isSuccess == false)
                {
                    return eventOptionOutcomes[i];
                }

            }



            //if nothing's picked up
            return eventOptionOutcomes[0];

        }

        public void DetermineEventOutcome(PlayerHandler player)
        {
            EventOptionOutcomes thisOutcome = CheckForSuccessOrFail(player);
        }

        #endregion

        public EventOption(String optionTypeString, String optionTextDescriptionString,String optionRequiredSkillString, String optionRequiredSkillRankingString,
            String optionRequiredTraitString, String optionAvoidedTraitString, EventOptionOutcomes[] eventoutcomes)
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

            //outcomes
            foreach (EventOptionOutcomes o in eventoutcomes)
            {
                eventOptionOutcomes.Add(o);
            }


        }
    }

    class EventOptionOutcomes
    {
        //enums
        public enum OptionOutcome { Success, Fail }
        public enum OptionOutcomeAction { Fight, CreatureFlee, CreaturePet, Run }
        public enum NextTransition { Combat, BackToTravelSilent, PassiveEvent }

        //properties
        public OptionOutcome thisOptionOutcome;
        OptionOutcomeAction thisOptionOutcomeAction;
        NextTransition thisNextTransition;




        public EventOptionOutcomes(String outcomename, String outcomeaction, String nexttransition)
        {
            thisOptionOutcome = (OptionOutcome)Enum.Parse(typeof(OptionOutcome), outcomename);
            thisOptionOutcomeAction = (OptionOutcomeAction)Enum.Parse(typeof(OptionOutcomeAction), outcomeaction);
            thisNextTransition = (NextTransition)Enum.Parse(typeof(NextTransition), nexttransition);


        }

    }
}
