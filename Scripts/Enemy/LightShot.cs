using UnityEngine;

public class LightShot : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;

    private Vector2 direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("❌ Rigidbody2D не знайдено на LightShot!");
        }
    }

    public void SetTarget(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized;
        if (rb != null)
        {
            rb.velocity = direction * speed;
        }
    }

    public void SlowDown(float factor)
    {
        if (rb != null)
        {
            speed *= factor;
            rb.velocity = direction * speed;
            Debug.Log("🌀 LightShot уповільнено");
        }
    }

    public void RestoreSpeed(float factor)
    {
        if (rb != null)
        {
            speed /= factor;
            rb.velocity = direction * speed;
            Debug.Log("⚡ LightShot відновив швидкість");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats stats = collision.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
 
        if (collision.CompareTag("Shield"))
        {
            Destroy(gameObject);
            return;
        }
    }


}
