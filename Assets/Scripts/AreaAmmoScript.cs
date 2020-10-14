using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAmmoScript : MonoBehaviour
{
    private Renderer rendererComponent;
    void Start()
    {
        Destroy(gameObject, 35); // 20sec
    }
    
    private void OnDestroy()
    {
        SpawnScript ss = FindObjectOfType<SpawnScript>();
        if(ss) ss.currentAmmo--;
    }
}
