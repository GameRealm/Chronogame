using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool IsGrounded { get; set; } = true;  // Чи стоїть персонаж на землі
    public bool IsClimbing { get; set; } = false; // Чи прилип до стіни
    public bool IsRunning { get; set; } = false;  // Чи персонаж біжить
    public bool IsOnStairs { get; set; } = false;
    public bool isOnStairs { get; set; } = false;
    public bool Lanter { get; set; } = false;
    public bool HasLantern { get; set; } = false; // За замовчуванням гравець не має ліхтаря

}
