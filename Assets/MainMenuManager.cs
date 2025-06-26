using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Containers")]
    public GameObject mainMenuContainer;
    public GameObject soundSettingsContainer;
    public Image fadePanel;
    public float fadeSpeed = 2f;
    public GameObject newGameDialog;

    [Header("Audio")]
    public Slider volumeSlider;
    public AudioSource musicSource;

    private float defaultVolume = 1f;

   

    public void ShowNewGameDialog()
    {
        newGameDialog.SetActive(true);
    }

    private void Start()
    {

        float savedVolume = PlayerPrefs.GetFloat("volume", defaultVolume);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
        if (musicSource != null)
            musicSource.volume = savedVolume;

        if (fadePanel != null)
        {
            fadePanel.color = new Color(0, 0, 0, 1);
            StartCoroutine(FadeIn());
        }
    }

    public void StartNewGame()
    {
        StartCoroutine(FadeOutAndStartGame("map")); 
    }
    public void OpenOptions()
    {
        mainMenuContainer.SetActive(false);
        soundSettingsContainer.SetActive(true);
    }

    public void CloseOptions()
    {
        soundSettingsContainer.SetActive(false);
        mainMenuContainer.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetSettings()
    {
        volumeSlider.value = defaultVolume;
        AudioListener.volume = defaultVolume;
        PlayerPrefs.SetFloat("volume", defaultVolume);
        PlayerPrefs.Save();
        musicSource.volume = defaultVolume;
    }

    public void ApplySettings()
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();

        
        MusicManager music = FindObjectOfType<MusicManager>();
        if (music != null)
            music.SetVolume(volume);
    }

    IEnumerator FadeIn()
    {
        while (fadePanel.color.a > 0)
        {
            fadePanel.color = new Color(0, 0, 0, fadePanel.color.a - Time.deltaTime * fadeSpeed);
            yield return null;
        }
    }

    IEnumerator FadeOutAndStartGame(string sceneName)
    {
        while (fadePanel.color.a < 1)
        {
            fadePanel.color = new Color(0, 0, 0, fadePanel.color.a + Time.deltaTime * fadeSpeed);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
