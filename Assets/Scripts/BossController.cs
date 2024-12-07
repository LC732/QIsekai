using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class NextPhaseTrigger : MonoBehaviour
{
    private bool playerCanAdvance = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Verifica se o chefão foi derrotado antes de permitir avançar
            BossController boss = FindObjectOfType<BossController>();
            if (boss != null && boss.IsDefeated)
            {
                playerCanAdvance = true;
                ShowPopup();
            }
        }
    }

    private void ShowPopup()
    {
        // Exibir um pop-up usando UI do Unity (como um Canvas)
        Debug.Log("Chefão derrotado! Você pode avançar para a próxima fase.");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCanAdvance = false;
            HidePopup();
        }
    }

    private void HidePopup()
    {
        // Ocultar o pop-up
        Debug.Log("Saindo da área da placa.");
    }
}

