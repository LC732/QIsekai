using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScrip : MonoBehaviour
{

    public void quit(){
        Application.Quit();
        Debug.Log("Fiz o L");
    }

    public void play(){
        SceneManager.LoadScene("Map1Scene");
    }

}
