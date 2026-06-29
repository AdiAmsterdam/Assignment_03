using DefaultNamespace;
using UnityEngine;

public class EnemyInRange : MonoBehaviour
{
    PlayerState playerStates;
    private EnemyBehavior enemyBehavior;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (playerStates == PlayerState.Jab)
            {
                enemyBehavior.EnemyDie();
                return;
            }
        }
    }
}
