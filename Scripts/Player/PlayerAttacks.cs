using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public Collider2D attackPoint;
    public int attackDamage = 10;
    public LayerMask enemyLayers;
    public float comboResetTime = 1f;

    private Animator animator;
    private int comboStep = 0;
    private float comboTimer = 0f;
    public bool isAttacking = false;
    public GameObject bladePrefab;
    public Transform bladeSpawnPoint;
    public PlayerStats stats;
    private bool canFireBlade = true;
    public ParticleSystem shortAttackEffect;
    public Transform attackEffectSpawnPoint;
    [Header("Chrono Sphere Settings")]
    public GameObject chronoSpherePrefab;
    public GameObject chargeEffectPrefab;
    public float chargeTime = 2f;
    public int chronoManaCost = 15;

    private float holdTimer = 0f;
    private bool isChargingChrono = false;
    private GameObject chargeEffectInstance;
    private Vector3 chronoTargetPos;
    [Header("Shield Settings")]
    public GameObject shieldPrefab;
    public Transform shieldSpawnPoint;
    public int rationalShieldStartCost = 5;
    public int rationalShieldManaPerSecond = 1;
    public int intuitiveShieldCost = 50;
    public float intuitiveShieldDuration = 15f;

    private GameObject activeShield;
    private bool shieldActive = false;
    private float shieldTimer = 0f;
    private bool isHoldingShieldKey = false;
    private float manaTickTimer = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {

        comboTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= chargeTime && !isChargingChrono)
            {
                if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                    && stats.currentMana >= chronoManaCost)
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;
                    chronoTargetPos = mousePos;

                    animator.SetTrigger("chronoShield");
                    isChargingChrono = true;

                    chargeEffectInstance = Instantiate(chargeEffectPrefab, mousePos, Quaternion.identity);
                }
            }

            if (isChargingChrono && chargeEffectInstance != null)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                chargeEffectInstance.transform.position = mousePos;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!isChargingChrono && chargeEffectInstance != null)
            {
                Destroy(chargeEffectInstance);
            }

            isChargingChrono = false;
            holdTimer = 0f;
        }


        if (comboTimer <= 0f)
        {
            comboStep = 0;
        }

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (!isAttacking && Input.GetMouseButtonDown(0))
        {
            if (state.IsName("Idle") || state.IsName("Walk"))
            {
                if (stats.currentMana >= 3)
                {
                    comboStep = 1;
                    animator.SetTrigger("attackTrigger1");
                    isAttacking = true;
                    comboTimer = comboResetTime;

                    stats.UseMana(3);
                    stats.ResetManaRegenDelay();

                    if (shortAttackEffect != null && attackEffectSpawnPoint != null)
                    {
                        ParticleSystem effect = Instantiate(shortAttackEffect, attackEffectSpawnPoint.position, Quaternion.identity);
                        effect.Play();
                        Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
                    }
                }
            }


            else if (state.IsName("Attack1") && comboStep == 1)
            {
                if (stats.currentMana >= 3)
                {
                    comboStep = 2;
                    animator.SetTrigger("attackTrigger2");
                    isAttacking = true;
                    comboTimer = comboResetTime;

                    stats.UseMana(3);
                    stats.ResetManaRegenDelay();

                    if (shortAttackEffect != null && attackEffectSpawnPoint != null)
                    {
                        ParticleSystem effect = Instantiate(shortAttackEffect, attackEffectSpawnPoint.position, Quaternion.identity);
                        effect.Play();
                        Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
                    }
                }
            }
        }


        if (isAttacking && !state.IsName("Attack1") && !state.IsName("Attack2"))
        {
            isAttacking = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("timeBladeTrigger"); 
        }

        // Правий Ctrl натиснуто
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            TryActivateShield();

        }

        if (Input.GetKey(KeyCode.RightControl))
        {
            isHoldingShieldKey = true; // 🟢 Важливо!
        }

        // Якщо утримується (тільки для раціонального)
        // Якщо щит щойно активовано — затримка перед першим зняттям мани
        if (shieldActive && ChoiceTrigger.lastChoice == PlayerChoiceType.Rational && isHoldingShieldKey)
        {
            manaTickTimer += Time.deltaTime;

            if (manaTickTimer >= 1f)
            {
                if (stats.currentMana >= rationalShieldManaPerSecond)
                {
                    stats.UseMana(rationalShieldManaPerSecond);
                    manaTickTimer = 0f;
                }
                else
                {
                    DeactivateShield();
                }
            }
        }

        // Коли гравець відпускає клавішу
        if (Input.GetKeyUp(KeyCode.RightControl))
        {
            isHoldingShieldKey = false;
            if (ChoiceTrigger.lastChoice == PlayerChoiceType.Rational)
            {
                DeactivateShield();
            }
        }

        // Інтуїтивний щит: таймер
        if (shieldActive && ChoiceTrigger.lastChoice == PlayerChoiceType.Intuitive)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0f)
            {
                DeactivateShield();
            }
        }

    }

    void DealDamage()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(enemyLayers);
        Collider2D[] hits = new Collider2D[10];

        int count = Physics2D.OverlapCollider(attackPoint, filter, hits);
        Debug.Log("🗡 Удар: перевірка ворогів...");

        for (int i = 0; i < count; i++)
        {
            Collider2D hit = hits[i]; // 👈 додай цю строчку

            // Якщо звичайний ворог
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
                continue; // не потрібно перевіряти інше
            }

            // Якщо бос
            BossController boss = hit.GetComponent<BossController>();
            if (boss != null)
            {
                boss.TakeDamage(attackDamage);
            }
        }
    }

    void LaunchBlade()
    {
        if (!canFireBlade) return;

        int requiredMana = 0;

        // 🔎 Визначаємо потрібну кількість мани
        if (ChoiceTrigger.lastChoice == PlayerChoiceType.Rational)
            requiredMana = 5;
        else if (ChoiceTrigger.lastChoice == PlayerChoiceType.Intuitive)
            requiredMana = 10;

        // ❌ Якщо мани не вистачає — виходимо
        if (stats.currentMana < requiredMana)
        {
            Debug.Log("🚫 Недостатньо мани для запуску леза!");
            return;
        }

        // ✅ Знімаємо ману
        stats.UseMana(requiredMana);

        // 🎯 Створюємо та налаштовуємо лезо
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        GameObject blade = Instantiate(bladePrefab, bladeSpawnPoint.position, Quaternion.identity);
        BladeProjectile projectile = blade.GetComponent<BladeProjectile>();

        if (ChoiceTrigger.lastChoice == PlayerChoiceType.Rational)
        {
            projectile.SetSlowEffect(2f);
        }
        else if (ChoiceTrigger.lastChoice == PlayerChoiceType.Intuitive)
        {
            projectile.SetAOE(true);
        }

        projectile.Launch(mouseWorldPos);
    }



    public void ResetBladeCast()
    {
        canFireBlade = true; 
    }

    public void OnChronoChargeComplete()
    {
        if (stats.currentMana >= chronoManaCost)
        {
            Instantiate(chronoSpherePrefab, chronoTargetPos, Quaternion.identity);
            stats.UseMana(chronoManaCost);
            stats.ResetManaRegenDelay();
        }
        else
        {
            Debug.Log("❌ Не вистачає мани для Хроносфери");
        }

        if (chargeEffectInstance != null)
            Destroy(chargeEffectInstance);

        isChargingChrono = false;
        holdTimer = 0f;
    }

    void TryActivateShield()
    {
        if (shieldActive) return;
        stats.isShielded = true;
        if (ChoiceTrigger.lastChoice == PlayerChoiceType.Intuitive)
        {
            if (stats.currentMana >= intuitiveShieldCost)
            {
                stats.UseMana(intuitiveShieldCost);
                activeShield = Instantiate(shieldPrefab, shieldSpawnPoint.position, Quaternion.identity, transform);
                shieldActive = true;
                shieldTimer = intuitiveShieldDuration;

                animator.SetTrigger("Protection"); 
            }
            else
            {
                Debug.Log("❌ Не вистачає мани для інтуїтивного щита");
            }
        }
        else if (ChoiceTrigger.lastChoice == PlayerChoiceType.Rational)
        {
            if (stats.currentMana >= rationalShieldStartCost)
            {
                stats.UseMana(rationalShieldStartCost);
                activeShield = Instantiate(shieldPrefab, shieldSpawnPoint.position, Quaternion.identity);
                shieldActive = true;
                manaTickTimer = 0f;

                animator.SetTrigger("Protection"); 

                GetComponent<PlayerController>().enabled = false; // якщо блокуєш рух
            }
            else
            {
                Debug.Log("❌ Не вистачає мани для раціонального щита");
            }
        }
    }


    void DeactivateShield()
    {
        if (activeShield != null)
        {
            Destroy(activeShield);
        }

        shieldActive = false;
        stats.isShielded = false;
        GetComponent<PlayerController>().enabled = true;
        animator.ResetTrigger("Protection");
    }

}
