using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    private string removeads = "com.ak2dstudios.dartsmaster.removeads";
    [SerializeField] SettingsPanel settingsPanel;

    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id == removeads)
        {
            Debug.Log("All ads removed");
            PlayerPrefs.SetInt("REMOVEADS", 1);
            settingsPanel.UpdateRemoveAdsButton();
        }
        
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + " failed due to " + failureReason);
    }
}
