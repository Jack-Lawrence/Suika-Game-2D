using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            UpdateScoreText();
        }
    }

    public void UpdateScoreText()
    {
        if (gameManager != null && scoreText != null)
        {
            scoreText.text = "SCORE: " + gameManager.score.ToString();
        }
    }
}
