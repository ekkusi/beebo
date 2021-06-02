using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class StateManager<StateEnum> : MonoBehaviour
{
  private StateEnum state;
  private bool isStateLocked;
  private Animator animator;

  public void Start()
  {
    animator = GetComponent<Animator>();
  }

  public void ChangeState(StateEnum newState, float duration = 0f)
  {
    if (isStateLocked || newState.Equals(state)) return;
    Debug.Log("Playing new animation: " + newState);
    animator.Play(newState.ToString());
    state = newState;
  }
}
