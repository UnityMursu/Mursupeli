using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 lastCheckpointPosition;

    private void Awake()
    {
        lastCheckpointPosition = GetStartPosition();

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Vector2 GetStartPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return new Vector2(player.transform.position.x, player.transform.position.y);
    }

}
