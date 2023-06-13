//Скрипт создан с поддержкой музыки The Living Tombstone - Ordinary Life. 
using UnityEngine;

public class OneBuyItems : MonoBehaviour
{
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private GameObject TreePanel;
    [SerializeField] private GameObject SkinPanel;

    [SerializeField] private LocationLogic ll;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PurchasesManager PM;
    [SerializeField] private AcessLogic AL;

    public int cost;
    public int Id;

    [SerializeField] private GameObject acessPanel;

    public void onBuyLocation()
    {
        //Покупка локаций и пересыл туда id.
        if (gameManager.appleCount >= cost && !PlayerPrefs.HasKey("isBuyed " + Id))
        {
            gameManager.AppleChange(-cost);
            PM.LocationCostText[Id - 1].text = "Куплено";
            PM.LocationCurrencyRender[Id - 1].gameObject.SetActive(false);
            ll.OnBuy(Id);
        }
        else
        {
            gameManager.isLackCurrencyA = true;
        }
    }
    public void onAcessBuy()
    {
        //Покупка аксессуаров и пересыл туда id.
        if(gameManager.appleCount >= cost)
        {
            acessPanel.SetActive(true);
            AL.itemid = Id;
            ShopPanel.SetActive(false);
            TreePanel.SetActive(true);
            SkinPanel.SetActive(false);
        }
        else
        {
            gameManager.isLackCurrencyA = true;
        }
    }
}
