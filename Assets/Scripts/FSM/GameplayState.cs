using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : IState
{
    private GameManager gameManager;
    private float elapsedTime;

    public GameplayState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Enter()
    {
        elapsedTime = 0f;
        gameManager.gameplayCanvas.SetActive(true);
        gameManager.GenerateInteractables();
    }

    public void Update()
    {
        // Incrementar el tiempo transcurrido con el tiempo de cada frame
        elapsedTime += Time.deltaTime;

        // Convertir el tiempo transcurrido a formato de minutos y segundos
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        // Actualizar el texto para mostrar el tiempo transcurrido
        gameManager.timeTxt.text = "Tiempo: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        
        // Condicion de salida
        if(elapsedTime > gameManager._duration)
        {
            gameManager.stateMachine.TransitionTo(gameManager.stateMachine.showScoreState);
        }
    }

    public void Exit()
    {
        gameManager.gameplayCanvas.SetActive(false);
    }
}
