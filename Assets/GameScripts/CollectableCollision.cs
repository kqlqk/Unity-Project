using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectableCollision : MonoBehaviour
{
    public GameObject VictoryScreen; // Объект экрана победы
    public Button restartButton; // Кнопка перезапуска игры
    public Button exitButton; // Кнопка выхода из игры
    public Button nextLevelButton; // Кнопка перехода на следующий уровень

    private void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);
        nextLevelButton.onClick.AddListener(NextLevel);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Collectable was triggered by player");

            // Пауза игры и отображение экрана победы
            PauseGame();
            ShowVictoryScreen();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Приостановка времени в игре
    }

    private void ShowVictoryScreen()
    {
        VictoryScreen.SetActive(true); // Активация экрана победы
        // Дополнительный код для отображения элементов на экране победы, например кнопок "Сыграть еще раз" и "Выйти в меню"
    }

    private void RestartGame()
    {
        Time.timeScale = 1f; // Возобновление времени в игре
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезапуск текущей сцены
    }

    private void ExitGame()
    {
        Application.Quit(); // Выход из игры
    }

    private void NextLevel()
    {
        Time.timeScale = 1f; // Возобновление времени в игре
        SceneManager.LoadScene("Lvl" + ++GlobalScript.currentLvl); // Загрузка следующего уровня
    }
}