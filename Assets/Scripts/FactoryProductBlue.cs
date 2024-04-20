using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FactoryProductBlue : Factory
{
    [SerializeField] private ProductBlue productBluePrefab;

    public override IProduct GetProduct(Vector3 position)
    {
        GameObject instance = Instantiate(productBluePrefab.gameObject, position, Quaternion.identity);
        ProductBlue newProduct = instance.GetComponent<ProductBlue>();
        newProduct.Initialize();
        return newProduct;
    }
}
