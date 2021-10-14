using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatsManager), typeof(Rigidbody2D), typeof(Animator))]
public abstract class EnemyController : MonoBehaviour
{
    protected Rigidbody2D rigidBody;
    protected StatsManager statsManager;
    public Transform playerInMoveDistance { get; set; }
    public bool isPlayerInHitDistance { get; set; }
    protected abstract bool isStateLocked { get; }
    // Start is called before the first frame update

    public virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        statsManager = GetComponent<StatsManager>();
        rigidBody.freezeRotation = true;
    }


    public virtual void FixedUpdate()
    {
        rigidBody.velocity = Vector2.zero;
        if (!isStateLocked)
        {
            if (isPlayerInHitDistance)
            {
                Attack();
            }
            else if (playerInMoveDistance != null)
            {
                Move();
            }
            else
            {
                Idle();
            }
        }
    }

    public virtual void Move()
    {
        TurnTowardsPlayer();
        Vector3? directionToPlayer = GetDirectionToPlayer();
        if (directionToPlayer != null)
        {
            rigidBody.MovePosition(transform.position + Vector3.Normalize((Vector3)directionToPlayer) * statsManager.movementSpeed.value * Time.deltaTime);
        }
    }
    public virtual void Attack()
    {
        TurnTowardsPlayer();
    }
    public abstract void Idle();

    public Vector2? GetDirectionToPlayer()
    {
        if (playerInMoveDistance)
        {
            Vector2 playerPos = playerInMoveDistance.position;
            Vector2 enemyPos = transform.position;
            return new Vector3(playerPos.x - enemyPos.x, playerPos.y - enemyPos.y, 0);
        }
        return null;
    }

    public void TurnTowardsPlayer()
    {
        Vector3? directionToPlayer = GetDirectionToPlayer();
        if (directionToPlayer != null)
        {
            transform.eulerAngles = MathUtils.DirectionInEuler((Vector3)directionToPlayer);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController.canTakeDamage)
            {
                playerController.TakeDamage(statsManager.attackDamage.value, gameObject);
                isPlayerInHitDistance = false;
                playerInMoveDistance = null;
            }
        }
    }
}
