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
        public enum EquipmentSlot { Head, Hands, Body, Jewelry, Trinket }

        EquipmentItemStatus thisItemStatus;
        EquipmentSlot thisEquipmentSlot;

        //attributes
        int itemAttack;
        int itemDefense;
        int itemAbility;

        public EquipmentItem(string name, int value, Boolean stackable, EquipmentItemStatus itemStatus, EquipmentSlot itemSlot, int attack, int defense, int ability) : base(name, value, stackable)
        {
            itemName = name;
            itemValue = value;
            canStack = stackable;
            thisItemType = ItemType.Equipment;
            thisItemStatus = itemStatus;
            thisEquipmentSlot = itemSlot;
            itemAttack = attack;
            itemDefense = defense;
            itemAbility = ability;
        }

    }
}
