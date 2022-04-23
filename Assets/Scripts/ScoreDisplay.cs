using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private GameSession gameSession;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        if (gameSession == null)
        {
            gameSession = FindObjectOfType<GameSession>();
        }

        if (gameSession != null)
        {
            scoreText.text = gameSession.GetScore().ToString();
        }
    }
}
