using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    // [SerializeField] private Image mainImage;
    [SerializeField] private Transform defaultParentTransform;
    [SerializeField] private GameObject wateringCanPrefab;
    [SerializeField] private GameObject ClickObject;

    [SerializeField] private Statistic stats;
    [SerializeField] private AudioManager AM;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ButtonManager BM;
    [SerializeField] private DisableSwipesAndClicks DSAC;
    [SerializeField] private BoostManager boostManager;
    [SerializeField] private EducationFirstTime Education;
    [SerializeField] private LocationLogic ll;

    [SerializeField] private TMP_Text countText;
 
    [HideInInspector] public bool isWateringCan;
    
    public bool isBeginDraged;

    public int seedType;
    [HideInInspector] public int myCount;

    void Start()
    {
        ll = FindObjectOfType<LocationLogic>();
        Education = FindObjectOfType<EducationFirstTime>();
        DSAC = FindObjectOfType<DisableSwipesAndClicks>();
        BM = FindObjectOfType<ButtonManager>();
        gameManager = FindObjectOfType<GameManager>();
        AM = FindObjectOfType<AudioManager>();
        boostManager = FindObjectOfType<BoostManager>();
    }
    private int siblingIndex;

    public int SiblingIndex
    {
        get { return siblingIndex; }
        set
        {
            if (value > 0)
                siblingIndex = value;
        }
    }
    public void Update()
    {
        if(!isWateringCan)
        {
            myCount = PlayerPrefs.GetInt(seedType.ToString());
            countText.text = myCount.ToString();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        AM.bgSound.PlayOneShot(AM.soundClips[1]);
        ClickObject.SetActive(false);
        //хз че тут делать, можешь делать звуки  или эффекты (когда лейка нажата)
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(myCount >= 1 && !isWateringCan)
        {
            ll.canSwipe = false;
            isBeginDraged = true;
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, 3.5f));
        }
        else if (isWateringCan && BM.CanTapOnBtns)
        {
            ll.canSwipe = false;
            Education.isWaterCanDragged = true;
            isBeginDraged = true;
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, 3.5f));
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ll.canSwipe = true;
        Education.isWaterCanDragged = false;
        isBeginDraged = false;
        //когда лейка отпустили
        AM.bgSound.PlayOneShot(AM.soundClips[1]);
        //возвращаем обратно в контент элемент
        transform.position = defaultParentTransform.transform.position;

        //создаем луч и хит
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //пускаем луч
        if (Physics.Raycast(ray, out hit))
        {
            if (!boostManager.noTreesOnLoc)
            {
                ClickObject.SetActive(true);
            }
            if (myCount >= 1 && gameManager.xpCount >= 100)
            {
                //Проверка на тэг + на стадию. Если стадия - метка и это не лейка то 
                if (hit.collider.gameObject.CompareTag("GrowingTrees") && hit.collider.gameObject.GetComponent<TreeSystem>().state == treeState.mark && isWateringCan == false)
                {
                    Inventory.AddItems(seedType, -1);
                    Debug.Log(1);
                    if (hit.collider.gameObject.TryGetComponent<TreeSystem>(out TreeSystem comp))
                    {
                        //Рост + сохранение в playerPrefs играла ли анимация или нет.
                        comp.Growing();
                        comp.treeTypeNum = seedType;
                        comp.ChangeTreeType();
                        stats.OnValueChanged(1, 1);
                        stats.OnValueChanged(2, 100);
                        PlayerPrefs.SetInt(name + " isSeedAnimationPlayed", 1);
                        Debug.Log(2);
                    }
                }
            }
            
            else if(hit.collider.gameObject.CompareTag("GrowingTrees") && hit.collider.gameObject.GetComponent<TreeSystem>().state != treeState.thirdStage)
            {
                if (gameManager.xpCount >= 50 && isWateringCan == true && hit.collider.gameObject.GetComponent<TreeSystem>().state != treeState.mark)
                {
                    Debug.Log(3);
                    stats.OnValueChanged(2, 50);
                    StartCoroutine(Watering(hit));
                }
            }
        }
    }

    private IEnumerator Watering(RaycastHit hit)
    {
        Vector3 vector3 = new Vector3(hit.collider.gameObject.transform.position.x + 1f, hit.collider.gameObject.transform.position.y + 0.5f, hit.collider.gameObject.transform.position.z);
        var a = Instantiate(wateringCanPrefab, vector3, new Quaternion(-90, 0, 180, 0));
        ParticleSystem water = a.GetComponentInChildren<ParticleSystem>();
        water.Play();
        BM.CanTapOnBtns = false;
        DSAC.isWatering = true;
        AM.bgSound.PlayOneShot(AM.soundClips[4]);
        yield return new WaitForSeconds(2f);
        AM.bgSound.Stop();
        if (hit.collider.gameObject.TryGetComponent<TreeSystem>(out TreeSystem comp))
           comp.Growing();
        Destroy(a.gameObject);
        BM.CanTapOnBtns = true;
        DSAC.isWatering = false;
    }
}