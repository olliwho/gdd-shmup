using UnityEngine;
using System.Collections;

/// <summary>
/// Projectile behavior
/// </summary>
public class ExplosionScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(FadeTo(0.0f, 0.5f));
        // 2 - Limited time to live to avoid any leak
        Destroy(gameObject, 1f); // 2sec
    }
    
    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = transform.GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
            transform.GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
    }
}