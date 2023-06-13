//Скрипт создан с поддержкой музыки The Living Tombstone - Ordinary Life. 
using UnityEngine.UI;
using UnityEngine;

public class AchievmentManager : MonoBehaviour
{
    [HideInInspector] public bool achievmentBoolClaimed;
    [HideInInspector] public bool achievmentWaitForClaim;
    //bool для вызова waitforclaim единожды ибо он в update.
    [HideInInspector] public bool Once = true;

    private int AchievmentID;

    [SerializeField] private Statistic stats;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LocationTreesManager LTM;
    [SerializeField] private LocationLogic ll;

    public Button aboutBtn;
    public Button claimBtn;
    public Text claimedRewardTxt;
    public Text progressTxt;
    [SerializeField] private int TreeTypeNums = -1;
    

    void Start()
    {
        LTM = FindObjectOfType<LocationTreesManager>();
        ll = FindObjectOfType<LocationLogic>();
        //Простое уточнение, что stats.Stats[0] - клики. Я присвоих их снова, ибо при запуске игры они не присваивались если не заходить в стату.
        stats.Stats[0] = PlayerPrefs.GetInt("0 stat");
        if(PlayerPrefs.HasKey(AchievmentID + " achievmentWait"))
        {
            Once = true;
            WaitForClaim(0,0);
        }
        else if(PlayerPrefs.HasKey(AchievmentID + " achievmentClaimed"))
        {
            AlreadyClaimed();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Здесь происходит этап проверки достижений на получаемость.
        //----------0----------
        if (AchievmentID == 0 && !achievmentBoolClaimed)
        {
            int timeParametr = 0;
            for(int i = 0; i < 8; i++)
            {
                if (LTM.TreesNLocations[ll.equipedLocation,i] == 0)
                {
                    timeParametr++;
                }
                else
                {
                    timeParametr = 0;
                    break;
                }
            }
            if(timeParametr == 8)
            {
                WaitForClaim(50, 500);
            }
        }
        //----------1----------
        if (stats.Stats[0] >= 64 && AchievmentID == 1 && !achievmentBoolClaimed)
        {
            //К примеру рассмотрим это: При наличии кликов в стате больше 64, и id ачивки (указывается в инспекторе)
            //равно единице и достижение не получено, то выполняем этот код. (Уточнение текста нужно, чтобы прогресс не уходил
            //дальше.
            WaitForClaim(64,64);
            progressTxt.text = "64/64";
        }
        else if (stats.Stats[0] < 64 && AchievmentID == 1)
        {
            //Здесь если достижение не выполнено, не готовое к взятию, то прогресс накапливается от статы.
            progressTxt.text = stats.Stats[0] + "/64";
        }
        //----------2----------
        if (stats.Stats[0] >= 256 && AchievmentID == 2 && !achievmentBoolClaimed)
        {
            WaitForClaim(256,256);
            progressTxt.text = "256/256";
        }
        else if (stats.Stats[0] < 256 && AchievmentID == 2)
        {
            progressTxt.text = stats.Stats[0] + "/256";
        }
        //----------3----------
        if (stats.Stats[0] >= 2048 && AchievmentID == 3 && !achievmentBoolClaimed)
        {
            WaitForClaim(2048,2048);
            progressTxt.text = "2048/2048";
        }
        else if (stats.Stats[0] < 2048 && AchievmentID == 3)
        {
            progressTxt.text = stats.Stats[0] + "/2048";
        }
        //----------4----------
        if (AchievmentID == 4 && !achievmentBoolClaimed)
        {
            int timeParametr = 0;
            for (int i = 0; i < 8; i++)
            {
                if (LTM.TreesNLocations[ll.equipedLocation, i] == 4)
                {
                    timeParametr++;
                }
                else
                {
                    timeParametr = 0;
                    break;
                }
            }
            if (timeParametr == 8)
            {
                WaitForClaim(1000, 500);
            }
        }
        //----------5----------
        if (AchievmentID == 5 && !achievmentBoolClaimed)
        {
            int timeParametr = 0;
            for (int i = 0; i < 8; i++)
            {
                if (LTM.TreesNLocations[ll.equipedLocation, i] == 3)
                {
                    timeParametr++;
                }
                else
                {
                    timeParametr = 0;
                    break;
                }
            }
            if (timeParametr == 8)
            {
                WaitForClaim(1000, 500);
            }
        }
        //----------6----------
        if (PlayerPrefs.HasKey("firstSeedPlanted") && AchievmentID == 6 && !achievmentBoolClaimed)
        {
            WaitForClaim(100, 150);
            progressTxt.text = "1/1";
        }
        //----------7----------

        //----------8----------
        if (PlayerPrefs.HasKey("FirstAcess") && AchievmentID == 8 && !achievmentBoolClaimed)
        {
            WaitForClaim(50, 500);
            progressTxt.text = "1/1";
        }
        else if(!PlayerPrefs.HasKey("FirstAcess") && AchievmentID == 8)
        {
            progressTxt.text = "0/1";
        }
        //----------9----------
        if(AchievmentID == 9 && !achievmentBoolClaimed)
        {
            int TimeIndex = 0;
            for (int i = 0; i < LTM.TreeTypes.Length; i++)
            {
                if (LTM.TreeTypes[i])
                {
                    TimeIndex++;
                }
                else
                {
                    TimeIndex = 0;
                    break;
                }
            }
            if (TimeIndex == 8)
            {
                WaitForClaim(500, 500);
            }
        }
        //----------10----------
        if (AchievmentID == 10 && !achievmentBoolClaimed)
        {
            int timeParametr = 0;
            for (int i = 0; i < 7; i++)
            {
                for(int k = 0; k < 8; k++)
                {
                    if (LTM.TreesNLocations[i, k] != -1)
                    {
                        timeParametr++;
                        break;
                    }
                }
            }
            if (timeParametr == 7)
            {
                WaitForClaim(200,200);
            }
        }
        //----------11----------
        if (AchievmentID == 11 && !achievmentBoolClaimed)
        {
            int timeIndex = 0;
            for (int i = 0; i < 8; i++)
            {
                if (LTM.TreesNLocations[ll.equipedLocation, i] == 3 || LTM.TreesNLocations[ll.equipedLocation, i] == 4)
                {
                    timeIndex++;
                }
                else
                {
                    timeIndex = 0;
                    break;
                }
            }
            if(timeIndex == 8)
            {
                WaitForClaim(2000, 2000);
            }
        }
        //----------12----------
        if(AchievmentID == 12 && !achievmentBoolClaimed)
        {
            if(gameManager.appleCount >= 4096 && gameManager.xpCount >= 4096)
            {
                WaitForClaim(20, 200);
            }
        }
        //----------13----------
        if (AchievmentID == 13 && !achievmentBoolClaimed)
        {
            int timeIndex = 0;
            for(int i = 0; i < ll.isBuyed.Length; i++)
            {
                if (ll.isBuyed[i])
                {
                    timeIndex++;
                    progressTxt.text = timeIndex + "/7";
                }
                else
                {
                    timeIndex = 0;
                    break;
                }
            }
            if(timeIndex == ll.isBuyed.Length)
            {
                WaitForClaim(200, 2000);
            }
        }
        //----------14----------
        if (AchievmentID == 14 && !achievmentBoolClaimed)
        {
            int locIndex = 0;
            int treeIndex = 0;
            for(int loc = 0; loc < 7; loc++)
            {
                for(int tree = 0; tree < 8; tree++)
                {
                    if (LTM.TreesNLocations[loc, tree] != -1)
                    {
                        treeIndex++;
                    }
                    else
                    {
                        treeIndex = 0;
                        break;
                    }
                    if (treeIndex == 8)
                    {
                        treeIndex = 0;
                        locIndex++;
                    }
                }
            }
            if(locIndex == 7)
            {
                locIndex = 0;
                WaitForClaim(5000, 3000);
            }
        }
        //----------15----------
        if (AchievmentID == 15 && !achievmentBoolClaimed)
        {
            int locIndex = 0;
            int treeIndex = 0;
            for (int loc = 0; loc < 7; loc++)
            {
                for (int tree = 0; tree < 8; tree++)
                { 
                    if (LTM.TreesNLocations[loc, tree] != -1 && TreeTypeNums == -1)
                    {
                        TreeTypeNums = LTM.TreesNLocations[loc, tree];
                        treeIndex++;
                    }
                    else
                    {
                        if (LTM.TreesNLocations[loc,tree] == TreeTypeNums && LTM.TreesNLocations[loc, tree] != -1)
                        {
                            treeIndex++;
                        }
                        else
                        {
                            treeIndex = 0;
                            TreeTypeNums = -1;
                            locIndex = 0;
                            break;
                        }
                    }
                }
                if(treeIndex >= 8)
                {
                    treeIndex = 0;
                    locIndex++;
                }
            }
            if (locIndex == 7)
            {
                WaitForClaim(50000, 50000);
            }
        }
    }

    public void WaitForClaim(int AppleCount, int XpCount)
    {
        if (Once)
        {
            //Кнопку подробнее вырубаю, включаю кнопку забрать и даю ей onclick метод, устанавливаю playerprefs и bool, что ачивка ждёт взятия.
            aboutBtn.gameObject.SetActive(false);
            claimBtn.gameObject.SetActive(true);
            claimBtn.onClick.AddListener(() => Claim(AppleCount, XpCount));
            Once = false;
            achievmentWaitForClaim = true;
            PlayerPrefsExtra.SetBool(AchievmentID + " achievmentWait", achievmentWaitForClaim);
        }
    }
    public void Claim(int AppleCount, int XpCount)
    {
        //Прибавляю к статам + к полученным достижениям, прибавляю награду, перехожу в alreadyClaimed();
        stats.OnValueChanged(4, 1);
        gameManager.AppleChange(AppleCount);
        gameManager.XPChange(XpCount);
        AlreadyClaimed();
    }
    public void AlreadyClaimed()
    {
        //Если progressTxt (текст прогресс) НЕ отсутствует, то вырубается. Все кнопки вырубаются, ключ удаляется,
        //текст полученной награды включается, устанавливаю ключ, что достижение получено
        if(progressTxt != null)
        {
            progressTxt.gameObject.SetActive(false);
        }
        aboutBtn.gameObject.SetActive(false);
        claimBtn.gameObject.SetActive(false);
        achievmentWaitForClaim = false;
        PlayerPrefs.DeleteKey(AchievmentID + " achievmentWait");
        achievmentBoolClaimed = true;
        PlayerPrefsExtra.SetBool(AchievmentID + " achievmentClaimed", achievmentBoolClaimed);
        claimedRewardTxt.gameObject.SetActive(true);
    }
}
