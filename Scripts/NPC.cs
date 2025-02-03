using UnityEngine;

public class NPC : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Швидкість руху
    private float originalSpeed;  // Початкова швидкість
    private Vector2 moveDirection = Vector2.right; // Початковий напрямок
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Зберігаємо початкову швидкість
        originalSpeed = moveSpeed;

        // Перевірка на наявність Rigidbody2D
        if (rb == null)
        {
            Debug.LogError("На об'єкті немає компонента Rigidbody2D! Додайте його в Inspector.");
            enabled = false; // Вимкнути скрипт, якщо Rigidbody2D немає
        }
    }

    void FixedUpdate()
    {
        // Рух NPC через Rigidbody2D
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Колізія з: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Wall"))
        {
            moveDirection = -moveDirection; // Реверс напряму руху
            FlipNPC();                      // Розворот візуально
        }
    }

    void FlipNPC()
    {
        // Розворот NPC по осі X
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void ModifySpeed(float factor)
    {
        // Змінюємо швидкість NPC
        moveSpeed = originalSpeed * factor;
        Debug.Log($"Швидкість NPC змінена на {moveSpeed}");
    }

    public void ResetSpeed()
    {
        // Відновлюємо початкову швидкість NPC
        moveSpeed = originalSpeed;
        Debug.Log("Швидкість NPC відновлено.");
    }
}
