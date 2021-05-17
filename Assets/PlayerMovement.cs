using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerAnimationState
{
  HeroIdle,
  HeroWalk,
  HeroWalkWithBasicSpear,
  HeroWalkWithBasicSword,
  HeroHit,
  HeroHitWithBasicSpear,
  HeroHitWithBasicSword,
}
public class PlayerMovement : MonoBehaviour
{
  public float speed = 5f;

  private Rigidbody2D rigidBody;
  private Animator walkAnimation;
  private AnimationManager animationManager;
  // Start is called before the first frame update
  void Start()
  {
    rigidBody = GetComponent<Rigidbody2D>();
    walkAnimation = GetComponent<Animator>();
    animationManager = GetComponent<AnimationManager>();
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 change = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

    if (change != Vector3.zero)
    {
      MovePlayer(change);
      animationManager.ChangeAnimationState(PlayerAnimationState.HeroWalk.ToString());
      Debug.Log(PlayerAnimationState.HeroWalk.ToString());
    }
    else
    {
      animationManager.ChangeAnimationState(PlayerAnimationState.HeroIdle.ToString());
    }
    // else
    // {
    //   walkAnimation.Stop();
    // }
  }

  void MovePlayer(Vector3 change)
  {
    // Move player, go slower if movement is bidirectional
    float speedChange = speed;
    if (change.x != 0 && change.y != 0)
    {
      speedChange *= 0.75f;
    }
    rigidBody.MovePosition(transform.position + change * speedChange * Time.deltaTime);

    // Rotate player based on movement
    float rotateDegrees;
    if (change.x < 0)
    {
      rotateDegrees = (change.x < 0 ? 180 : 0) - change.y * 90;
      Debug.Log(rotateDegrees);
      if (change.y == -1) rotateDegrees -= 45;
      else if (change.y == 1) rotateDegrees += 45;
    }
    else
    {
      rotateDegrees = change.y * 90;
      if (change.x == 1 && change.y == -1) rotateDegrees += 45;
      else if (change.x == 1 && change.y == 1) rotateDegrees -= 45;
    }
    transform.eulerAngles = Vector3.forward * rotateDegrees;

  }
}
