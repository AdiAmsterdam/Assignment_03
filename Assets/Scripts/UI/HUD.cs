using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Image heartImage;
    private int heartsSizeImage = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateHearts(int hearts)
    {
        heartImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hearts * heartsSizeImage);
    }
}
