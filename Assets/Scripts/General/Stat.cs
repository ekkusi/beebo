using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public float baseValue;

    public float value
    {
        get
        {
            if (isDirty)
            {
                _value = CalculateFinalValue();
                isDirty = false;

            }
            return _value;
        }
    }

    private bool isDirty = true;
    private float _value;

    private readonly List<StatModifier> statModifiers;

    public Stat(float baseValue)
    {
        this.baseValue = baseValue;
        statModifiers = new List<StatModifier>();
    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort((a, b) =>
        {
            if (a.type == StatModType.Percent && b.type == StatModType.Flat)
            {
                return 1;
            }
            else if (a.type == StatModType.Flat && b.type == StatModType.Percent)
            {
                return -1;
            }
            return 0;
        });
    }


    public bool RemoveModifier(StatModifier mod)
    {
        isDirty = true;
        return statModifiers.Remove(mod);
    }

    private float CalculateFinalValue()
    {
        float finalValue = baseValue;

        foreach (StatModifier mod in statModifiers)
        {
            if (mod.type == StatModType.Flat)
            {
                finalValue += mod.value;
            }
            else if (mod.type == StatModType.Percent)
            {
                finalValue *= 1 + (mod.value / 100);
            }
        }

        return finalValue;
    }

    public void AddToBaseStat(float amount)
    {
        isDirty = true;
        baseValue += amount;
    }
}
