using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private Dialog[] dialogQueue; // Fila de diálogos

    public void OnTriggerEnter2D(Collider2D collider)
    {
        // Acessa o DialogManager e passa a fila de diálogos
        if(collider.tag == "Player"){
            DialogManagerScrip.Instance.StartDialog(dialogQueue);
        }
    }

}
