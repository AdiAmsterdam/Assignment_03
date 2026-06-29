using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public HUD hud;
    public GameOver gameOverScreen;
    public int health = 3;
    void Awake()
    {
        InitPlayerData();
    }

    private void InitPlayerData()
    {
        if (hud)
        {
            hud.UpdateHearts(health);
            hud.UpdateScore(0);
        }
    }


    void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame && health > 0)
        {
            TakeDamage();
        }

        if (health <= 0)
        {
            gameOverScreen.GameOverScreen();
        }
    }

    public void TakeDamage()
    {
        if (this.health > 0)
        {
            health--;
            if(hud)
                hud.UpdateHearts(health);
        }
    }

}
