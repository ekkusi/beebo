using System;
using System.Collections;
using System.Collections.Generic;
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
public class PlayerStateManager : StateManager<PlayerState> { }
