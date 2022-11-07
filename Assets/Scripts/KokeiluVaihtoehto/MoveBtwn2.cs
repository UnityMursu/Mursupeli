using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBtwn2 : MonoBehaviour
{

    
    [SerializeField] private float speed;
    [SerializeField] private Vector3[] positions;

    private int index;
    //private SpriteRenderer m_Sprite;

    void Start()
    {
        //m_Sprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);

        if (transform.position == positions[index]) {
            if (index == positions.Length - 1){
                index = 0;
                
                transform.Rotate(0f, 180f, 0f);
            }
            else {
                index++;
                transform.Rotate(0f, 180f, 0f);
            }
        }


    }
}
