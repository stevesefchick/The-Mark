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
        int optionBaseSuccessRate;
        List<Person.TraitType> influencialTraits = new List<Person.TraitType>();
        List<PersonSkill.SkillType> influencialSkills = new List<PersonSkill.SkillType>();


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
        EventOptionOutcomes CheckForSuccessOrFail(PlayerHandler player,Random rando)
        {
            Boolean isSuccess = false;

            //do checks here
            #region check for success
            int modifiedchance = optionBaseSuccessRate;
            int influencetick = (int)(modifiedchance * 0.1f);

            //traits
            for (int i =0;i<influencialTraits.Count;++i)
            {
                for (int i2=0;i2<player.theMark.personTraits.Count;++i2)
                {
                    if (influencialTraits[i] == player.theMark.personTraits[i2])
                    {

                    }
                }
            }










            if (rando.Next(1, 101) <= modifiedchance)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            #endregion



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

        public void DetermineEventOutcome(PlayerHandler player,GameMain game, TravelHandler travel,Random rando,UI_Helper uihelper)
        {
            //determine success or failure of the outcome and return the outcome
            EventOptionOutcomes thisOutcome = CheckForSuccessOrFail(player,rando);


            //take actions depending on what the event outcome's transition is
            if (thisOutcome.thisNextTransition == EventOptionOutcomes.NextTransition.BackToTravelSilent)
            {
                travel.ClearWorldEvent();
            }
            else if (thisOutcome.thisNextTransition == EventOptionOutcomes.NextTransition.Combat)
            {
                //TODO: Add Combat
                //game.ChangeGameState(GameMain.GameState.Combat);
                travel.ClearWorldEvent();

            }
            else if (thisOutcome.thisNextTransition == EventOptionOutcomes.NextTransition.PassiveEvent)
            {
                Event thisPassiveEvent = game.dataManager.passiveEventData[thisOutcome.GetPassiveEventString()];
                thisPassiveEvent.DetermineValidText(rando); 
                travel.SetCurrentPassiveEvent(thisPassiveEvent, false, rando,player,travel.GetWorldEventCreatureName());
                travel.ClearWorldEvent();
                travel.RunCurrentPassiveEvent(player, rando, uihelper);

            }
        }


        #endregion

        public EventOption(String optionTypeString, String optionTextDescriptionString,String optionRequiredSkillString, String optionRequiredSkillRankingString,
            String optionRequiredTraitString, String optionAvoidedTraitString, int basesuccessrate, String[] influenceSkills, String[] influenceTraits, EventOptionOutcomes[] eventoutcomes)
        {
            thisOptionType = (EventOptionType)Enum.Parse(typeof(EventOptionType), optionTypeString);
            optionTextDescription = optionTextDescriptionString;
            optionBaseSuccessRate = basesuccessrate;

            //skills and traits stuff
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
            foreach (String s in influenceSkills)
            {
                influencialSkills.Add((PersonSkill.SkillType)Enum.Parse(typeof(PersonSkill.SkillType), s));
            }
            foreach (String t in influenceTraits)
            {
                influencialTraits.Add((Person.TraitType)Enum.Parse(typeof(Person.TraitType), t));
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
        public NextTransition thisNextTransition;
        String thisOutcomePassiveEventString;

        public String GetPassiveEventString()
        {
            return thisOutcomePassiveEventString;
        }


        public EventOptionOutcomes(String outcomename, String outcomeaction, String nexttransition,String passiveEvent)
        {
            thisOptionOutcome = (OptionOutcome)Enum.Parse(typeof(OptionOutcome), outcomename);
            thisOptionOutcomeAction = (OptionOutcomeAction)Enum.Parse(typeof(OptionOutcomeAction), outcomeaction);
            thisNextTransition = (NextTransition)Enum.Parse(typeof(NextTransition), nexttransition);
            thisOutcomePassiveEventString = passiveEvent;

        }

    }
}
