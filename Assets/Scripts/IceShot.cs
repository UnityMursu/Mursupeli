using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePower : MonoBehaviour
{
    [SerializeField] private Transform icePoint;
    [SerializeField] private GameObject ice;
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

                Debug.Log("lol");
            }

        } else
        {
            Debug.Log("No PowerUp");
        }

    }
}