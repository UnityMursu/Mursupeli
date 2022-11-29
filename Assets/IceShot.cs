using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShot : MonoBehaviour
{
    public Transform icePoint;
    public GameObject ice;
    public itemCollection items;
    [SerializeField] private AudioSource shoot;
    [SerializeField] private AudioSource powerdown;

    // Update is called once per frame
    void Update()
    {
        
        if (items.isPowerUp)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                items.ammo--;
                Debug.Log(items.ammo);
                items.iceText.text = "Ammo: " + items.ammo.ToString();
            }

            void Shoot()
            {
                shoot.Play();
                Instantiate(ice, icePoint.position, icePoint.rotation);

            }
        }
      
        if (items.ammo == 0 && items.isPowerUp == true)
        {
            items.isPowerUp = false;
            GetComponent<SpriteRenderer>().color = Color.white;
            powerdown.Play();

        }

    }
}