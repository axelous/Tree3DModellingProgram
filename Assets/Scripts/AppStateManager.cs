using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AppStateManager : MonoBehaviour {
    public void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void Start() {
        // TreesFileLoader.GotSuccessfulTreeListLoad += OnSuccessfulTreeListTreeListLoad;
    }

    public void AppExit() {
        Application.Quit();
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    
    /*private*/ public void OnSuccessfulTreeListTreeListLoad() {
        SceneManager.LoadScene("MainScene");
    }
}