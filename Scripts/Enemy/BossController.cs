using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class BossController : MonoBehaviour
{
    public GameObject lightAfterDeath;
    [Header("Налаштування боса")]
    public int maxHealth = 500;
    public int currentHealth;

    [Header("Фази")]
    public BossPhase currentPhase = BossPhase.Phase1;
    public enum BossPhase { Phase1, Phase2, Phase3 } 

    [Header("Точки телепортації")]
    public Transform[] teleportPoints;
    private Transform currentTargetPoint;

    [Header("Атаки")]
    public BossAttack attackScript;

    [Header("Телепортація")]
    public float teleportCooldown = 8f;
    private float teleportTimer;

    [Header("Анімація")]
    public Animator animator;

    private bool isAttacking = false;
    private bool isDead = false;

    [Header("Гравець")]
    public Transform player;
    public float aggroRange = 10f;
    private bool hasStarted = false;


    void Start()
    {
        currentHealth = maxHealth;
        teleportTimer = teleportCooldown;

        if (attackScript == null)
            attackScript = GetComponent<BossAttack>();
    }

    void Update()
    {
        if (!hasStarted)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= aggroRange)
            {
                hasStarted = true;
                Debug.Log("⚔️ Бій з босом почався!");
            }
            else return;
        }

        if (isDead) return;

        UpdatePhase();

        teleportTimer -= Time.deltaTime;
        if (teleportTimer <= 0f)
        {
            Teleport();
            teleportTimer = GetTeleportCooldownForPhase();
        }

        if (!isAttacking)
        {
            StartCoroutine(AttackCycle());
        }
        FacePlayer();
    }

    void UpdatePhase()
    {
        float hpPercent = (float)currentHealth / maxHealth;

        if (hpPercent <= 0.2f && currentPhase != BossPhase.Phase3)
        {
            currentPhase = BossPhase.Phase3;
            animator.speed = 1.5f; 
        }
        else if (hpPercent <= 0.6f && currentPhase == BossPhase.Phase1)
        {
            currentPhase = BossPhase.Phase2;
        }
    }

    IEnumerator AttackCycle()
    {
        isAttacking = true;

        yield return new WaitForSeconds(1f); 

        switch (currentPhase)
        {
            case BossPhase.Phase1:
                animator.SetTrigger("attack2Trigger");
                animator.SetTrigger("attack2Trigger");
                break;

            case BossPhase.Phase2:
                animator.SetTrigger("attack1Trigger");

                break;

            case BossPhase.Phase3:
                int pattern = Random.Range(0, 2); 

                if (pattern == 0)
                {
                    animator.SetTrigger("attack1Trigger");

                }
                else
                {
                    animator.SetTrigger("attack2Trigger");

                }

                yield return new WaitForSeconds(0.8f);

                if (Random.value < 0.3f)
                {
                    Teleport();
                    yield return new WaitForSeconds(0.5f); 
                }

                if (pattern == 0)
                {
                    animator.SetTrigger("attack2Trigger");
                }
                else
                {
                    animator.SetTrigger("attack1Trigger");

                }
                break;


        }

        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }

    void Teleport()
    {
        int index = Random.Range(0, teleportPoints.Length);
        currentTargetPoint = teleportPoints[index];
        transform.position = currentTargetPoint.position;
        Debug.Log("📍 Телепорт на точку: " + currentTargetPoint.name);
    }

    float GetTeleportCooldownForPhase()
    {
        switch (currentPhase)
        {
            case BossPhase.Phase1: return 8f;
            case BossPhase.Phase2: return 6f;
            case BossPhase.Phase3: return 4f;
            default: return 8f;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // 🔥 Програвання анімації отримання урону
        animator.SetTrigger("hurtTrigger");

        if (currentHealth <= 0)
            Die();
    }


    void Die()
    {
        isDead = true;
        StopAllCoroutines();

        animator.SetBool("isDead", true);

        if (lightAfterDeath != null)
        {
            lightAfterDeath.SetActive(true); // Вмикає світло або будь-який об’єкт
            Debug.Log("💡 Об'єкт світла активовано");
        }
        else
        {
            Debug.LogWarning("⚠️ Об'єкт світла не призначений у інспекторі");
        }

        Destroy(gameObject, 2f);
    }

    void FacePlayer()
    {
        if (player != null)
        {
            Vector3 scale = transform.localScale;
            scale.x = player.position.x < transform.position.x ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

}
