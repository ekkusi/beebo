using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Malee.List;

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
}