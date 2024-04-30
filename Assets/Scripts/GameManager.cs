using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] int numberOfProducts = 1;

    [Header("Main menu")]
    [SerializeField] public GameObject mainMenuCanvas;
    [SerializeField] public Button startBtn;

    [Header("Gameplay")]
    [SerializeField] float _duration = 5.0f;

    public StateMachine stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new(this);
        stateMachine.Initialize(stateMachine.mainMenuState);
    }

    public void Gameplay()
    {
        // spawn area
        spawnAreaCenter = spawnArea.transform.position;
        spawnAreaSize = spawnArea.transform.localScale;

        // blue product factory
        FactoryProductBlue factoryProductBlue = this.GetComponent<FactoryProductBlue>();
        FactoryProductRed factoryProductRed = this.GetComponent<FactoryProductRed>();

        // create n products
        for (int i = 0; i < numberOfProducts; i++)
        {
            Vector3 randomPos = GetRandomPointInArea();
            // verify if pos is occupied
            int randomFactory = Random.Range(1, 3);
            if (randomFactory == 1)
            {
                IProduct item = factoryProductBlue.GetProduct(randomPos);
            }
            else
            {
                IProduct item = factoryProductRed.GetProduct(randomPos);
            }
        }

        StartCoroutine("GameplayTime");
    }

    IEnumerator GameplayTime()
    {
        while(_duration > 0)
        {
            _duration -= Time.deltaTime;
            Debug.Log("Tiempo limite: " +  _duration.ToString("F1"));
            yield return null;
        }
        yield return new WaitForSeconds(1);
        stateMachine.TransitionTo(stateMachine.showScoreState);
    }

    private Vector3 GetRandomPointInArea()
    {
        float x = UnityEngine.Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2);
        float y = UnityEngine.Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2);
        float z = UnityEngine.Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2);
        return new Vector3(x, y, z);
    }
}
