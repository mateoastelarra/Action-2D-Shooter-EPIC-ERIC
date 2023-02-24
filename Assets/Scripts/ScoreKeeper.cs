using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int score = 0;
    UIDisplay uIDisplay;
    public static ScoreKeeper instance;

    void Awake() 
    { 
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        instance.uIDisplay = FindObjectOfType<UIDisplay>();
         
    }

    public int GetScore()
    {
        return score;
    }

    public void UpdateScore(int points)
    {
        score += points;
        uIDisplay.UpdateScoreText(score);
    }

    public void ResetScore()
    {
        score = 0;
    }
}
