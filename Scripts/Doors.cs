using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour
{
    public Transform teleportTarget; // ���� ���� (���� �������������)
    private bool isPlayerInDoor = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>(); // ��������� Animator �������� �������
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

        // ��������� ��������, ��� ���������� ���������� �������
        StartCoroutine(TeleportPlayerAfterAnimation());
    }

    IEnumerator TeleportPlayerAfterAnimation()
    {
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
