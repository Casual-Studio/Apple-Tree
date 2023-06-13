//—крипт создан с поддержкой музыки The Living Tombstone - Ordinary Life. 
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    [SerializeField] private LocationLogic ll;
    [SerializeField] private LocationTreesManager LTM;

    public int[] ClickCounter = new int[6];
    public int ApplePerClick = 1;
    public int XpPerClick = 1;
    public int HowManyClicks;
    public int locationId;

    public bool noTreesOnLoc;

    [SerializeField] private GameObject ClickObject;

    [SerializeField] private bool Once;
    private void Update()
    {
        bool treeTypeChanged = false;
        int timeParametr = 0;
        for (int i = 0; i < 7; i++)
        {
            if (LTM.TreesNLocations[ll.equipedLocation,i] != -1)
            {
                treeTypeChanged = true;
                break;
            }
            else
            {
                timeParametr++;
            }
        }
        if(timeParametr == 7)
        {
            timeParametr = 0;
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
        if (treeTypeChanged)
        {
            CalculateBoost();
            treeTypeChanged = false;
        }
    }

    public void CalculateBoost()
    {
        ApplePerClick = 0;
        XpPerClick = 0;
        if (PlayerPrefs.HasKey("Acess " + 0 + " buyed on " + ll.equipedLocation))
        {
            ApplePerClick += 30;
        }
        if (PlayerPrefs.HasKey("Acess " + 1 + " buyed on " + ll.equipedLocation))
        {
            if(ApplePerClick == 0)
            {
                ApplePerClick = 1 * 2;
            }
            else
            {
                ApplePerClick *= 2;
            }

        }
        if (PlayerPrefs.HasKey("Acess " + 2 + " buyed on " + ll.equipedLocation))
        {
            if (XpPerClick == 0)
            {
                XpPerClick = 1 * 4;
            }
            else
            {
                XpPerClick *= 4;
            }
        }
        if (PlayerPrefs.HasKey("Acess " + 3 + " buyed on " + ll.equipedLocation))
        {
            XpPerClick += 10;
        }
        //«десь вот так € прописал дл€ буста от разных локаций. ћ€гко говор€ так себе.
        switch (ll.equipedLocation)
        {
            case 0:
                locationId = -1;
                HowManyClicks = -1;
                break;
            case 1:
                if (ClickCounter[0] >= 5)
                {
                    ClickCounter[0] = 0;
                    ApplePerClick += 2;
                    locationId = 0;
                    HowManyClicks = 5;
                }
                else
                {
                    locationId = 0;
                    HowManyClicks = 5;
                }
                break;
            case 2:
                if (ClickCounter[1] >= 5)
                {
                    ClickCounter[1] = 0;
                    ApplePerClick += 4;
                    XpPerClick += 2;
                    locationId = 1;
                    HowManyClicks = 5;
                }
                else
                {
                    locationId = 1;
                    HowManyClicks = 5;
                }
                break;
            case 3:
                if (ClickCounter[2] >= 5)
                {
                    ClickCounter[2] = 0;
                    ApplePerClick += 6;
                    XpPerClick += 3;
                    locationId = 2;
                    HowManyClicks = 5;
                }
                else
                {
                    locationId = 2;
                    HowManyClicks = 5;
                }
                break;
            case 4:
                if (ClickCounter[3] >= 4)
                {
                    ClickCounter[3] = 0;
                    ApplePerClick += 6;
                    XpPerClick += 3;
                    locationId = 3;
                    HowManyClicks = 4;
                }
                else
                {
                    locationId = 3;
                    HowManyClicks = 5;
                }
                break;
            case 5:
                if (ClickCounter[4] >= 3)
                {
                    ClickCounter[4] = 0;
                    ApplePerClick += 12;
                    XpPerClick += 6;
                    locationId = 4;
                    HowManyClicks = 3;
                }
                else
                {
                    locationId = 4;
                    HowManyClicks = 5;
                }
                break;
            case 6:
                if (ClickCounter[5] >= 3)
                {
                    ClickCounter[5] = 0;
                    ApplePerClick += 18;
                    XpPerClick += 9;
                    locationId = 5;
                    HowManyClicks = 3;
                }
                else
                {
                    locationId = 5;
                    HowManyClicks = 5;
                }
                break;
        }
        for (int i = 0; i < 7; i++)
        {
            switch (LTM.TreesNLocations[ll.equipedLocation,i])
            {
                case 0:
                    ApplePerClick += 1;
                    XpPerClick += 1;
                    break;
                case 1:
                    ApplePerClick += 1;
                    XpPerClick += 2;
                    break;
                case 2:
                    if (ApplePerClick == 0)
                    {
                        ApplePerClick = 1 * 2;
                        XpPerClick += 1;
                    }
                    else
                    {
                        ApplePerClick *= 2;
                        XpPerClick += 1;
                    }
                    break;
                case 3:
                    ApplePerClick += 3;
                    XpPerClick += 1;
                    break;
                case 4:
                    XpPerClick += 3;
                    ApplePerClick += 1;
                    break;
                case 5:
                    if (XpPerClick == 0)
                    {
                        XpPerClick = 1 * 3;
                        ApplePerClick += 1;
                    }
                    else
                    {
                        XpPerClick *= 3;
                        ApplePerClick += 1;
                    }
                    break;
                case 6:
                    if (ApplePerClick == 0)
                    {
                        ApplePerClick = 1 * 3;
                        XpPerClick += 1;
                    }
                    else
                    {
                        ApplePerClick *= 3;
                        XpPerClick += 1;
                    }
                    break;
                case 7:
                    if (XpPerClick == 0)
                    {
                        ApplePerClick += 1;
                        XpPerClick = 1 * 4;
                    }
                    else
                    {
                        ApplePerClick += 1;
                        XpPerClick *= 4;
                    }
                    break;
            }
        }
    }
    public void DeleteBoostProgress()
    {
        for (int i = 0; i < 5; i++)
        {
            ClickCounter[i] = 0;
        }
        ApplePerClick = 0;
        XpPerClick = 0;
    }
}
