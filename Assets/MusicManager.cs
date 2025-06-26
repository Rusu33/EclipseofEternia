using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        float volume = PlayerPrefs.GetFloat("volume", 1f);
        if (musicSource != null)
            musicSource.volume = volume;
    }

    public void SetVolume(float value)
    {
        if (musicSource != null)
            musicSource.volume = value;
        PlayerPrefs.SetFloat("volume", value);
        PlayerPrefs.Save();
    }
}
