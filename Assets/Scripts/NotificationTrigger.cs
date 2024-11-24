using UnityEngine;

public class NotificationTrigger : MonoBehaviour
{
    public GameObject popUp; // Referência ao painel do pop-up

    void Start()
    {
        if (popUp != null)
        {
            popUp.SetActive(false); // Garante que o pop-up começa desativado
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Certifique-se de que o jogador tenha a tag "Player"
        {
            popUp.SetActive(true); // Ativa o pop-up
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popUp.SetActive(false); // Desativa o pop-up quando o jogador sai da área
        }
    }
}
