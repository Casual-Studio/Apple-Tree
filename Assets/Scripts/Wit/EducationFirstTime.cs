//Скрипт создан с поддержкой музыки The Living Tombstone - Ordinary Life. 
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EducationFirstTime : MonoBehaviour
{
    [HideInInspector] public bool FirstTimePlayin;

    private bool isShopChecked;
    private bool isAppleBuyed;
    private bool isInventoryOpened;
    private bool Once;
    private bool Clicked;

    [HideInInspector] public bool isTreeIsGrowed;
    [HideInInspector] public bool isWaterCanDragged;

    [SerializeField] private Button ShopFade;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject Inventory;

    [SerializeField] private GameObject[] stages = new GameObject[6];
    [SerializeField] private GameObject[] ClickTexts = new GameObject[3];

    [SerializeField] private Button[] gameObjectPool = new Button[7];

    private int StageParameter;
    private int appleCount;

    //Массив с текстами. Нужен для анимации печати.
    /*Инструкция по массиву Texts для чайников:
     * 0 - 1 = Первая стадия
     * 2 - Текст клика
     * 3 - 7 = Вторая стадия
     * 8 - Текст клика
     * 9 - 13 = Третья стадия
     * 14 - 17 = Четвертая стадия
     * 18 - 21 = Пятая стадия
     * 21 - 23 = Шестая стадия
     * 24 - Текст клика
    */
    [SerializeField] private Text[] Texts = new Text[22];
    
    private string text;

    [SerializeField] private CameraMovement CM;
    void Start()
    {
        if (!PlayerPrefs.HasKey("FirstTimePlayed"))
        {
            gameManager.DestroyAllProgress();
            FirstTimePlayin = false;
            StageParameter = 0;
            Education();
        }
    }
    private void Update()
    {
        //Проверка на возможность пройти дальше на некоторых стадиях
        appleCount = PlayerPrefs.GetInt("0");
        if(appleCount == 1)
        {
            isAppleBuyed = true;
        }
        if (!isAppleBuyed && StageParameter == 2)
        {
            ShopFade.interactable = false;
        }
        else 
        { 
            ShopFade.onClick.AddListener(() => stageCounterPlus());
            ShopFade.interactable = true;
        }
        if (isShopChecked)
        {
            stages[StageParameter].SetActive(false);
        }
        if(isInventoryOpened && StageParameter == 3)
        {
            stages[StageParameter].SetActive(false);
        }
        if (PlayerPrefs.HasKey("firstSeedPlanted") && StageParameter == 3)
        {
            stageCounterPlus();
        }
        if(isWaterCanDragged && StageParameter == 4)
        {
            stages[StageParameter].SetActive(false);
        }
        if (isTreeIsGrowed && StageParameter == 4)
        {
            stageCounterPlus();
        }
    }
    void Education()
    {
        //ТЗ игре что делать при том или ином параметре стадии
        CM.enabled = false;
        switch (StageParameter)
        {
            case 0:
                stages[StageParameter].SetActive(true);
                if (Once)
                {
                    Once = false;
                    StartCoroutine(TextTypeCoroutine(0, 2));
                }
                break;
            case 1:
                stages[StageParameter -1].SetActive(false);
                stages[StageParameter].SetActive(true);
                if (Once)
                {
                    Once = false;
                    StartCoroutine(TextTypeCoroutine(3, 6));
                }
                break;
            case 2:
                stages[StageParameter - 1].SetActive(false);
                stages[StageParameter].SetActive(true);
                if (Once)
                {
                    Once = false;
                    StartCoroutine(TextTypeCoroutine(6, 11));
                }
                gameObjectPool[0].interactable = false;
                gameObjectPool[2].interactable = false;
                gameObjectPool[1].interactable = false;
                gameObjectPool[3].interactable = false;
                gameObjectPool[4].interactable = false;
                gameObjectPool[5].interactable = false;
                gameObjectPool[6].interactable = false;
                break;
            case 3:
                stages[StageParameter - 1].SetActive(false);
                stages[StageParameter].SetActive(true);
                if (Once)
                {
                    Once = false;
                    StartCoroutine(TextTypeCoroutine(10, 15));
                }
                gameObjectPool[0].interactable = false;
                gameObjectPool[2].interactable = false;
                gameObjectPool[3].interactable = false;
                gameObjectPool[4].interactable = false;
                
                break;
            case 4:
                stages[StageParameter - 1].SetActive(false);
                stages[StageParameter].SetActive(true);
                if (Once)
                {
                    Once = false;
                    StartCoroutine(TextTypeCoroutine(15, 19));
                }
                Inventory.SetActive(false);
                gameObjectPool[1].interactable = false;
                gameObjectPool[2].interactable = false;
                gameObjectPool[3].interactable = false;
                gameObjectPool[4].interactable = false;
                break;
            case 5:
                stages[StageParameter -1].SetActive(false);
                stages[StageParameter].SetActive(true);
                if (Once)
                {
                    Once = false;
                    StartCoroutine(TextTypeCoroutine(19, 21));
                }
                break;
            case 6:
                stages[5].SetActive(false);
                FirstTimePlayin = true;
                PlayerPrefsExtra.SetBool("FirstTimePlayed", FirstTimePlayin);
                CM.enabled = true;
                break;
        }
    }
    public void stageCounterPlus()
    {
        //При клике дальше проверяет, есть ли возможность кликать.
        Clicked = true;
        if (StageParameter == 0 && !ClickTexts[0].activeSelf)
            return;
        if (StageParameter == 1 && !ClickTexts[1].activeSelf)
            return;
        if (StageParameter == 5 && !ClickTexts[2].activeSelf)
            return;
        if (!isShopChecked && StageParameter == 2)
            return;
        if (!isInventoryOpened && StageParameter == 3)
            return;
        if (!isTreeIsGrowed && StageParameter == 4)
            return;
        isShopChecked = false;
        ShopFade.onClick.RemoveListener(() => stageCounterPlus());
        StageParameter++;
        Once = true;
        StopAllCoroutines();
        Education();
    }

    public void BoolChanged(int type)
    {
        //Пустышка для изменения булевых переменных
        if(type == 0)
            isShopChecked = true;
        if (type == 1)
            isInventoryOpened = true;
    }

    public void DeleteEducationProgress()
    {
        PlayerPrefs.DeleteKey("FirstTimePlayed");
        FirstTimePlayin = false;
        StageParameter = 0;
        for (int i = 0; i < Texts.Length; i++)
        {
            Texts[i].gameObject.SetActive(false);
            if(i < 3)
            {
                ClickTexts[i].gameObject.SetActive(false);
            }
        }
        Once = true;
        Clicked = false;
        Education();
    }
    //Корутина для печати текста. Она работает странно в коде. Но делает это. Если сможешь, перепиши по лучше
    //буду рад. Удачи.
    IEnumerator TextTypeCoroutine(int WhichText, int HowMuchTexts)
    {
        for(int i = WhichText; i <= HowMuchTexts; i++)
        {
            Texts[i].gameObject.SetActive(true);
            text = Texts[i].text;
            Texts[i].text = "";
            foreach (char abc in text)
            {
                Texts[i].text += abc;
                if (Clicked)
                {
                    Texts[i].text = text;
                    text = "";
                    Clicked = false;
                    break;
                }
            yield return new WaitForSeconds(0.04f);
            }
            if (i == HowMuchTexts)
            {
                yield return new WaitForSeconds(0.5f);
                switch (StageParameter)
                {
                    case 0:
                        ClickTexts[0].SetActive(true);
                        break;
                    case 1:
                        ClickTexts[1].SetActive(true);
                        break;
                    case 2:
                        gameObjectPool[2].interactable = true;
                        break;
                    case 3:
                        gameObjectPool[1].interactable = true;
                        gameObjectPool[5].interactable = true;
                        gameObjectPool[6].interactable = true;
                        break;
                    case 4:
                        gameObjectPool[0].interactable = true;
                        break;
                    case 5:
                        ClickTexts[2].SetActive(true);
                        break;
                }
                StopAllCoroutines();
            }
        }
    }
}
