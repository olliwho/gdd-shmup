using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAmmoScript : MonoBehaviour
{
    private SpriteRenderer rendererComponent;
    void Start()
    {
        // Destroy(gameObject, 25); // 20sec
        rendererComponent = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!rendererComponent.IsVisibleFrom(Camera.main))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SpawnScript ss = FindObjectOfType<SpawnScript>();
        if(ss) ss.currentAmmo--;
    }
}
