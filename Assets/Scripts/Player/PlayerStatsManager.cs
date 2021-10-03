using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum StatType
{
    Strength,
    Agility,
    Intelligence,
    AttackDamage,
    Defence
}
public class PlayerStatsManager : MonoBehaviour
{
    public RectTransform statsPanel;
    public Stat strength { get; private set; }
    public Stat agility { get; private set; }
    public Stat intelligence { get; private set; }
    public Stat attackDamage { get; private set; }
    public Stat defence { get; private set; }

    public float baseStrength;
    public float baseAgility;
    public float baseIntelligence;
    public float baseAttackDamage;
    public float baseDefence;
    private TextMeshProUGUI strengthText;
    private TextMeshProUGUI agilityText;
    private TextMeshProUGUI intelligenceText;
    private TextMeshProUGUI attackDamageText;
    private TextMeshProUGUI defenceText;
    void Start()
    {
        strength = new Stat(baseStrength);
        agility = new Stat(baseAgility);
        intelligence = new Stat(baseIntelligence);
        attackDamage = new Stat(baseAttackDamage);
        defence = new Stat(baseDefence);
        Debug.Log("Strength at start: " + strength.value);
        Debug.Log("Base Strength at start: " + baseStrength);

        strengthText = statsPanel.Find("Strength").GetComponent<TextMeshProUGUI>();
        agilityText = statsPanel.Find("Agility").GetComponent<TextMeshProUGUI>();
        intelligenceText = statsPanel.Find("Intelligence").GetComponent<TextMeshProUGUI>();
        attackDamageText = statsPanel.Find("Attack Damage").GetComponent<TextMeshProUGUI>();
        defenceText = statsPanel.Find("Defence").GetComponent<TextMeshProUGUI>();

        SetStatTexts();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            statsPanel.gameObject.SetActive(!statsPanel.gameObject.activeInHierarchy);
        }
    }

    private void SetStatTexts()
    {
        strengthText.SetText(string.Format("Strength: {0}", strength.value));
        agilityText.SetText(string.Format("Agility: {0}", agility.value));
        intelligenceText.SetText(string.Format("Intelligence: {0}", intelligence.value));
        attackDamageText.SetText(string.Format("Attack damage: {0}", attackDamage.value));
        defenceText.SetText(string.Format("Defence: {0}", defence.value));
    }

    public void AddStatBonus(StatBonus bonus)
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
        }
        SetStatTexts();
    }
    public void RemoveStatBonus(StatBonus bonus)
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
        }
        SetStatTexts();
    }

    public override string ToString()
    {
        return string.Format("Stats: \n strength: {0} \n agility: {1} \n intelligence {2} \n attack damage: {3} \n defence {4}", strength.value, agility.value, intelligence.value, attackDamage.value, defence.value);
    }
}
