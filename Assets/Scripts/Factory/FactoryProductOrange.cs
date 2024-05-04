using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FactoryProductOrange : Factory
{
    [SerializeField] private ProductOrange productOrangePrefab;

    public override IProduct GetProduct(Vector3 position)
    {
        GameObject instance = Instantiate(productOrangePrefab.gameObject, position, Quaternion.identity);
        ProductOrange newProduct = instance.GetComponent<ProductOrange>();
        newProduct.Initialize();
        return newProduct;
    }
}
