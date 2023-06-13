//Скрипт изменён с поддержкой музыки The Living Tombstone - Ordinary Life, оригинальный автор - Арсен. 
using UnityEngine;

public class Item : MonoBehaviour 
{
    public int Cost; 

    [SerializeField] private string ItemName;
    [SerializeField] private int itemId;
    [SerializeField] private int count;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private Statistic stats;
    [SerializeField] private LocationLogic ll;

    //Короче это скрипт для деревьев в магазине. Я хотел бы сделать его всеобщим и для локаций с аксессуарами тоже, но проебался
    //так что теперь у нас два скрипта и у меня чуть даже клюёт душа.
    public void Buy() 
    {
        //Покупки.
        if(gameManager.xpCount >= Cost) 
        {
            gameManager.XPChange(-Cost);
            stats.OnValueChanged(3, Cost);
            PlayerPrefs.SetInt("XpCount", gameManager.xpCount);
            PlayerPrefs.Save();
            Inventory.AddItems(itemId, count);
        }
        else
        {
            gameManager.isLackCurrencyX = true;
        }
    }
}

