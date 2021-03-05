using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class PlayerHandler
    {
        //enums
        public enum StaminaDrainType { Travel, Event }
        public enum StaminaGainType { Sleeping,Event }
        public enum StressDrainType {  Event }
        public enum StressReduceType { Sleeping,Event }
        public enum HealthDrainType { Event}
        public enum HealthGainType { Sleeping,Event }
        public enum RestedLossType { Event, Travel }
        public enum RestedGainType { Event, Sleeping }

        //const
        const int travel_stamdrain = 2;
        const int sleep_stamgain = 2;
        const int sleep_stressreduce = 1;
        const int sleep_healthgain = 1;
        const int travel_restedloss = 1;
        const int sleep_restedgain = 1;

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


        #region stat gains/loss

        //STRESS
        #region stress
        public void ReduceStress(StressReduceType stresstype)
        {
            RemoveStressFromMark(stresstype, 0);
            RemoveStressFromCharacters(stresstype, 0, true, null);
        }
        public void ReduceStressSingleMark(StressReduceType stresstype, int eventamount)
        {
            RemoveStressFromMark(stresstype, eventamount);
        }
        public void ReduceStressSingleCharacter(StressReduceType stresstype, int eventamount, Person person)
        {
            RemoveStressFromCharacters(stresstype, eventamount, false, person);
        }
        public void GainStress(StressDrainType stresstype)
        {
            AddStressToMark(stresstype, 0);
            AddStressToCharacters(stresstype, 0, true, null);
        }
        public void GainStressSingleMark(StressDrainType stresstype, int eventamount)
        {
            AddStressToMark(stresstype, eventamount);
        }
        public void GainStressSingleCharacter(StressDrainType stresstype, int eventamount, Person person)
        {
            AddStressToCharacters(stresstype, eventamount, false, person);
        }


        void AddStressToMark(StressDrainType stresstype, int eventAmount)
        {
            int amount = eventAmount;


            theMark.currentStress += amount;
        }
        void RemoveStressFromMark(StressReduceType stresstype, int eventAmount)
        {
            int amount = eventAmount;

            if (stresstype == StressReduceType.Sleeping)
            {
                amount = sleep_stressreduce;
            }


            theMark.currentStress -= amount;
        }
        void RemoveStressFromCharacters(StressReduceType stresstype, int eventAmount, Boolean isAll, Person affectedperson)
        {
            int amount = eventAmount;

            if (stresstype == StressReduceType.Sleeping)
            {
                amount = sleep_stressreduce;
            }


            if (isAll == true)
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {

                    partyMembers[i].currentStress -= amount;
                }
            }
            else
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {
                    if (partyMembers[i] == affectedperson)
                    {

                        partyMembers[i].currentStress -= amount;

                    }

                }
            }
        }
        void AddStressToCharacters(StressDrainType stresstype, int eventAmount, Boolean isAll, Person affectedperson)
        {
            int amount = eventAmount;

            if (isAll == true)
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {

                    partyMembers[i].currentStress += amount;
                }
            }
            else
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {
                    if (partyMembers[i] == affectedperson)
                    {

                        partyMembers[i].currentStress += amount;

                    }

                }
            }
        }
        #endregion

        //HEALTH
        #region health
        public void GainHealth(HealthGainType healthtype)
        {
            AddHealthToMark(healthtype, 0);
            AddHealthToCharacters(healthtype, 0, true, null);
        }
        public void GainHealthSingleMark(HealthGainType healthtype, int eventamount)
        {
            AddHealthToMark(healthtype, eventamount);
        }
        public void GainHealthSingleCharacter(HealthGainType healthtype, int eventamount, Person person)
        {
            AddHealthToCharacters(healthtype, eventamount, false, person);
        }
        public void LoseHealth(HealthDrainType healthtype)
        {
            SubtractHealthFromMark(healthtype, 0);
            SubtractHealthFromCharacters(healthtype, 0, true, null);
        }

        public void LoseHealthSingleMark(HealthDrainType healthtype, int eventamount)
        {
            SubtractHealthFromMark(healthtype, eventamount);
        }
        public void LoseHealthSingleCharacter(HealthDrainType healthtype, int eventamount, Person person)
        {
            SubtractHealthFromCharacters(healthtype, eventamount, false, person);
        }


        void SubtractHealthFromMark(HealthDrainType healthtype, int eventAmount)
        {
            int amount = eventAmount;

            theMark.currentHealth -= amount;
        }
        void AddHealthToMark(HealthGainType healthtype, int eventAmount)
        {
            int amount = eventAmount;
            if (healthtype == HealthGainType.Sleeping)
            {
                amount = sleep_healthgain;
            }



            theMark.currentHealth += amount;
        }
        void SubtractHealthFromCharacters(HealthDrainType healthtype, int eventAmount, Boolean isAll, Person affectedperson)
        {
            int amount = eventAmount;

            if (isAll == true)
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {
                    partyMembers[i].currentHealth -= amount;
                }
            }
            else
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {
                    if (partyMembers[i] == affectedperson)
                    {

                        partyMembers[i].currentHealth -= amount;

                    }

                }
            }
        }
        void AddHealthToCharacters(HealthGainType healthtype, int eventAmount, Boolean isAll, Person affectedperson)
        {
            int amount = eventAmount;

            if (healthtype == HealthGainType.Sleeping)
            {
                amount = sleep_healthgain;
            }

            if (isAll == true)
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {
                    partyMembers[i].currentHealth += amount;
                }
            }
            else
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {
                    if (partyMembers[i] == affectedperson)
                    {

                        partyMembers[i].currentHealth += amount;

                    }

                }
            }
        }
        #endregion

        //STAMINA
        #region stamina
        public void GainStamina(StaminaGainType stamtype)
        {
            AddStaminaToMark(stamtype, 0);
            AddStaminaToCharacters(stamtype, 0, true, null);
        }
        public void GainStaminaSingleMark(StaminaGainType stamtype, int eventamount)
        {
            AddStaminaToMark(stamtype, eventamount);
        }
        public void GainStaminaSingleCharacter(StaminaGainType stamtype, int eventamount, Person person)
        {
            AddStaminaToCharacters(stamtype, eventamount, false, person);
        }

        public void LoseStamina(StaminaDrainType stamtype)
        {
            SubtractStaminaFromMark(stamtype,0);
            SubtractStaminaFromCharacters(stamtype,0,true,null);
        }
        public void LoseStaminaSingleMark(StaminaDrainType stamtype, int eventamount)
        {
            SubtractStaminaFromMark(stamtype,eventamount);
        }
        public void LoseStaminaSingleCharacter(StaminaDrainType stamtype, int eventamount,Person person)
        {
            SubtractStaminaFromCharacters(stamtype,eventamount,false,person);
        }

        void SubtractStaminaFromMark(StaminaDrainType stamtype, int eventAmount)
        {
            int amount = eventAmount;
            if (stamtype== StaminaDrainType.Travel)
            {
                amount = travel_stamdrain;
            }
            theMark.currentStamina -= amount;
        }
        void AddStaminaToMark(StaminaGainType stamtype, int eventAmount)
        {
            int amount = eventAmount;
            if (stamtype == StaminaGainType.Sleeping)
            {
                amount = sleep_stamgain;
            }
            theMark.currentStamina += amount;
        }
        void AddStaminaToCharacters(StaminaGainType stamtype, int eventAmount, Boolean isAll, Person affectedperson)
        {
            int amount = eventAmount;

            if (stamtype == StaminaGainType.Sleeping)
            {
                amount = sleep_stamgain;
            }


            if (isAll == true)
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {


                    partyMembers[i].currentStamina += amount;
                }
            }
            else
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {
                    if (partyMembers[i] == affectedperson)
                    {
                        partyMembers[i].currentStamina += amount;

                    }

                }
            }
        }
        void SubtractStaminaFromCharacters(StaminaDrainType stamtype, int eventAmount, Boolean isAll, Person affectedperson)
        {
            int amount = eventAmount;

            if (isAll == true)
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {
                    if (stamtype == StaminaDrainType.Travel)
                    {
                        amount = travel_stamdrain;
                    }

                    partyMembers[i].currentStamina -= amount;
                }
            }
            else
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {
                    if (partyMembers[i] == affectedperson)
                    {
                        if (stamtype == StaminaDrainType.Travel)
                        {
                            amount = travel_stamdrain;
                        }

                        partyMembers[i].currentStamina -= amount;

                    }

                }
            }
        }
        #endregion

        //RESTED
        #region rested

        public void GainRested(RestedGainType restedtype)
        {
            AddRestedToMark(restedtype, 0);
            AddRestedToCharacters(restedtype, 0, true, null);
        }
        public void GainRestedSingleMark(RestedGainType restedtype, int eventamount)
        {
            AddRestedToMark(restedtype, eventamount);
        }
        public void GainRestedSingleCharacter(RestedGainType restedtype, int eventamount, Person person)
        {
            AddRestedToCharacters(restedtype, eventamount, false, person);
        }

        public void LoseRested(RestedLossType restedtype)
        {
            SubtractRestedFromMark(restedtype, 0);
            SubtractRestedFromCharacters(restedtype, 0, true, null);
        }
        public void LoseRestedSingleMark(RestedLossType restedtype, int eventamount)
        {
            SubtractRestedFromMark(restedtype,eventamount);
        }
        public void LoseStaminaSingleCharacter(RestedLossType restedtype, int eventamount, Person person)
        {
            SubtractRestedFromCharacters(restedtype, eventamount, false, person);
        }




        void SubtractRestedFromMark(RestedLossType restedtype, int eventAmount)
        {
            int amount = eventAmount;
            if (restedtype == RestedLossType.Travel)
            {
                amount = travel_restedloss;
            }
            theMark.currentRested -= amount;
        }
        void AddRestedToMark(RestedGainType restedtype, int eventAmount)
        {
            int amount = eventAmount;
            if (restedtype == RestedGainType.Sleeping)
            {
                amount = sleep_restedgain;
            }
            theMark.currentRested += amount;
        }
        void AddRestedToCharacters(RestedGainType restedtype, int eventAmount, Boolean isAll, Person affectedperson)
        {
            int amount = eventAmount;

            if (restedtype == RestedGainType.Sleeping)
            {
                amount = sleep_restedgain;
            }


            if (isAll == true)
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {


                    partyMembers[i].currentRested += amount;
                }
            }
            else
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {
                    if (partyMembers[i] == affectedperson)
                    {
                        partyMembers[i].currentRested += amount;

                    }

                }
            }
        }
        void SubtractRestedFromCharacters(RestedLossType restedtype, int eventAmount, Boolean isAll, Person affectedperson)
        {
            int amount = eventAmount;
            if (restedtype == RestedLossType.Travel)
            {
                amount = travel_restedloss;
            }

            if (isAll == true)
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {
                    partyMembers[i].currentRested -= amount;
                }
            }
            else
            {
                for (int i = 0; i < partyMembers.Count; ++i)
                {
                    if (partyMembers[i] == affectedperson)
                    {
                        partyMembers[i].currentRested -= amount;

                    }

                }
            }
        }
        #endregion



        #endregion

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

        public void RestTick()
        {
            ReduceStress(StressReduceType.Sleeping);
            GainHealth(HealthGainType.Sleeping);
            GainStamina(StaminaGainType.Sleeping);
            GainRested(RestedGainType.Sleeping);
        }

        public Boolean OKToWakeUp()
        {
            if (theMark.currentRested >= theMark.maxRested &&
                theMark.currentStamina >= (theMark.maxStamina*0.5f))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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

        #region return stat values
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
        #endregion

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
