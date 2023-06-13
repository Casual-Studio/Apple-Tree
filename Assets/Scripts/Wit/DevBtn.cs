//Скрипт создан с поддержкой музыки The Living Tombstone - Ordinary Life. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevBtn : MonoBehaviour
{
    //Просто девкнопки.
    public DeleteAchievments AM;
    public GameManager gameManager;
    public void devAdd()
    {
        gameManager.XPChange(500);
        gameManager.AppleChange(500);
    }
    public void devAchievment()
    {
        for (int i = 0; i < 16; i++)
        {
            AM.AM[i].WaitForClaim(0,0);
        }
    }
}
