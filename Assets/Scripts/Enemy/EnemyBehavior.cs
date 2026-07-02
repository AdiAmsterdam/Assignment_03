using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBehavior : MonoBehaviour
{
    private bool arrivedToPlayer;
    private string walkingTrigger = "isWalking";
    private string punchTrigger = "punch";
    private string dyingTrigger = "die";
    public float speed = 1.0f;
    private float distanceFromPlayer;
    private Vector3 playerPosition;
    private Vector3 enemyOriginalPosition;
    private Vector3 originalScale;
    private EnemyState enemystates;
    private PlayerState playerStates;
    private Animator animator;
    private bool isPressed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        InitEnemy();
    }

    private void InitEnemy()
    {
        arrivedToPlayer = false;
        animator = GetComponent<Animator>();
        enemystates = EnemyState.Idle;
        originalScale = transform.localScale;
        enemyOriginalPosition = transform.position;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemystates == EnemyState.Die)
        {
            enemystates = EnemyState.Die;
            return;
        }
        //if (!IsPunchAnimationFinished("punch") && IsAttacking()) return;
        HandleEnemyMovementInput();
    }

    private void HandleEnemyMovementInput()
    {
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            isPressed = true;
        }

        if (distanceFromPlayer < 1.2f) arrivedToPlayer = true;
        
        if (isPressed)
        {
            MoveEnemy(); 
        }

        if (transform.position == enemyOriginalPosition)
        {
            enemystates = EnemyState.Idle;
            isPressed = false;
        }
    }

    public void MoveEnemy()
    {
        if (!arrivedToPlayer)
        {
            enemystates = EnemyState.Walk;
            AnimationControl(enemystates);

            if (transform.position.x > playerPosition.x)
                FlipEnemy(false);
            else FlipEnemy(true);

            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, Time.deltaTime * speed);
        }
        else
        {
            enemystates = EnemyState.Walk;
            AnimationControl(enemystates);
            FlipEnemy(true);
            transform.position = Vector3.MoveTowards(transform.position, enemyOriginalPosition, Time.deltaTime * speed);
        }
    }
    
    void FlipEnemy(bool flip)
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

    public void AnimationControl(EnemyState enemyState)
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                animator.SetBool(walkingTrigger, false);
                break;
            case EnemyState.Walk:
                animator.SetBool(walkingTrigger, true);
                break;
            case EnemyState.Punch:
                animator.SetTrigger(punchTrigger);
                break;
            case EnemyState.Die:
                animator.SetTrigger(dyingTrigger);
                break;
        }
    }
    public bool IsPunchAnimationFinished(string animationName)
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        
        //if (!info.IsName(animationName)) return true;
        
        if (info.normalizedTime >0.95f||!info.IsName(animationName)) return true;
        
        return false;
    }

    public bool IsAttacking()
    {
        if (enemystates == EnemyState.Punch) return true;
        return false;
    }
    public void EnemyDie()
    {
        enemystates = EnemyState.Die;
        enabled = false;
    }
}
