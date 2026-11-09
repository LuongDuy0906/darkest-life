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
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip levelUpSound;
    [SerializeField] private AudioClip outOfArrowSound;
    [SerializeField] private AudioClip hitEnemySound;
    [SerializeField] private AudioClip buttonClickSound;


    private float defaultEffectVolume = 0.4f;

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

    private void PlaySoundEffect(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayBowShootingSound() => PlaySoundEffect(bowShootingSound, defaultEffectVolume);
    public void PlayItemCollectedSound() => PlaySoundEffect(itemCollectedSound, defaultEffectVolume);
    public void PlayExpOrbCollectedSound() => PlaySoundEffect(expOrbCollectedSound, defaultEffectVolume);
    public void PlayDamageTakenSound() => PlaySoundEffect(damageTakenSound, defaultEffectVolume);
    public void PlayGameOverSound() => PlaySoundEffect(gameOverSound, defaultEffectVolume);
    public void PlayLevelUpSound() => PlaySoundEffect(levelUpSound, defaultEffectVolume);
    public void PlayOutOfArrowSound() => PlaySoundEffect(outOfArrowSound, defaultEffectVolume);
    public void PlayHitEnemySound() => PlaySoundEffect(hitEnemySound, 0.25f);
    public void PlayButtonClickSound() => PlaySoundEffect(buttonClickSound, 0.5f);
}
