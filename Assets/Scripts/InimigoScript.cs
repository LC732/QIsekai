using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoScript : MonoBehaviour
{
    public Animator animator;  // Referência para o Animator do inimigo
    public SpriteRenderer sprite;  // Referência para o SpriteRenderer do inimigo
    public LayerMask groundLayer;  // Referência para o Layer do chão
    public Transform groundCheck;  // Referência para o GroundCheck

    public float speed = 0.02f;  // Velocidade do inimigo (reduzida para um valor muito lento)
    private bool isGrounded = false;  // Verifica se o inimigo está tocando o chão

    void Update()
    {
        // Verifica se o inimigo está no chão
        isGrounded = IsGrounded();

        // Se está no chão, move para a esquerda
        if (isGrounded)
        {
            MoveLeft();
        }

        // Atualiza a animação
        UpdateAnimation();
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
        // Aplica um movimento bem lento para a esquerda
        transform.Translate(Vector2.left * speed * Time.deltaTime * 0.5f); // Multiplicador extra para diminuir a velocidade
    }

    // Atualiza a animação do inimigo
    private void UpdateAnimation()
    {
        // Define o parâmetro "Speed" no Animator de acordo com a velocidade
        animator.SetFloat("Speed", Mathf.Abs(speed));  // Aplica a velocidade ajustada no Animator
    }
}
