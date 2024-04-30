using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScoreState : IState
{
    private GameManager gameManager;

    public ShowScoreState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Show score state - enter");
    }

    public void Update()
    {
        Debug.Log("Show score state - update");
    }

    public void Exit()
    {
        Debug.Log("Show score state - exit");
    }
}
