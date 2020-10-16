using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public int score;
    public int highscore;
    private GameObject scoreCanvas;
    private Text scoreText;
    private SpawnScript ss;
    
    
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
        ss = FindObjectOfType<SpawnScript>();
    }


    public void UpdateScore(int update)
    {
        score += update;
        scoreText.text = score.ToString();
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
        }
        
        //make it harder
        if (score % 50 == 0)
        {
            ss.maxEnemy += 1;
            ss.cooldown -= 0.5f;
        }
    }
}
