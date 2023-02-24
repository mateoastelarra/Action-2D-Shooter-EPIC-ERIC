using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    Player player;
    float playerInitialHealth;
    Slider slider;
    TextMeshProUGUI scoreText;
    

    void Awake() 
    {
        player = FindObjectOfType<Player>();
    }
    
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        slider.value = 1;
        
        playerInitialHealth = player.GetComponent<HealthScript>().GetHealth();
        scoreText =GetComponentInChildren<TextMeshProUGUI>();
        UpdateScoreText(0);
    }


    public void UpdateHealthSlider(int health)
    {
        slider.value = health / playerInitialHealth;
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
