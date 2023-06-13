using UnityEngine;
using System.Collections;
using Kilosoft.Tools;

public enum treeState
{
    mark,
    seed,
    firstStage,
    secondStage,
    thirdStage
}

public class TreeSystem : MonoBehaviour
{
    [Header("Стадия дерева:")]
    [InspectorName("Стадия дерева (марка, семечка, первая стадия, вторая стадия, третья стадия)")] public treeState state;
    public int treeTypeNum;
    [Header("Ссылки на стадии дерева и прочее:")]
    [InspectorName("Модель марки")] [SerializeField] private GameObject markModel;
    [Space(32.5f)]


    [Header("Семечки:")]
    [InspectorName("Модель семечки")] [SerializeField] private GameObject seedModel;
    [InspectorName("Аниматор семечки")] [SerializeField] private Animator seedAnimator;
    [Space(32.5f)]


    [Header("Модели стадий (массив для нескольких видов):")]
    [InspectorName("Первая стадия")] [SerializeField] private GameObject[] firstStageModel;
    [InspectorName("Вторая стадия")] [SerializeField] private GameObject[] secondStageModel;
    [InspectorName("Третья стадия")] [SerializeField] private GameObject[] thirdStageModel;
    [Space(32.5f)]
    [Header("Модели колец:")]
    [InspectorName("Кольцо")][SerializeField] private GameObject MarkRing;
    [InspectorName("Кольцо")][SerializeField] private GameObject TreeRing;
    [Space(32.5f)]
    [Header("Скрипты:")]
    [InspectorName("Менеджер игры")] [SerializeField] private GameManager gameManager;
    [InspectorName("Логика локаций")] [SerializeField] private LocationLogic ll;
    [InspectorName("Грязный скрипт")] [SerializeField] private DirtyScript DS;
    [InspectorName("Менеджер кнопок")] [SerializeField] private ButtonManager BM;
    [InspectorName("Скрипт буста")] [SerializeField] private LocationTreesManager LTM;
    [InspectorName("Скрипт обучения")][SerializeField] private EducationFirstTime Education;

    private Vector3 seedPosBeforePlant;
    private Vector3 seedPosAfterPlant;


    [SerializeField] private GameObject ClickObject;
    [HideInInspector] public bool firstPlantedSeed;
    [EditorButton("СБРОСИТЬ ВСЕ СОХРАНЕНИЯ")]

    public void DeleteTreeProgress()
    {
        PlayerPrefs.DeleteKey("firstSeedPlanted");
        PlayerPrefs.DeleteKey("isSeedAnimationPlayed");
        PlayerPrefs.DeleteKey(this.name + " type");
        PlayerPrefs.DeleteKey(this.name);
        firstPlantedSeed = false;
        //При удалении сейвов - семечко возвращается на старую позицию.
        seedModel.transform.position = seedPosBeforePlant;
        state = treeState.mark;
        Start();    
    }
    public void ChangeTreeType()
    {
        PlayerPrefs.SetInt(this.name + " type", treeTypeNum);
        PlayerPrefs.Save();
    }
    private void OnMeshFilterChanged()
    {
        Start();
    }
    
    private void Awake() 
    {

        treeTypeNum = PlayerPrefs.GetInt(this.name + " type");    
    }

    
    private void Start()
    {
        Education = FindObjectOfType<EducationFirstTime>();
        LTM = FindObjectOfType<LocationTreesManager>();
        BM = FindObjectOfType<ButtonManager>();
        ll = FindObjectOfType<LocationLogic>();
        //Назначение векторов для семечка
        seedPosBeforePlant = new Vector3(transform.position.x, 1.55f, transform.position.z);
        seedPosAfterPlant = new Vector3(transform.position.x, 0.571f, transform.position.z);
        gameManager = FindObjectOfType<GameManager>();
        if(PlayerPrefs.GetString(this.name) != null)
        {
            string a = PlayerPrefs.GetString(this.name);
            if (a == "mark")
                state = treeState.mark;
            if(a == "seed")
            {
                state = treeState.seed;
                //Если при загрузке стадия - семечко, то загрузить позицию для уже посаженного семечка.
                seedModel.transform.position = seedPosAfterPlant;
            }
            if (a == "firstStage")
            {
                state = treeState.firstStage;
                LTM.TreesNLocations[this.name[0] - 48, this.name[5] - 48] = -1;
            }
            if (a == "secondStage")
            {
                state = treeState.secondStage;
                LTM.TreesNLocations[this.name[0] - 48, this.name[5] - 48] = -1;
            }
            if (a == "thirdStage")
            {
                state = treeState.thirdStage;
                LTM.TreesNLocations[this.name[0] - 48,this.name[5] - 48] = treeTypeNum;
                LTM.TreeTypes[treeTypeNum] = true;
            }
        }
        else
            PlayerPrefs.SetString(this.name, state.ToString());

        

        markModel.SetActive(false);
        seedModel.SetActive(false);
        foreach (var item in GetComponentsInChildren<Transform>())
        {
            if(item.name != this.name)
                item.gameObject.SetActive(false);
        }

        if(state == treeState.mark)
            markModel.SetActive(true);
        if(state == treeState.seed)
        { 
            seedModel.SetActive(true);
            if (PlayerPrefs.HasKey("isSeedAnimationPlayed"))
            {
                seedAnimator.enabled = false;
            }
            //Если ключ "играла ли анимация" равна 1, то аниматор просто отключается, чтобы анимация не стартовала при старте уровня.
        }
        if(state == treeState.firstStage)
            firstStageModel[treeTypeNum].SetActive(true);
        if(state == treeState.secondStage)
            secondStageModel[treeTypeNum].SetActive(true);
        if(state == treeState.thirdStage)
            thirdStageModel[treeTypeNum].SetActive(true);

        PlayerPrefs.Save();
    }

    private void Update()
    {
        if(state == treeState.mark && DS.isSeedDraged)
        {
            MarkRing.SetActive(true);
        }
        else if(state != treeState.thirdStage && treeTypeNum != 0 && DS.isWateringCanDraged)
        {
            MarkRing.SetActive(true);
        }
        else if(treeTypeNum == 0 && state == treeState.firstStage && DS.isWateringCanDraged 
            || state == treeState.secondStage && DS.isWateringCanDraged && treeTypeNum == 0)
        {
            TreeRing.SetActive(true);
        }
        else if(state == treeState.seed && DS.isWateringCanDraged)
        {
            MarkRing.SetActive(true);
        }
        else
        {
            MarkRing.SetActive(false);
            TreeRing.SetActive(false);
        }
        if (state == treeState.thirdStage)
            LTM.TreesNLocations[this.name[0] - 48, this.name[5] - 48] = treeTypeNum;        
    }

    void SeedAnimationValidate()
    {
        //Аниматор включен, сыграть анимацию, загрузить в playerprefs, что анимация сыграна
        seedAnimator.enabled = true;
        StartCoroutine(WaitAnimation());
    }

    public void Growing()
    {
        if(state != treeState.thirdStage)
        {
            if(state == treeState.mark)
            {
                if (gameManager.xpCount >= 100)
                {
                    state = treeState.seed;
                    PlayerPrefs.SetString(this.name, state.ToString());
                    gameManager.XPChange(-100);
                    firstPlantedSeed = true;
                    PlayerPrefs.SetInt("isSeedAnimationPlayed", 1);
                    SeedAnimationValidate();
                    PlayerPrefsExtra.SetBool("firstSeedPlanted", firstPlantedSeed);
                }
                else
                {
                    StartCoroutine(gameManager.WaitLackOfCurrency('x'));
                }
            }

            else if(state == treeState.seed)
            {
                if(gameManager.xpCount >= 50)
                {
                    state = treeState.firstStage;
                    PlayerPrefs.SetString(this.name, state.ToString());
                    gameManager.XPChange(-50);
                    PlayerPrefs.SetInt("XpCount", gameManager.xpCount);
                }
                else
                {
                    StartCoroutine(gameManager.WaitLackOfCurrency('x'));
                }
            }

            else if(state == treeState.firstStage)
            {
                if(gameManager.xpCount >= 50)
                {
                    state = treeState.secondStage;
                    PlayerPrefs.SetString(this.name, state.ToString());
                    gameManager.XPChange(-50);
                    PlayerPrefs.SetInt("XpCount", gameManager.xpCount);
                }
                else
                {
                    StartCoroutine(gameManager.WaitLackOfCurrency('x'));
                }
            }

            else if(state == treeState.secondStage)
            {
                if (gameManager.xpCount >= 50)
                {
                    state = treeState.thirdStage;
                    PlayerPrefs.SetString(this.name, state.ToString());
                    LTM.TreeTypes[treeTypeNum] = true;
                    gameManager.XPChange(-50);
                    PlayerPrefs.SetInt("XpCount", gameManager.xpCount);
                    LTM.TreesNLocations[ll.equipedLocation, this.name[5] - 48] = treeTypeNum;
                    if (!Education.FirstTimePlayin)
                    {
                        Education.isTreeIsGrowed = true;
                    }
                }
                else
                {
                    StartCoroutine(gameManager.WaitLackOfCurrency('x'));
                }
            }

            markModel.SetActive(false);
            seedModel.SetActive(false);
            firstStageModel[treeTypeNum].SetActive(false);
            secondStageModel[treeTypeNum].SetActive(false);
            thirdStageModel[treeTypeNum].SetActive(false);

            if (state == treeState.mark)
                markModel.SetActive(true);
            if (state == treeState.seed)
                seedModel.SetActive(true);
            if(state == treeState.firstStage)
                firstStageModel[treeTypeNum].SetActive(true);
            if(state == treeState.secondStage)
                secondStageModel[treeTypeNum].SetActive(true);
            if (state == treeState.thirdStage)
            {
                thirdStageModel[treeTypeNum].SetActive(true);
            }           
            PlayerPrefs.Save();
        }
    }

    IEnumerator WaitAnimation()
    {
        BM.CanTapOnBtns = false;
        ll.isAnimationPlaying = true;
        yield return new WaitForSeconds(1f);
        seedAnimator.enabled = false;
        ll.isAnimationPlaying = false;
        BM.CanTapOnBtns = true;
    }
}