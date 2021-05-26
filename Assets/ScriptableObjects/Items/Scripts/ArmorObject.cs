using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType {
    Chest,
    Boots,
    Helmet,
    Ring,
    Pants,
    Cloak,
    Gloves
}
[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Equipment/Armor")]
public class ArmorObject : EquipmentObject
{
    public ArmorType armorType;
    public void Awake() {
        itemType = CustomItemType.Armor;
        armorType = ArmorType.Chest;
    }
}