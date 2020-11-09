using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gameOverMenu;
    private int score = 0;

    public static UIManager instance;

    private void Start()
    {
        instance = this;
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }

    public void ShowDeathMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void Restart()
    {
        RestartScene.StartScene();
    }
}
