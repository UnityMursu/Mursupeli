using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.gameObject.CompareTag("Ground"))
        {
            Destroy(info.transform.parent.gameObject);
        }
        if (info.gameObject.CompareTag("Trap"))
        {
            Destroy(info.transform.parent.gameObject);
        }
        if (info.gameObject.CompareTag("Water"))
        {
            Destroy(info.transform.parent.gameObject);
        }
    }
}
