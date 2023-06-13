//Скрипт создан с поддержкой музыки The Living Tombstone - Ordinary Life. 
using UnityEngine;
using UnityEngine.UI;
public class Statistic : MonoBehaviour
{
    [SerializeField] public Text[] statisticText = new Text[8];

    private float seconds = 0;
    private int minutes;
    private int hours;

    [HideInInspector] public int[] Stats = new int[7];

    /*
     * Инструкция по массиву statisticText для чайников:
     * 1. ClickStatistic
     * 2. PlantedTreeStatistic
     * 3. expSpented
     * 4. AppleSpented
     * 5. AchievmentStatistic
     * 6. LocationBuyed
     * 7. timeInGame
     * 8. gameEntires
    */
    void Start()
    {
        //Загрузка всех данных
        seconds = PlayerPrefs.GetInt("seconds");
        minutes = PlayerPrefs.GetInt("minutes");
        hours = PlayerPrefs.GetInt("hours");
        statisticText[6].text = "Времени в игре: " + hours + "ч " + minutes + "м";
        for (int i = 0; i < 6; i++)
        {
            if (PlayerPrefs.HasKey(i.ToString() + " stat"))
            {
                Stats[i] = PlayerPrefs.GetInt(i.ToString() + " stat");
                OnValueChanged(i, 0);
            }
            else
            {
                Stats[i] = 0;
                OnValueChanged(i, 0);
            }
        }
        if (PlayerPrefs.HasKey("6 stat"))
        {
            Stats[6] = PlayerPrefs.GetInt("6 stat");
            OnValueChanged(6, 1);
        }
        else
        {
            OnValueChanged(6, 1);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Тута чисто работа часов. Они кстати хуево работают сейчас.
        seconds += Time.deltaTime;
        PlayerPrefs.SetFloat("seconds", seconds);
        PlayerPrefs.Save();
        if (seconds >= 60f)
        {
            minutes++;
            statisticText[6].text = "Времени в игре: " + hours + "ч " + minutes + "м";
            PlayerPrefs.SetInt("minutes", minutes);
            PlayerPrefs.Save();
            seconds = 0f;
        }
        if(minutes == 60)
        {
            hours++;
            statisticText[6].text = "Времени в игре: " + hours + "ч " + minutes + "м";
            PlayerPrefs.SetInt("hours", hours);
            PlayerPrefs.Save();
            seconds = 0f;
            minutes = 0;
        }
    }
    public void OnValueChanged(int Parametr, int ammount)
    {
        //Если какой то параметр меняется, то исходя из значения меняется и стата.
        switch (Parametr)
        {
            case 0:
                Stats[0] += ammount;
                statisticText[0].text = "Кликов: " + Stats[0];
                PlayerPrefs.SetInt("0 stat", Stats[0]);
                PlayerPrefs.Save();
                break;
            case 1:
                Stats[1] += ammount;
                statisticText[1].text = "Посажено деревьев: " + Stats[1];
                PlayerPrefs.SetInt("1 stat", Stats[1]);
                PlayerPrefs.Save();
                break;
            case 2:
                Stats[2] += ammount;
                statisticText[2].text = "Потрачено опыта: " + Stats[2];
                PlayerPrefs.SetInt("2 stat", Stats[2]);
                PlayerPrefs.Save();
                break;
            case 3:
                Stats[3] += ammount;
                statisticText[3].text = "Потрачено яблок: " + Stats[3];
                PlayerPrefs.SetInt("3 stat", Stats[3]);
                PlayerPrefs.Save();
                break;
            case 4:
                Stats[4] += ammount;
                statisticText[4].text = "Получено достижений: " + Stats[4];
                PlayerPrefs.SetInt("4 stat", Stats[4]);
                PlayerPrefs.Save();
                break;
            case 5:
                Stats[5] += ammount;
                statisticText[5].text = "Куплено локаций: " + Stats[5];
                PlayerPrefs.SetInt("5 stat", Stats[5]);
                PlayerPrefs.Save();
                break;
            case 6:
                Stats[6] += ammount;
                statisticText[7].text = "Заходов в игру: " + Stats[6];
                PlayerPrefs.SetInt("6 stat", Stats[6]);
                PlayerPrefs.Save();
                break;
            default:
                for (int i = 0; i < 7; i++)
                {
                    if(i == 6)
                    {
                        Stats[i] = 1;
                    }
                    else
                    {
                        Stats[i] = 0;
                    }
                }
                statisticText[0].text = "Кликов: " + Stats[0];
                statisticText[1].text = "Посажено деревьев: " + Stats[1];
                statisticText[2].text = "Потрачено опыта: " + Stats[2];
                statisticText[3].text = "Потрачено яблок: " + Stats[3];
                statisticText[4].text = "Получено достижений: " + Stats[4];
                statisticText[5].text = "Куплено локаций: " + Stats[5];
                statisticText[7].text = "Заходов в игру: " + Stats[6];
                PlayerPrefs.SetInt("6 stat", Stats[6]);
                PlayerPrefs.Save();
                break;
        }
    }
    public void RestoreStatistic()
    {
        //Удаление прогресса
        seconds = 0f;
        minutes = 0;
        hours = 0;
        statisticText[6].text = "Времени в игре: " + hours + "ч " + minutes + "м";
        for (int i = 0; i < 7; i++)
        {
            PlayerPrefs.DeleteKey(i.ToString() + " stat");
        }
        OnValueChanged(7, 0);
    }
}