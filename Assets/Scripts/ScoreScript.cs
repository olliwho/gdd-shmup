using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public int score;
    public int highscore;
    private GameObject scoreCanvas;
    private Text scoreText;
    
    
    //reset highscore
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        PlayerPrefs.SetInt("highscore", 0);
    }

    void Awake()
    {
        highscore = PlayerPrefs.GetInt("highscore");
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
        PlayerPrefs.SetInt("highscore", highscore);
    }
}
