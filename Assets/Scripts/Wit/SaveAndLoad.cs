using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject[] treePrefabs;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Delete();
        }
    }
    void OnApplicationQuit()
    {
        Save();
    }
    void Save()
    {
        PlayerPrefs.SetInt("xp", gameManager.xpCount);
        PlayerPrefs.SetInt("apple", gameManager.appleCount);
    }
    void Load()
    {

        gameManager.XPChange(PlayerPrefs.GetInt("xp"), '+');
        gameManager.AppleChange(PlayerPrefs.GetInt("apple"), '+');
    }
    void Delete()
    {
        PlayerPrefs.DeleteAll();
        gameManager.xpCount = 0;
        gameManager.xpText.text = "0";
        gameManager.appleCount = 0;
        gameManager.appleText.text = "0";
    }
}
