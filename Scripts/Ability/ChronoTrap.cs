using UnityEngine;

public class ChronoTrap : MonoBehaviour
{
    public float lifetimeAfterEnemyEnter = 5.0f; // Час до видалення після входу ворога
    private bool isEnemyInside = false;          // Чи є ворог всередині
    private float timer = 0f;                    // Лічильник часу
    private bool isTimerActive = false;          // Чи активний таймер
    public bool startTimerImmediately = false;   // Чи стартує таймер одразу (для клавіші E)
    public float slowDownFactor = 0.5f;          // Фактор уповільнення ворога (0.5 = наполовину)

    void Start()
    {
        if (startTimerImmediately)
        {
            isTimerActive = true;
        }
    }

    void Update()
    {
        if (isTimerActive)
        {
            timer += Time.deltaTime; // Збільшуємо таймер
            if (timer >= lifetimeAfterEnemyEnter)
            {
                Destroy(gameObject); // Видаляємо хроносферу
                Debug.Log("ChronoTrap знищено після завершення таймера.");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            isEnemyInside = true;

            if (!isTimerActive) // Запускаємо таймер при вході ворога
            {
                isTimerActive = true;
                Debug.Log("Ворог увійшов у хроносферу! Запускаємо таймер.");
            }

            // Зменшуємо швидкість ворога
            NPC enemyScript = other.GetComponent<NPC>();
            if (enemyScript != null)
            {
                enemyScript.ModifySpeed(slowDownFactor); // Уповільнюємо ворога
                Debug.Log("Швидкість ворога зменшено.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            isEnemyInside = false;

            // Відновлюємо швидкість ворога
            NPC enemyScript = other.GetComponent<NPC>();
            if (enemyScript != null)
            {
                enemyScript.ResetSpeed(); // Відновлюємо нормальну швидкість
                Debug.Log("Швидкість ворога відновлено.");
            }

            Debug.Log("Ворог вийшов із хроносфери!");
        }
    }
}
