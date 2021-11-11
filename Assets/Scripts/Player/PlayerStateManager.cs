using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public enum PlayerState
{
    HeroIdle,
    HeroWalk,
    HeroWalkWithBasicSpear,
    HeroWalkWithBasicSword,
    HeroHit,
    HeroHitWithBasicSpear,
    HeroHitWithBasicSword,
}
public class PlayerStateManager : StateManager<PlayerState>
{
    [PunRPC]
    protected override void OnStateChanged(PlayerState state)
    {
        Debug.Log("Triggering on state changed");
        base.OnStateChanged(state);
    }

    [PunRPC]
    protected override void OnTriggerStateForLength(PlayerState state, float length)
    {
        base.OnStateChanged(state);
    }
}
