using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 5f;
    public float moveSpeed = 2f;
    private float originalSpeed;

    [Header("Attack Settings")]
    public Transform attackPoint;
    public float attackCooldown = 2f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isPlayerInAttackRange = false;
    private bool isAttacking = false;
    private float attackCooldownTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalSpeed = moveSpeed;
    }

    private void Update()
    {
        if (player == null) return;

        attackCooldownTimer -= Time.deltaTime;

        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (isPlayerInAttackRange && attackCooldownTimer <= 0f && !isAttacking)
        {
            StartAttack();
        }
        else
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= detectionRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

                animator.SetBool("isWalking", true);
                FaceDirection(direction.x);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.SetBool("isWalking", false);
            }
        }
    }

    private void FaceDirection(float directionX)
    {
        GetComponent<SpriteRenderer>().flipX = directionX > 0;
    }

    private void StartAttack()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("isWalking", false);
        animator.SetTrigger("attackTrigger"); 
    }

    public void CheckAttackHit()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, 0.5f);

        foreach (var hit in hitPlayers)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<PlayerStats>()?.TakeDamage(5);
            }
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
        attackCooldownTimer = attackCooldown; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerInAttackRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInAttackRange = false;
            if (isAttacking)
            {
                isAttacking = false;
                animator.ResetTrigger("attackTrigger");
                attackCooldownTimer = attackCooldown;
            }
        }
    }

    public void SlowDown(float factor)
    {
        moveSpeed *= factor;
    }

    public void RestoreSpeed(float factor)
    {
        moveSpeed /= factor;
    }

}
