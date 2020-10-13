using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Start or quit the game
/// </summary>
public class GameOverScript : MonoBehaviour
{
    private Button[] buttons;
    private Text[] texts;
    private Image canvasImage;
    private GameObject scripts;
    private GameObject scoreCanvas;
    private Color bgc;

    void Awake()
    {
        // Get the buttons
        buttons = GetComponentsInChildren<Button>();
        texts = GetComponentsInChildren<Text>();
        canvasImage = GetComponentInChildren<Image>();
        scripts = GameObject.Find("Scripts");
        scoreCanvas = GameObject.Find("ScoreCanvas");
        bgc = new Color(0.0f, 0.0f, 0.0f, 0.9f);
        // Disable them
        HideGui();
    }

    public void HideGui()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(false);
        }
        foreach (var t in texts)
        {
            t.gameObject.SetActive(false);
        }
        canvasImage.color = Color.clear;
        scoreCanvas.SetActive(true);
    }

    public void ShowGui()
    {
        var score =  scripts.GetComponent<ScoreScript>();
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(true);
        }
        foreach (var t in texts)
        {
            t.gameObject.SetActive(true);
            if (t.name == "Score") t.text = score.score.ToString();
            if (t.name == "Highscore") t.text = "Highscore: " + score.highscore;
            
        }

        canvasImage.color = bgc;
        scoreCanvas.SetActive(false);
    }

    public void ExitToMenu()
    {
        // Reload the level
        Application.LoadLevel("Menu");
    }

    public void RestartGame()
    {
        // Reload the level
        Application.LoadLevel("Stage1");
    }
}
