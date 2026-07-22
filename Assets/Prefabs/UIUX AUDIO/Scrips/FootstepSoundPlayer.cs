using UnityEngine;

public class FootstepSoundPlayer : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Footstep Clip Groups")]
    public AudioClip[] landClips;
    public AudioClip[] jumpStartClips;
    public AudioClip[] walkClips;
    public AudioClip[] runClips;

    [Header("Random Variation")]
    public float pitchMin = 0.95f;
    public float pitchMax = 1.05f;
    public float volumeMin = 0.9f;
    public float volumeMax = 1.0f;

    // Gọi hàm này ở chỗ bạn đang trigger âm thanh land
    public void PlayLand() => PlayRandom(landClips);

    public void PlayJumpStart() => PlayRandom(jumpStartClips);

    public void PlayWalk() => PlayRandom(walkClips);

    public void PlayRun() => PlayRandom(runClips);

    private void PlayRandom(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;

        AudioClip clip = clips[Random.Range(0, clips.Length)];
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        audioSource.PlayOneShot(clip, Random.Range(volumeMin, volumeMax));
    }
}