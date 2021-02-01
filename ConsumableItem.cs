using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class ConsumableItem : Item
    {
        //enums
        enum RecoveryType { Health, Stamina, Stress }

        //properties
        RecoveryType thisRecoveryType;
        int itemRecoveryAmount;
        int itemRecoveryAmountVariance;

        public ConsumableItem(string name,int value, Boolean stackable, String recoverytype, int recoveryamount, int amountvariance) : base (name,value,stackable)
        {
            itemName = name;
            itemValue = value;
            canStack = stackable;
            thisItemType = ItemType.Consumable;

            thisRecoveryType = (RecoveryType)Enum.Parse(typeof(RecoveryType), recoverytype);
            itemRecoveryAmount = recoveryamount;
            itemRecoveryAmountVariance = amountvariance;

        }

    }
}
