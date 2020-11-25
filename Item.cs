using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Item
    {
        //main item properties
        public String itemName;
        public String itemDescription;
        public int itemValue;
        public Boolean canStack;
        public String rareDescription;

        //drop rates
        public int dropRate;
        public int maxDropQuantity;

        //enums
        public enum ItemType { Loot, Consumable, Equipment }
        public ItemType thisItemType;
        public enum RarityType { Normal, Rare}
        public RarityType thisRarityType;

        //inventory variables
        public int currentQuantity;

        public Item(String itemname,int value, Boolean stackable)
        {
            itemName = itemname;
            itemValue = value;
            canStack = stackable;
            thisRarityType = RarityType.Normal;
            thisItemType = ItemType.Loot;
        }

        public void MakeItemRare()
        {
            thisRarityType = RarityType.Rare;
            itemName = rareDescription + " " + itemName;

        }


    }
}
