using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BlorBlorStateManager), typeof(StatsManager))]
public class BlorBlorController : EnemyController
{
    private BlorBlorStateManager stateManager;
    protected override bool isStateLocked { get { return stateManager.isStateLocked; } }

    public override void Start()
    {
        base.Start();
        stateManager = GetComponent<BlorBlorStateManager>();
    }

    public override void Attack()
    {
        base.Attack();
        stateManager.TriggerStateForLength(BlorBlorState.BlorBlorHit, statsManager.attackSpeed.value);
        rigidBody.isKinematic = true;

    }
    public override void Idle()
    {
        rigidBody.isKinematic = false;
        stateManager.ChangeState(BlorBlorState.BlorBlorIdle);
    }
    public override void Move()
    {
        base.Move();
        rigidBody.isKinematic = false;
        stateManager.ChangeState(BlorBlorState.BlorBlorIdle);
    }
}
