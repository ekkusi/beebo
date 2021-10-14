using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStatsManager : StatsManager
{
    private PlayerEquipmentManager equipmentManager;
    [SerializeField]
    private RectTransform statsPanel;
    [SerializeField]
    private Image hpBarFront;
    [SerializeField]
    private Image manaBarFront;
    [SerializeField]
    private Image hpBarBack;
    [SerializeField]
    private Image manaBarBack;
    [SerializeField]
    private float barChipLength = 1f;
    private float manaLerpTimer = 0f;
    private float hpLerpTimer = 0f;
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
    private bool isInitialized;
    public override void Start()
    {
        base.Start();
        equipmentManager = GetComponent<PlayerEquipmentManager>();

        RectTransform panelItems = statsPanel.Find("Items").GetComponent<RectTransform>();
        levelText = panelItems.Find("Level Title").GetComponent<TextMeshProUGUI>();
        addPointsText = panelItems.Find("Add Points Text").GetComponent<TextMeshProUGUI>();
        strengthText = panelItems.Find("Strength").Find("Stat Label").GetComponent<TextMeshProUGUI>();
        agilityText = panelItems.Find("Agility").Find("Stat Label").GetComponent<TextMeshProUGUI>();
        intelligenceText = panelItems.Find("Intelligence").Find("Stat Label").GetComponent<TextMeshProUGUI>();
        attackDamageText = panelItems.Find("Attack Damage").GetComponent<TextMeshProUGUI>();
        defenceText = panelItems.Find("Defence").GetComponent<TextMeshProUGUI>();
        healthText = panelItems.Find("Health").GetComponent<TextMeshProUGUI>();
        manaText = panelItems.Find("Mana").GetComponent<TextMeshProUGUI>();

        strengthAddButton = panelItems.Find("Strength").Find("Increase Button").GetComponent<Button>();
        agilityAddButton = panelItems.Find("Agility").Find("Increase Button").GetComponent<Button>();
        intelligenceAddButton = panelItems.Find("Intelligence").Find("Increase Button").GetComponent<Button>();

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
        statsPanel.gameObject.SetActive(false);

        isInitialized = true;
        SetStatTexts();
        UpdateHpBar();
        UpdateManaBar();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            statsPanel.gameObject.SetActive(!statsPanel.gameObject.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GainMana(20);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            UseMana(20);
        }
        UpdateBarsUI(hpBarFront, hpBarBack, currentHealth / health.value, hpLerpTimer);
        UpdateBarsUI(manaBarFront, manaBarBack, currentMana / mana.value, manaLerpTimer);
    }

    public void SetStatTexts()
    {
        if (isInitialized)
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

    public override void LevelUp()
    {
        base.LevelUp();
        pointsToAdd += statPointsInLevelUp;
        SetStatTexts();
    }

    public float GetAttackSpeed()
    {
        WeaponObject playerWeapon = (WeaponObject)equipmentManager.GetEquippedItem(EquipmentSlot.WeaponMainHand);
        float weaponAttackSpeed = playerWeapon?.attackSpeed ?? 1.0f;
        return weaponAttackSpeed * attackSpeed.value;
    }

    public override bool TakeDamage(float attackDamage, GameObject source)
    {
        bool isPlayerDead = base.TakeDamage(attackDamage, source);
        hpLerpTimer = 0f;
        return isPlayerDead;
    }

    public override void Heal(float amount)
    {
        base.Heal(amount);
        hpLerpTimer = 0f;
    }

    public override void UseMana(float amount)
    {
        base.UseMana(amount);
        manaLerpTimer = 0f;
    }

    public override void GainMana(float amount)
    {
        base.GainMana(amount);
        manaLerpTimer = 0f;
    }
    public void UpdateHpBar()
    {
        hpBarFront.fillAmount = currentHealth / health.value;
    }

    public void UpdateBarsUI(Image frontBar, Image backBar, float currentFraction, float lerpTimer)
    {
        float filledFront = frontBar.fillAmount;
        float filledBack = backBar.fillAmount;
        if (filledBack > currentFraction)
        {
            frontBar.fillAmount = currentFraction;
            backBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / barChipLength;
            backBar.fillAmount = Mathf.Lerp(filledBack, currentFraction, percentComplete);
        }
        else if (filledFront < currentFraction)
        {
            backBar.fillAmount = currentFraction;
            backBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / barChipLength;
            frontBar.fillAmount = Mathf.Lerp(filledFront, currentFraction, percentComplete);
        }
    }

    public void UpdateManaUI()
    {

    }

    public void UpdateManaBar()
    {
        manaBarFront.fillAmount = currentMana / mana.value;
    }
}
