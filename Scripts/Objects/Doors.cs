using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour
{
    public Transform teleportTarget; // ƒруг≥ двер≥ (куди телепортувати)
    private bool isPlayerInDoor = false;
    private Animator animator;

    void Start()
    {
        // ѕерев≥р€Їмо, чи Ї компонент Animator на об'Їкт≥
        animator = GetComponentInChildren<Animator>(); // «находимо Animator всередин≥ префаба
        if (animator == null)
        {
            Debug.LogError("Animator не знайдений на об'Їкт≥!");
        }
    }

    void Update()
    {
        if (isPlayerInDoor && Input.GetKeyDown(KeyCode.W)) // якщо персонаж у двер€х ≥ натиснув "¬гору"
        {
            OpenDoor(); // «апускаЇмо ан≥мац≥ю в≥дкриванн€
        }
    }

    void OpenDoor()
    {
        if (animator != null)
        {
            animator.SetTrigger("Open"); // «апускаЇмо ан≥мац≥ю
        }
        else
        {
            Debug.LogError("Animator не ≥н≥ц≥ал≥зовано. ѕереконайтес€, що компонент Animator доданий до об'Їкта.");
            return; // якщо ан≥матор не знайдено, не виконуЇмо подальш≥ д≥њ
        }

        // «апускаЇмо корутину, щоб дочекатись завершенн€ ан≥мац≥њ
        StartCoroutine(TeleportPlayerAfterAnimation());
    }

    IEnumerator TeleportPlayerAfterAnimation()
    {
        if (animator == null)
        {
            yield break; // якщо ан≥матор все ж не ≥н≥ц≥ал≥зовано, корутина н≥чого не робить
        }

        // „екаЇмо поки ан≥мац≥€ не завершитьс€
        float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationDuration); // „екаЇмо час ан≥мац≥њ

        // ѕ≥сл€ того, €к ан≥мац≥€ завершилась, телепортуЇмо гравц€
        TeleportPlayer();
    }

    void TeleportPlayer()
    {
        if (teleportTarget != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player"); // «находимо гравц€
            if (player != null)
            {
                player.transform.position = teleportTarget.position; // ѕерем≥щаЇмо його до зв'€заних дверей
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // якщо персонаж ув≥йшов у двер≥
        {
            isPlayerInDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // якщо персонаж вийшов ≥з дверей
        {
            isPlayerInDoor = false;
        }
    }
}
