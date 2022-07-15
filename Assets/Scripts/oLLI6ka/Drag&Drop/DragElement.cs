using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [SerializeField] private Image mainImage;
    [SerializeField] private Transform defaultParentTransform;
    [SerializeField] private GameObject leikaPrefab;
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private int siblingIndex;

    public int SiblingIndex
    {
        get { return siblingIndex; }
        set
        {
            if (value > 0)
                siblingIndex = value;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //хз че тут делать, можешь делать звуки  или эффекты (когда лейка нажата)
    }

    public void OnDrag(PointerEventData eventData)
    {
        //когда лейка зажата
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, 3.5f));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //когда лейка отпустили

        //возвращаем обратно в контент элемент
        transform.position = defaultParentTransform.transform.position;

        //создаем луч и хит
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //пускаем луч
        if (Physics.Raycast(ray, out hit))
        {
            //если ты умеешь работать с хитами, для тебя это будет легко

            if (gameManager.xpCount >= 50)
            {
                if (hit.collider.gameObject.CompareTag("GrowingTrees"))
                {
                    StartCoroutine(leikaSpawn(hit));
                    gameManager.XPChange(50, '-');
                }
            }
        }
    }

    private IEnumerator leikaSpawn(RaycastHit hit)
    {
        Vector3 vector3 = new Vector3(hit.point.x - 0.5f, hit.point.y, hit.point.z);
        var a = Instantiate(leikaPrefab, vector3, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        if (hit.collider.gameObject.TryGetComponent<TreeGrowing>(out TreeGrowing comp))
            comp.GrowingProcess();
        Destroy(a.gameObject);
    }

}