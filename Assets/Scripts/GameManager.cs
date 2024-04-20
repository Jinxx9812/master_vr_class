using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Spawn reference")]
    [SerializeField] GameObject spawnArea;
    Vector3 spawnAreaCenter;
    Vector3 spawnAreaSize;

    [Header("Product references")]
    [SerializeField] int numberOfProducts = 1;

    // Start is called before the first frame update
    void Start()
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
    }

    private Vector3 GetRandomPointInArea()
    {
        float x = UnityEngine.Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2);
        float y = UnityEngine.Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2);
        float z = UnityEngine.Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2);
        return new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
