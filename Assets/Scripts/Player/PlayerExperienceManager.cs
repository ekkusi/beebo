using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperienceManager : MonoBehaviour
{
    public int level = 1;
    public float currentXp = 0f;
    public float requiredXp = 100f;
    public float additionMultiplier = 300f;
    public float powerMultiplier = 2f;
    public float divisionMultiplier = 7f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GainExperience(float xpGained)
    {
        currentXp += xpGained;
        if (currentXp > requiredXp)
        {
            LevelUp();
            float surplus = currentXp - requiredXp;
            GainExperience(surplus);
        }
    }

    public void LevelUp()
    {
        level++;
        currentXp = 0;
        CalculateNewRequiredXp();
    }

    public void CalculateNewRequiredXp()
    {
        requiredXp += Mathf.Floor(level - 1 + additionMultiplier * (powerMultiplier * ((level - 1) / divisionMultiplier)));
    }
}
