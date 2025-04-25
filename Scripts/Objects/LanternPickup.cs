using UnityEngine;

public class LanternPickup : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private PlayerState playerState;

    void Start()
    {
        playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerState.HasLantern = true; // Гравець тепер має ліхтар
            Destroy(gameObject); // Видаляємо ліхтар зі сцени
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
