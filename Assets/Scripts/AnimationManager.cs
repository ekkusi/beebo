using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
  private Animator animator;
  private string currentState;
  // Start is called before the first frame update
  void Start()
  {
    animator = GetComponent<Animator>();
  }

  public void ChangeAnimationState(string newState)
  {
    if (newState == currentState) return;

    animator.Play(newState);

    currentState = newState;
  }
}
