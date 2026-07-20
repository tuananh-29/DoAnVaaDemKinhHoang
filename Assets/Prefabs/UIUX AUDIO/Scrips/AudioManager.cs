using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Mixer")]
    public AudioMixer mainMixer;

    [Header("Audio Sources")]
    public AudioSource musicSource;      // phat nhac nen
    public AudioSource ambienceSource;   // phat am thanh moi truong
    public AudioSource sfxSource;        // phat hieu ung ngan (canh bao, mo cua...)
    public AudioSource footstepSource;   // phat tieng buoc chan rieng

    [Header("Audio Clips")]
    public AudioClip nhacNenMenu;
    public AudioClip nhacNenGameplay;
    public AudioClip ambienceGameplay;
    public AudioClip[] tiengBuocChan;
    public AudioClip amThanhCanhBao;

    void Awake()
    {
        // Dam bao chi co 1 AudioManager duy nhat, khong bi mat khi doi Scene
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

    // ---- NHAC NEN ----
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return; // dang phat roi thi khong phat lai
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // ---- AMBIENCE (tieng gio, moi truong...) ----
    public void PlayAmbience(AudioClip clip)
    {
        ambienceSource.clip = clip;
        ambienceSource.loop = true;
        ambienceSource.Play();
    }

    // ---- HIEU UNG NGAN (mo cua, nhat vat pham, canh bao...) ----
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayCanhBao()
    {
        sfxSource.PlayOneShot(amThanhCanhBao);
    }

    // ---- TIENG BUOC CHAN (goi lien tuc khi Player di chuyen) ----
    public void PlayFootstep()
    {
        if (tiengBuocChan.Length == 0) return;
        AudioClip clip = tiengBuocChan[Random.Range(0, tiengBuocChan.Length)];
        footstepSource.PlayOneShot(clip);
    }

    // ---- DIEU CHINH AM LUONG (dung cho Settings Slider) ----
    public void SetMasterVolume(float value)
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    public void SetMusicVolume(float value)
    {
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    public void SetSFXVolume(float value)
    {
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
    }
}