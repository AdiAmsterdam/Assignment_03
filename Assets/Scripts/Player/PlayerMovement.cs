using UnityEngine;
using DefaultNamespace;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private PlayerState playerStates;
    private Vector3 originalScale;
    private PlayerAttack attack;
    
    //movement components
    private string walkingTrigger = "isWalking";
    private string jabTrigger = "jab";
    private string hurtTrigger = "hurt";
    private bool isWASD_Pressed;
    public float speed = 1.0f;
    void Awake()
    {
        InitComponents();
    }

    private void InitComponents()
    {
        anim =  GetComponent<Animator>();
        attack = GetComponent<PlayerAttack>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        isWASD_Pressed = false;
        if (!attack.IsJabAnimationFinished("Jab")) return;
        if (Keyboard.current.dKey.isPressed)
        {
            MovePlayer(1, 0);
            FlipCharacter(false);
            isWASD_Pressed = true;
        }
        
        if (Keyboard.current.aKey.isPressed)
        {
            MovePlayer(-1, 0);
            FlipCharacter(true);
            isWASD_Pressed = true;
        }
        
        if (Keyboard.current.wKey.isPressed)
        {
            MovePlayer(0, 1);
            isWASD_Pressed = true;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            
            MovePlayer(0, -1);
            isWASD_Pressed = true;
        }

        if (!isWASD_Pressed) //If WASD arent pressed
        {
            MovePlayer(0, 0);
        }
        
    }

    private void MovePlayer(int horizontal, int vertical)
    {
        if (horizontal == 0 && vertical == 0)
        {
            playerStates = PlayerState.Idle;
            AnimationControl(playerStates);
            return;
        }
        Vector3 offset = Vector3.zero;
        
        if (horizontal > 0)
            offset = Vector3.right * (speed * Time.deltaTime);

        if (horizontal < 0)
            offset = Vector3.left * (speed * Time.deltaTime);
        
        if (vertical > 0)
            offset = Vector3.up * (speed * Time.deltaTime);

        if (vertical < 0)
            offset = Vector3.down * (speed * Time.deltaTime);
        transform.Translate(offset);
        playerStates = PlayerState.Walk;
        AnimationControl(playerStates);
    }
    
    void FlipCharacter(bool flip)
    {
        if (flip)
        {
            transform.localScale = new Vector3(
                -originalScale.x,
                originalScale.y,
                originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(
                originalScale.x,
                originalScale.y,
                originalScale.z);
        }
    }

    public void AnimationControl(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                anim.SetBool(walkingTrigger, false);
                break;
            case PlayerState.Walk:
                anim.SetBool(walkingTrigger, true);
                break;
            case PlayerState.Jab:
                anim.SetTrigger(jabTrigger);
                break;
            case PlayerState.Hurt:
                anim.SetTrigger(hurtTrigger);
                break;
        }
    }
}
