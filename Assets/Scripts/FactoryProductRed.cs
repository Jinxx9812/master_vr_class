using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FactoryProductRed : Factory
{
    [SerializeField] private ProductRed productRedPrefab;

    public override IProduct GetProduct(Vector3 position)
    {
        GameObject instance = Instantiate(productRedPrefab.gameObject, position, Quaternion.identity);
        ProductRed newProduct = instance.GetComponent<ProductRed>();
        newProduct.Initialize();
        return newProduct;
    }
}
