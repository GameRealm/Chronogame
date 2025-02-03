using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour
{
    public Transform teleportTarget; // Другі двері (куди телепортувати)
    private bool isPlayerInDoor = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>(); // Знаходимо Animator всередині префаба
    }

    void Update()
    {
        if (isPlayerInDoor && Input.GetKeyDown(KeyCode.W)) // Якщо персонаж у дверях і натиснув "Вгору"
        {
            OpenDoor(); // Запускаємо анімацію відкривання
        }
    }

    void OpenDoor()
    {
        if (animator != null)
        {
            animator.SetTrigger("Open"); // Запускаємо анімацію
        }

        // Запускаємо корутину, щоб дочекатись завершення анімації
        StartCoroutine(TeleportPlayerAfterAnimation());
    }

    IEnumerator TeleportPlayerAfterAnimation()
    {
        // Чекаємо поки анімація не завершиться
        float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationDuration); // Чекаємо час анімації

        // Після того, як анімація завершилась, телепортуємо гравця
        TeleportPlayer();
    }

    void TeleportPlayer()
    {
        if (teleportTarget != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player"); // Знаходимо гравця
            if (player != null)
            {
                player.transform.position = teleportTarget.position; // Переміщаємо його до зв'язаних дверей
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Якщо персонаж увійшов у двері
        {
            isPlayerInDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Якщо персонаж вийшов із дверей
        {
            isPlayerInDoor = false;
        }
    }
}
