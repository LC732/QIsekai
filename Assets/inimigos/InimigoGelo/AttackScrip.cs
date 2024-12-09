using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackScrip : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public bool isVisible = false;
    [SerializeField] private float shootTimer = 1f;
    private float timeShoot;
    [SerializeField] private GameObject shoot;
    [SerializeField] private SpriteRenderer spr;

    [SerializeField] private float shootSpeed = 2f;
    [SerializeField] private float shootSpeedUntilTrigger = 2f;

    [SerializeField] private float timeToTrigger = 0.25f;
    [SerializeField] private Animator animator;


    void Start(){
        timeShoot =  shootTimer;
    }

    void FixedUpdate()
    {
        timeShoot -= Time.deltaTime;

        // Verifica se é hora de disparar e se o objeto está visível
        if (timeShoot <= 0 && isVisible)
        {
            // Executa a animação de ataque
            animator.SetTrigger("Attack");

            // Inicia a corrotina que espera a animação acabar para disparar o projétil
            StartCoroutine(WaitForAttackAnimationToEnd());
            timeShoot = shootTimer;  // Reinicia o timer de disparo
        }
    }

    private IEnumerator WaitForAttackAnimationToEnd()
    {
        // Espera a animação de ataque ser concluída
        float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length/2f;
        yield return new WaitForSeconds(animationDuration);

        // Depois da animação, instanciamos o projétil
        FireProjectile();
    }

    private void FireProjectile()
    {
        // Instancia o projétil
        GameObject st = Instantiate(shoot, transform.position, Quaternion.identity);
        st.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45f));

        // Configura o movimento inicial do projétil
        Rigidbody2D rb = st.GetComponent<Rigidbody2D>();
        PolygonCollider2D pc = st.GetComponent<PolygonCollider2D>();

        if (rb != null)
        {
            if (!spr.flipY)
            {
                rb.velocity = new Vector2(0f, shootSpeedUntilTrigger);  // Atira para cima
            }
            else
            {
                rb.velocity = new Vector2(0f, -shootSpeedUntilTrigger);  // Atira para baixo
            }
        }

        // Desabilita o collider temporariamente e aguarda antes de ajustar a direção
        if (pc != null)
            pc.enabled = false;

        // Inicia a contagem para ajustar a direção do projétil
        StartCoroutine(TriggerCountDown(rb, pc));
    }

    private IEnumerator TriggerCountDown(Rigidbody2D rb, PolygonCollider2D pc)
    {
        // Espera um tempo antes de ajustar a direção do projétil
        yield return new WaitForSeconds(timeToTrigger);

        
        // Habilita o collider após o tempo de espera
        if (pc != null)
            pc.enabled = true;

        // Ajusta a direção do projétil para mirar no jogador
        if (rb != null && player != null)
        {
            Vector2 direction = (player.transform.position - rb.transform.position).normalized;
            rb.velocity = direction * shootSpeed;

            // Calcula o ângulo entre o projétil e o jogador
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // Converte de radianos para graus

            // Aplica a rotação ao projétil
            rb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 45 ));

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // Verifica se o jogador entrou no alcance
        {
            player = collision.gameObject;  // Atribui o jogador
            isVisible = true;  // Torna o ataque possível
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (player != null && collision.CompareTag("Player"))  // Verifica se o jogador saiu do alcance
        {
            isVisible = false;  // Torna o ataque impossível
        }
    }
}