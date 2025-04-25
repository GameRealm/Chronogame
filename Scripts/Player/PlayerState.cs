using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool IsGrounded { get; set; } = true;  // �� ����� �������� �� ����
    public bool IsClimbing { get; set; } = false; // �� ������ �� ����
    public bool IsRunning { get; set; } = false;  // �� �������� �����
    public bool IsOnStairs { get; set; } = false;
    public bool isOnStairs { get; set; } = false;
    public bool Lanter { get; set; } = false;
    public bool HasLantern { get; set; } = false; // �� ������������� ������� �� �� ������

}
