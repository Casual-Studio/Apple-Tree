//Скрипт создан с поддержкой музыки The Living Tombstone - Ordinary Life. 
using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgMusic;
    public AudioSource bgSound;

    public Slider musicSlider;
    public Slider soundSlider;

    public AudioClip[] soundClips = new AudioClip[5];

    [SerializeField] private AudioClip[] audioClips = new AudioClip[2];

    [SerializeField] private Image standartMusicImage;
    [SerializeField] private Image turnedOffMusicImage;

    [SerializeField] private int NumberOfTrack;

    [SerializeField] private Image standartSoundImage;
    [SerializeField] private Image turnedOffSoundImage;
    void Start()
    {
        if(!PlayerPrefs.HasKey("MusicSliderValue") && !PlayerPrefs.HasKey("SoundSliderValue"))
        {
            musicSlider.value = 0.5f;
            bgMusic.volume = musicSlider.value;
            soundSlider.value = 0.5f;
            bgSound.volume = soundSlider.value;
            PlayerPrefs.SetFloat("MusicSliderValue", 0.5f);
            PlayerPrefs.SetFloat("SoundSliderValue", 0.5f);
        }
        musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue");
        bgMusic.volume = musicSlider.value;
        soundSlider.value = PlayerPrefs.GetFloat("SoundSliderValue");
        bgSound.volume = soundSlider.value;
        TrackShuffle();
    }
    void Update()
    {
        if(musicSlider.value == 0)
        {
            standartMusicImage.enabled = false;
            turnedOffMusicImage.enabled = true;
            bgMusic.mute = true;
        }
        else
        {
            bgMusic.mute = false;
            standartMusicImage.enabled = true;
            turnedOffMusicImage.enabled = false;
        }
        if (soundSlider.value == 0)
        {
            standartSoundImage.enabled = false;
            turnedOffSoundImage.enabled = true;
        }
        else
        {
            standartSoundImage.enabled = true;
            turnedOffSoundImage.enabled = false;
        }
        if (!bgMusic.isPlaying)
        {
            TrackShuffle();
        }
    }
    public void Changed(int musicOrSound)
    {
        if(musicOrSound == 0)
        {
            PlayerPrefs.SetFloat("MusicSliderValue", musicSlider.value);
            bgMusic.volume = musicSlider.value;
            PlayerPrefs.Save();
        }
        if(musicOrSound == 1)
        {
            PlayerPrefs.SetFloat("SoundSliderValue", soundSlider.value);
            PlayerPrefs.Save();
            bgSound.volume = soundSlider.value;
        }
    }

    public void TrackShuffle()
    {
        NumberOfTrack = Random.Range(0, 2);
        bgMusic.PlayOneShot(audioClips[NumberOfTrack]);
    }
}
