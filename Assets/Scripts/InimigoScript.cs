using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoScript : MonoBehaviour
{
    public Animator animator;  // Referência para o Animator do inimigo
    public SpriteRenderer sprite;  // Referência para o SpriteRenderer do inimigo
    public LayerMask groundLayer;  // Referência para o Layer do chão
    public Transform groundCheck;  // Referência para verificar o chão
    public Transform wallCheck;    // Referência para verificar paredes

    public float speed = 0.02f;  // Velocidade do inimigo
    private bool isGrounded = false;  // Verifica se o inimigo está tocando o chão
    private bool movingLeft = true;  // Controla a direção do movimento

    void Update()
    {
        // Verifica se o inimigo está no chão
        isGrounded = IsGrounded();

        // Se está no chão, move na direção atual
        if (isGrounded)
        {
            if (movingLeft)
                MoveLeft();
            else
                MoveRight();
        }

        // Atualiza a animação
        UpdateAnimation();

        // Verifica limites ou colisões para mudar de direção
        CheckDirectionChange();
    }

    // Função para verificar se o inimigo está tocando o chão
    private bool IsGrounded()
    {
        // Verifica se há colisão com o chão
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    // Função para mover o inimigo para a esquerda
    private void MoveLeft()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        
    }

    // Função para mover o inimigo para a direita
    private void MoveRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    // Atualiza a animação do inimigo
    private void UpdateAnimation()
    {
        // Define o parâmetro "Speed" no Animator de acordo com a velocidade
        animator.SetFloat("Speed", Mathf.Abs(speed));
    }

    // Verifica se o inimigo precisa mudar de direção
    private void CheckDirectionChange()
    {
        // Usa um Raycast para verificar limites (fim de plataforma)
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);

        // Usa um Raycast para verificar colisões com paredes
        Vector2 direction = movingLeft ? Vector2.left : Vector2.right;
        RaycastHit2D wallInfo = Physics2D.Raycast(wallCheck.position, direction, 0.1f, groundLayer);

        // Se não há chão à frente ou colidiu com uma parede, muda de direção
        if (!groundInfo.collider || wallInfo.collider)
        {
            movingLeft = !movingLeft;
            sprite.flipX = !movingLeft;  // Ajusta a direção visual se necessário
            if(movingLeft){
                wallCheck.position = wallCheck.position - new Vector3(1f, 0f, 0f);
            }else{
                wallCheck.position = wallCheck.position + new Vector3(1f, 0f, 0f);
            }
        }
    }
}
