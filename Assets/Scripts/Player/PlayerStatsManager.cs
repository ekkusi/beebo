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
    private PlayerExperienceManager playerExperienceManager;
    private TextMeshProUGUI levelText;
    private TextMeshProUGUI addPointsText;
    private TextMeshProUGUI strengthText;
    private TextMeshProUGUI agilityText;
    private TextMeshProUGUI intelligenceText;
    private TextMeshProUGUI attackDamageText;
    private TextMeshProUGUI defenceText;
    private Button strengthAddButton;
    private Button agilityAddButton;
    private Button intelligenceAddButton;
    private int pointsToAdd;
    void Start()
    {
        playerExperienceManager = GetComponent<PlayerExperienceManager>();
        strength = new Stat(baseStrength);
        agility = new Stat(baseAgility);
        intelligence = new Stat(baseIntelligence);
        attackDamage = new Stat(baseAttackDamage);
        defence = new Stat(baseDefence);

        levelText = statsPanel.Find("Level Title").GetComponent<TextMeshProUGUI>();
        addPointsText = statsPanel.Find("Add Points Text").GetComponent<TextMeshProUGUI>();
        strengthText = statsPanel.Find("Strength").Find("Stat Label").GetComponent<TextMeshProUGUI>();
        agilityText = statsPanel.Find("Agility").Find("Stat Label").GetComponent<TextMeshProUGUI>();
        intelligenceText = statsPanel.Find("Intelligence").Find("Stat Label").GetComponent<TextMeshProUGUI>();
        attackDamageText = statsPanel.Find("Attack Damage").GetComponent<TextMeshProUGUI>();
        defenceText = statsPanel.Find("Defence").GetComponent<TextMeshProUGUI>();

        strengthAddButton = statsPanel.Find("Strength").Find("Increase Button").GetComponent<Button>();
        agilityAddButton = statsPanel.Find("Agility").Find("Increase Button").GetComponent<Button>();
        intelligenceAddButton = statsPanel.Find("Intelligence").Find("Increase Button").GetComponent<Button>();

        strengthAddButton.onClick.AddListener(() =>
        {
            IncreaseBaseStat(StatType.Strength);
        });
        agilityAddButton.onClick.AddListener(() =>
        {
            IncreaseBaseStat(StatType.Agility);
        });
        intelligenceAddButton.onClick.AddListener(() =>
        {
            IncreaseBaseStat(StatType.Intelligence);
        });

        SetStatTexts();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            statsPanel.gameObject.SetActive(!statsPanel.gameObject.activeInHierarchy);
        }
    }

    public void SetStatTexts()
    {
        levelText.SetText(string.Format("Level: {0}", playerExperienceManager.level));
        if (pointsToAdd > 0)
        {
            addPointsText.gameObject.SetActive(true);
            addPointsText.SetText(string.Format("Level up! {0} points to add", pointsToAdd));
            strengthAddButton.gameObject.SetActive(true);
            agilityAddButton.gameObject.SetActive(true);
            intelligenceAddButton.gameObject.SetActive(true);
        }
        else
        {
            addPointsText.gameObject.SetActive(false);
            strengthAddButton.gameObject.SetActive(false);
            agilityAddButton.gameObject.SetActive(false);
            intelligenceAddButton.gameObject.SetActive(false);
        }
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
    public void IncreaseBaseStat(StatType statType)
    {
        if (pointsToAdd > 0)
        {
            switch (statType)
            {
                case StatType.Strength:
                    strength.AddToBaseStat(1);
                    break;
                case StatType.Agility:
                    agility.AddToBaseStat(1);
                    break;
                case StatType.Intelligence:
                    intelligence.AddToBaseStat(1);
                    break;
                case StatType.AttackDamage:
                    attackDamage.AddToBaseStat(1);
                    break;
                case StatType.Defence:
                    defence.AddToBaseStat(1);
                    break;
            }
            pointsToAdd--;
        }
        SetStatTexts();
    }

    public override string ToString()
    {
        return string.Format("Stats: \n strength: {0} \n agility: {1} \n intelligence {2} \n attack damage: {3} \n defence {4}", strength.value, agility.value, intelligence.value, attackDamage.value, defence.value);
    }

    public void IncreasePointsToAdd(int amount)
    {
        pointsToAdd += amount;
        SetStatTexts();
    }
}
