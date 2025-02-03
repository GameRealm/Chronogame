using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool IsGrounded { get; set; } = true;  // �� ����� �������� �� ����
    public bool IsClinging { get; set; } = false; // �� ������ �� ����
    public bool IsRunning { get; set; } = false;  // �� �������� �����
    public bool IsOnStairs { get; set; } = false;

    public bool isClimbing { get; set; } = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            IsOnStairs = true;
            GetComponent<Rigidbody2D>().gravityScale = 0; // �������� ��������� �� ������
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            IsOnStairs = false;
            GetComponent<Rigidbody2D>().gravityScale = 1; // ������� ��������� ���� ������ � �����
        }


    }
}
