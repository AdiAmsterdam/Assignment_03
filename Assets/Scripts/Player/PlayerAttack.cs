using UnityEngine;
using DefaultNamespace;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerState playerStates;
    private PlayerMovement playerControl;

    void Awake()
    {
        InitComponents();
    }

    private void InitComponents()
    {
        anim = GetComponent<Animator>();
        playerControl = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            playerStates = PlayerState.Jab;
            playerControl.AnimationControl(playerStates);
        }
        else playerStates = PlayerState.Idle;
    }
    
    public bool IsJabAnimationFinished(string animationName)
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        
        if (!info.IsName(animationName)) return true;
        
        if (info.normalizedTime >0.95f) return true;
        
        return false;
    }
}
