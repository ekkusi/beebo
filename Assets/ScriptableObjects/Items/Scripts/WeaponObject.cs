﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    Sword,
    Axe,
    Spear
}
[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Equipment/Weapon")]
public class WeaponObject : EquipmentObject
{
    public WeaponType weaponType;
    public float attackSpeed = 1f;
    public void Awake() {
        itemType = CustomItemType.Weapon;
        weaponType = WeaponType.Sword;
    }
}