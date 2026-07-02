using System;
using DefaultNamespace;
using UnityEngine;

public class EnemyInRange : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public EnemyBehavior enemyBehavior;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && playerAttack.IsAttacking())
        {
                enemyBehavior.EnemyDie();
        }
    }
}
