//Скрипт написан с помощью The Living Tombstone - My Ordinary Life;
using UnityEngine;
using UnityEngine.UI;

public class AcessLogic : MonoBehaviour
{ 
    private int currentLocationOnScreen = 0;

    [HideInInspector] public int itemid;
    [HideInInspector] public bool isFirstBuyedAcess;

    [SerializeField] private GameObject[] Acesses = new GameObject[28];
    [SerializeField] private GameObject[] locations = new GameObject[7];

    [SerializeField] private GameObject AcessPanel;

    [SerializeField] private Button[] equipBtn = new Button[7];

    [SerializeField] private Button backArr;
    [SerializeField] private Button nextArr;
    [SerializeField] private Button backToTheShop;
    [SerializeField] private Button Fade;

    [SerializeField] private Text[] alreadyEquipedTxt = new Text[7];
    [SerializeField] private Text[] locationDidntBuyedTxt = new Text[7];

    [SerializeField] private LocationLogic ll;
    [SerializeField] private GameManager GM;

    private void Start()
    {
        //Добавляю на старте onclick на fade за окном магазина и на кнопку назад.
        Fade.onClick.AddListener(() => ListenerToButtons());
        backToTheShop.onClick.AddListener(() => ListenerToButtons());
        for (int i = 1; i < locations.Length; i++)
        {
            Fade.onClick.AddListener(() => locations[i].SetActive(false));
            backToTheShop.onClick.AddListener(() => locations[i].SetActive(false));
        }
        currentLocationOnScreen = 0;
        for (int i = 0; i < 8; i++)
        {
            if (PlayerPrefs.HasKey("Acess buyed on location " + i))
            {
                for (int k = 0; k < 4; k++)
                {
                    if (PlayerPrefs.HasKey("Acess " + k + " buyed on " + i))
                    {
                        Acesses[i * 4 + k].SetActive(true);
                    }
                }
            }
        }
    }

    void Update()
    {
        //Логика проверки состояния аксессуара. Куплен, нет локации, нет аксессура на локации.
        if (PlayerPrefs.HasKey("Acess " + itemid + " buyed on " + currentLocationOnScreen))
        {
            equipBtn[currentLocationOnScreen].gameObject.SetActive(false);
            locationDidntBuyedTxt[currentLocationOnScreen].gameObject.SetActive(false);
            alreadyEquipedTxt[currentLocationOnScreen].gameObject.SetActive(true);
        }
        else if (!ll.isBuyed[currentLocationOnScreen] && !PlayerPrefs.HasKey("Acess " + itemid + " buyed on " + currentLocationOnScreen))
        {
            alreadyEquipedTxt[currentLocationOnScreen].gameObject.SetActive(false);
            equipBtn[currentLocationOnScreen].gameObject.SetActive(false);
            locationDidntBuyedTxt[currentLocationOnScreen].gameObject.SetActive(true);
        }
        else
        {
            alreadyEquipedTxt[currentLocationOnScreen].gameObject.SetActive(false);
            locationDidntBuyedTxt[currentLocationOnScreen].gameObject.SetActive(false);
            equipBtn[currentLocationOnScreen].gameObject.SetActive(true);
        }
    }

    void ListenerToButtons()
    {
        AcessPanel.SetActive(false);
        locations[0].SetActive(true);
        currentLocationOnScreen = 0;
        backArr.gameObject.SetActive(false);
        nextArr.gameObject.SetActive(true);
    }
    //Функция для кнопки
    public void nextLocation()
    {
        locations[currentLocationOnScreen].SetActive(false);
        currentLocationOnScreen++;
        locations[currentLocationOnScreen].SetActive(true);
        if (currentLocationOnScreen == 6)
        {
            nextArr.gameObject.SetActive(false);
        }
        else
        {
            nextArr.gameObject.SetActive(true);
            backArr.gameObject.SetActive(true);
        }
    }
    //Функция для кнопки
    public void previousLocation()
    {
        locations[currentLocationOnScreen].SetActive(false);
        currentLocationOnScreen--;
        locations[currentLocationOnScreen].SetActive(true);
        if(currentLocationOnScreen == 0)
        {
            backArr.gameObject.SetActive(false);
        }
        else
        {
            nextArr.gameObject.SetActive(true);
            backArr.gameObject.SetActive(true);
        }
    }
    //То, что происходит при покупке локации.
    public void onAcessBuy()
    {
        if (!isFirstBuyedAcess)
        {
            isFirstBuyedAcess = true;
            PlayerPrefsExtra.SetBool("FirstAcess", isFirstBuyedAcess);
        }
        PlayerPrefs.SetInt("Acess " + itemid + " buyed on " + currentLocationOnScreen, 1);
        if (!PlayerPrefs.HasKey("Acess buyed on location " + currentLocationOnScreen))
        {
            PlayerPrefs.SetInt("Acess buyed on location " + currentLocationOnScreen, 1);
        }
        Acesses[currentLocationOnScreen * 4 + itemid].SetActive(true);
        switch (itemid) {
            case 0:
                GM.AppleChange(-1200);
                break;
            case 1:
                GM.AppleChange(-700);
                break;
            case 2:
                GM.AppleChange(-1300);
                break;
            case 3:
                GM.AppleChange(-600);
                break;
        }
        equipBtn[currentLocationOnScreen].gameObject.SetActive(false);
        alreadyEquipedTxt[currentLocationOnScreen].gameObject.SetActive(false);
        locationDidntBuyedTxt[currentLocationOnScreen].gameObject.SetActive(false);
        AcessPanel.SetActive(false);
    }
    public void DeleteAllAcess()
    {
        for (int i = 0; i < 8; i++)
        {
            PlayerPrefs.DeleteKey("Acess buyed on location " + i);
            for (int k = 0; k < 4; k++)
            {
                PlayerPrefs.DeleteKey("Acess " + k + " buyed on " + i);
            }
        }
        for (int i = 0; i < Acesses.Length; i++)
        {
            Acesses[i].SetActive(false);
        }
        PlayerPrefs.DeleteKey("FirstAcess");
    }
}
