using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperienceManager : MonoBehaviour
{
    public int level = 1;
    private PlayerStatsManager playerStatsManager;
    public float currentXp = 0f;
    public float requiredXp = 100f;
    public float additionMultiplier = 300f;
    public float powerMultiplier = 2f;
    public float divisionMultiplier = 7f;
    public int statPointsInLevelUp = 3;
    // Start is called before the first frame update
    void Start()
    {
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GainExperience(level * 20);
        }
    }

    public void GainExperience(float xpGained)
    {
        currentXp += xpGained;
        if (currentXp >= requiredXp)
        {
            float surplus = currentXp - requiredXp;
            LevelUp();
            GainExperience(surplus);
        }
    }

    public void LevelUp()
    {
        level++;
        currentXp = 0;
        CalculateNewRequiredXp();
        playerStatsManager.IncreasePointsToAdd(statPointsInLevelUp);
        playerStatsManager.SetStatTexts();
    }

    public void CalculateNewRequiredXp()
    {
        requiredXp += Mathf.Floor(level - 1 + additionMultiplier * (powerMultiplier * ((level - 1) / divisionMultiplier)));
    }
}
