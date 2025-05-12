using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Mana Settings")]
    public int maxMana = 100;
    public int currentMana;

    [Header("UI Elements")]
    public Image healthBar; 
    public Image manaBar;
    [Header("Mana Regen")]
    public float manaRegenRate = 5f;
    public float regenDelay = 2f;
    private float regenTimer = 0f;
    public bool isRegenerating = false;
    private float manaRegenBuffer = 0f;

    public bool isShielded = false;
    private bool isDead = false;
    public GameObject panel;
    private void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;

        UpdateUI();
    }

    private float healTimer = 0f;
    public float healRate = 0.2f; 

    private void Update()
    {
        isRegenerating = true;

        // Відновлення мани (як і було)
        if (regenTimer > 0f)
            regenTimer -= Time.deltaTime;

        if (isRegenerating && regenTimer <= 0f && currentMana < maxMana)
        {
            manaRegenBuffer += manaRegenRate * Time.deltaTime;

            if (manaRegenBuffer >= 1f)
            {
                int manaToAdd = Mathf.FloorToInt(manaRegenBuffer);
                currentMana += manaToAdd;
                manaRegenBuffer -= manaToAdd;
                currentMana = Mathf.Clamp(currentMana, 0, maxMana);
                UpdateUI();
            }
        }

        if (Input.GetKey(KeyCode.R) && currentMana >= 3 && currentHealth < maxHealth)
        {
            healTimer += Time.deltaTime;
            if (healTimer >= healRate)
            {
                UseMana(3);
                RestoreHealth(1);
                healTimer = 0f;
            }
        }
        else
        {
            healTimer = 0f;
        }
    }


    private void UpdateUI()
    {
        if (healthBar != null)
            healthBar.fillAmount = (float)currentHealth / maxHealth;

        if (manaBar != null)
            manaBar.fillAmount = (float)currentMana / maxMana;
    }

    public void TakeDamage(int damage)
    {
        if (isShielded)
        {
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();

        GetComponent<Animator>()?.SetTrigger("HurtTrigger");
        if (currentHealth <= 0)
            Die();
    }

    public void UseMana(int amount)
    {
        currentMana -= amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        UpdateUI();
    }

    public void RestoreMana(int amount)
    {
        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);

        UpdateUI();
    }

    public void ResetManaRegenDelay()
    {
        regenTimer = regenDelay;
    }

     private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<PlayerController>().enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        Time.timeScale = 0f;

        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        StartCoroutine(HandleDeathSequence_Unscaled());
    }

    private IEnumerator HandleDeathSequence_Unscaled()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        Animator animator = GetComponent<Animator>();
        if (animator != null)
            animator.enabled = false;

        yield return new WaitForSecondsRealtime(0.5f);
        panel.SetActive(true);
    }
    private void RestoreHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();
    }
}
