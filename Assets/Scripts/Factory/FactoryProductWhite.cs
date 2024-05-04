using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FactoryProductWhite : Factory
{
    [SerializeField] private ProductWhite productWhitePrefab;

    public override IProduct GetProduct(Vector3 position)
    {
        GameObject instance = Instantiate(productWhitePrefab.gameObject, position, Quaternion.identity);
        ProductWhite newProduct = instance.GetComponent<ProductWhite>();
        newProduct.Initialize();
        return newProduct;
    }
}
