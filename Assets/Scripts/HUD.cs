﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public Level level;
    public GameOver gameOver;

    public Text remainingText;
    public Text remainingSubText;
    public Text targetText;
    public Text targetSubText;
    public Text scoreText;

    public Image[] stars;

    private int starIndex;
    private bool isGameOver;

    private void Start()
    {
        UpdateStars();
    }

    public void UpdateStars()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (i == starIndex)
            {
                stars[i].enabled = true;
            } else
            {
                stars[i].enabled = false;
            }
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();

        int visibleStar = 0;

        if (score >= level.score1Star && score < level.score2Star)
        {
            visibleStar = 1;
        } 
        else if (score >= level.score2Star && score < level.score3Star)
        {
            visibleStar = 2;
        }
        else if (score >= level.score3Star)
        {
            visibleStar = 3;
        }

        starIndex = visibleStar;

        UpdateStars();
    }

    public void SetTarget(int target)
    {
        targetText.text = target.ToString();
    }

    public void SetRemaining(int remaining)
    {
        remainingText.text = remaining.ToString();
    }

    public void SetRemaining (string remaining)
    {
        remainingText.text = remaining;
    }

    public void SetLevelType(Level.LevelType type)
    {
        switch (type)
        {
            case Level.LevelType.MOVES:
                remainingSubText.text = "Moves Remaining";
                targetSubText.text = "Target Score";
                break;
            case Level.LevelType.OBSTACLE:
                remainingSubText.text = "Moves Remaining";
                targetSubText.text = "Dishes Remaining";
                break;
            case Level.LevelType.TIMER:
                remainingSubText.text = "Time Remaining";
                targetSubText.text = "Target Score";
                break;
        }
    }

    public void OnGameWin(int score)
    {
        gameOver.ShowWin(score, starIndex);

        if (starIndex > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, 0))
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, starIndex);
        }
    }
    public void OnGameLose()
    {
        gameOver.ShowLose();
    }
}
