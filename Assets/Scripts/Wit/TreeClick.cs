using UnityEngine;

public class TreeClick : MonoBehaviour
{
    //Скрипт для получения чего-то за клик по дереву
    public GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void OnMouseDown()
    {
        //При клике.
        gameManager.AppleChange(2, '+');
        gameManager.XPChange(1, '+');
    }
}
