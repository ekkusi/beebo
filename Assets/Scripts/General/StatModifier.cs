using System.Collections.Generic;

public enum StatModType
{
    Flat,
    Percent
}

[System.Serializable]
public struct StatModifier
{
    public float value;
    public StatModType type;
}
[System.Serializable]
public struct StatBonus
{
    public StatType type;
    public StatModifier modifier;
}

