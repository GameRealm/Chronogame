using UnityEngine;

public class ChronoTrap : MonoBehaviour
{
    public float lifetimeAfterEnemyEnter = 5.0f; // ��� �� ��������� ���� ����� ������
    private bool isEnemyInside = false;          // �� � ����� ��������
    private float timer = 0f;                    // ˳������� ����
    private bool isTimerActive = false;          // �� �������� ������
    public bool startTimerImmediately = false;   // �� ������ ������ ������ (��� ������ E)
    public float slowDownFactor = 0.5f;          // ������ ����������� ������ (0.5 = ����������)

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
            timer += Time.deltaTime; // �������� ������
            if (timer >= lifetimeAfterEnemyEnter)
            {
                Destroy(gameObject); // ��������� ����������
                Debug.Log("ChronoTrap ������� ���� ���������� �������.");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            isEnemyInside = true;

            if (!isTimerActive) // ��������� ������ ��� ���� ������
            {
                isTimerActive = true;
                Debug.Log("����� ������ � ����������! ��������� ������.");
            }

            // �������� �������� ������
            NPC enemyScript = other.GetComponent<NPC>();
            if (enemyScript != null)
            {
                enemyScript.ModifySpeed(slowDownFactor); // ����������� ������
                Debug.Log("�������� ������ ��������.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            isEnemyInside = false;

            // ³��������� �������� ������
            NPC enemyScript = other.GetComponent<NPC>();
            if (enemyScript != null)
            {
                enemyScript.ResetSpeed(); // ³��������� ��������� ��������
                Debug.Log("�������� ������ ���������.");
            }

            Debug.Log("����� ������ �� ����������!");
        }
    }
}
