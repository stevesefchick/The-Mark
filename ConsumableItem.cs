using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class ConsumableItem : Item
    {

        public ConsumableItem(string name,int value, Boolean stackable) : base (name,value,stackable)
        {
            itemName = name;
            itemValue = value;
            canStack = stackable;
            thisItemType = ItemType.Consumable;
        }

    }
}
