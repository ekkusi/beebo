using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperienceManager : MonoBehaviour
{
    private PlayerStatsManager playerStatsManager;
    public float currentXp = 0f;
    public float requiredXp = 100f;
    public float additionMultiplier = 300f;
    public float powerMultiplier = 2f;
    public float divisionMultiplier = 7f;
    public Image expBarFront;
    public Image expBarBack;
    public TextMeshProUGUI levelTitle;
    private float lerpTimer;
    private float delayTimer;
    // Start is called before the first frame update
    void Start()
    {
        playerStatsManager = GetComponent<PlayerStatsManager>();
        expBarFront.fillAmount = currentXp / requiredXp;
        expBarBack.fillAmount = currentXp / requiredXp;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXpUI();
        if (Input.GetKeyDown(KeyCode.H))
        {
            GainExperience(playerStatsManager.level * 20);
        }
    }

    void UpdateXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float filledXp = expBarFront.fillAmount;
        if (filledXp < xpFraction)
        {
            delayTimer += Time.deltaTime;
            expBarBack.fillAmount = xpFraction;
            if (delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                expBarFront.fillAmount = Mathf.Lerp(filledXp, xpFraction, percentComplete);
            }
        }
    }

    public void GainExperience(float xpGained)
    {
        currentXp += xpGained;
        lerpTimer = 0f;
        delayTimer = 0f;
        if (currentXp >= requiredXp)
        {
            float surplus = currentXp - requiredXp;
            LevelUp();
            GainExperience(surplus);
        }
    }

    public void LevelUp()
    {
        currentXp = 0;
        expBarBack.fillAmount = 0f;
        expBarFront.fillAmount = 0f;
        CalculateNewRequiredXp();
        playerStatsManager.LevelUp();
        playerStatsManager.SetStatTexts();
        levelTitle.SetText(playerStatsManager.level.ToString());
    }

    public void CalculateNewRequiredXp()
    {
        int level = playerStatsManager.level;
        requiredXp += Mathf.Floor(level - 1 + additionMultiplier * (powerMultiplier * ((level - 1) / divisionMultiplier)));
    }
}
