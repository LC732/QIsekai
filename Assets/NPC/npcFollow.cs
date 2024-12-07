using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class npcFollow : MonoBehaviour
{

    public PlayerScript pScript;
    public Transform player;
    public float force = 0.3f;
    private Vector3 starposition;
    private Vector3 endposition;
    public Vector3 offset = new Vector3(-1f,1f,0);
    public float timer = 1f;
    public float moveT =  0;


    // Start is called before the first frame update
    void Start()
    {
        starposition = transform.position;
        endposition = player.position + offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        moveT += Time.deltaTime;
        float porcentege = moveT / force;

        if(pScript.isFlip){
            offset.x = 1f;
        }else{
            offset.x = -1f;
        }

        transform.position = Vector3.Lerp(starposition, endposition, porcentege);
        if(timer <= 0){
            starposition = transform.position;
            endposition = player.position + offset;
            timer = 0.1f;
            moveT = 0;
        }
    }
}
