using UnityEngine;

public class GameOver : MonoBehaviour
{
    private Canvas gameOver;
    private PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Init();
    }

    private void Init()
    {
        playerMovement = GetComponent<PlayerMovement>();
        gameOver = GetComponent<Canvas>(); 
        gameOver.enabled = false;
    }

    public void GameOverScreen()
    {
        Time.timeScale = 0;
        gameOver.enabled = true;
        playerMovement.enabled = false;
    }
}
