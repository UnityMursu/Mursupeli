using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCollection : MonoBehaviour
{
    private int clams = 0;

    [SerializeField] private Text clamText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Clam"))
        {
            Destroy(collision.gameObject);
            clams++;
            clamText.text = clams.ToString();
        }
    }
}
