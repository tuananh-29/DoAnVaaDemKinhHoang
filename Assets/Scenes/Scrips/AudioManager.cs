using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Tạo Singleton để các bạn khác trong nhóm (Người 1, 2, 3) dễ dàng gọi âm thanh từ bất cứ đâu
    public static AudioManager Instance { get; private set; }

    [Header("---- Audio Sources ----")]
    public AudioSource ambienceSource;
    public AudioSource bgmSource;
    public AudioSource sfxSource; // Dùng để phát tiếng click UI, tiếng mở cửa, v.v.

    [Header("---- Audio Clips ----")]
    public AudioClip horrorAmbience;
    public AudioClip chaseMusic;
    public AudioClip jumpscareSFX;

    private void Awake()
    {
        // Khởi tạo Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ âm thanh xuyên suốt các màn chơi
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Khi game bắt đầu, tự động phát âm thanh môi trường âm u
        if (horrorAmbience != null)
        {
            ambienceSource.clip = horrorAmbience;
            ambienceSource.loop = true;
            ambienceSource.Play();
        }
    }

    // Hàm phát tiếng động ngắn (nhặt đồ, mở cửa, click nút...)
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Hàm đổi nhạc khi bị quái đuổi (Dành cho người số 3 gọi)
    public void ChangeBGM(AudioClip newTrack)
    {
        bgmSource.Stop();
        bgmSource.clip = newTrack;
        bgmSource.loop = true;
        bgmSource.Play();
    }
}