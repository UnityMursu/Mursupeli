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
        }
        if (collision.gameObject.CompareTag("Powerup"))
        {
            Destroy(collision.gameObject);
            Debug.Log("PowerUp");
            isPowerUp = true;
            GetComponent<SpriteRenderer>().color = Color.blue;
            ammo = 5;
            iceText.text = "Ammo: " + ammo.ToString();
        }
    }
}
