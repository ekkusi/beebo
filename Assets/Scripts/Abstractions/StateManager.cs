using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class StateManager<StateEnum> : MonoBehaviour
{
    private StateEnum state;
    public bool isStateLocked { get { return stateLockedTime > 0; } }
    private Animator animator;
    private float stateLockedTime = 0f;

    private bool isDefaultAnimSpeed = false;
    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (isStateLocked)
        {
            stateLockedTime -= Time.deltaTime;
        }
        else if (!isDefaultAnimSpeed)
        {
            isDefaultAnimSpeed = true;
            SetAnimationSpeed();
        }
    }

    public void ChangeState(StateEnum newState, float stateLockTime = 0f)
    {
        // Only play animation if state isn't locked and new state isn't the same animation
        if (!isStateLocked && !newState.Equals(state))
        {
            animator.Play(newState.ToString(), 0);
            state = newState;
        }
        stateLockedTime = stateLockTime;
    }

    public void TriggerStateForLength(StateEnum newState, float length)
    {
        AnimationClip animation = GetAnimationClip(newState);
        float animationLength = animation?.length ?? 0;
        float animSpeed = animationLength / length;
        isDefaultAnimSpeed = false;
        SetAnimationSpeed(animSpeed);
        ChangeState(newState, length);
    }

    public AnimationClip GetAnimationClip(StateEnum state)
    {

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == state.ToString())
            {
                return clip;
            }
        }
        return null; // no clip by that name
    }

    public void SetAnimationSpeed(float speed = 1.0f)
    {
        animator.SetFloat("animSpeed", speed);
    }
}
