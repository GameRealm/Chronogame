using UnityEngine;

public class ChronoSphere : MonoBehaviour
{
    public float slowDownFactor = 0.1f; // ������� ��� ����������� ����
    private bool isInsideZone = false;  // �� � ����� ���� �����������

    void Update()
    {
        // � ��� ������ ����� �������� �������� 䳿, ���� �������
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ���� ��'��� ������� � ���� �����������
        if (other.CompareTag("Enemy"))  // ����������, �� �� �����
        {
            NPC enemy = other.GetComponent<NPC>();  // �������� ��������� Enemy
            if (enemy != null)
            {
                enemy.moveSpeed *= slowDownFactor;  // ����������� ��� ������
                Debug.Log("����� ����������� �������� ����!");
            }
            isInsideZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // ���� ��'��� �������� � ���� �����������
        if (other.CompareTag("Enemy"))  // ����������, �� �� �����
        {
            NPC enemy = other.GetComponent<NPC>();  // �������� ��������� Enemy
            if (enemy != null)
            {
                enemy.moveSpeed /= slowDownFactor;  // ³��������� ��������� �������� ������
                Debug.Log("����� ���������� �� ��������� ��������!");
            }
            isInsideZone = false;
        }
    }
}
