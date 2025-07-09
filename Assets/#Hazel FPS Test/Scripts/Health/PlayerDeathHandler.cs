using System;
using UnityEngine;
public class PlayerDeathHandler : MonoBehaviour
{
    public GameObject gameOverUI;
    [HideInInspector] public HealthComponent health;

    private void Awake()
    {
        health = GetComponent<HealthComponent>();
    }

    private void Start()
    {
        health.OnDeath += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // freeze game
        Cursor.lockState = CursorLockMode.None;
    }
}