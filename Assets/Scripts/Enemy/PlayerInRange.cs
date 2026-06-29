using DefaultNamespace;
using UnityEngine;

public class PlayerInRange : MonoBehaviour
{
    private EnemyState enemyStates;
    private PlayerState playerStates;
    public PlayerHealth playerHealth;
    public EnemyBehavior enemyBehavior;
    public PlayerMovement playerControl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerStates == PlayerState.Jab)
            {
                enemyBehavior.EnemyDie();
                return;
            }
            enemyStates = EnemyState.Punch;
            enemyBehavior.AnimationControl(enemyStates);
            playerStates = PlayerState.Hurt;
            playerControl.AnimationControl(playerStates);
            playerHealth.TakeDamage();
        }
    }
}
