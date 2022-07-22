using UnityEngine;

public class TreeGrowing : MonoBehaviour
{
    [SerializeField] private Vector3 treePos;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject nextTree;
    void Start()
    {
        //Записываю текущую позицию в treePos, ищу gameManager, если y >= 0.85 то я перезаписываю перемунную
        //без + к высоте (Нужно для того, чтобы уровнять дерево после семечка.
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
        //Трата xp для роста
        if(gameManager.xpCount >= 50)
        {
            gameManager.XPChange(50, '-');
            GrowingProcess();
        }
    }
    public void GrowingProcess()
    {
        //Создание обьекта и уничтожение текущего
        Instantiate(nextTree, treePos, Quaternion.Euler(-90,0,0));;
        Destroy(gameObject);
    }

    private void OnApplicationQuit()
    {
    }
}
