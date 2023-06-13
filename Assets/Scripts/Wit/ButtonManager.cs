//������ ������ � ���������� ������ The Living Tombstone - Ordinary Life. 
using UnityEngine.UI;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private AudioManager AM;
    [SerializeField] LocationLogic ll;

    [SerializeField] private Button seedsBtn;
    [SerializeField] private Button waterCanBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button achievmentBtn;
    [SerializeField] private Button shopBtn;

    public bool CanTapOnBtns;

    [SerializeField] private GameObject seedPanel;

    private void Start()
    {
        CanTapOnBtns = true;
    }

    public void ButtonClick()
    {
        //������ ����� ������ ��� ����� ���.
        AM.bgSound.PlayOneShot(AM.soundClips[1]);
    }
    
    public void ShopSound()
    {
        //������ ���� �������� ��� ����� ���.
        AM.bgSound.PlayOneShot(AM.soundClips[0]);
    }

    void Update()
    {
        //������ ���, ����� ������ � �������� ����� ���� ��������� �� ������� �� ������ �������.
        if (!seedPanel.activeSelf)
        {
            seedsBtn.onClick.RemoveAllListeners();
            seedsBtn.onClick.AddListener(() => seedPanel.SetActive(true));
        }
        else
        {
            seedsBtn.onClick.RemoveAllListeners();
            seedsBtn.onClick.AddListener(() => seedPanel.SetActive(false));
        }
        if (PlayerPrefs.HasKey("FirstTimePlayed"))
        {
            if (CanTapOnBtns)
            {
                ll.canSwipe = true;
                seedsBtn.interactable = true;
                shopBtn.interactable = true;
                settingsBtn.interactable = true;
                achievmentBtn.interactable = true;
                waterCanBtn.interactable = true;
            }
            else
            {
                ll.canSwipe = false;
                seedsBtn.interactable = false;
                shopBtn.interactable = false;
                settingsBtn.interactable = false;
                achievmentBtn.interactable = false;
                waterCanBtn.interactable = false;
            }
        }
    }
}
