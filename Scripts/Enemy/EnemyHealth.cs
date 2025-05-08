using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public enum EnemyType { Default, Ghost, Boss } 
    public EnemyType enemyType = EnemyType.Default;

    public int maxHealth = 20;
    public int currentHealth;

    private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth > 0)
        {
            animator.SetTrigger("hurtTrigger");
        }
        else
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        switch (enemyType)
        {
            case EnemyType.Ghost:
                animator.SetTrigger("ghostDeath"); // <- зроби тригер в аніматорі
                break;
            case EnemyType.Boss:
                animator.SetTrigger("bossDeath"); // <- тригер для босса
                break;
            default:
                animator.SetBool("isDead", true); // для звичайних ворогів
                break;
        }

        // Вимкнути рух
        EnemyFollow follow = GetComponent<EnemyFollow>();
        if (follow != null)
            follow.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;

        Destroy(gameObject, 0.9f);
    }
}
