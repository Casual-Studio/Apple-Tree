//Скрипт создан с поддержкой музыки The Living Tombstone - Ordinary Life. 
using UnityEngine.UI;
using UnityEngine;

public class PurchasesManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [Header("Локации: ")]
    public Text[] LocationCostText = new Text[7];
    public SpriteRenderer[] LocationCurrencyRender = new SpriteRenderer[7];
    [Header("Украшения: ")]
    public Text[] AcessCostText = new Text[4];
    public SpriteRenderer[] AcessCurrencyRender = new SpriteRenderer[4];
    void Start()
    {
        //Восстанавливает покупки. Если что то куплено, то заменяет текст.
        for (int i = 0; i < LocationCostText.Length; i++)
        {
            if (PlayerPrefs.HasKey("isBuyed " + (i + 1)))
            {
                LocationCostText[i].text = "Куплено";
                LocationCurrencyRender[i].gameObject.SetActive(false);
            }
            if (PlayerPrefs.HasKey("AcessBuyed " + i) && i < 4)
            {
                AcessCostText[i].text = "Куплено";
                AcessCurrencyRender[i].gameObject.SetActive(false);
            }
        }
    }

    public void RestorePurchases()
    {
        //Удаляет всё нахуй
        for (int i = 0; i < LocationCurrencyRender.Length; i++)
        {
            LocationCurrencyRender[i].gameObject.SetActive(true);
            if(i < 4)
            {
                AcessCurrencyRender[i].gameObject.SetActive(true);
            }
        }
        LocationCostText[0].text = "400";
        LocationCostText[1].text = "600";
        LocationCostText[2].text = "900";
        LocationCostText[3].text = "1400";
        LocationCostText[4].text = "3200";
        LocationCostText[5].text = "4800";
        AcessCostText[0].text = "600";
        AcessCostText[1].text = "700";
        AcessCostText[2].text = "1200";
        AcessCostText[3].text = "1300";
    }
}
