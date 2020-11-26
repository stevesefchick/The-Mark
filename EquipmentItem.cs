using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class EquipmentItem : Item
    {
        //enum
        public enum EquipmentItemStatus { Stock, Equipped }
        public EquipmentItemStatus thisItemStatus;

        public EquipmentItem(string name, int value, Boolean stackable, EquipmentItemStatus itemStatus) : base(name, value, stackable)
        {
            itemName = name;
            itemValue = value;
            canStack = stackable;
            thisItemType = ItemType.Equipment;
        }

    }
}
