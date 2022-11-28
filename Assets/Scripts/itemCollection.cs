using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCollection : MonoBehaviour
{
    private int clams = 0;
    public bool isPowerUp;
    public int ammo;

    [SerializeField] private Text clamText;
    [SerializeField] public Text iceText;
    [SerializeField] private AudioSource collectSfx;
    [SerializeField] private AudioSource powerup;
    [SerializeField] private AudioSource powerdown;
    //clamStats used upon level complete in stat screen
    [SerializeField] private Text clamStats;

    void start()
    {
        isPowerUp = false;
        ammo = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Clam"))
        {
            Destroy(collision.gameObject);
            clams++;
            clamText.text = clams.ToString();
            clamStats.text = clams.ToString();
            collectSfx.Play();
        }
        if (collision.gameObject.CompareTag("Powerup"))
        {
            Destroy(collision.gameObject);
            powerup.Play();
            Debug.Log("PowerUp");
            isPowerUp = true;
            GetComponent<SpriteRenderer>().color = Color.blue;
            ammo = 5;
            iceText.text = "Ammo: " + ammo.ToString();
        }
    }
}
