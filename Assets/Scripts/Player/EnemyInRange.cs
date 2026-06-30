using DefaultNamespace;
using UnityEngine;

public class EnemyInRange : MonoBehaviour
{
    public PlayerAttack playerStates;
    private EnemyBehavior enemyBehavior;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && playerStates.playerStates == PlayerState.Jab)
        {
                enemyBehavior.EnemyDie();
        }
    }
}
