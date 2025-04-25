using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour
{
    public Transform teleportTarget; // ���� ���� (���� �������������)
    private bool isPlayerInDoor = false;
    private Animator animator;

    void Start()
    {
        // ����������, �� � ��������� Animator �� ��'���
        animator = GetComponentInChildren<Animator>(); // ��������� Animator �������� �������
        if (animator == null)
        {
            Debug.LogError("Animator �� ��������� �� ��'���!");
        }
    }

    void Update()
    {
        if (isPlayerInDoor && Input.GetKeyDown(KeyCode.W)) // ���� �������� � ������ � �������� "�����"
        {
            OpenDoor(); // ��������� ������� ����������
        }
    }

    void OpenDoor()
    {
        if (animator != null)
        {
            animator.SetTrigger("Open"); // ��������� �������
        }
        else
        {
            Debug.LogError("Animator �� ������������. �������������, �� ��������� Animator ������� �� ��'����.");
            return; // ���� ������� �� ��������, �� �������� �������� 䳿
        }

        // ��������� ��������, ��� ���������� ���������� �������
        StartCoroutine(TeleportPlayerAfterAnimation());
    }

    IEnumerator TeleportPlayerAfterAnimation()
    {
        if (animator == null)
        {
            yield break; // ���� ������� ��� � �� ������������, �������� ����� �� ������
        }

        // ������ ���� ������� �� �����������
        float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationDuration); // ������ ��� �������

        // ϳ��� ����, �� ������� �����������, ����������� ������
        TeleportPlayer();
    }

    void TeleportPlayer()
    {
        if (teleportTarget != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player"); // ��������� ������
            if (player != null)
            {
                player.transform.position = teleportTarget.position; // ��������� ���� �� ��'������ ������
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ���� �������� ������ � ����
        {
            isPlayerInDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ���� �������� ������ �� ������
        {
            isPlayerInDoor = false;
        }
    }
}
