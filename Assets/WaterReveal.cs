using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterReveal : MonoBehaviour
{
    private AudioLowPassFilter Underwater;
    public float fadeOutTime = 1f;
    [SerializeField] private AudioSource Splash;
    // Start is called before the first frame update
    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject water = GameObject.FindWithTag("Water");
        GameObject Music = GameObject.Find("Music");
        Underwater = Music.GetComponent<AudioLowPassFilter>();

    }
    // Is called when something enters objects collider area
    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.gameObject.CompareTag("Player"))
        {
            Debug.Log("underwater");
            StartCoroutine(FadeAlphaToZero(GetComponent<Tilemap>(), fadeOutTime));
            Underwater.cutoffFrequency = 500;
            Splash.Play();
        }
    }
    // Is called when something exits objects collider area
    private void OnTriggerExit2D(Collider2D info2)
    {
        if (info2.gameObject.CompareTag("Player"))
        {
            Debug.Log("out of water");
            StartCoroutine(FadeZeroToAlpha(GetComponent<Tilemap>(), fadeOutTime));
            Underwater.cutoffFrequency = 22000;
            Splash.Play();
        }
    }

    // slowly fadesout the object depeding on set time
    IEnumerator FadeAlphaToZero(Tilemap tilemap, float duration)
    {
        Color startColor = tilemap.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.7f);
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            tilemap.color = Color.Lerp(startColor, endColor, time / duration);
            yield return null;
        }
    }
    // slowly fade in the object depeding on set time
    IEnumerator FadeZeroToAlpha(Tilemap tilemap, float duration)
    {
        Color startColor = tilemap.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            tilemap.color = Color.Lerp(startColor, endColor, time / duration);
            yield return null;
        }
    }
}
