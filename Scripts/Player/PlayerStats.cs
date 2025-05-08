using UnityEngine;
using UnityEngine.UI;

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
    private void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;

        UpdateUI();
    }

    private void Update()
    {
        isRegenerating = true;

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
                Debug.Log("✅ Мана регенерується на: " + manaToAdd);
                UpdateUI();
            }
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
            Debug.Log("🛡 Гравець захищений щитом. Урон заблоковано.");
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
        Debug.Log("💀 Гравець помер!");
    }
}
