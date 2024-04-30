using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : IState
{
    private GameManager gameManager;

    public GameplayState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Game play state - enter");
        gameManager.Gameplay();
    }

    public void Update()
    {
        Debug.Log("Game play state - update");
    }

    public void Exit()
    {
        Debug.Log("Game play state - exit");
    }
}
