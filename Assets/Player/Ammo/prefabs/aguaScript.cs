using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aguaScript : MonoBehaviour
{
    // Start is called before the first frame update

    
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private Color spreadColor;
    [SerializeField] private ParticleSystem droplets;
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
        ParticleSystem bom = Instantiate(droplets, transform.position, Quaternion.identity);
        bom.startColor = spreadColor;
        SoundEFManager.instance.PlaySoundFXclip(breakSound, transform, 1f);
        Destroy(self);
    }

}