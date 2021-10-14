using System;
using System.Collections;
using System.Collections.Generic;
using Malee.List;
using UnityEngine;

[Serializable]
public class LootTable
{
    [SerializeField, Reorderable]
    public LootItems items;
    [Serializable]
    public class LootItems : ReorderableArray<LootItem> { }
}

[Serializable]
public class LootItem
{
    public ItemObject item;
    [Range(0, 1)]
    public float probability;
    public int amount;
}
