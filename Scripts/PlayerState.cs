using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool IsGrounded { get; set; } = true;  // Чи стоїть персонаж на землі
    public bool IsClinging { get; set; } = false; // Чи прилип до стіни
    public bool IsRunning { get; set; } = false;  // Чи персонаж біжить
    public bool IsOnStairs { get; set; } = false;

    public bool isClimbing { get; set; } = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            IsOnStairs = true;
            GetComponent<Rigidbody2D>().gravityScale = 0; // Вимикаємо гравітацію на сходах
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            IsOnStairs = false;
            GetComponent<Rigidbody2D>().gravityScale = 1; // Вмикаємо гравітацію після виходу зі сходів
        }


    }
}
