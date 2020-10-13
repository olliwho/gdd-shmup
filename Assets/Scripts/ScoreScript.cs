using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public int score = 0;
    public int highscore = 0;
    private GameObject scoreCanvas;
    private Text scoreText;
    void Start()
    {
        scoreCanvas = GameObject.Find("ScoreCanvas");
        Text[] texts = scoreCanvas.GetComponentsInChildren<Text>();
        foreach (var t in texts)
        {
            if (t.name == "Score") scoreText = t;
        }
    }


    public void UpdateScore(int update)
    {
        score += update;
        if (score > highscore) highscore = score;
        scoreText.text = score.ToString();
    }
}
