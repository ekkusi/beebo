using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Default,
    Equipment
}
[Serializable]
public abstract class ItemObject : ScriptableObject {
    public Sprite sprite;
    public ItemType itemType;
    [TextArea(15, 20)]
    public string description;
    public bool isSingleSlot = false;
}
