using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI score;
    static public int lifeScore = 1;
    private float k = 1000f;
    private float c = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Certifique-se de que TimerScript.time seja inicializado corretamente
        float timeSpent = TimerScript.time; // Substitua com a lógica real
        if (timeSpent <= 0)
        {
            timeSpent = 1f; // Evita divisões por zero
        }

        float scoreValor = k / (timeSpent + c);
        if(lifeScore >= 0) lifeScore = 1;
        scoreValor *= lifeScore;
        score.text = string.Format("Score: {0:D4}", (int)scoreValor); // Mostra com 2 casas decimais

        TimerScript.time = 0f;
        lifeScore = 1;
    }

    public void play(UnityEngine.Object scene){
        TransitionManager.intent.TransitionTo(scene.name);
    }




}
