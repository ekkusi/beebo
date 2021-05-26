using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomItemType {
    Default,
    Armor,
    Weapon
}
[Serializable]
public abstract class ItemObject : ScriptableObject {
    public Sprite sprite;
    public CustomItemType itemType;
    [TextArea(15, 20)]
    public string description;
}
