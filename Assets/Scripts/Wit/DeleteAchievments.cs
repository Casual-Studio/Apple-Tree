//—крипт создан с поддержкой музыки The Living Tombstone - Ordinary Life. 
using UnityEngine;

public class DeleteAchievments : MonoBehaviour
{
    public AchievmentManager[] AM = new AchievmentManager[16];
    public void DeleteAchievmentProgress()
    {
       //”дал€ю каждое достижение... Ёх...
        for (int i = 0; i < 16; i++)
        {
            AM[i].claimBtn.onClick.RemoveAllListeners();
            AM[i].aboutBtn.gameObject.SetActive(true);
            AM[i].claimBtn.gameObject.SetActive(false);
            AM[i].achievmentWaitForClaim = false;
            AM[i].achievmentBoolClaimed = false;
            AM[i].claimedRewardTxt.gameObject.SetActive(false);
            if (AM[i].progressTxt != null)
            {
                AM[i].progressTxt.gameObject.SetActive(true);
            }
            AM[i].Once = true;
            PlayerPrefs.DeleteKey(i + " achievmentWait");
            PlayerPrefs.DeleteKey(i + " achievmentClaimed");
        }
    }
}
