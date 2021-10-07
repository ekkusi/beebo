using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[SerializeField]
public enum StatType
{
    Strength,
    Agility,
    Intelligence,
    Health,
    Mana,
    AttackDamage,
    Defence
}
public class StatsManager : MonoBehaviour
{
    public Stat strength { get; private set; }
    public Stat agility { get; private set; }
    public Stat intelligence { get; private set; }
    public Stat health { get; private set; }
    public Stat mana { get; private set; }
    public Stat attackDamage { get; private set; }
    public Stat defence { get; private set; }

    public float baseStrength = 5;
    public float baseAgility = 5;
    public float baseIntelligence = 5;
    public float baseAttackDamage = 0;
    public float baseDefence = 0;
    public float baseHealth = 100;
    public float baseMana = 100;
    public int level = 1;
    public virtual void Start()
    {
        strength = new Stat(baseStrength);
        agility = new Stat(baseAgility);
        intelligence = new Stat(baseIntelligence);
        attackDamage = new Stat(baseAttackDamage);
        defence = new Stat(baseDefence);
        health = new Stat(baseHealth);
        mana = new Stat(baseMana);

        // Add level effect to health
        for (int i = 0; i < level; i++)
        {
            AddLevelToHealth();
        }
    }

    public virtual void AddStatBonus(StatBonus bonus)
    {
        switch (bonus.type)
        {
            case StatType.Strength:
                strength.AddModifier(bonus.modifier);
                break;
            case StatType.Agility:
                agility.AddModifier(bonus.modifier);
                break;
            case StatType.Intelligence:
                intelligence.AddModifier(bonus.modifier);
                break;
            case StatType.AttackDamage:
                attackDamage.AddModifier(bonus.modifier);
                break;
            case StatType.Defence:
                defence.AddModifier(bonus.modifier);
                break;
            case StatType.Health:
                health.AddModifier(bonus.modifier);
                break;
            case StatType.Mana:
                mana.AddModifier(bonus.modifier);
                break;
        }
    }
    public virtual void RemoveStatBonus(StatBonus bonus)
    {
        switch (bonus.type)
        {
            case StatType.Strength:
                strength.RemoveModifier(bonus.modifier);
                break;
            case StatType.Agility:
                agility.RemoveModifier(bonus.modifier);
                break;
            case StatType.Intelligence:
                intelligence.RemoveModifier(bonus.modifier);
                break;
            case StatType.AttackDamage:
                attackDamage.RemoveModifier(bonus.modifier);
                break;
            case StatType.Defence:
                defence.RemoveModifier(bonus.modifier);
                break;
            case StatType.Health:
                health.RemoveModifier(bonus.modifier);
                break;
            case StatType.Mana:
                mana.RemoveModifier(bonus.modifier);
                break;
        }
    }
    public virtual void AddToBaseStat(StatType statType, int amount)
    {
        switch (statType)
        {
            case StatType.Strength:
                strength.AddToBaseStat(amount);
                break;
            case StatType.Agility:
                agility.AddToBaseStat(amount);
                break;
            case StatType.Intelligence:
                intelligence.AddToBaseStat(amount);
                break;
            case StatType.AttackDamage:
                attackDamage.AddToBaseStat(amount);
                break;
            case StatType.Defence:
                defence.AddToBaseStat(amount);
                break;
            case StatType.Health:
                health.AddToBaseStat(amount);
                break;
            case StatType.Mana:
                mana.AddToBaseStat(amount);
                break;
        }
    }

    public override string ToString()
    {
        return string.Format("Stats: \n strength: {0} \n agility: {1} \n intelligence {2} \n attack damage: {3} \n defence {4}", strength.value, agility.value, intelligence.value, attackDamage.value, defence.value);
    }

    public virtual void LevelUp()
    {
        level++;
    }

    public virtual void AddLevelToHealth()
    {
        // Add 10% hp increase on every level
        AddStatBonus(new StatBonus(StatType.Health, new StatModifier(10, StatModType.Percent)));
    }
}
