using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aguaScript : MonoBehaviour
{
    // Start is called before the first frame update

    
    
    void Start()
    {
        
    }

    public GameObject self;
    public Rigidbody2D rb;
    public float rotationSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision) 
    { 
        Destroy(self);
    } 
}