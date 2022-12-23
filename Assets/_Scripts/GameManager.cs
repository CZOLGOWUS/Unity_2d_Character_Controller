using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static Action OnPlayerDeath;
    public static Action OnSceneLoad;

    [SerializeField] GameObject deathScreen;

    private void Start()
    {
        // DontDestroyOnLoad(deathScreen);
        // DontDestroyOnLoad(this);

        OnPlayerDeath += ShowDeathScreen;
        OnPlayerDeath += stopTime;
    }

    private void OnDisable()
    {
        OnPlayerDeath -= ShowDeathScreen;
        OnPlayerDeath -= stopTime;
    }

    private void ShowDeathScreen()
    {
        deathScreen?.SetActive(true);
    }

    public void stopTime()
    {
        Time.timeScale = 0f;
    }

    public static void ReloadLevel()
    {
        Time.timeScale = 1f;
        var deathScreen = GameObject.Find("DeathScreen");
        deathScreen.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
