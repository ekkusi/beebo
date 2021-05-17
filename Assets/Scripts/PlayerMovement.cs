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
  void FixedUpdate()
  {
    Vector3 change = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

    if (change != Vector3.zero)
    {
      MovePlayer(change);
      animationManager.ChangeAnimationState(PlayerAnimationState.HeroWalk.ToString());
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
    // Vector3 vector = Vector3.Normalize();
    // float speedChange = speed / (float)Math.Sqrt(Math.Pow(change.x, 2) + Math.Pow(change.y, 2));
    rigidBody.MovePosition(transform.position + Vector3.Normalize(change) * speed * Time.deltaTime);

    // Rotate player based on movement
    float rotateDegrees = (float)Math.Atan2(change.y, change.x) * (float)(360 / (2 * Math.PI));
    // Vector3.Angle
    transform.eulerAngles = Vector3.forward * rotateDegrees;

  }
}
