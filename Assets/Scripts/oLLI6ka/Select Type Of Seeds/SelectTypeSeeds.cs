using UnityEngine;
using TMPro;

public class SelectTypeSeeds : MonoBehaviour
{
    [SerializeField] private DragAndDrop seedScript;

    public void ValueChanged(int value)
    {
        seedScript.seedType = value;
    }
}