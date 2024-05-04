using UnityEngine;

public class GameplayState : IState
{
    private GameManager gameManager;
    public float elapsedTime = 0f;  // Tiempo transcurrido en el estado

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
        elapsedTime += Time.deltaTime;  // Actualizar el tiempo transcurrido
        gameManager.timeTxt.text = "Tiempo: " + elapsedTime.ToString("F2");  // Mostrar el tiempo en el UI

        // Aquí agregamos un chequeo manual para la condición de completitud
        gameManager.CheckAllItemsPlacedCorrectly();
    }

    public void Exit()
    {
        gameManager.gameplayCanvas.SetActive(false);
    }
}
