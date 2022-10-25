using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePower : MonoBehaviour
{
    [SerializeField] private Transform icePoint;
    [SerializeField] private GameObject ice;

    // Update is called once per frame
    void Update()
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
}