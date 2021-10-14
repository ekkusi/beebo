using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : StatsManager
{
    private EnemyUIManager uiManager;
    [SerializeField]
    private LootTable lootTable;
    [SerializeField]
    public GameObject groundItemPrefab;
    [SerializeField]
    private float experienceOnKill = 10;

    public override void Start()
    {
        base.Start();
        uiManager = GetComponent<EnemyUIManager>();
        uiManager.UpdateHealthBar();
    }
    public override bool TakeDamage(float attackDamage, GameObject source)
    {
        bool isDead = base.TakeDamage(attackDamage, source);
        uiManager.UpdateHealthBar();
        if (isDead)
        {
            PlayerExperienceManager playerExperience = source.GetComponent<PlayerExperienceManager>();
            if (playerExperience != null) {
                playerExperience.GainExperience(experienceOnKill);
            }
            DropLoot();
            Destroy(gameObject);
        }
        return isDead;
    }
    public override void Heal(float amount)
    {
        base.Heal(amount);
        uiManager.UpdateHealthBar();
    }

    public void DropLoot()
    {
        Debug.Log("Dropping loot");
        foreach (LootItem loot in lootTable.items)
        {
            // Debug.Log("Instantiating loot: " + loot.item.name + " with amount " + loot.amount);
            float dropProbability = Random.Range(0f, 1f);
            Debug.Log("Drop probability: " + dropProbability);
            if (loot.item.isSingleSlot)
            {
                for (int i = 0; i < loot.amount; i++)
                {
                    if (loot.probability >= dropProbability) {
                        Debug.Log("Dropping loot " + loot.item.name + " because drop probability: " + dropProbability + " is smaller than " + loot.probability);
                        GroundItemManager.InstantiateGroundItem(loot.item, transform.position);
                    }
                }
            }
            else
            {
                if (loot.probability >= dropProbability) {
                    Debug.Log("Dropping loot " + loot.item.name + " because drop probability: " + dropProbability + " is smaller than " + loot.probability);
                    GroundItemManager.InstantiateGroundItem(loot.item, transform.position, loot.amount);
                }
            }
        }
    }

}
