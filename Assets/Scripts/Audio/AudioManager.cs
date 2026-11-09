using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip gamePlayBackgroundMusic;
    [SerializeField] private AudioClip mainMenuBackgroundMusic;
    [SerializeField] private AudioClip bowShootingSound;

    void Start()
    {
        PlayMusic();
    }

    private void PlayMusic()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            musicSource.clip = mainMenuBackgroundMusic;
        } else
        {
            musicSource.clip = gamePlayBackgroundMusic;
        }

        musicSource.Play();
    }

    private void PlaySoundEffect(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayBowShootingSound() => PlaySoundEffect(bowShootingSound);
}
