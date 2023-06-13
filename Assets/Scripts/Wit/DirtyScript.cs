using UnityEngine;

public class DirtyScript : MonoBehaviour
{
    public bool isSeedDraged;
    public bool isWateringCanDraged;
    [SerializeField] private DragAndDrop WateringCanDD;
    [SerializeField] private DragAndDrop[] SeedDD = new DragAndDrop[8];
    void Update()
    {
        if (WateringCanDD.isBeginDraged)
        {
            isWateringCanDraged = true;
        }
        else if (!WateringCanDD.isBeginDraged)
        {
            //TreeRing.SetActive(false);
            //MarkRing.SetActive(false);
            isWateringCanDraged = false;
        }
        if (SeedDD[0].isBeginDraged || SeedDD[1].isBeginDraged || SeedDD[2].isBeginDraged || SeedDD[3].isBeginDraged 
            || SeedDD[4].isBeginDraged || SeedDD[5].isBeginDraged || SeedDD[6].isBeginDraged || SeedDD[7].isBeginDraged)
        {
            isSeedDraged = true;
        }
        else
        {
            isSeedDraged = false;
        }
    }
}
