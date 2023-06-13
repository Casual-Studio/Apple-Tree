using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static void AddItems(int itemId, int count)
    {
        int a = PlayerPrefs.GetInt(itemId.ToString());
        a += count;
        PlayerPrefs.SetInt(itemId.ToString(), a);
        PlayerPrefs.Save();
        foreach (var item in Object.FindObjectsOfType<DragAndDrop>())
        {
            if(!item.isWateringCan)
            {
                item.myCount = PlayerPrefs.GetInt(itemId.ToString());
            }
        }
    }
    public static void DeleteInventorySaves()
    {
        for (int i = 0; i < 8; i++)
        {
            PlayerPrefs.DeleteKey(i.ToString());
        }
    }
}