using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverText : MonoBehaviour
{
    public GameObject logoText;

    // Start is called before the first frame update
    public void Start()
    {
       logoText.SetActive(false);
    }

    public void onMouseEnter()
    {
        logoText.SetActive(true);
        Debug.Log ("hovered");
    }

       public void onMouseExit()
    {
        logoText.SetActive(false);
    }
}


