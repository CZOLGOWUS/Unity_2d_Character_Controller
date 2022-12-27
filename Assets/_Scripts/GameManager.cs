using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static Action OnPlayerDeath;
    public static Action OnSceneLoad;
    public static Action OnGameFinish;

    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject gameFinishScreen;

    private void Start()
    {
        OnPlayerDeath += ShowDeathScreen;
        OnPlayerDeath += stopTime;

        OnGameFinish += stopTime;
        OnGameFinish += ShowGameFinishScreen;
    }

    private void OnDisable()
    {
        OnPlayerDeath -= ShowDeathScreen;
        OnPlayerDeath -= stopTime;

        OnGameFinish -= stopTime;
        OnGameFinish -= ShowGameFinishScreen;
    }

    private void ShowDeathScreen()
    {
        deathScreen?.SetActive(true);
    }

    private void ShowGameFinishScreen()
    {
        gameFinishScreen?.SetActive(true);
    }

    public void stopTime()
    {
        Time.timeScale = 0f;
    }

    public static void ReloadLevel()
    {
        Time.timeScale = 1f;
        var deathScreen = GameObject.Find("DeathScreen");
        deathScreen?.SetActive(false);

        var gameFinishScreen = GameObject.Find("GameFinishScreen");
        gameFinishScreen?.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
