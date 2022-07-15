using UnityEngine;

public class TreeClick : MonoBehaviour
{
    //������ ��� ��������� ����-�� �� ���� �� ������
    public GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void OnMouseDown()
    {
        //��� �����.
        gameManager.AppleChange(2, '+');
        gameManager.XPChange(1, '+');
    }
}
