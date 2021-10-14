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
    Defence,
    AttackSpeed,
    MovementSpeed
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
    public Stat attackSpeed { get; private set; }
    public Stat movementSpeed { get; private set; }

    public float baseStrength = 5f;
    public float baseAgility = 5f;
    public float baseIntelligence = 5f;
    public float baseAttackDamage = 10f;
    public float baseDefence = 10f;
    public float baseAttackSpeed = 1.0f;
    public float baseHealth = 100f;
    public float baseMana = 100f;
    public float baseMoveSpeed = 1.0f;
    public float currentHealth { get; private set; }
    public float currentMana { get; private set; }
    public int level = 1;
    public virtual void Start()
    {
        strength = new Stat(baseStrength);
        agility = new Stat(baseAgility);
        intelligence = new Stat(baseIntelligence);
        attackDamage = new Stat(baseAttackDamage);
        attackSpeed = new Stat(baseAttackSpeed);
        defence = new Stat(baseDefence);
        health = new Stat(baseHealth);
        mana = new Stat(baseMana);
        movementSpeed = new Stat(baseMoveSpeed);

        // // Add level effect to health
        for (int i = 1; i < level; i++)
        {
            AddLevelToHealth();
        }
        currentHealth = health.value;
        currentMana = mana.value;
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
                float currentHealthPercent = currentHealth / health.value;
                health.AddModifier(bonus.modifier);
                currentHealth = health.value * currentHealthPercent;
                break;
            case StatType.Mana:
                float currentManaPercent = currentMana / mana.value;
                mana.AddModifier(bonus.modifier);
                currentMana = mana.value / currentManaPercent;
                break;
            case StatType.AttackSpeed:
                attackSpeed.AddModifier(bonus.modifier);
                break;
            case StatType.MovementSpeed:
                movementSpeed.AddModifier(bonus.modifier);
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
                float currentHealthPercent = currentHealth / health.value;
                health.RemoveModifier(bonus.modifier);
                currentHealth = health.value * currentHealthPercent;
                break;
            case StatType.Mana:
                float currentManaPercent = currentMana / mana.value;
                mana.RemoveModifier(bonus.modifier);
                currentMana = mana.value / currentManaPercent;
                break;
            case StatType.AttackSpeed:
                attackSpeed.RemoveModifier(bonus.modifier);
                break;
            case StatType.MovementSpeed:
                movementSpeed.RemoveModifier(bonus.modifier);
                break;
        }
    }
    public virtual void AddToBaseStat(StatType statType, float amount)
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
                float currentHealthPercent = currentHealth / health.value;
                health.AddToBaseStat(amount);
                currentHealth = health.value * currentHealthPercent;
                break;
            case StatType.Mana:
                float currentManaPercent = currentMana / mana.value;
                mana.AddToBaseStat(amount);
                currentMana = mana.value / currentManaPercent;
                break;
            case StatType.AttackSpeed:
                attackSpeed.AddToBaseStat(amount);
                break;
            case StatType.MovementSpeed:
                movementSpeed.AddToBaseStat(amount);
                break;
        }
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

    // Returns true if current health goes to zero => object dies
    public virtual bool TakeDamage(float attackDamage, GameObject source)
    {
        // Formula from https://gamedev.stackexchange.com/questions/129319/rpg-formula-attack-and-defense
        float damageTaken = attackDamage * attackDamage / (attackDamage + defence.value);
        Debug.Log("Hit with damage: " + attackDamage);
        Debug.Log("Taking hit for : " + damageTaken);
        if (currentHealth > damageTaken)
        {
            currentHealth -= damageTaken;
            return false;
        }
        currentHealth = 0;
        return true;
    }


    public virtual void Heal(float amount)
    {
        if ((health.value - currentHealth) > amount)
        {
            currentHealth += amount;
        }
        else
        {
            currentHealth = health.value;
        }
    }
    public virtual void UseMana(float amount)
    {
        if (amount > currentMana) throw new NotEnoughManaException();
        currentMana -= amount;
    }
    public virtual void GainMana(float amount)
    {

        if ((mana.value - currentMana) > amount)
        {
            currentMana += amount;
        }
        else
        {
            currentMana = health.value;
        }
    }
}
