using UnityEngine;

public class ChronoSphere : MonoBehaviour
{
    public float slowDownFactor = 0.1f;
    public float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyFollow enemy = other.GetComponent<EnemyFollow>();
        if (enemy != null)
        {
            enemy.SlowDown(slowDownFactor);
        }

        LightShot lightShot = other.GetComponent<LightShot>();
        if (lightShot != null)
        {
            lightShot.SlowDown(slowDownFactor);
          
        }

        GhostAI ghost = other.GetComponent<GhostAI>();
        if (ghost != null)
        {
            ghost.SlowDown(slowDownFactor);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyFollow enemy = other.GetComponent<EnemyFollow>();
        if (enemy != null)
        {
            enemy.RestoreSpeed(slowDownFactor);
        }

        LightShot lightShot = other.GetComponent<LightShot>();
        if (lightShot != null)
        {
            lightShot.RestoreSpeed(slowDownFactor);
        }

        GhostAI ghost = other.GetComponent<GhostAI>();
        if (ghost != null)
        {
            ghost.RestoreSpeed(slowDownFactor);
        }
    }
}
