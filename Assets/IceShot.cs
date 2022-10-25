using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShot : MonoBehaviour
{
    public Transform icePoint;
    public GameObject ice;

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