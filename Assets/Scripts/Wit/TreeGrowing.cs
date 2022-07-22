using UnityEngine;

public class TreeGrowing : MonoBehaviour
{
    [SerializeField] private Vector3 treePos;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject nextTree;
    void Start()
    {
        //��������� ������� ������� � treePos, ��� gameManager, ���� y >= 0.85 �� � ������������� ����������
        //��� + � ������ (����� ��� ����, ����� �������� ������ ����� �������.
        treePos = new Vector3(transform.position.x, transform.position.y + 0.82f, transform.position.z);
        gameManager = FindObjectOfType<GameManager>();
        if(transform.position.y >= 0.85)
        {
            treePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
    void Update()
    {

    }
    void OnMouseDown()
    {
        //����� xp ��� �����
        if(gameManager.xpCount >= 50)
        {
            gameManager.XPChange(50, '-');
            GrowingProcess();
        }
    }
    public void GrowingProcess()
    {
        //�������� ������� � ����������� ��������
        Instantiate(nextTree, treePos, Quaternion.Euler(-90,0,0));;
        Destroy(gameObject);
    }

    private void OnApplicationQuit()
    {
    }
}
