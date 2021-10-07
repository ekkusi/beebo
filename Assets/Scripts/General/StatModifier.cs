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
    public StatModifier(float value, StatModType type)
    {
        this.value = value; this.type = type;
    }
}
[System.Serializable]
public struct StatBonus
{
    public StatType type;
    public StatModifier modifier;
    public StatBonus(StatType type, StatModifier modifier)
    {
        this.type = type;
        this.modifier = modifier;
    }

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

