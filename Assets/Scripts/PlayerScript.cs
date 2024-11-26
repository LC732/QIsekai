using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.iOS;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Animator animator;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpForce = 16f;
    public float speed = 200f;
    float horizontalMoviment;
    public float shootForce = 5f;
    public float shootRate = 200f;
    public float shootTimer;
    public GameObject agua;  
    public GameObject acid;  
    public GameObject salt;  
    private bool isFlip = false;

    public List<GameObject> vidas;
    private int vidasI = 3;

    


    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        horizontalMoviment = GetDirecao();    
        animator.SetFloat("Speed", Mathf.Abs(horizontalMoviment));
        Jump();
        Shoot();
        rb.velocity = new Vector2(horizontalMoviment * speed, rb.velocity.y);
        if(isFlip){
            sprite.flipX = true;
        }else{
            sprite.flipX = false;
        }
    }


    int GetDirecao(){
        var keyboard = Keyboard.current;
        if (keyboard.dKey.isPressed)
        {
            isFlip = false;
            return 1; 
        }
        else if (keyboard.aKey.isPressed)
        {
            isFlip = true;
            return -1; 
        }
        return 0;
    }
   
   void Jump(){
        var keyboard = Keyboard.current;
        if(keyboard.spaceKey.isPressed && IsGrounded()){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }else if(!keyboard.spaceKey.isPressed && rb.velocity.y > 0f){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
        }
   }

   private bool IsGrounded(){
    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
   }

   void Shoot(){
    shootTimer -= Time.deltaTime;

    var keyboard = Keyboard.current;

    if(keyboard.jKey.isPressed && shootTimer < 0f){
        shootTimer = shootRate;
        GameObject stAgua = Instantiate(agua, transform.position, transform.rotation);
        if(isFlip){
            stAgua.GetComponent<Rigidbody2D>().velocity =new Vector2(-1f,1f)*shootForce + rb.velocity;
        }else{
            stAgua.GetComponent<Rigidbody2D>().velocity =new Vector2(1f,1f)*shootForce + rb.velocity;
        }
    }

    if(keyboard.kKey.isPressed && shootTimer < 0f){
        shootTimer = shootRate;
        GameObject stAcid = Instantiate(acid, transform.position, transform.rotation);
        if(isFlip){
            stAcid.GetComponent<Rigidbody2D>().velocity =new Vector2(-1f,1f)*shootForce + rb.velocity;
        }else{
            stAcid.GetComponent<Rigidbody2D>().velocity =new Vector2(1f,1f)*shootForce + rb.velocity;
        }
    }

    if(keyboard.lKey.isPressed && shootTimer < 0f){
        shootTimer = shootRate;
        GameObject stSalt = Instantiate(salt, transform.position, transform.rotation);
        if(isFlip){
            stSalt.GetComponent<Rigidbody2D>().velocity =new Vector2(-1f,1f)*shootForce + rb.velocity;
        }else{
            stSalt.GetComponent<Rigidbody2D>().velocity =new Vector2(1f,1f)*shootForce + rb.velocity;
        }
    }
   }

void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido tem a tag "Damage"
        if (collision.gameObject.CompareTag("Damage") && vidasI > 0)
        {
            // Reduz o contador de vidas
            vidasI--;

            // Torna invisível o elemento de vida correspondente
            vidas[vidasI].SetActive(false);
        }

        // Verifica se as vidas acabaram e, se sim, reinicia a cena
        if (vidasI == 0)
        {
            ReiniciarCena();
        }
    }

    void ReiniciarCena()
    {
        // Recarrega a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
