using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgScript : MonoBehaviour
{

public GameObject targetObject; // Objeto cuja tag será comparada

void OnCollisionEnter2D(Collision2D collision)
{
    // Verifica se o targetObject foi atribuído
    if (targetObject != null)
    {
        // Compara a tag da colisão com a tag do targetObject
        if (collision.gameObject.tag == targetObject.tag)
        {
            // Destroi o próprio GameObject
            Destroy(gameObject);
        }
    }
    else
    {
        Debug.LogWarning("TargetObject não foi atribuído no inspetor!");
    }
}


}
