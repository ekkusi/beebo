using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int attackBonus;
    public int defenseBonus;
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
}