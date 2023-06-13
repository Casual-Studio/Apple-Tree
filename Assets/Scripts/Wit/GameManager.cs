//Скрипт создан с поддержкой музыки The Living Tombstone - Ordinary Life. 
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public int appleCount;
    [HideInInspector] public int xpCount;

    [HideInInspector] public bool isLackCurrencyX = false;
    [HideInInspector] public bool isLackCurrencyA = false;
    
    private bool once = true;

    [SerializeField] private TextMeshProUGUI appleText;
    [SerializeField] private TextMeshProUGUI xpText;

    [SerializeField] private TextMeshProUGUI appleText1;
    [SerializeField] private TextMeshProUGUI xpText1;

    [SerializeField] private DeleteTreesSaves deleteTreesSaves;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Statistic stats;
    [SerializeField] private DeleteAchievments AM;
    [SerializeField] private LocationLogic ll;
    [SerializeField] private PurchasesManager PM;
    [SerializeField] private AcessLogic AL;
    [SerializeField] private BoostManager BM;
    [SerializeField] private LocationTreesManager LTM;
    [SerializeField] private EducationFirstTime Education;

    [SerializeField] private Button ShopFade;
    void Start()
    {
        Application.targetFrameRate = 60;
        deleteTreesSaves = Object.FindObjectOfType<DeleteTreesSaves>();

        //Восстановка валюты при старте
        if (PlayerPrefs.HasKey("AppleCount"))
        {
            AppleChange(PlayerPrefs.GetInt("AppleCount"));
            XPChange(PlayerPrefs.GetInt("XpCount"));
        }
        else
        {
            appleCount = 0;
            xpCount = 550;
            PlayerPrefs.SetInt("AppleCount", appleCount);
            PlayerPrefs.SetInt("XpCount", xpCount);
        }
    }

    void Update()
    {
       if(isLackCurrencyX && once)
       {
            once = false;
            StartCoroutine(WaitLackOfCurrency('x'));
       }
       if(isLackCurrencyA && once)
       {
            once = false;
            StartCoroutine(WaitLackOfCurrency('a'));
        }
    }

    public void DestroyAllProgress()
    {
        //Ух... сколько же здесь функций... (Удаление прогресса)
        CurrencyRestore();
        deleteTreesSaves.DeleteTreeProgress();
        stats.RestoreStatistic();
        Inventory.DeleteInventorySaves();
        AM.DeleteAchievmentProgress();
        ll.DeleteLocationProgress();
        PM.RestorePurchases();
        AL.DeleteAllAcess();
        BM.DeleteBoostProgress();
        LTM.DestroyLTMProgress();
        Education.DeleteEducationProgress();
    }
    //Функция для изменения счёта опыта.
    public void XPChange(int ammount)
    {
        xpCount += ammount;
        xpText.text = xpCount.ToString();
        xpText1.text = xpCount.ToString();
    }
    //Функция для изменения счёта яблок.
    public void AppleChange(int ammount)
    {
        appleCount += ammount;
        appleText.text = appleCount.ToString();
        appleText1.text = appleCount.ToString();
        PlayerPrefs.SetInt("AppleCount", appleCount);
    }
    //Полный возврат валют к началу.
    public void CurrencyRestore()
    {
        xpCount = 550;
        xpText.text = xpCount.ToString();
        xpText1.text = xpCount.ToString();
        appleCount = 0;
        appleText.text = appleCount.ToString();
        appleText1.text = appleCount.ToString();
        PlayerPrefs.SetInt("AppleCount", appleCount);
        PlayerPrefs.SetInt("XpCount", xpCount);
    }   

    //Корутина когда нет тенге на балансе.
    public IEnumerator WaitLackOfCurrency(char aORx)
    {
        if(aORx == 'a')
        {
            Handheld.Vibrate();
            appleText.color = new Color(255, 0, 0);
            appleText1.color = new Color(255, 0, 0);
            yield return new WaitForSeconds(0.80f);
            appleText.color = new Color(255, 255, 255);
            appleText1.color = new Color(255, 255, 255);
            isLackCurrencyA = false;
            once = true;
        }
        else
        {
            Handheld.Vibrate();
            xpText.color = new Color(255, 0, 0);
            xpText1.color = new Color(255, 0, 0);
            yield return new WaitForSeconds(0.80f);
            xpText.color = new Color(255, 255, 255);
            xpText1.color = new Color(255, 255, 255);
            isLackCurrencyX = false;
            once = true;
        }
    }
}