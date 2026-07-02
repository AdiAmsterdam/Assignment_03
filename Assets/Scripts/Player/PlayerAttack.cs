using UnityEngine;
using DefaultNamespace;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public PlayerState playerStates;
    private PlayerMovement playerMovement;

    void Awake()
    {
        InitComponents();
    }

    private void InitComponents()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        HandleAttackInput();
    }

    private void HandleAttackInput()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            playerStates = PlayerState.Jab;
            playerMovement.AnimationControl(playerStates);
        }
    }

    public bool IsJabAnimationFinished(string animationName)
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
       /* 
        if (!info.IsName(animationName)) return true;
        
        if (info.normalizedTime >0.95f) return true;
        
        return false;
        */
       return !info.IsName(animationName) || info.normalizedTime > 0.95f;
    }

    public bool IsAttacking()
    {
        if(playerStates == PlayerState.Jab) return true;
        return false;
    }
}
