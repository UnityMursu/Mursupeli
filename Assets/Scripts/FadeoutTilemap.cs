using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FadeoutTilemap : MonoBehaviour
{
    public float fadeOutTime = 1f;
    // Start is called before the first frame update
    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject hidden = GameObject.FindWithTag("Hidden");

    }
    // Is called when something enters objects collider area
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("I'm hidden");
            StartCoroutine(FadeAlphaToZero(GetComponent<Tilemap>(), fadeOutTime));
        }
    }

    // slowly fadesout the object depeding on set time
    IEnumerator FadeAlphaToZero(Tilemap tilemap, float duration)
    {
        Color startColor = tilemap.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            tilemap.color = Color.Lerp(startColor, endColor, time / duration);
            yield return null;
        }
    }
}
