using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Malee.List;
using System;

public enum EquipmentSlot
{
    Chest,
    Boots,
    Helmet,
    Ring,
    Pants,
    Cloak,
    Gloves,
    WeaponMainHand,
    WeaponOffHand,
}

public abstract class EquipmentObject : ItemObject
{

    [SerializeField, Reorderable]
    private StatBonuses statBonuses;
    [ReadOnly]
    protected EquipmentSlot equipmentSlot;

    public void Awake()
    {
        isSingleSlot = true;
        itemType = ItemType.Equipment;
    }

    public EquipmentSlot GetSlot()
    {
        return equipmentSlot;
    }
    public StatBonuses GetStatBonuses() { return statBonuses; }
    [System.Serializable]
    public class StatBonuses : ReorderableArray<StatBonus>
    {
    }

    public override string ToString()
    {
        string message = string.Format("{0}\n", name);
        foreach (StatBonus bonus in statBonuses)
        {
            message += "\n";
            message += StatBonus.TypeToString(bonus.type) + " +";
            if (bonus.modifier.type == StatModType.Flat)
            {
                message += bonus.modifier.value;
            }
            else if (bonus.modifier.type == StatModType.Percent)
            {
                message += bonus.modifier.value + "%";
            }
        }
        return message;
    }
}