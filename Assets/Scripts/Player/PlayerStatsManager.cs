using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStatsManager : StatsManager
{
    public RectTransform statsPanel;
    private TextMeshProUGUI levelText;
    private TextMeshProUGUI addPointsText;
    private TextMeshProUGUI strengthText;
    private TextMeshProUGUI agilityText;
    private TextMeshProUGUI intelligenceText;
    private TextMeshProUGUI attackDamageText;
    private TextMeshProUGUI defenceText;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI manaText;
    private Button strengthAddButton;
    private Button agilityAddButton;
    private Button intelligenceAddButton;
    private int pointsToAdd;
    [SerializeField]
    private int statPointsInLevelUp = 3;
    public override void Start()
    {
        base.Start();

        levelText = statsPanel.Find("Level Title").GetComponent<TextMeshProUGUI>();
        addPointsText = statsPanel.Find("Add Points Text").GetComponent<TextMeshProUGUI>();
        strengthText = statsPanel.Find("Strength").Find("Stat Label").GetComponent<TextMeshProUGUI>();
        agilityText = statsPanel.Find("Agility").Find("Stat Label").GetComponent<TextMeshProUGUI>();
        intelligenceText = statsPanel.Find("Intelligence").Find("Stat Label").GetComponent<TextMeshProUGUI>();
        attackDamageText = statsPanel.Find("Attack Damage").GetComponent<TextMeshProUGUI>();
        defenceText = statsPanel.Find("Defence").GetComponent<TextMeshProUGUI>();
        healthText = statsPanel.Find("Health").GetComponent<TextMeshProUGUI>();
        manaText = statsPanel.Find("Mana").GetComponent<TextMeshProUGUI>();

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
        levelText.SetText(string.Format("Level: {0}", level));
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
        healthText.SetText(string.Format("Health: {0}", health.value));
        manaText.SetText(string.Format("Mana: {0}", mana.value));
    }

    public override void AddStatBonus(StatBonus bonus)
    {
        base.AddStatBonus(bonus);
        SetStatTexts();
    }
    public override void RemoveStatBonus(StatBonus bonus)
    {
        base.RemoveStatBonus(bonus);
        SetStatTexts();
    }
    public void IncreaseBaseStat(StatType statType)
    {
        if (pointsToAdd > 0)
        {
            base.AddToBaseStat(statType, 1);
            pointsToAdd--;
        }
        SetStatTexts();
    }

    public override string ToString()
    {
        return string.Format("Stats: \n strength: {0} \n agility: {1} \n intelligence {2} \n attack damage: {3} \n defence {4}", strength.value, agility.value, intelligence.value, attackDamage.value, defence.value);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        pointsToAdd += statPointsInLevelUp;
        SetStatTexts();
    }
}
