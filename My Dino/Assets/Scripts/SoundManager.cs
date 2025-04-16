using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

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
        //PlayBGM();
    }

    //public void PlayBGM()
    //{
    //    if (bgmClip != null)
    //    {
    //        bgmSource.clip = bgmClip;
    //        bgmSource.loop = true;
    //        bgmSource.Play();
    //    }
    //}

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

    //public void SetBGMVolume(float volume)
    //{
    //    bgmSource.volume = volume;
    //}

    public void SetMuteVolume(bool status)
    {
        //sfxTouchSource.volume = volume;
        sfxTouchSource.mute = status;
        sfxGameOverSource.mute = status;
        sfxStartGameSource.mute = status;
    }
}
