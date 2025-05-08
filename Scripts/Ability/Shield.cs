using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // ❌ Блокуємо атаку і знищуємо снаряд (якщо потрібно)
            if (collision.GetComponent<LightShot>() != null)
                Destroy(collision.gameObject);

            // Або можна відштовхнути, показати ефект тощо
            Debug.Log("🛡 Щит заблокував атаку!");
        }
    }
}
