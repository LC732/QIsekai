using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class theVoid : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag(player.tag))
        {
            ScoreScript.lifeScore -= 3;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


}
