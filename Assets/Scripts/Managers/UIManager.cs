using System.Collections;
using UnityEngine.UI;
using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class UIManager : MonoBehaviour
{

    public LevelManager levelmanager;

    // References to UI Panels
    public GameObject mainMenuUI;
    public GameObject gamePlayUI;
    public GameObject gameOverUI;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject creditsMenuUI;
    public GameObject LoadingScreenUI;

    public Image LaodingBar;

    // Gameplay Specific UI Elements
    public Text LevelCount;

    public void UpdateLevelCount(int count)
    {
        if (LevelCount != null)
        { LevelCount.text = count.ToString(); }

        if (LevelCount = null)
        { Debug.LogError("LevelCount is not assigned to UIManager in the inspector!"); }
    }


    public void UIMainMenu()
    {
        DisableAllUIPanels();
        mainMenuUI.SetActive(true);
    }

    public void UIGamePlay()
    {
        DisableAllUIPanels();
        gamePlayUI.SetActive(true);
    }

    public void UIGameOver()
    {
        DisableAllUIPanels();
        gameOverUI.SetActive(true);
    }

    public void UIPaused()
    {
        DisableAllUIPanels();
        pauseMenuUI.SetActive(true);
    }

    public void UIOptions()
    {
        DisableAllUIPanels();
        optionsMenuUI.SetActive(true);
    }
    public IEnumerator LoadingBarProgress()
    {
        while(!levelmanager.sceneLoad.isDone)
        {
            LaodingBar.fillAmount = levelmanager.GetLoadingProgress();
            yield return null;
        }
    }

    public void UICredits()
    {
        DisableAllUIPanels();
        creditsMenuUI.SetActive(true);
    }

    public void UILoadingScreen(GameObject targetUI)
    {
        DisableAllUIPanels();
        LoadingScreenUI.SetActive(true);
    }
    public void DisableLoadScreen(GameObject targetUI)
    {
        DisableAllUIPanels();
        if(targetUI == gamePlayUI)
        {
            gamePlayUI.SetActive(true);
        }
        else if(targetUI == mainMenuUI)
        {
            mainMenuUI.SetActive(true);
        }
    }

    public void DisableAllUIPanels()
    {
        mainMenuUI.SetActive(false);
        gamePlayUI.SetActive(false);
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        creditsMenuUI.SetActive(false);
        LoadingScreenUI.SetActive(false);
    }

    public void EnableAllUIPanels()
    {
        mainMenuUI.SetActive(true);
        gamePlayUI.SetActive(true);
        gameOverUI.SetActive(true);
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(true);
        creditsMenuUI.SetActive(false);
    }



}
