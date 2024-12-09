using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTriggerScript : MonoBehaviour
{
    [SerializeField] private UnityEngine.Object scene; 

    public void OnTriggerEnter2D(Collider2D collider)
    {
        // Acessa o DialogManager e passa a fila de diálogos
        if(collider.tag == "Player"){
            TransitionManager.intent.TransitionTo(scene.name);
        }
    }


}
