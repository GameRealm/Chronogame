using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement; // Додаємо для роботи з завантаженням сцен

public class FinalSceneTrigger : MonoBehaviour
{
    public PlayableDirector finalSceneDirector; // PlayableDirector для фінальної катсцени
    public PlayerDataSO playerData; // Дані гравця для перевірки вибору шляху

    public double rationalStartTime = 0.0; // Час старту для раціонального шляху
    public double intuitiveStartTime = 10.0; // Час старту для інтуїтивного шляху
    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Перевірка, чи гравець увійшов в тригер і чи катсцена ще не запущена
        if (!hasPlayed && other.CompareTag("Player"))
        {
            hasPlayed = true; // Встановлюємо, що катсцена запущена

            if (finalSceneDirector != null && playerData != null)
            {
                // Отримуємо вибір шляху гравця
                var pathChoice = playerData.data.lastChoice;

                // Задаємо початковий час залежно від вибору шляху
                switch (pathChoice)
                {
                    case PlayerChoiceType.Rational:
                        finalSceneDirector.time = rationalStartTime;
                        finalSceneDirector.Play();
                        Invoke(nameof(StopCutscene), 12.0f); // Перериваємо катсцену через 12 секунд для раціонального шляху
                        break;

                    case PlayerChoiceType.Intuitive:
                        finalSceneDirector.time = intuitiveStartTime;
                        finalSceneDirector.Play();
                        // Додаємо подію, щоб викликати перехід до головного меню після завершення катсцени
                        finalSceneDirector.stopped += OnFinalSceneFinished;
                        break;

                    default:
                        Debug.LogWarning("Невідомий шлях вибору, запускаємо з початку.");
                        finalSceneDirector.time = 0.0; // Якщо шлях не визначений, запускаємо з початку
                        finalSceneDirector.Play();
                        break;
                }

                Debug.Log("Фінальна катсцена розпочалася.");
            }
            else
            {
                Debug.LogError("FinalSceneDirector або PlayerDataSO не призначені!");
            }
        }
    }

    // Метод для зупинки катсцени
    void StopCutscene()
    {
        if (finalSceneDirector.state == PlayState.Playing)
        {
            finalSceneDirector.Stop();
            Debug.Log("Катсцена перервана на 12 секунді (раціональний шлях).");
        }

        LoadMainMenu();
    }

    // Обробник завершення катсцени (для інтуїтивного шляху)
    void OnFinalSceneFinished(PlayableDirector director)
    {

            LoadMainMenu();
            Debug.Log("Фінальна катсцена завершена, перехід до головного меню.");
        
    }

    // Завантаження головного меню
    void LoadMainMenu()
    {
        SceneManager.LoadScene("Main menu"); // Замініть "Main menu" на точну назву вашої сцени головного меню
        Debug.Log("Перехід до головного меню.");
    }
}
