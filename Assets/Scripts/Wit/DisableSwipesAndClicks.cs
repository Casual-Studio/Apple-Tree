//Скрипт создан с поддержкой музыки The Living Tombstone - Ordinary Life. 
using UnityEngine;

public class DisableSwipesAndClicks : MonoBehaviour
{
    [SerializeField] private GameObject[] functionalPanels = new GameObject[6];
    [SerializeField] private GameObject ClickObject;

    public bool isWatering;

    [SerializeField] private bool Once;

    [SerializeField] private LocationLogic ll;
    [SerializeField] private BoostManager bm;
    void Update()
    {
        //Скрипт создан для того, чтобы через менюшку люди не ебашили деревья и не свайпали локации лол.
        if (functionalPanels[0].activeSelf || functionalPanels[1].activeSelf || functionalPanels[2].activeSelf || functionalPanels[3].activeSelf || functionalPanels[4].activeSelf || functionalPanels[5].activeSelf)
        {
            ll.canSwipe = false;
            ll.treesOnLocation[ll.equipedLocation].SetActive(false);
            ClickObject.SetActive(false);
            Once = true;
        }
        else if (ll.isAnimationPlaying)
        {
            ll.canSwipe = false;
        }
        else if (isWatering)
        {
            ll.canSwipe = false;
        }
        else
        {
            if (Once && !bm.noTreesOnLoc)
            {
                ClickObject.SetActive(true);
                Once = false;
            }
            ll.canSwipe = true;
            ll.treesOnLocation[ll.equipedLocation].SetActive(true);
        }
    }
}
