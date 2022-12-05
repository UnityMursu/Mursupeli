using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRunner : MonoBehaviour
{
    public int clams;

    [SerializeField] public Text clamText;
    [SerializeField] private AudioSource collectSfx;
    //clamStats used upon level complete in stat screen
    //[SerializeField] private Text clamStats;

    void start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Clam"))
        {
            Destroy(collision.gameObject);
            clams++;
            clamText.text = clams.ToString();
            //clamStats.text = clams.ToString();

            collectSfx.Play();
        }
    }
}
