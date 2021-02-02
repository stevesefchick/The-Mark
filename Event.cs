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
        public enum PassiveEventBase { Find, Harvest, Say, Eat, Injure, Joke, Sing}
        public enum PassiveEventSuccess { Success, Fail}

        //requirements
        Boolean requiresSkill = false;
        PersonSkill.SkillType requiredSkill;
        PersonSkill.SkillRanking requiredRanking;
        Boolean requiresTrait = false;
        Person.TraitType requiredTraitType;
        Boolean requiresTerrain = false;
        GridTile.GridTerrain requiredTerrainType;
        Boolean requiresRoad = false;
        Boolean isonroadrequirement;

        int eventStaminaRequirementMin;
        int eventStaminaRequirementMax;
        int eventStressRequirementMin;
        int eventStressRequirementMax;
        int eventHealthRequirementMin;
        int eventHealthRequirementMax;



        //item stuff
        Boolean hasItem = false;
        Item.ItemType eventItemType;
        List<String> eventItemPosibilities = new List<String>();
        List<String> thisEventTextPossibilities = new List<String>();

        //properties
        EventType thisEventType;
        PassiveEventBase thisEventBaseType;
        int eventChance;
        String thisEventText;
        List<Person> eligiblePeople = new List<Person>();
        Person associatedPerson;
        Item earnedLootItem;
        ConsumableItem earnedConsumableItem;
        EquipmentItem earnedEquipmentItem;
        String earnedItemText;
        int eventStaminaEffect;
        int eventStressEffect;
        int eventHealthEffect;


        public void DetermineValidItem(DataManager datamanager,Random rando)
        {
            if (hasItem==true)
            {
                if (eventItemType == Item.ItemType.Consumable)
                {
                    earnedConsumableItem = datamanager.itemConsumableData[eventItemPosibilities[rando.Next(0, eventItemPosibilities.Count)]];
                    earnedItemText = earnedConsumableItem.itemName;
                }
                else if (eventItemType == Item.ItemType.Equipment)
                {
                    earnedEquipmentItem = datamanager.itemEquipmentData[eventItemPosibilities[rando.Next(0, eventItemPosibilities.Count)]];
                    earnedItemText = earnedEquipmentItem.itemName;
                }
                else if (eventItemType == Item.ItemType.Loot)
                {
                    earnedLootItem = datamanager.itemLootData[eventItemPosibilities[rando.Next(0,eventItemPosibilities.Count)]];
                    earnedItemText = earnedLootItem.itemName;
                }

            }

        }

        public void PerformPassiveEventActivity(PlayerHandler player, Random rando)
        {
            switch (thisEventBaseType)
            {
                #region find
                case PassiveEventBase.Find:
                    if (eventItemType == Item.ItemType.Loot)
                    {
                        player.addLootItemToInventory(earnedLootItem);
                    }
                    else if (eventItemType == Item.ItemType.Consumable)
                    {
                        player.AddConsumableItemToInventory(earnedConsumableItem);
                    }
                    else if (eventItemType == Item.ItemType.Equipment)
                    {
                        player.AddEquipmentItemToInventory(earnedEquipmentItem);
                    }

                    break;
                #endregion
                
                    
               #region Eat
                case PassiveEventBase.Eat:
                    Console.WriteLine("Case 2");
                    break;
                #endregion
            }


            //stat impacts
            if (eventHealthEffect != 0 || eventStaminaEffect != 0 || eventStressEffect != 0)
            {
                if (player.theMark == associatedPerson)
                {
                    player.LoseStaminaSingleMark(PlayerHandler.StaminaDrainType.Event,eventStaminaEffect*-1);
                    player.GainStressSingleMark(PlayerHandler.StressDrainType.Event, eventStressEffect * -1);
                    player.LoseHealthSingleMark(PlayerHandler.HealthDrainType.Event, eventHealthEffect * -1);
                }
                else
                {
                    for (int i=0;i<player.partyMembers.Count;++i)
                    {
                        if (player.partyMembers[i] == associatedPerson)
                        {
                            player.LoseStaminaSingleCharacter(PlayerHandler.StaminaDrainType.Event, eventStaminaEffect * -1,player.partyMembers[i]);
                            player.GainStressSingleCharacter(PlayerHandler.StressDrainType.Event, eventStressEffect * -1, player.partyMembers[i]);
                            player.LoseHealthSingleCharacter(PlayerHandler.HealthDrainType.Event, eventHealthEffect * -1, player.partyMembers[i]);

                        }
                    }
                }
            }

        }

        public void GetRandomAssociatedPerson(Random rando)
        {
            associatedPerson = eligiblePeople[rando.Next(0, eligiblePeople.Count)];
            eligiblePeople.Clear();
        }

        public void UpdateTextForName()
        {
            String newtext = thisEventText.Replace("%char%", associatedPerson.personFirstName + " " + associatedPerson.personLastName);
            thisEventText = newtext;
        }

        public void UpdateTextForItem()
        {
            String newtext = thisEventText.Replace("%item%", earnedItemText);
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

        Boolean CheckForStatRequirements(Person person)
        {
            if (person.currentStress >= eventStressRequirementMin && person.currentStress <= eventStressRequirementMax &&
                person.currentStamina >= eventStaminaRequirementMin && person.currentStamina <= eventStaminaRequirementMax &&
                person.currentHealth >= eventHealthRequirementMin && person.currentHealth <= eventHealthRequirementMax)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean CheckForGridRequirements(GridTile.GridTerrain terraintype, Boolean isonroad)
        {
            if (requiresRoad==false && requiresTerrain==false)
            {
                return true;
            }
            else if (requiresRoad==true && isonroadrequirement == isonroad)
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

        public Boolean IsPersonEligible(Person person)
        {
            if (CheckForTraits(person) == true && CheckForSkills(person)==true && CheckForStatRequirements(person)==true)
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

        public void DetermineValidText(Random rando)
        {
            thisEventText = thisEventTextPossibilities[rando.Next(0, thisEventTextPossibilities.Count)];
        }

        public String ReturnEventText()
        {
            return thisEventText;
        }

        //passive event builder
        public Event(String thisEventTypeString, String thisEventBaseString, int percentChance, String[] eventText, 
            String requiredSkillString, String requireSkillRankingString, String requiredTraitString,
            String itemType, String[] listOfItems, int stammin, int stammax, int stressmin, int stressmax, int healthmin, int healthmax,
            String requiredTerrain, String requiredRoad, int staminaeffect,int stresseffect, int healtheffect)
        {
            //properties
            thisEventType = (EventType) Enum.Parse(typeof(EventType), thisEventTypeString);
            thisEventBaseType = (PassiveEventBase) Enum.Parse(typeof(PassiveEventBase), thisEventBaseString);
            eventChance = percentChance;
            //text
            foreach (String t in eventText)
            {
                thisEventTextPossibilities.Add(t);
            }
            eventStaminaEffect = staminaeffect;
            eventStressEffect = stresseffect;
            eventHealthEffect = healtheffect;


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
            if (requiredTerrain != "")
            {
                requiresTerrain = true;
                requiredTerrainType = (GridTile.GridTerrain)Enum.Parse(typeof(GridTile.GridTerrain), requiredTerrain);
            }
            if (requiredRoad != "")
            {
                requiresRoad = true;
                if (requiredRoad=="true")
                {
                    isonroadrequirement = true;
                }
                else if (requiredRoad=="false")
                {
                    isonroadrequirement = false;
                }

            }

            eventStaminaRequirementMin = stammin;
            eventStaminaRequirementMax = stammax;
            eventStressRequirementMin = stressmin;
            eventStressRequirementMax = stressmax;
            eventHealthRequirementMin = healthmin;
            eventHealthRequirementMax = healthmax;

            //items
            if (itemType != "")
            {
                hasItem = true;
                eventItemType = (Item.ItemType)Enum.Parse(typeof(Item.ItemType), itemType);

                foreach (String i in listOfItems)
                {
                    eventItemPosibilities.Add(i);
                }
            }



        }
    }
}
