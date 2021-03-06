using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Start or quit the game
/// </summary>
public class GameOverScript : MonoBehaviour
{
    private Button[] buttons;
    private Text[] texts;
    private Image canvasImage;
    private ScoreScript scoreScript;
    private GameObject scoreCanvas;
    private Color bgc;

    void Awake()
    {
        // Get the buttons
        buttons = GetComponentsInChildren<Button>();
        texts = GetComponentsInChildren<Text>();
        canvasImage = GetComponentInChildren<Image>();
        scoreScript = GameObject.Find("Scripts").GetComponent<ScoreScript>();
        scoreCanvas = GameObject.Find("ScoreCanvas");
        bgc = new Color(0.0f, 0.0f, 0.0f, 0.95f);
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
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(true);
        }
        foreach (var t in texts)
        {
            t.gameObject.SetActive(true);
            if (t.name == "Score") t.text = scoreScript.score.ToString();
            if (t.name == "Highscore") t.text = "Highscore: " + scoreScript.highscore;
            
        }
        StopTime();

        canvasImage.color = bgc;
        scoreCanvas.SetActive(false);
    }
    
    public void StopTime()
    {
        var ss = FindObjectOfType<SpawnScript>();
        ss.stop = true;
        
        var scollers = FindObjectsOfType<ScrollingScript>();
        // stop all scrollers
        foreach (var scroller in scollers)
        {
            scroller.enabled = false;
        }

        // stop all enemys shooting
        EnemyScript[] enemies = FindObjectsOfType<EnemyScript>();
        foreach (var enemy in enemies)
        {
            if (enemy.hasSpawn)
            {
                WeaponScript[] weapons = enemy.gameObject.GetComponentsInChildren<WeaponScript>();
                foreach (var weapon in weapons)
                {
                    weapon.enabled = false;
                }
            }
        }
        
        //stop all bullets moving
        ShotScript[] shots = FindObjectsOfType<ShotScript>();
        foreach (var shot in shots)
        {
            MoveScript move = shot.gameObject.GetComponent<MoveScript>();
            move.paused = true;
        }
    }

    public void ExitToMenu()
    {
        // Reload the level
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        // Reload the level
        SceneManager.LoadScene("Stage1");
    }
}
