using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeGrab : MonoBehaviour
{

    private bool isGrabbing = false; // �� �������� ������� ����
    private Transform grabPoint; // ����� ����������

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // �������� �� ������
        {
            Debug.Log("������� � ��� ����������!");
            isGrabbing = true;
            grabPoint = transform; // ����'���� �� ������ ��'����
            collision.transform.position = grabPoint.position; // ���������� ������
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero; // ��������� ��� ������
            rb.bodyType = RigidbodyType2D.Kinematic; // �������� ���������
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isGrabbing)
        {
            Debug.Log("������� ������ �� ���� ����������.");
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic; // ³��������� ������
            isGrabbing = false;
        }
    }
}
