using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Item
    {
        //main item variables
        public String itemName;
        public String itemDescription;
        public int itemValue;
        public Boolean canStack;
        public String rareDescription;

        //drop rates
        public int dropRate;
        public int maxDropQuantity;

        //enums
        public enum ItemType { Loot }
        public ItemType thisItemType;
        public enum RarityType { Normal, Rare}
        public RarityType thisRarityType;

        public Item(String itemname,int value, Boolean stackable)
        {
            itemName = itemname;
            itemValue = value;
            canStack = stackable;
            thisRarityType = RarityType.Normal;
        }

        public void MakeItemRare()
        {
            thisRarityType = RarityType.Rare;
            itemName = rareDescription + " " + itemName;

        }


    }
}
