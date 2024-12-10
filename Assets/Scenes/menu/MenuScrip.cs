using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScrip : MonoBehaviour
{

    public void quit(){
        Application.Quit();
    }

    public void play(String scene){
        TransitionManager.intent.TransitionTo(scene);
    }

    public void play2(String scene){
        SceneManager.LoadScene(scene);
    }

}
