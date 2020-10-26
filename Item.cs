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
        public int itemValue;
        public Boolean canStack;

        //enums
        public enum ItemType { Loot }
        public ItemType thisItemType;

        public Item(String itemname,int value, Boolean stackable)
        {
            itemName = itemname;
            itemValue = value;
            canStack = stackable;

        }


    }
}
