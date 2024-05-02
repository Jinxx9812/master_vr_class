using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("Endgame")]
    [SerializeField] public GameObject scoreCanvas;
    [SerializeField] public Button saveBtn;

    public StateMachine stateMachine;

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

        // create n products
        for (int i = 0; i < numberOfItems; i++)
        {
            Vector3 randomPos = GetRandomPointInArea();

            IProduct item;

            int randomFactory = Random.Range(1, 3);
            if (randomFactory == 1)
            {
                item = factoryProductBlue.GetProduct(randomPos);
            }
            else
            {
                item = factoryProductRed.GetProduct(randomPos);
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

        // Reinicio las variables de cada caja
        BoxCounter boxCounterBlue = blueBox.GetComponentInChildren<BoxCounter>();
        boxCounterBlue.RestartScore();
        BoxCounter boxCounterRed = redBox.GetComponentInChildren<BoxCounter>();
        boxCounterRed.RestartScore();
    }
}
