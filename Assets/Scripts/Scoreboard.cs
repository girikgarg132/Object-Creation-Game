using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField]TMP_Text[] scoreTexts;

    private int score = 0;

    private void Update()
    {
        foreach (TMP_Text scoreText in scoreTexts)
        {
            scoreText.text = "" + score;
        }
    }

    public void IncrementScore()
    {
        score++;
    }

    public int GetScore()
    {
        return score;
    }
}
