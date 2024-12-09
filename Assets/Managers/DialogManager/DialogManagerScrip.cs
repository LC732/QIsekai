using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogManagerScrip : MonoBehaviour
{  
    
    public static DialogManagerScrip Instance; // Singleton para fácil acesso
    [SerializeField] public TextMeshProUGUI nameText; // Texto do nome
    [SerializeField] public TextMeshProUGUI dialogText; // Texto do diálogo
    [SerializeField] private Canvas dialogCanvas; // Caixa de diálogo
    [SerializeField] private Canvas playerCanvas;
    private Queue<Dialog> dialogQueue; // Fila de diálogos

    private void Awake()
    {
        // Configura o Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dialogQueue = new Queue<Dialog>();
    }

    public void StartDialog(Dialog[] dialogs)
    {
        dialogCanvas.enabled = true; // Ativa a caixa de diálogo
        playerCanvas.enabled = false;


        dialogQueue.Clear(); // Limpa qualquer diálogo anterior

        foreach (var dialog in dialogs)
        {
            dialogQueue.Enqueue(dialog); // Enfileira os novos diálogos
        }

        DisplayNextDialog();
    }

    public void DisplayNextDialog()
    {
        if (dialogQueue.Count == 0)
        {
            EndDialog();
            return;
        }

        Dialog currentDialog = dialogQueue.Dequeue(); // Obtém o próximo diálogo
        nameText.text = currentDialog.name; // Exibe o nome
        dialogText.text = currentDialog.text; // Escreve o texto gradualmente
    }

    
    private void EndDialog()
    {
        dialogCanvas.enabled = false ; // Esconde a caixa de diálogo
        playerCanvas.enabled = true ; 
        Debug.Log("Diálogo encerrado.");
    }
}
