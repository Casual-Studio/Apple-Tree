using UnityEngine;
using System.IO;
using System;

public class SaveSystem : MonoBehaviour
{
    public GameManager gameManager;
    private Save sv = new Save();
    private string path;

    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "Save.json");
        gameManager = FindObjectOfType<GameManager>();
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            gameManager.XPChange(sv.xpCount, '+');
            gameManager.xpCount = sv.xpCount;
            gameManager.AppleChange(sv.appleCount, '+');
            gameManager.appleCount = sv.appleCount;
        }
        else
        {
            Debug.Log("лох");
        }
    }
    public void Delete()
    {
        File.Delete(path);
        gameManager.CurrencyRestore();
    }

    private void OnApplicationQuit()
    {
        sv.xpCount = gameManager.xpCount;
        sv.appleCount = gameManager.appleCount;
        File.WriteAllText(path, JsonUtility.ToJson(sv));
    }
}
[Serializable]
public class Save
{
    public int appleCount;
    public int xpCount;
    public float[] treePos;
}
