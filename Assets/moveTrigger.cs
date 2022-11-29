using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTrigger : MonoBehaviour
{
    public Pathfinding.AIPath path;

    // Start is called before the first frame update
    void Start()
    {
        path.canMove = false;
    }

    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.gameObject.CompareTag("Player"))
        {
            path.canMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D info2)
    {
        if (info2.gameObject.CompareTag("Player"))
        {
            path.canMove = false;  
        }
    }
}
