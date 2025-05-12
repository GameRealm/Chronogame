using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject meleeHitbox;
    public GameObject rangedWaveCollider;

    public int damage = 10;

    public void MeleeAttack()
    {
        ApplyDamageInHitbox(meleeHitbox, "Melee");
    }

    public void RangedAttack()
    {
        ApplyDamageInHitbox(rangedWaveCollider, "Ranged");
    }

    private void ApplyDamageInHitbox(GameObject hitbox, string type)
    {
        Collider2D col = hitbox.GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogWarning($"⚠️ {type} hitbox не має Collider2D!");
            return;
        }

        Collider2D[] hits = Physics2D.OverlapBoxAll(col.bounds.center, col.bounds.size, 0f);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerStats player = hit.GetComponent<PlayerStats>();
                if (player != null)
                {
                    Debug.Log($"💢 {type} атака влучила у гравця");
                    player.TakeDamage(damage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (meleeHitbox != null)
        {
            Collider2D col = meleeHitbox.GetComponent<Collider2D>();
            if (col != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
            }
        }

        if (rangedWaveCollider != null)
        {
            Collider2D col = rangedWaveCollider.GetComponent<Collider2D>();
            if (col != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
            }
        }
    }
}
