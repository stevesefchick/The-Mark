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

        //people textures
        public  Texture2D torsoTiles;
        public Texture2D headTiles;
        public Texture2D hairTiles;
        public Texture2D faceTiles;
        public Texture2D legTiles;
        public Texture2D armTiles;

        //location and travel
        public Point currentGridLocation;
        public String currentLocationText;
        public String currentLocationDescription;
        public Boolean displayDestination = false;
        public String currentDestinationText;
        public String currentDestinationDescription;

        public PlayerHandler(GameMain gamedeets)
        {
            LoadTextures(gamedeets);





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

        void LoadTextures(GameMain gamedeets)
        {
            torsoTiles = gamedeets.Content.Load<Texture2D>("Sprites/Person/torsoSheet");
            headTiles = gamedeets.Content.Load<Texture2D>("Sprites/Person/headSheet");
            hairTiles = gamedeets.Content.Load<Texture2D>("Sprites/Person/hairSheet");
            faceTiles = gamedeets.Content.Load<Texture2D>("Sprites/Person/faceSheet");
            legTiles = gamedeets.Content.Load<Texture2D>("Sprites/Person/legSheet");
            armTiles = gamedeets.Content.Load<Texture2D>("Sprites/Person/armSheet");
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
        public void AddEquipmentItemToInventory(EquipmentItem item)
        {
                item.currentQuantity = 1;
                partyInventory.Add(item);
        }

        public void removeEquipmentItemFromInventory(String name)
        {
            for (int i = 0; i < partyInventory.Count; ++i)
            {
                if (partyInventory[i].itemName == name)
                {
                        partyInventory.RemoveAt(i);
                        break;
                    
                }
            }

        }

        public List<EquipmentItem> getAllEquipmentItems()
        {
            List<EquipmentItem> items = new List<EquipmentItem>();

            for (int i = 0; i < partyInventory.Count; ++i)
            {
                if (partyInventory[i].thisItemType == Item.ItemType.Equipment)
                {
                    items.Add((EquipmentItem)partyInventory[i]);
                }
            }

            return items;
        }

        public EquipmentItem getEquipmentItemDetails(String name)
        {

            for (int i = 0; i < partyInventory.Count; ++i)
            {
                if (partyInventory[i].itemName == name)
                {
                    return (EquipmentItem)partyInventory[i];
                }
            }

            return null;
        }









        #endregion

        #region Equipment Methods
        public void EquipItem(EquipmentItem item, Boolean isMark, int partymember)
        {
            //body
            if (item.thisEquipmentSlot == EquipmentItem.EquipmentSlot.Body)
            {
                if (isMark==true)
                {
                    AddEquipmentItemToInventory(theMark.bodyEquipment);
                    theMark.EquipBody(item);
                }
                else
                {
                    AddEquipmentItemToInventory(partyMembers[partymember].bodyEquipment);
                    partyMembers[partymember].EquipBody(item);
                }
            }
            else if (item.thisEquipmentSlot == EquipmentItem.EquipmentSlot.Head)
            {
                if (isMark == true)
                {
                    AddEquipmentItemToInventory(theMark.headEquipment);
                    theMark.EquipHead(item);
                }
                else
                {
                    AddEquipmentItemToInventory(partyMembers[partymember].headEquipment);
                    partyMembers[partymember].EquipHead(item);
                }
            }
            else if (item.thisEquipmentSlot == EquipmentItem.EquipmentSlot.Hands)
            {
                if (isMark == true)
                {
                    AddEquipmentItemToInventory(theMark.handsEquipment);
                    theMark.EquipHands(item);
                }
                else
                {
                    AddEquipmentItemToInventory(partyMembers[partymember].handsEquipment);
                    partyMembers[partymember].EquipHands(item);
                }
            }
            else if (item.thisEquipmentSlot == EquipmentItem.EquipmentSlot.Jewelry)
            {
                if (isMark == true)
                {
                    AddEquipmentItemToInventory(theMark.jewelryEquipment);
                    theMark.EquipJewelry(item);
                }
                else
                {
                    AddEquipmentItemToInventory(partyMembers[partymember].jewelryEquipment);
                    partyMembers[partymember].EquipJewelry(item);
                }
            }
            else if (item.thisEquipmentSlot == EquipmentItem.EquipmentSlot.Trinket)
            {
                if (isMark == true)
                {
                    AddEquipmentItemToInventory(theMark.trinketEquipment);
                    theMark.EquipTrinket(item);
                }
                else
                {
                    AddEquipmentItemToInventory(partyMembers[partymember].trinketEquipment);
                    partyMembers[partymember].EquipTrinket(item);
                }
            }

        }
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

        public void DrawLocationUI(SpriteBatch spriteBatch,SpriteFont worldfont,Vector2 baseposition,Texture2D currentLocIcon,Texture2D destinationIcon)
        {
            spriteBatch.Draw(currentLocIcon, new Rectangle((int)baseposition.X - 40, (int)baseposition.Y+30, 50, 50), new Rectangle(0,0,100,100), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.81f);

            //location
            spriteBatch.DrawString(worldfont, currentLocationText, new Vector2(baseposition.X + 10, baseposition.Y + 30), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
            spriteBatch.DrawString(worldfont, currentLocationText, new Vector2(baseposition.X + 11, baseposition.Y + 31), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
            spriteBatch.DrawString(worldfont, currentLocationText, new Vector2(baseposition.X + 12, baseposition.Y + 32), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
            //description
            spriteBatch.DrawString(worldfont, currentLocationDescription, new Vector2(baseposition.X + 35, baseposition.Y + 55), Color.LightGray, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
            spriteBatch.DrawString(worldfont, currentLocationDescription, new Vector2(baseposition.X + 36, baseposition.Y + 56), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
            spriteBatch.DrawString(worldfont, currentLocationDescription, new Vector2(baseposition.X + 37, baseposition.Y + 57), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);

            if (displayDestination == true)
            {
                spriteBatch.Draw(destinationIcon, new Rectangle((int)baseposition.X, (int)baseposition.Y + 100, 50, 50), new Rectangle(0, 0, 100, 100), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.81f);
                
                //location
                spriteBatch.DrawString(worldfont, currentDestinationText, new Vector2(baseposition.X + 150, baseposition.Y + 100), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
                spriteBatch.DrawString(worldfont, currentDestinationText, new Vector2(baseposition.X + 151, baseposition.Y + 101), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                spriteBatch.DrawString(worldfont, currentDestinationText, new Vector2(baseposition.X + 152, baseposition.Y + 102), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                //description
                spriteBatch.DrawString(worldfont, currentDestinationDescription, new Vector2(baseposition.X + 175, baseposition.Y + 125), Color.LightGray, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
                spriteBatch.DrawString(worldfont, currentDestinationDescription, new Vector2(baseposition.X + 176, baseposition.Y + 126), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                spriteBatch.DrawString(worldfont, currentDestinationDescription, new Vector2(baseposition.X + 177, baseposition.Y + 127), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);



            }
        }
    }
}
