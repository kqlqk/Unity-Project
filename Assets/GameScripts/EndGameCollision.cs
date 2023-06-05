using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameCollision : MonoBehaviour
{
    public GameObject DeathScreen; // Объявляем переменную DeathScreen
    private bool isDeathMenuActive = false; // Флаг, указывающий, активно ли меню смерти

    public Button restartButton; // Кнопка перезапуска уровня
    public Button mainMenuButton; // Кнопка возврата в главное меню

    private void Start()
    {
        // Назначаем методы обработки событий кнопок
        restartButton.onClick.AddListener(RestartLevel);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player was triggered by enemy");

            DeathScreen.SetActive(true); // Активируем объект DeathScreen
            isDeathMenuActive = true; // Устанавливаем флаг активности меню смерти

            // Останавливаем перезапуск уровня и игру, если меню смерти активно
            if (isDeathMenuActive)
            {
                Time.timeScale = 0f; // Приостанавливаем игру
                return;
            }

            SceneManager.LoadScene("Lvl" + GlobalScript.currentLvl);
        }
    }

    private void RestartLevel()
    {
        Debug.Log("1");
        SceneManager.LoadScene("Lvl" + GlobalScript.currentLvl);
        Time.timeScale = 1f; // Возобновляем игру
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f; // Возобновляем игру
    }
}


