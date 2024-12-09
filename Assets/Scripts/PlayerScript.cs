using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    [SerializeField] private LayerMask iceLayer;

    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float speed = 200f;
    private string detectedLayer = ""; // Para armazenar o tipo detectado. 

    [Header("Ice Settings")]

    [SerializeField] private float iceMoveInput;
    [SerializeField] private float iceBaseSpeed = 0f;
    [SerializeField] private float iceMaxSpeed;


    [Header("Shooting Settings")]
    [SerializeField] private float shootForce = 5f;
    [SerializeField] private float shootRate = 0.2f; // Time in seconds between shots
    private float shootTimer;

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float invincibilityDuration = 0.15f;
    public bool canDamage = true;

    [Header("Projectiles")]
    public GameObject agua;
    public GameObject acid;
    public GameObject salt;

    [Header("Life Settings")]
    public List<GameObject> vidas; // Lista dos ícones de vida no HUD
    private int vidasI = 3; // Quantidade de vidas restantes
    static int lifeScore = 0;

    private float horizontalMovement;
    public bool isFlip;

    void Start()
    {
        ValidateComponents();
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (!canDamage) return;

        Move();
        Shoot();
        Jump();

    }

    private void ValidateComponents()
    {
        if (animator == null || sprite == null || rb == null || groundCheck == null)
        {
            Debug.LogError("Missing essential components. Check Inspector.");
        }
    }

    private void Move()
    {
        float inputMove;
        horizontalMovement = GetDirection();
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        sprite.flipX = isFlip;


        if (IsOnIce())
        {
            if (horizontalMovement != 0)
            {
                iceMoveInput += horizontalMovement * Time.deltaTime;
            }
            else
            {
                if (iceMoveInput > 0)
                {
                    iceMoveInput -= Time.deltaTime;
                }
                else if (iceMoveInput < 0)
                {
                    iceMoveInput += Time.deltaTime;
                }
            }

            if(iceMoveInput > iceMaxSpeed){
                iceMoveInput = iceMaxSpeed;
            }else if(iceMoveInput < -iceMaxSpeed){
                iceMoveInput = -iceMaxSpeed;
            }
            inputMove = iceMoveInput;
        }
        else
        {
            iceMoveInput = iceBaseSpeed;
            inputMove = horizontalMovement;
        }

        rb.velocity = new Vector2(inputMove * speed, rb.velocity.y);
    }

    private int GetDirection()
    {
        var keyboard = Keyboard.current;
        if (keyboard.dKey.isPressed) { isFlip = false; return 1; }
        if (keyboard.aKey.isPressed) { isFlip = true; return -1; }
        return 0;
    }

    private void Jump()
    {

        var keyboard = Keyboard.current;
        if (keyboard.spaceKey.isPressed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (!keyboard.spaceKey.isPressed && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsOnIce(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, iceLayer);
    }

    private void Shoot()
    {
        if (shootTimer > 0) return;

        var keyboard = Keyboard.current;
        if (keyboard.jKey.isPressed) ShootProjectile(agua);
        else if (keyboard.kKey.isPressed) ShootProjectile(acid);
        else if (keyboard.lKey.isPressed) ShootProjectile(salt);
    }

    private void ShootProjectile(GameObject projectile)
    {
        Vector2 direction = isFlip ? new Vector2(-1f, 1f) : new Vector2(1f, 1f);
        GameObject instance = Instantiate(projectile, transform.position, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().velocity = direction * shootForce + rb.velocity;
        shootTimer = shootRate;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isDamaged(collision);
        isHealed(collision);

    }

    private void isHealed(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LifeCrystal") && vidasI < 3)
        {
            //Almentar vida
            vidas[vidasI].SetActive(true);
            vidasI++;

        }
        // Reiniciar cena se as vidas chegarem a zero
        if (vidasI == 3)
        {
            ScoreScript.lifeScore++;
        }
    }

    private void isDamaged(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Damage") && vidasI > 0 && canDamage)
        {
            // Ativar invulnerabilidade temporária
            canDamage = false;
            StartCoroutine(InvulnerabilityCooldown());

            // Aplicar knockback com base na direção da colisão
            if (collision.transform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(knockbackForce, knockbackForce);

            }
            else if (collision.transform.position.x >= transform.position.x)
            {
                rb.velocity = new Vector2(-knockbackForce, knockbackForce);
            }

            //Reduzir vida
            vidasI--;
            vidas[vidasI].SetActive(false);
        }
        // Reiniciar cena se as vidas chegarem a zero
        if (vidasI == 0)
        {
            ScoreScript.lifeScore -= 3;
            ReiniciarCena();
        }
    }

    private void ApplyKnockback(Vector2 direction)
    {
        rb.velocity = direction * knockbackForce;
    }

    private IEnumerator InvulnerabilityCooldown()
    {
        yield return new WaitForSeconds(invincibilityDuration);
        canDamage = true;
    }

    private void ReiniciarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
