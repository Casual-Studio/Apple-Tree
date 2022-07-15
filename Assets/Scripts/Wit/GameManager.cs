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
    //������� ��� ��������� �����.
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
    //������� ��� ��������� �����.
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
