using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [Header("Spawn reference")]
    [SerializeField] GameObject spawnArea;
    Vector3 spawnAreaCenter;
    Vector3 spawnAreaSize;

    [Header("Product references")]
    [SerializeField] int numberOfItems = 4;

    [Header("Main menu")]
    [SerializeField] public GameObject mainMenuCanvas;
    [SerializeField] public Button startBtn;

    [Header("Gameplay")]
    [SerializeField] public GameObject gameplayCanvas;
    [SerializeField] public TMP_Text timeTxt;
    [SerializeField] public float _duration = 10.0f;
    [SerializeField] public GameObject blueBox;
    [SerializeField] public GameObject redBox;
    [SerializeField] public GameObject orangeBox;
    [SerializeField] public GameObject whiteBox;

    [Header("Endgame")]
    [SerializeField] public GameObject scoreCanvas;
    [SerializeField] public Button saveBtn;
    [SerializeField] private GameObject reportCanvas;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;  // Referencia al AudioSource
    [SerializeField] private AudioClip correctPlacementSound;  // Clip de sonido para cuando un objeto se coloca correctamente


    public StateMachine stateMachine;

    private Dictionary<string, int> itemsSpawned = new Dictionary<string, int>()
    {
        {"Blue", 0},
        {"Red", 0},
        {"White", 0},
        {"Orange", 0}
    };

    private Dictionary<string, int> itemsCorrectlyPlaced = new Dictionary<string, int>()
    {
        {"Blue", 0},
        {"Red", 0},
        {"White", 0},
        {"Orange", 0}
    };

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new(this);
        stateMachine.Initialize(stateMachine.mainMenuState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void GenerateInteractables()
    {
        // spawn area
        spawnAreaCenter = spawnArea.transform.position;
        spawnAreaSize = spawnArea.transform.localScale;

        // blue product factory
        FactoryProductBlue factoryProductBlue = this.GetComponent<FactoryProductBlue>();
        FactoryProductRed factoryProductRed = this.GetComponent<FactoryProductRed>();
        FactoryProductWhite factoryProductWhite = this.GetComponent<FactoryProductWhite>();
        FactoryProductOrange factoryProductOrange = this.GetComponent<FactoryProductOrange>();


        // create n products
        for (int i = 0; i < numberOfItems; i++)
        {
            Vector3 randomPos = GetRandomPointInArea();
            int randomFactory = Random.Range(1, 5);
            switch (randomFactory)
            {
                case 1:
                    this.GetComponent<FactoryProductBlue>().GetProduct(randomPos);
                    itemsSpawned["Blue"]++;
                    break;
                case 2:
                    this.GetComponent<FactoryProductRed>().GetProduct(randomPos);
                    itemsSpawned["Red"]++;
                    break;
                case 3:
                    this.GetComponent<FactoryProductOrange>().GetProduct(randomPos);
                    itemsSpawned["Orange"]++;
                    break;
                case 4:
                    this.GetComponent<FactoryProductWhite>().GetProduct(randomPos);
                    itemsSpawned["White"]++;
                    break;
            }
        }
    }

    private Vector3 GetRandomPointInArea()
    {
        float x = UnityEngine.Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2);
        float y = UnityEngine.Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2);
        float z = UnityEngine.Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2);
        return new Vector3(x, y, z);
    }

    public void ClearIProductsOnScene()
    {
        // Encuentra todos los GameObjects en la escena con el componente "ProductBlue"
        ProductBlue[] productsBlue = FindObjectsOfType<ProductBlue>();

        // Itera sobre cada GameObject encontrado
        foreach (ProductBlue product in productsBlue)
        {
            // Haz algo con cada GameObject encontrado
            Destroy(product.gameObject);
        }

        // Encuentra todos los GameObjects en la escena con el componente "ProductRed"
        ProductRed[] productsRed = FindObjectsOfType<ProductRed>();

        // Itera sobre cada GameObject encontrado
        foreach (ProductRed product in productsRed)
        {
            // Haz algo con cada GameObject encontrado
            Destroy(product.gameObject);
        }

        ProductWhite[] productsWhite = FindObjectsOfType<ProductWhite>();
        foreach (ProductWhite product in productsWhite)
        {
            // Haz algo con cada GameObject encontrado
            Destroy(product.gameObject);
        }

        ProductOrange[] productsOrange = FindObjectsOfType<ProductOrange>();
        foreach (ProductOrange product in productsOrange)
        {
            // Haz algo con cada GameObject encontrado
            Destroy(product.gameObject);
        }

        // Reinicio las variables de cada caja
        BoxCounter boxCounterBlue = blueBox.GetComponentInChildren<BoxCounter>();
        boxCounterBlue.RestartScore();
        BoxCounter boxCounterRed = redBox.GetComponentInChildren<BoxCounter>();
        boxCounterRed.RestartScore();
        BoxCounter boxCounterOrange = orangeBox.GetComponentInChildren<BoxCounter>();
        boxCounterRed.RestartScore();
        BoxCounter boxCounterWhite = whiteBox.GetComponentInChildren<BoxCounter>();
        boxCounterRed.RestartScore();
    }

    public void ItemPlacedCorrectly(string color)
    {
        if (itemsSpawned[color] > itemsCorrectlyPlaced[color])
        {
            itemsCorrectlyPlaced[color]++;
            PlayCorrectPlacementSound();  // Reproducir el sonido
        }
        CheckAllItemsPlacedCorrectly();
    }

    private void PlayCorrectPlacementSound()
    {
        if (audioSource != null)
        {
            audioSource.Play();  // Solo llama a Play ya que el AudioSource ya tiene el clip asignado
        }
        else
        {
            Debug.LogError("AudioSource is missing from GameManager!");
        }
    }

    public void ItemRemovedCorrectly(string color)
    {
        if (itemsCorrectlyPlaced[color] > 0)
        {
            itemsCorrectlyPlaced[color]--;
        }
    }

    public void CheckAllItemsPlacedCorrectly()
    {
        foreach (var color in itemsSpawned.Keys)
        {
            if (itemsSpawned[color] != itemsCorrectlyPlaced[color])
                return; // Si algún color no cumple con la condición, sale de la función.
        }
        // Si todos los objetos están correctamente colocados
        stateMachine.TransitionTo(stateMachine.showScoreState);
        ShowFinalScore();  // Llamar a mostrar el puntaje final
    }





    private void ShowFinalScore()
    {
        Debug.Log("Showing final score. Activating report canvas.");
        reportCanvas.SetActive(true);

        TMP_Text reportText = reportCanvas.GetComponentInChildren<TMP_Text>();
        if (reportText != null)
        {
            Debug.Log("Report text component found.");
            int totalPieces = itemsSpawned.Values.Sum();  // Usar LINQ para sumar puede simplificar el código
            string timeElapsed = timeTxt.text.Replace("Tiempo: ", "");
            reportText.text = $"Todos los objetos están correctamente colocados en sus respectivas cajas.\nNúmero de piezas totales: {totalPieces}\nTiempo de la prueba: {timeElapsed} Segundos";

            Debug.Log("Report text set: " + reportText.text);
        }
        else
        {
            Debug.Log("Report text component NOT found.");
        }
    }







}
