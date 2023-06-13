//������ ������ � ���������� ������ The Living Tombstone - Ordinary Life, ������������ ����� - �����. 
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

    //������ ��� ������ ��� �������� � ��������. � ����� �� ������� ��� �������� � ��� ������� � ������������ ����, �� ���������
    //��� ��� ������ � ��� ��� ������� � � ���� ���� ���� ����� ����.
    public void Buy() 
    {
        //�������.
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

