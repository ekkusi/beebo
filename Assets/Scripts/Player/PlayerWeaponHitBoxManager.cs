using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHitBoxManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private PlayerStatsManager statsManager;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something entered player trigger: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StatsManager enemyStats = collision.gameObject.GetComponent<StatsManager>();
            enemyStats.TakeDamage(statsManager.attackDamage.value, statsManager.gameObject);
        }
    }
}
