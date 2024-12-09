using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class LifeCrystalScript : MonoBehaviour
{

[SerializeField] GameObject targetObject; // Objeto cuja tag será comparada
[SerializeField] GameObject player;
[SerializeField] SpriteRenderer IceHeart;
[SerializeField] SpriteRenderer Heart;


void OnCollisionEnter2D(Collision2D collision)
{
    // Verifica se o targetObject foi atribuído
    if (targetObject != null && player != null)
    {
        // Compara a tag da colisão com a tag do targetObject
        if (collision.gameObject.tag == targetObject.tag && IceHeart.enabled)
        {
            // Destroi o próprio GameObject
            changeToCrystal();
        }
        if (collision.gameObject.tag == player.tag && Heart.enabled){
            Destroy(gameObject);
        }
    }
    else
    {
        Debug.LogWarning("TargetObject não foi atribuído no inspetor!");
    }
}

    private void changeToCrystal()
    {
        IceHeart.enabled = false;
        Heart.enabled = true;
    }
}


