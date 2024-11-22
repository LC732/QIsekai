using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowP : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform player;

    // Update is called once per frames
    void LateUpdate()
    {
        transform.position =new  Vector3(player.position.x, player.position.y+1f, transform.position.z);
    }

}
