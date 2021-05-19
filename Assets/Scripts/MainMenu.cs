using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelectorMenu;

    public void PlayGame()
    {
        Debug.Log("Loading Next Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game!");
        Application.Quit();
    }

    public void LoadLevelSelector()
    {
        mainMenu.SetActive(false);
        levelSelectorMenu.SetActive(true);
    }

    public void LoadMainMenu()
    {
        mainMenu.SetActive(true);
        levelSelectorMenu.SetActive(false);
    }

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void Start()
    {
        Time.timeScale = 1;

    }
}
