using System;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounterScript : MonoBehaviour
{
    private int current = 0;
    private Text counter;
    
    void Start()
    {
        GameObject scoreCanvas = GameObject.Find("ScoreCanvas");
        Text[] texts = scoreCanvas.GetComponentsInChildren<Text>();
        foreach (var t in texts)
        {
            if (t.name == "Ammo") counter = t;
        }
    }

    public void IncreaseCounter()
    {
        current++;
        counter.text = current.ToString();
    }
    public void DecreaseCounter()
    {
        current--;
        counter.text = current.ToString();
    }
}
