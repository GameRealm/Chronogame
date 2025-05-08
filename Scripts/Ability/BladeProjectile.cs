using UnityEngine;
using System.Collections;
public class BladeProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 15;
    public float lifetime = 2f;

    private Vector2 direction;
    private bool aoe = false;
    private float slowTime = 0f;

    public void Launch(Vector2 targetPos)
    {
        Vector2 start = transform.position;
        direction = (targetPos - start).normalized;
        transform.right = direction;
        Destroy(gameObject, lifetime);
    }

    public void SetAOE(bool enabled) => aoe = enabled;
    public void SetSlowEffect(float time) => slowTime = time;

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHealth>();
            var follow = other.GetComponent<EnemyFollow>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                if (slowTime > 0 && follow != null)
                {
                    follow.SlowDown(0.5f); // Можна винести множник як поле
                    StartCoroutine(RestoreAfterDelay(follow, 0.5f, slowTime));

                }

                if (aoe)
                {
                    CreateAOE(transform.position);
                }
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator RestoreAfterDelay(EnemyFollow enemy, float factor, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (enemy != null)
            enemy.RestoreSpeed(factor);
    }


    private void CreateAOE(Vector2 position)
    {
        // Тут можна створити ефект або логіку вибуху — для простоти:
        Collider2D[] enemies = Physics2D.OverlapCircleAll(position, 1.5f);

        foreach (var col in enemies)
        {
            if (col.CompareTag("Enemy"))
            {
                var enemy = col.GetComponent<EnemyHealth>();
                if (enemy != null)
                    enemy.TakeDamage(damage); // Може викликати урон по області
            }
        }

        // Можеш додати візуальний ефект вибуху
    }

    // 🔵 Для тесту в інспекторі
    private void OnDrawGizmosSelected()
    {
        if (aoe)
            Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
