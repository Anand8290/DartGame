using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    private string removeads = "com.ak2dstudios.dartsmaster.removeads";


    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id == removeads)
        {
            Debug.Log("All ads removed");
        }
        
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + " failed due to " + failureReason);
    }
}
