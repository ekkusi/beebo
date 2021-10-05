using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Equipment
}
[Serializable]
public abstract class ItemObject : ScriptableObject
{
    public Sprite sprite;
    public ItemType itemType;
    [TextArea(15, 20)]
    public string description;
    public bool isSingleSlot = false;
    public int basePrice = 100;
    public bool isSellable = true;

    public override string ToString()
    {
        return string.Format("{0}\n{1}", name, description);
    }

    public string ToString(string appendToStart)
    {
        return appendToStart + ToString();
    }

    public string ToString(string appendToStart, string appendToEnd)
    {
        return appendToStart + ToString() + appendToEnd;
    }
}
