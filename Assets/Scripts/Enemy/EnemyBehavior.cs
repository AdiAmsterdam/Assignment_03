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
    private Animator anim;
    private bool isPressed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        InitEnemy();
    }

    private void InitEnemy()
    {
        arrivedToPlayer = false;
        anim = GetComponent<Animator>();
        enemystates = EnemyState.Idle;
        originalScale = transform.localScale;
        enemyOriginalPosition = transform.position;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemystates == EnemyState.Die) return;
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
            if (!IsPunchAnimationFinished("punch")) return;
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
                anim.SetBool(walkingTrigger, false);
                break;
            case EnemyState.Walk:
                anim.SetBool(walkingTrigger, true);
                break;
            case EnemyState.Punch:
                anim.SetTrigger(punchTrigger);
                break;
            case EnemyState.Die:
                anim.SetTrigger(dyingTrigger);
                break;
        }
    }
    public bool IsPunchAnimationFinished(string animationName)
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        
        //if (!info.IsName(animationName)) return true;
        
        if (info.normalizedTime >0.95f) return true;
        
        return false;
    }

    public void EnemyDie()
    {
        enemystates = EnemyState.Die;
    }
}
