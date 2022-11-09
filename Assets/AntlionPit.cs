using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntlionPit : MonoBehaviour
{
    public PlayerMovement player;
    private bool inPit;


    // Start is called before the first frame update
    void Start()
    {
        inPit = false;
    }

    void Update()
    {
        if (inPit)
        {
            player.rigidBody.gravityScale = player.pitGravity;
        }
        else if (!inPit)
        {
            player.rigidBody.gravityScale = player.normalGravity;
        }

    }

    // add gravity when standing on antlion pit
    private void OnTriggerEnter2D(Collider2D info)
    {
        inPit = true;
    }
    // return normal gravity when not on antlion pit
    private void OnTriggerExit2D(Collider2D info2)
    {
        inPit = false;
    }

}
