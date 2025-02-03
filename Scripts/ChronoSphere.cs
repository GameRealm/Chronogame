using UnityEngine;

public class ChronoSphere : MonoBehaviour
{
    public float slowDownFactor = 0.1f; // Множник для сповільнення руху
    private bool isInsideZone = false;  // Чи в межах зони сповільнення

    void Update()
    {
        // У цій області можна додавати додаткові дії, якщо потрібно
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Коли об'єкт входить в зону сповільнення
        if (other.CompareTag("Enemy"))  // Перевіряємо, чи це ворог
        {
            NPC enemy = other.GetComponent<NPC>();  // Отримуємо компонент Enemy
            if (enemy != null)
            {
                enemy.moveSpeed *= slowDownFactor;  // Сповільнюємо рух ворога
                Debug.Log("Ворог сповільнений всередині кола!");
            }
            isInsideZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Коли об'єкт виходить з зони сповільнення
        if (other.CompareTag("Enemy"))  // Перевіряємо, чи це ворог
        {
            NPC enemy = other.GetComponent<NPC>();  // Отримуємо компонент Enemy
            if (enemy != null)
            {
                enemy.moveSpeed /= slowDownFactor;  // Відновлюємо нормальну швидкість ворога
                Debug.Log("Ворог повернувся до нормальної швидкості!");
            }
            isInsideZone = false;
        }
    }
}
