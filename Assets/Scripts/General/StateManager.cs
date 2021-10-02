using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class StateManager<StateEnum> : MonoBehaviour
{
  private StateEnum state;
  private readonly bool isStateLocked;
  private Animator animator;

  public void Start()
  {
    animator = GetComponent<Animator>();
  }

  public void ChangeState(StateEnum newState)
  {
    if (isStateLocked || newState.Equals(state)) return;
    Debug.Log("Playing new animation: " + newState);
    animator.Play(newState.ToString());
    state = newState;
  }
}
