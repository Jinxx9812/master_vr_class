using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuState : IState
{
    private GameManager gameManager;

    public MainMenuState(GameManager gameManager) 
    {
        this.gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Estado main menu - enter");
        gameManager.startBtn.onClick.AddListener(Update);
        gameManager.mainMenuCanvas.SetActive(true);
    }

    public void Update()
    {
        Debug.Log("Estado main menu - update");
        gameManager.stateMachine.TransitionTo(gameManager.stateMachine.gameplayState);
    }

    public void Exit()
    {
        Debug.Log("Estado main menu - exit");
        gameManager.mainMenuCanvas.SetActive(false);
    }
}
