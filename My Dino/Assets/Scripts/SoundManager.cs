using UnityEngine;
using UnityEngine.UI;

public enum SoundState
{
    AllOn,          // Nhạc + hiệu ứng mở
    MusicMuted,     // Tắt nhạc nền
    AllMuted        // Tắt cả 2
}

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;
    public Button soundToggleButton;
    public Sprite[] soundStateIcons;
    public AudioSource backgroundMusic;
    public AudioSource[] effectSounds;

    private SoundState currentSoundState = SoundState.AllOn;


    [Header("Audio Sources")]
    public AudioSource sfxGameOverSource;
    public AudioSource sfxTouchSource;
    public AudioSource sfxStartGameSource;

    [Header("Audio Clips")]
    public AudioClip clickSFX;
    public AudioClip sfxGameOverClip;
    public AudioClip sfxStartGameClip;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateSoundState();
        soundToggleButton.onClick.AddListener(OnSoundToggleClicked);
    }

    void OnSoundToggleClicked()
    {
        currentSoundState = (SoundState)(((int)currentSoundState + 1) % 3);
        UpdateSoundState();
    }

    void UpdateSoundState()
    {
        switch (currentSoundState)
        {
            case SoundState.AllOn:
                backgroundMusic.mute = false;
                sfxTouchSource.mute = false;
                sfxGameOverSource.mute = false;
                sfxStartGameSource.mute = false;
                //SetEffectsMute(false);
                break;

            case SoundState.MusicMuted:
                backgroundMusic.mute = true;
                sfxTouchSource.mute = false;
                sfxGameOverSource.mute = false;
                sfxStartGameSource.mute = false;
                //SetEffectsMute(false);
                break;

            case SoundState.AllMuted:
                backgroundMusic.mute = true;
                sfxTouchSource.mute = true;
                sfxGameOverSource.mute = true;
                sfxStartGameSource.mute = true;
                //SetEffectsMute(true);
                break;
        }

        // Cập nhật icon tương ứng
        soundToggleButton.image.sprite = soundStateIcons[(int)currentSoundState];
    }

    void SetEffectsMute(bool mute)
    {
        foreach (var effect in effectSounds)
        {
            effect.mute = mute;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxTouchSource.PlayOneShot(clip);
        }
    }

    public void PlayClickSound()
    {
        PlaySFX(clickSFX);
    }
    public void PlayGameOveround()
    {
        PlaySFX(sfxGameOverClip);
    }
    public void PlayStartGameSound()
    {
        PlaySFX(sfxStartGameClip);
    }

}
