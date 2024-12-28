using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float score = 0;
    public UIManager uiManager;

    void Start()
    {
        if (uiManager != null)
        {
            uiManager.UpdateScoreText();
        }
    }

    public void AddScore(float amount)
    {
        score += amount;
        if (uiManager != null)
        {
            uiManager.UpdateScoreText();
        }
    }
}
