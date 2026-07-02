using UnityEngine;

public class GameOver : MonoBehaviour
{
    private Canvas gameOver;
    public PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Init();
    }

    private void Init()
    {
        gameOver = GetComponent<Canvas>(); 
        gameOver.enabled = false;
    }

    public void GameOverScreen()
    {
        Time.timeScale = 0;
        gameOver.enabled = true;
        playerMovement.enabled = false;
        //disable character movement
    }
}
