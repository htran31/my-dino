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
    public Button soundToggleButton;
    public Sprite[] soundStateIcons;
    public AudioSource backgroundMusic;
    public AudioSource[] effectSounds;

    private SoundState currentSoundState = SoundState.AllOn;

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
                SetEffectsMute(false);
                break;

            case SoundState.MusicMuted:
                backgroundMusic.mute = true;
                SetEffectsMute(false);
                break;

            case SoundState.AllMuted:
                backgroundMusic.mute = true;
                SetEffectsMute(true);
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
}
