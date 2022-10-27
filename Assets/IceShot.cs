using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShot : MonoBehaviour
{
    public Transform icePoint;
    public GameObject ice;
    public itemCollection items;

    // Update is called once per frame
    void Update()
    {
        if (items.isPowerUp)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

            void Shoot()
            {
                Instantiate(ice, icePoint.position, icePoint.rotation);

            }
        }
        else
        {
            Debug.Log("No PowerUp");
        }


    }
}