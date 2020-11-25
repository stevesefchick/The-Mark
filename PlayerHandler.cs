using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class PlayerHandler
    {
        //party information
        public Person theMark;
        public List<Person> partyMembers = new List<Person>();
        //inventory
        List<Item> partyInventory = new List<Item>();

        public PlayerHandler()
        {






            //save this - this is how you access items within the inventory list
            /*
            List<Item> yeahitems = new List<Item>();
            ConsumableItem yeahayummyboi = new ConsumableItem("horse", 50, true);
            yeahitems.Add(yeahayummyboi);

            if (yeahitems[0] is ConsumableItem item)
            {
                int ok = item.prance;
            }
            */
        }

        #region Inventory Methods

        //LOOT ITEMS
        public void addLootItemToInventory(Item item)
        {
            Boolean isInInventory = false;

            for (int i =0;i < partyInventory.Count;++i)
            {
                if (partyInventory[i].itemName == item.itemName && partyInventory[i].thisRarityType == item.thisRarityType)
                {
                    partyInventory[i].currentQuantity += 1;
                    isInInventory = true;
                    break;
                }
            }

            if (isInInventory == false)
            {
                item.currentQuantity = 1;
                partyInventory.Add(item);
            }
        }

        public void removeLootItemFromInventory(String name, int quantity, Item.RarityType rarity)
        {
            for (int i = 0; i < partyInventory.Count; ++i)
            {
                if (partyInventory[i].itemName == name && partyInventory[i].thisRarityType == rarity)
                {
                    partyInventory[i].currentQuantity -= quantity;
                    if (partyInventory[i].currentQuantity==0)
                    {
                        partyInventory.RemoveAt(i);
                        break;
                    }
                }
            }

        }

        public int getItemQuantity(String name, Item.RarityType rarity)
        {
            int quantity = 0;

            for (int i = 0; i < partyInventory.Count; ++i)
            {
                if (partyInventory[i].itemName == name && partyInventory[i].thisRarityType == rarity)
                {
                    return partyInventory[i].currentQuantity;
                }
            }

            return quantity;
        }

        public List<Item> getAllLootItems()
        {
            List<Item> items = new List<Item>();

            for (int i = 0; i < partyInventory.Count; ++i)
            {
                if (partyInventory[i].thisItemType == Item.ItemType.Loot)
                {
                    items.Add(partyInventory[i]);
                }
            }

            return items;
        }

        public Item getLootItemDetails(String name, Item.RarityType rarity)
        {

            for (int i = 0; i < partyInventory.Count; ++i)
            {
                if (partyInventory[i].itemName == name && partyInventory[i].thisRarityType == rarity)
                {
                    return partyInventory[i];
                }
            }

            return null;
        }


        //CONSUMABLE ITEMS
        public void AddConsumableItemToInventory(ConsumableItem item)
        {
            Boolean isInInventory = false;

            for (int i = 0; i < partyInventory.Count; ++i)
            {
                if (partyInventory[i].itemName == item.itemName)
                {
                    partyInventory[i].currentQuantity += 1;
                    isInInventory = true;
                    break;
                }
            }

            if (isInInventory == false)
            {
                item.currentQuantity = 1;
                partyInventory.Add(item);
            }
        }

        public void removeConsumableItemFromInventory(String name, int quantity)
        {
            for (int i = 0; i < partyInventory.Count; ++i)
            {
                if (partyInventory[i].itemName == name)
                {
                    partyInventory[i].currentQuantity -= quantity;
                    if (partyInventory[i].currentQuantity == 0)
                    {
                        partyInventory.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public int getConsumableItemQuantity(String name)
        {
            int quantity = 0;

            for (int i = 0; i < partyInventory.Count; ++i)
            {
                if (partyInventory[i].itemName == name)
                {
                    return partyInventory[i].currentQuantity;
                }
            }

            return quantity;
        }

        public List<ConsumableItem> getAllConsumableItemsItems()
        {
            List<ConsumableItem> items = new List<ConsumableItem>();

            for (int i = 0; i < partyInventory.Count; ++i)
            {
                if (partyInventory[i].thisItemType == Item.ItemType.Consumable)
                {
                    items.Add((ConsumableItem)partyInventory[i]);
                }
            }

            return items;
        }

        public ConsumableItem getConsumableItemDetails(String name)
        {

            for (int i = 0; i < partyInventory.Count; ++i)
            {
                if (partyInventory[i].itemName == name)
                {
                    return (ConsumableItem)partyInventory[i];
                }
            }

            return null;
        }



        //EQUIPMENT ITEMS


        #endregion

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
