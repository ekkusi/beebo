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

    public static string TypeToString(StatType type)
    {
        switch (type)
        {
            case StatType.Strength:
                return "Strength";
            case StatType.Agility:
                return "Agility";
            case StatType.Intelligence:
                return "Intelligence";
            case StatType.AttackDamage:
                return "Attack Damage";
            case StatType.Defence:
                return "Defence";
        }
        return "Unknown";
    }
}

