//Скрипт создан с поддержкой музыки The Living Tombstone - Ordinary Life. 
using UnityEngine.EventSystems;
using UnityEngine;

public class ClickOnScreen : MonoBehaviour, IPointerDownHandler
{
    //Скрипт для получения чего-то за клик по дереву
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Statistic stats;
    [SerializeField] private BoostManager BM;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        BM = FindObjectOfType<BoostManager>();
    }

    public void Click(int applePerClick, int xpPerClick, int locationId, int howManyClicks)
    {
        if(locationId != -1 && howManyClicks != -1)
        {
            if (BM.ClickCounter[locationId] >= howManyClicks)
            {
                BM.ClickCounter[locationId] = 0;
            }
            else
            {
                BM.ClickCounter[locationId]++;
            }
        }
        gameManager.AppleChange(applePerClick);
        PlayerPrefs.SetInt("AppleCount", gameManager.appleCount);
        gameManager.XPChange(xpPerClick);
        PlayerPrefs.SetInt("XpCount", gameManager.xpCount);
        stats.OnValueChanged(0, 1);
        PlayerPrefs.Save();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BM.CalculateBoost();
        Click(BM.ApplePerClick, BM.XpPerClick, BM.locationId, BM.HowManyClicks);
    }
}
