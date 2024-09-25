using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class LevelManager : MonoBehaviour
{
    [Header("Script References")]
    public GameStateManager _gameStateManager;
    public CameraManager _cameraManager;
    public GameManager _gameManager;
    public PlayerManager _playerManager;
    public UIManager _uIManager;


    public int nextScene;

    public AsyncOperation sceneLoad;

    public void Awake()
    {
        // Check for missing script references
        if (_gameStateManager == null) { Debug.LogError("LevelManager is not assigned to LevelManager in the inspector!"); }
        if (_cameraManager == null) { Debug.LogError("CameraManager is not assigned to LevelManager in the inspector!"); }
        if (_gameManager == null) { Debug.LogError("GameManager is not assigned to LevelManager in the inspector!"); }
        if (_playerManager == null) { Debug.LogError("PlayerManager is not assigned to LevelManager in the inspector!"); }
        if (_uIManager == null) { Debug.LogError("UIManager is not assigned to LevelManager in the inspector!"); }
    }

    void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void LoadNextlevel()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        LoadScene(nextScene);
        _gameStateManager.SwitchToState(_gameStateManager.gameState_GamePlay);       
    }
    public void LoadScene(string name)
    {
        switch(name)
        {
            case "MainMenu":
                _uIManager.UILoadingScreen(_uIManager.mainMenuUI);
                _gameStateManager.SwitchToState(_gameStateManager.gameState_MainMenu);
                break;
            case "TestLevel":
                _uIManager.UILoadingScreen(_uIManager.gamePlayUI);
                _gameStateManager.SwitchToState(_gameStateManager.gameState_GamePlay);
                break;
        }
        StartSceneLoad(name);
    }
    void StartSceneLoad(string Scenename)
    {
        sceneLoad = SceneManager.LoadSceneAsync(Scenename);
        sceneLoad.completed += OperationCompleted;
    }
    void OperationCompleted(AsyncOperation operation)
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            _uIManager.DisableLoadScreen(_uIManager.mainMenuUI);
        }
        else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TestLevel"))
        {
            _uIManager.DisableLoadScreen(_uIManager.gamePlayUI);
        }
    }
    public float GetLoadingProgress()
    {
        return sceneLoad.progress;
    }
    public void LoadMainMenuScene()
    {
        LoadScene(0);
        _gameStateManager.SwitchToState(_gameStateManager.gameState_GameInit);
    }

    public void ReloadCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
        _gameStateManager.SwitchToState(_gameStateManager.gameState_GamePlay);
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}



