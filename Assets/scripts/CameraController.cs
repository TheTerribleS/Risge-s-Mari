using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject GameObjectPlayer;

    private Vector3 offset;

    void Start()
    {
        
        //set camera to be behind the player
        offset = transform.position - GameObjectPlayer.transform.position;

    }

    void LateUpdate()
    {

        if (Player.CameraNeedsToSeparateMore)
        {
            offset.z += Player.linearSizeFactor;
            Player.CameraNeedsToSeparateMore = false;
        }

        //change position as player moves
        transform.position = GameObjectPlayer.transform.position + offset;

       
    }
}
