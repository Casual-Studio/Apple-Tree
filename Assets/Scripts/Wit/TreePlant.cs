using UnityEngine;

public class TreePlant : MonoBehaviour
{
    [SerializeField] Vector3 markPos;

    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject seedPrefab;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //Присваиваю вектору markpos позицию маркера и + повышаю высоту, чтобы дерево не впечаталось в землю
        markPos = new Vector3(transform.position.x, transform.position.y - 0.42f, transform.position.z);
    }
    void Update()
    {
    }
    void OnMouseDown()
    {
        //Если опыта больше или равно 100
        if(gameManager.xpCount >= 100)
        {
            //Создаю обьект, уменьшаю опыт на 100, меняю текст, удаляю обьект
            Instantiate(seedPrefab, markPos, Quaternion.Euler(-90,0,0), transform.parent);
            gameManager.XPChange(100, '-');
            gameObject.SetActive(false);
        }
    }
}
