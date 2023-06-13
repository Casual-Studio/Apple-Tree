using UnityEngine;

public class LocationTreesManager : MonoBehaviour
{
    public int[,] TreesNLocations = new int[7,8];
    public bool[] TreeTypes = new bool[8];
    private void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int k = 0; k < 8; k++)
            {
                TreesNLocations[i, k] = -1;
            }
        }
    }
    public void DestroyLTMProgress()
    {
        for(int i = 0; i < TreeTypes.Length; i++)
        {
            TreeTypes[i] = false;
        }
    }
}
