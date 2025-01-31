﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Event
    {
        //passive builders
        public enum PassiveEventBase { Find, Harvest, Say, Eat, Injure, Joke, Sing,Run, PetCreature, SleepDream, SleepWakeCramp}
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
        int eventPartySizeMinimum;
        Boolean occursWhenTravelling;
        Boolean occursWhileSleeping;
        Boolean occursWhenWakingUp;

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

        //properties
        List<String> thisEventTextPossibilities = new List<String>();
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
                    #region methods
                    //health
                    if (eventHealthEffect > 0)
                    {
                        player.GainHealthSingleMark(PlayerHandler.HealthGainType.Event, eventHealthEffect);
                    }
                    else if (eventHealthEffect < 0)
                    {
                        player.LoseHealthSingleMark(PlayerHandler.HealthDrainType.Event, eventHealthEffect * -1);
                    }

                    //stamina
                    if (eventStaminaEffect > 0)
                    {
                        player.GainStaminaSingleMark(PlayerHandler.StaminaGainType.Event, eventStaminaEffect);
                    }
                    else if (eventStaminaEffect < 0)
                    {
                        player.LoseStaminaSingleMark(PlayerHandler.StaminaDrainType.Event, eventStaminaEffect * -1);
                    }
                    //stress
                    if (eventStressEffect > 0)
                    {
                        player.GainStressSingleMark(PlayerHandler.StressDrainType.Event, eventStressEffect * -1);
                    }
                    else if (eventStressEffect < 0)
                    {
                        player.ReduceStressSingleMark(PlayerHandler.StressReduceType.Event, eventStressEffect);
                    }
                    #endregion
                }
                else
                {
                    for (int i=0;i<player.partyMembers.Count;++i)
                    {
                        if (player.partyMembers[i] == associatedPerson)
                        {
                            #region methods
                            //health
                            if (eventHealthEffect > 0)
                            {
                                player.GainHealthSingleCharacter(PlayerHandler.HealthGainType.Event, eventHealthEffect, player.partyMembers[i]);
                            }
                            else if (eventHealthEffect < 0)
                            {
                                player.LoseHealthSingleCharacter(PlayerHandler.HealthDrainType.Event, eventHealthEffect * -1, player.partyMembers[i]);
                            }

                            //stamina
                            if (eventStaminaEffect > 0)
                            {
                                player.GainStaminaSingleCharacter(PlayerHandler.StaminaGainType.Event, eventStaminaEffect, player.partyMembers[i]);
                            }
                            else if (eventStaminaEffect < 0)
                            {
                                player.LoseStaminaSingleCharacter(PlayerHandler.StaminaDrainType.Event, eventStaminaEffect * -1, player.partyMembers[i]);
                            }
                            //stress
                            if (eventStressEffect > 0)
                            {
                                player.GainStressSingleCharacter(PlayerHandler.StressDrainType.Event, eventStressEffect * -1, player.partyMembers[i]);
                            }
                            else if (eventStressEffect < 0)
                            {
                                player.ReduceStressSingleCharacter(PlayerHandler.StressReduceType.Event, eventStressEffect, player.partyMembers[i]);
                            }
                            #endregion

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

        public void AssociateMarkToEvent(Person person)
        {
            associatedPerson = person;
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

        public void UpdateTextForEnemy(String enemyname)
        {
            String newtext = thisEventText.Replace("%enemy%", enemyname);
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


        public Boolean CanOccurWhenTravelling()
        {
            return occursWhenTravelling;
        }

        public Boolean CanOccurWhenSleeping()
        {
            return occursWhileSleeping;
        }

        public Boolean OccursWhenWakingUp()
        {
            return occursWhenWakingUp;
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

        public Boolean CheckForPartySizeRequirements(int partysize)
        {
            if (partysize >= eventPartySizeMinimum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        Boolean CheckForStatRequirements(Person person)
        {
            int stressMin = (int)(((float)eventStressRequirementMin/100) * person.maxStress);
            int stressMax = (int)(((float)eventStressRequirementMax/100) * person.maxStress);
            int healthMin = (int)(((float)eventHealthRequirementMin / 100) * person.maxHealth);
            int healthMax = (int)(((float)eventHealthRequirementMax / 100) * person.maxHealth);
            int staminaMin = (int)(((float)eventStaminaRequirementMin / 100) * person.maxStamina);
            int staminaMax = (int)(((float)eventStaminaRequirementMax / 100) * person.maxStamina);


            if (person.currentStress >= stressMin && person.currentStress <= stressMax &&
                person.currentStamina >= staminaMin && person.currentStamina <= staminaMax &&
                person.currentHealth >= healthMin && person.currentHealth <= healthMax)
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
        public Event(String thisEventBaseString, int percentChance, String[] eventText, 
            String requiredSkillString, String requireSkillRankingString, String requiredTraitString,
            String itemType, String[] listOfItems, int stammin, int stammax, int stressmin, int stressmax, int healthmin, int healthmax,
            String requiredTerrain, String requiredRoad, int staminaeffect,int stresseffect, int healtheffect, int partysizemin, 
            Boolean occurswhentravelling, Boolean occurswhensleeping, Boolean occurswhenwakingup)
        {
            //properties
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

            eventPartySizeMinimum = partysizemin;

            occursWhenTravelling = occurswhentravelling;
            occursWhileSleeping = occurswhensleeping;
            occursWhenWakingUp = occurswhenwakingup;

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
