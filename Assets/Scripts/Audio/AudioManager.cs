using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip gamePlayBackgroundMusic;
    [SerializeField] private AudioClip mainMenuBackgroundMusic;
    [SerializeField] private AudioClip bowShootingSound;
    [SerializeField] private AudioClip itemCollectedSound;
    [SerializeField] private AudioClip expOrbCollectedSound;
    [SerializeField] private AudioClip damageTakenSound;

    void Awake()
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
    public void PlayItemCollectedSound() => PlaySoundEffect(itemCollectedSound);
    public void PlayExpOrbCollectedSound() => PlaySoundEffect(expOrbCollectedSound);
    public void PlayDamageTakenSound() => PlaySoundEffect(damageTakenSound);
}
