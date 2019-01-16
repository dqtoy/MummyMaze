using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;
    public GameObject nextLevelMenu;
    public GameObject wonMenu;
    public GameObject levelFailMenu;

    public GameObject loadingLevel;
    public Slider progressBar;

    private void Start()
    {
        string menuType = PlayerPrefs.GetString("MenuType", "MainMenu");
        if (menuType == "MainMenu")
        {
            mainMenu.SetActive(true);
            optionMenu.SetActive(false);
            nextLevelMenu.SetActive(false);
            wonMenu.SetActive(false);
            levelFailMenu.SetActive(false);
        }
        else if(menuType == "OptionMenu")
        {
            mainMenu.SetActive(false);
            optionMenu.SetActive(true);
            nextLevelMenu.SetActive(false);
            wonMenu.SetActive(false);
            levelFailMenu.SetActive(false);
        }
        else if(menuType == "NextLevelMenu")
        {
            mainMenu.SetActive(false);
            optionMenu.SetActive(false);
            nextLevelMenu.SetActive(true);
            wonMenu.SetActive(false);
            levelFailMenu.SetActive(false);
        }
        else if(menuType == "WonMenu")
        {
            mainMenu.SetActive(false);
            optionMenu.SetActive(false);
            nextLevelMenu.SetActive(false);
            wonMenu.SetActive(true);
            levelFailMenu.SetActive(false);
        }
        if (menuType == "LevelFailMenu")
        {
            mainMenu.SetActive(false);
            optionMenu.SetActive(false);
            nextLevelMenu.SetActive(false);
            wonMenu.SetActive(false);
            levelFailMenu.SetActive(true);
        }
       
    }

    public void PlayGame()
    {
        // The start scene is after the menu scene
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetPlayerPrefsMenu(string menu)
    {
        PlayerPrefs.SetString("MenuType", menu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    // Build index of the scene which call this menu
    public void NextLevel()
    {
        int buildIndex = PlayerPrefs.GetInt("SceneBuildIndex");
        LoadLevel(buildIndex + 1);
    }

    public void Retry()
    {
        int buildIndex = PlayerPrefs.GetInt("SceneBuildIndex");
        LoadLevel(buildIndex);
    }

    public void Undo()
    {
        SceneManager.UnloadSceneAsync(0);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingLevel.SetActive(true);
        while (!operation.isDone)
        {
            //0 - 0.9 is used for loading material, 0.9 - 1 is used for loading scene
        
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressBar.value = progress;

            yield return null;
        }
    }
}
