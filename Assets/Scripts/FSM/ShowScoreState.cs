using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowScoreState : IState
{
    private GameManager gameManager;

    public ShowScoreState(GameManager gameManager)
    {
        this.gameManager = gameManager;
        gameManager.saveBtn.onClick.AddListener(RestartGame);
    }

    public void Enter()
    {
        gameManager.scoreCanvas.SetActive(true);
    }

    public void Update()
    {

    }

    private void RestartGame()
    {
        gameManager.stateMachine.TransitionTo(gameManager.stateMachine.mainMenuState);
    }

    public void Exit()
    {
        gameManager.ClearIProductsOnScene();
        gameManager.scoreCanvas.SetActive(false);
    }
}
