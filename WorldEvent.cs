using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Mark
{
    class WorldEvent
    {
        //enums
        public enum WorldEventBase { CreatureEncounter }

        //requirements
        Boolean requiresTerrain = false;
        GridTile.GridTerrain requiredTerrainType;
        Boolean requiresRoad = false;
        Boolean isonroadrequirement;

        //properties
        WorldEventBase thisWorldEventBaseType;
        int eventChance;
        String thisEventTitle;
        List<String> thisEventTextPossibilities = new List<String>();
        List<EventOption> eventOptions = new List<EventOption>();
        String thisEventText;
        Creature associatedCreature;

        public Boolean CheckForGridRequirements(GridTile.GridTerrain terraintype, Boolean isonroad)
        {
            if (requiresRoad == false && requiresTerrain == false)
            {
                return true;
            }
            else if (requiresRoad == true && isonroadrequirement == isonroad)
            {
                return true;
            }
            else if (requiresTerrain == true && requiredTerrainType == terraintype)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //checks for creatures on this tile if applicable
        //if not creature related, returns true
        public Boolean ValidCreatureIfApplicable(WorldMap world, Point loc, Random rando)
        {
            if (thisWorldEventBaseType == WorldEventBase.CreatureEncounter)
            {
                associatedCreature = world.ReturnCreatureOnTile(loc, rando);

                if (associatedCreature == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }    


        }

        public int GetCountofEventOptions()
        {
            return eventOptions.Count;
        }
        public List<EventOption> GetEventOptions()
        {
            return eventOptions;
        }

        public void DetermineEventOptionAvailability(PlayerHandler player)
        {
            for (int i =0;i < eventOptions.Count;++i)
            {
                if (eventOptions[i].DoesRequireSkill() == false && eventOptions[i].DoesRequireTrait() ==false)
                {
                    eventOptions[i].SetAvailability(true);
                }
                else
                {
                    if (eventOptions[i].DoesRequireSkill() == true && CheckForSkills(player.theMark,eventOptions[i]) == true)
                    {
                        eventOptions[i].SetAvailability(true);
                    }
                    else if (eventOptions[i].DoesRequireTrait() == true && CheckForTraits(player.theMark, eventOptions[i]) == true)
                    {
                        eventOptions[i].SetAvailability(true);
                    }
                }
                

            }


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

        Boolean CheckForSkills(Person person, EventOption eo)
        {
                for (int i = 0; i < person.personSkills.Count; ++i)
                {
                    if (eo.ReturnRequiredSkillType() == person.personSkills[i].skillType &&
                        isSkillRankingBetterThan(eo.ReturnRequiredSkillRanking(), person.personSkills[i].skillRanking) == true)
                    {

                        return true;
                    }
                }


            return false;
        }

        Boolean CheckForTraits(Person person, EventOption eo)
        {
                for (int i = 0; i < person.personTraits.Count; ++i)
                {
                    if (eo.ReturnRequiredTraitType() == person.personTraits[i])
                    {
                        return true;
                    }
                    else if (eo.ReturnAvoidedTraitType() == person.personTraits[i])
                    {
                    return false;
                    }
                }


            return false;
        }

        public String ReturnEventTitleText()
        {
            return thisEventTitle;
        }

        public String ReturnEventDescriptionText()
        {
            return thisEventText;
        }

        void UpdateTextForEnemy()
        {
            //update to include enemy
            String newtext = thisEventText.Replace("%enemy%", "Bad Boy");
            thisEventText = newtext;
        }

        public void DetermineValidText(Random rando)
        {
            thisEventText = thisEventTextPossibilities[rando.Next(0, thisEventTextPossibilities.Count)];
            UpdateTextForEnemy();
        }

        public int ReturnEventChance()
        {
            return eventChance;
        }

        //world event builder
        public WorldEvent(String thisEventBaseTypeString, int percentChance, String eventTitleText, String[] eventDescriptionText,
            EventOption[] eventoptions, String requiredterrainString, String requiredroadString)
        {
            thisWorldEventBaseType = (WorldEventBase)Enum.Parse(typeof(WorldEventBase), thisEventBaseTypeString);
            eventChance = percentChance;
            //text
            thisEventTitle = eventTitleText;
            foreach (String t in eventDescriptionText)
            {
                thisEventTextPossibilities.Add(t);
            }
            //event options
            foreach (EventOption o in eventoptions)
            {
                eventOptions.Add(o);
            }

            //requirements
            if (requiredterrainString != "")
            {
                requiresTerrain = true;
                requiredTerrainType = (GridTile.GridTerrain)Enum.Parse(typeof(GridTile.GridTerrain), requiredterrainString);
            }
            if (requiredroadString != "")
            {
                requiresRoad = true;
                if (requiredroadString == "true")
                {
                    isonroadrequirement = true;
                }
                else if (requiredroadString == "false")
                {
                    isonroadrequirement = false;
                }

            }



        }

    }
}
