//������ ������ � ���������� ������ The Living Tombstone - Ordinary Life. 
using UnityEngine.UI;
using UnityEngine;

public class LocationLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] locationArray = new GameObject[7];
    [SerializeField] private GameObject ClickObject;
    [SerializeField] private GameObject nextLoc;
    [SerializeField] private GameObject prevLoc;

    [SerializeField] private GameObject cameraRotate;

    [SerializeField] private bool isFirtsLocationRn;
    [SerializeField] private bool isLastLocationRn;
    [SerializeField] private bool Once;
    [SerializeField] private bool Once1;

    [HideInInspector] public bool[] isBuyed = new bool[7];

    [SerializeField] private bool[] isEquiped = new bool[7];

    [SerializeField] private Button buyBtn;
    [SerializeField] private Button equipBtn;

    [HideInInspector] public int equipedLocation;
    [HideInInspector] public bool canSwipe = true;
    [HideInInspector] public bool isAnimationPlaying;

    public GameObject[] treesOnLocation = new GameObject[7];

    //���������� "TimeIndex" ���������� ��� ��������� ���������� (������ � ��������)
    [HideInInspector] private int TimeIndex;

    [SerializeField] private AudioManager AM;
    [SerializeField] private BoostManager BM;
    [SerializeField] private CameraMovement CM;
    [SerializeField] private EducationFirstTime EFT;
    /*���������� �� ���������� locationArray � isBuyed ��� ��������:
     * 0 - Standart (�� buyed)
     * 1 - Mushroom
     * 2 - Desert
     * 3 - Boloto
     * 4 - Flowers
     * 5 - Gryadka
     * 6 - Teplica
    */

    void Start()
    {
        //����, ��� ����� ������.
        //��������� ������������� �������, ������������� ������ � ���������� ���������.
        if (PlayerPrefs.GetInt("equipedLocation") != 0)
        {
            equipedLocation = PlayerPrefs.GetInt("equipedLocation");
            isEquiped[equipedLocation] = true;
            Vector3 tempVector = new Vector3(cameraRotate.transform.position.x + 17, cameraRotate.transform.position.y, cameraRotate.transform.position.z);
            cameraRotate.transform.position = tempVector;
            cameraRotate.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            TimeIndex = equipedLocation;
            for(int i = 0; i < locationArray.Length; i++)
            {
                locationArray[i].SetActive(false);
            }
            locationArray[equipedLocation].SetActive(true);
        }
        else
        {
            equipedLocation = 0;
            TimeIndex = 0;
            locationArray[0].SetActive(true);
            isEquiped[0] = true;
            isBuyed[0] = true;
            PlayerPrefsExtra.SetBool("isBuyed " + 0, isBuyed[0]);
            PlayerPrefsExtra.SetBool("isEquiped " + 0, isEquiped[0]);
            PlayerPrefs.SetInt("equipedLocation", 0);
        }
        //�������������, ���� �� ������� �������. � ���� ����, �� ����� ������������ ������.
        for(int i = 0; i < isBuyed.Length; i++)
        {
            if(PlayerPrefs.HasKey("isBuyed " + i))
            {
                isBuyed[i] = true;
            }
        }
    }
    void Update()
    {
        //��� ��� �������� �� ����� �� ������� �������� (��. clickThroughCanvas ������)
        if (!canSwipe)
        {
            nextLoc.SetActive(false);
            prevLoc.SetActive(false);
        }
        //��� � ���������� ������, ����� ������ �� ������� ������ ���� ���������� ������ ��� ��������� �������.
        if (locationArray[0].activeSelf)
        {
            if (EFT.FirstTimePlayin)
            {
                nextLoc.SetActive(true);
            }
            prevLoc.SetActive(false);
            isLastLocationRn = false;
            isFirtsLocationRn = true;
            buyBtn.gameObject.SetActive(false);
            if(equipedLocation != 0)
            {
                equipBtn.gameObject.SetActive(true);
            }
        }
        else if (locationArray[6].activeSelf)
        {
            nextLoc.SetActive(false);
            prevLoc.SetActive(true);
            isFirtsLocationRn = false;
            isLastLocationRn = true;
        }
        else
        {
            nextLoc.SetActive(true);
            prevLoc.SetActive(true);
            isFirtsLocationRn = false;
            isLastLocationRn = false;
        }
        if(equipedLocation != TimeIndex)
        {
            ClickObject.SetActive(false);
            Once = true;
        }
        else
        {
            if (Once)
            {
                ClickObject.SetActive(true);
                Once = false;
            }
        }
        if (!isEquiped[TimeIndex])
        {
            CM.enabled = false;
            Once1 = true;
        }
        else if(Once1)
        {
            Once1 = false;
            CM.enabled = true;
        }
    }

    public void previousLocation()
    {
        //�� �����! ����. ����� � ��������, ����� ������� �� ���� ������, ����� ��������� ������ �� -, ������� ��� ������
        //� ��� ��������� ����� �������� �� ���������� � �������.
        if (!isFirtsLocationRn)
        {
            Vector3 tempVector = new Vector3(cameraRotate.transform.position.x - 17, cameraRotate.transform.position.y, cameraRotate.transform.position.z);
            cameraRotate.transform.position = tempVector;
            cameraRotate.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            locationArray[TimeIndex].SetActive(false);
            TimeIndex--;
            locationArray[TimeIndex].SetActive(true);
            buyBtn.gameObject.SetActive(false);
            equipBtn.gameObject.SetActive(false);
            if (!isBuyed[TimeIndex])
            {
                buyBtn.gameObject.SetActive(true);
                equipBtn.gameObject.SetActive(false);
            }
            else if (!isEquiped[TimeIndex])
            {
                equipBtn.gameObject.SetActive(true);
                buyBtn.gameObject.SetActive(false);
            }
        }
    }

    public void nextLocation()
    {
        //����� ����������. ������ �������� �� ��������� ������� � ������ ������� �� +. ������ +17 � ���������� ���� �����.
        if (!isLastLocationRn)
        {
            Vector3 tempVector = new Vector3(cameraRotate.transform.position.x + 17, cameraRotate.transform.position.y, cameraRotate.transform.position.z);
            cameraRotate.transform.position = tempVector;
            cameraRotate.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            locationArray[TimeIndex].SetActive(false);
            TimeIndex++;
            locationArray[TimeIndex].SetActive(true);
            buyBtn.gameObject.SetActive(false);
            equipBtn.gameObject.SetActive(false);
            if (!isBuyed[TimeIndex])
            {
                buyBtn.gameObject.SetActive(true);
                equipBtn.gameObject.SetActive(false);
            }
            else if(!isEquiped[TimeIndex]){
                equipBtn.gameObject.SetActive(true);
                buyBtn.gameObject.SetActive(false);
            }
        }
    }

    public void OnEquip()
    {
        //��, ��� ���������� ��� ������� �� ������ "�����������"
        //������������ ����, ������ ������, ����������� ��� �������� isEquiped �� false � ����� ������������� ������ �� true
        //������������ ��� �� ���� � ������ �������.
        equipedLocation = TimeIndex;
        PlayerPrefs.SetInt("equipedLocation", equipedLocation);
        equipBtn.gameObject.SetActive(false);
        for(int i = 0; i < isEquiped.Length; i++)
        {
            isEquiped[i] = false;
            PlayerPrefs.DeleteKey("isEquiped " + i);
        }
        isEquiped[equipedLocation] = true;
        PlayerPrefsExtra.SetBool("isEquiped " + equipedLocation, isEquiped[equipedLocation]);
        treesOnLocation[equipedLocation].SetActive(true);
        BM.CalculateBoost();
    }

    public void OnBuy(int locationId)
    {
        //��, ��� ���������� ��� ������� �� ��������. (�� PurchaseManager ������)
        isBuyed[locationId] = true;
        PlayerPrefsExtra.SetBool("isBuyed " + locationId, isBuyed[locationId]);
    }

    public void BuyBtnFunction()
    {
        //��, ��� ���������� ��� ������� �� ������ "������" �� ������. � �����, ����� ������� ������������ � ������� ��� �������
        //� �������� ��������.
        if(TimeIndex > equipedLocation)
        {
            Vector3 tempVector = new Vector3(cameraRotate.transform.position.x - (17 * (TimeIndex - equipedLocation)), cameraRotate.transform.position.y, cameraRotate.transform.position.z);
            cameraRotate.transform.position = tempVector;
            cameraRotate.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            locationArray[TimeIndex].SetActive(false);
            locationArray[equipedLocation].SetActive(true);
        }
        else if(TimeIndex < equipedLocation)
        {
            Vector3 tempVector = new Vector3(cameraRotate.transform.position.x - 17, cameraRotate.transform.position.y, cameraRotate.transform.position.z);
            cameraRotate.transform.position = tempVector;
            cameraRotate.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            locationArray[TimeIndex].SetActive(false);
            locationArray[equipedLocation].SetActive(true);
        }
        TimeIndex = equipedLocation;
        buyBtn.gameObject.SetActive(false);
        equipBtn.gameObject.SetActive(false);
    }

    public void DeleteLocationProgress()
    {
        //��� ��� �������� ����� ���������. Sheesh!
        for(int i = 0; i < 7; i++)
        {
            isBuyed[i] = false;
            isEquiped[i] = false;
            locationArray[i].SetActive(false);
            PlayerPrefs.DeleteKey("isEquiped " + i);
            PlayerPrefs.DeleteKey("isBuyed " + i);
        }
        isBuyed[0] = true;
        PlayerPrefsExtra.SetBool("isBuyed " + 0, isBuyed[0]);
        locationArray[0].SetActive(true);
        isEquiped[0] = true;
        PlayerPrefsExtra.SetBool("isEquiped " + 0, isBuyed[0]);
        PlayerPrefs.SetInt("equipedLocation", 0);
        Vector3 tempVector = new Vector3(-0.14f, cameraRotate.transform.position.y, cameraRotate.transform.position.z);
        cameraRotate.transform.position = tempVector;
        cameraRotate.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        TimeIndex = 0;
        equipedLocation = 0;
    }
}