using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

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
    [SerializeField] private AudioClip buttonHoverSound;


    private float defaultEffectVolume = 0.4f;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void PlayMusic(AudioClip clip)
    {
        if(musicSource.clip == clip && musicSource.isPlaying) return;
        
        musicSource.clip = clip;
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
    public void PlayButtonHoverSound() => PlaySoundEffect(buttonHoverSound, 0.1f);

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip clipToPlay;

        if(scene.name == "MainMenu")
        {
            clipToPlay = mainMenuBackgroundMusic;
        } else
        {
            clipToPlay = gamePlayBackgroundMusic;
        }

        PlayMusic(clipToPlay);
    }
}
