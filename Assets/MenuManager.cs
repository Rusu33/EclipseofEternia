using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Button newGameButton;
    public Button settingsButton;
    public Button quitButton;
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public AudioSource musicSource;

    private void Start()
    {
        settingsPanel.SetActive(false);

        newGameButton.onClick.AddListener(StartNewGame);
        settingsButton.onClick.AddListener(ToggleSettings);
        quitButton.onClick.AddListener(QuitGame);

        volumeSlider.onValueChanged.AddListener(SetVolume);
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
        SetVolume(volumeSlider.value);
    }

    void StartNewGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace with your actual game scene name
    }

    void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void SetVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }
}
