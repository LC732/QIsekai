using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScrip : MonoBehaviour
{

    public void quit(){
        Application.Quit();
    }

    public void play(UnityEngine.Object scene){
        SceneManager.LoadScene(scene.name);
    }

}
