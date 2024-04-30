using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class StateMachine
{
    public IState CurrentState {  get; private set; }

    public MainMenuState mainMenuState;
    public GameplayState gameplayState;
    public ShowScoreState showScoreState;

    public StateMachine(GameManager gameManager)
    {
        this.mainMenuState = new MainMenuState(gameManager);
        this.gameplayState = new GameplayState(gameManager);
        this.showScoreState = new ShowScoreState(gameManager);
    }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public void Update()
    {
        if(CurrentState != null) 
        {
            CurrentState.Update();
        }
    }
}
