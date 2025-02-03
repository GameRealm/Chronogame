using UnityEngine;

public class NPC : MonoBehaviour
{
    public float moveSpeed = 2.0f; // �������� ����
    private float originalSpeed;  // ��������� ��������
    private Vector2 moveDirection = Vector2.right; // ���������� ��������
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // �������� ��������� ��������
        originalSpeed = moveSpeed;

        // �������� �� �������� Rigidbody2D
        if (rb == null)
        {
            Debug.LogError("�� ��'��� ���� ���������� Rigidbody2D! ������� ���� � Inspector.");
            enabled = false; // �������� ������, ���� Rigidbody2D ����
        }
    }

    void FixedUpdate()
    {
        // ��� NPC ����� Rigidbody2D
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("����� �: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Wall"))
        {
            moveDirection = -moveDirection; // ������ ������� ����
            FlipNPC();                      // �������� ��������
        }
    }

    void FlipNPC()
    {
        // �������� NPC �� �� X
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void ModifySpeed(float factor)
    {
        // ������� �������� NPC
        moveSpeed = originalSpeed * factor;
        Debug.Log($"�������� NPC ������ �� {moveSpeed}");
    }

    public void ResetSpeed()
    {
        // ³��������� ��������� �������� NPC
        moveSpeed = originalSpeed;
        Debug.Log("�������� NPC ���������.");
    }
}
