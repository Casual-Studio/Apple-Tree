using UnityEngine;
using Kilosoft.Tools;

public class DeleteTreesSaves : MonoBehaviour
{
    [SerializeField] private TreeSystem[] Trees = new TreeSystem[56];

    [EditorButton("Delete Trees Saves")]



    public void DeleteTreeProgress()
    {
        foreach (var i in Trees)
        {
            i.DeleteTreeProgress();
            i.state = treeState.mark;
        }
    }
}