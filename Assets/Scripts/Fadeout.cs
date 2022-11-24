using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadeout : MonoBehaviour
{
    public float fadeOutTime = 1f;
    [SerializeField] private AudioSource partySfx;

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
            partySfx.Play();
            StartCoroutine(FadeAlphaToZero(GetComponent<SpriteRenderer>(), fadeOutTime));
        }
    }

    // slowly fadesout the object depeding on set time
    IEnumerator FadeAlphaToZero(SpriteRenderer renderer, float duration) {
        Color startColor = renderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        float time = 0;
        while (time < duration) {
            time += Time.deltaTime;
            renderer.color = Color.Lerp(startColor, endColor, time/duration);
            yield return null;
        }
    }

    
}
