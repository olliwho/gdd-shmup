using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeStopScript : MonoBehaviour
{
    public float duration = 5f;
    
    private bool stopped;
    private float cooldown;

    private ScrollingScript[] scollers;
    private List<WeaponScript> allWeapons;
    private List<MoveScript> shotsMoving;


    // Start is called before the first frame update
    void Start()
    {
        scollers = FindObjectsOfType<ScrollingScript>();
        allWeapons = new List<WeaponScript>();
        shotsMoving = new List<MoveScript>();
        cooldown = 0.0f;
    }

    private void Update()
    {
        if (stopped)
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                stopped = false;
                Restart();
            }
        }
    }

    public void StopTime()
    {
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
                    allWeapons.Add(weapon);
                }
            }
        }
        
        //stop all bullets moving
        ShotScript[] shots = FindObjectsOfType<ShotScript>();
        foreach (var shot in shots)
        {
            MoveScript move = shot.gameObject.GetComponent<MoveScript>();
            move.paused = true;
            shotsMoving.Add(move);
        }

        cooldown = duration;
        stopped = true;
    }

    private void Restart()
    {
        // restart all scrollers
        foreach (var scroller in scollers)
        {
            scroller.enabled = true;
        }
        foreach (var weapon in allWeapons)
        {
            if(weapon) weapon.enabled = true;
        }
        foreach (var move in shotsMoving)
        {
            move.paused = false;
        }
        
        
        Destroy(gameObject);
    }
}
