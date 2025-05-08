using System.Collections;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 4f;
    public float moveSpeed = 2f;
    private float originalSpeed;

    public GameObject lightShotPrefab;
    public Transform firePoint;

    public int numberOfShots = 5;
    public float timeBetweenShots = 0.5f;
    public int damagePerShot = 10;

    private Animator animator;
    private Rigidbody2D rb;
    private EnemyHealth ghostHealth;

    private bool isAttacking = false;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ghostHealth = GetComponent<EnemyHealth>();
        originalSpeed = moveSpeed;
    }

    private void Update()
    {
        if (ghostHealth.currentHealth <= 0 || isAttacking)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            if (distance > attackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                rb.velocity = Vector2.zero;
                animator.SetTrigger("attackTrigger"); 
                isAttacking = true; 
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void Shoot()
    {
        GameObject shot = Instantiate(lightShotPrefab, firePoint.position, Quaternion.identity);
        if (shot == null)
        {
            Debug.LogError("❌ Не вдалося створити префаб світлового залпу!");
            return;
        }

        LightShot projectile = shot.GetComponent<LightShot>();
        if (projectile == null)
        {
            Debug.LogError("❌ LightShot компонент не знайдений на префабі!");
            return;
        }

        projectile.SetTarget(player.position);
        projectile.damage = damagePerShot;
    }


    public void EndAttack()
    {
        isAttacking = false; 
    }

    public void SlowDown(float factor)
    {
        moveSpeed *= factor;
        Debug.Log($"🌀 GhostAI: Швидкість зменшена до {moveSpeed}");
    }

    public void RestoreSpeed(float factor)
    {
        moveSpeed /= factor;
        Debug.Log($"🌀 GhostAI: Швидкість відновлена до {moveSpeed}");
    }

}
