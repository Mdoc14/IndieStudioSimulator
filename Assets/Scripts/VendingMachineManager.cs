using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineManager : MonoBehaviour
{
    [SerializeField] int maxNumberOfProducts;
    [SerializeField] int numberOfProducts;

    private void Start()
    {
        numberOfProducts = maxNumberOfProducts;
    }

    public bool IsEmpty()
    {
        return numberOfProducts == 0;
    }

    public void RefillMachine()
    {
        numberOfProducts = maxNumberOfProducts;
    }

    public void TakeProduct()
    {
        if (numberOfProducts > 0)
        {
            numberOfProducts--;
        }
    }
}
