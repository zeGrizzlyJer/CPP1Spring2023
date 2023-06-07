using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button startButton;
    public Button settingsButton;
    public Button backButton;
    public Button quitButton;
    public Button returnToMenu;
    public Button resumeGame;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    [Header("Text")]
    public Text livesText;
    public Text volSliderText;

    [Header("Slider")]
    public Slider volSlider;

    private void Start()
    {
        if (startButton)    startButton.onClick.AddListener(StartGame);
        if (settingsButton) settingsButton.onClick.AddListener(ShowSettingsMenu);
        if (backButton)     backButton.onClick.AddListener(ShowMainMenu);
        if (quitButton)     quitButton.onClick.AddListener(Quit);
        if (volSlider)
        {
            volSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value));
            if (volSliderText) volSliderText.text = volSlider.value.ToString();
        }
        if (livesText)
        {
            GameManager.instance.OnLifeValueChanged.AddListener((value) => UpdateLifeText(value));
            livesText.text = "Lives: " + GameManager.instance.lives;
        }
        if (resumeGame)     resumeGame.onClick.AddListener(UnpauseGame);
        if (returnToMenu)   returnToMenu.onClick.AddListener(LoadTitle);
    }
    private void Update()
    {
        if (!pauseMenu) return;
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
    private void StartGame()
    {
        SceneManager.LoadScene("Level");
    }
    private void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    private void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    private void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
    private void OnSliderValueChanged(float value)
    {
        volSliderText.text = value.ToString();
    }
    private void UpdateLifeText(int value)
    {
        livesText.text = "Lives: " + value.ToString();
    }
    private void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    private void LoadTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
}
