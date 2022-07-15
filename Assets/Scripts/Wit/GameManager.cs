using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //CurrencyText---BEGIN
    public TextMeshProUGUI appleText;
    public TextMeshProUGUI xpText;
    public int appleCount;
    public int xpCount;
    //CurencyText---END
    //Функция для изменения опыта.
    public void XPChange(int ammount, char addOrSub)
    {
        if(addOrSub == '-')
        {
            xpCount -= ammount;
            xpText.text = xpCount.ToString();
        }
        if(addOrSub == '+')
        {
            xpCount += ammount;
            xpText.text = xpCount.ToString();
        }
    }
    //Функция для изменения яблок.
    public void AppleChange(int ammount, char addOrSub)
    {
        if (addOrSub == '-')
        {
            appleCount -= ammount;
            appleText.text = appleCount.ToString();
        }
        if (addOrSub == '+')
        {
            appleCount += ammount;
            appleText.text = appleCount.ToString();
        }
    }

}
