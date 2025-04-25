using SupanthaPaul;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float jumpForce = 5f;
    public float climbSpeed = 1f;

    private Rigidbody2D rb;
    private PlayerState playerState;
    private Collider2D currentStairs;

    private GameObject flashlight; // Об'єкт світла
    private bool hasFlashlight = false; // Чи активний ліхтар

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
      
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Оновлюємо стан бігу
        playerState.IsRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = playerState.IsRunning ? speed * 2f : speed;

        // Рух по землі
        if (!playerState.IsClimbing)
        {
            transform.Translate(horizontalInput * currentSpeed * Time.deltaTime * Vector2.right);
        }

        // Стрибок (лише якщо не на сходах)
        if (Input.GetButtonDown("Jump") && playerState.IsGrounded && !playerState.IsClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            playerState.IsGrounded = false;
        }

        // Підйом по сходах
        if (playerState.isOnStairs && verticalInput != 0)
        {
            playerState.IsClimbing = true;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, verticalInput * climbSpeed);
        }
        else if (playerState.IsClimbing)
        {
            playerState.IsClimbing = false;
            rb.gravityScale = 1;
        }

        // **Перемикання ліхтаря**
        if (Input.GetKeyDown(KeyCode.F) && playerState.HasLantern)
        {

            hasFlashlight = !hasFlashlight;

            if (hasFlashlight)
            {
                CreateFlashlight();
                playerState.Lanter = true;
            }
            else
            {
                DestroyFlashlight();
                playerState.Lanter = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            playerState.isOnStairs = true;
            currentStairs = other;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == currentStairs)
        {
            playerState.isOnStairs = false;
            currentStairs = null;
            playerState.IsClimbing = false;
            rb.gravityScale = 1;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            playerState.IsGrounded = true;
        }
    }

    void CreateFlashlight()
    {
        if (flashlight == null)
        {
            flashlight = new GameObject("Flashlight");
            Light2D light = flashlight.AddComponent<Light2D>(); // 2D світло
            light.lightType = Light2D.LightType.Point;
            light.pointLightOuterRadius = 3f; // Радіус світла
            light.intensity = 1.3f; // Яскравість
            light.color = Color.yellow; // Жовтуватий відтінок

            flashlight.transform.SetParent(transform);
            flashlight.transform.localPosition = new Vector3(0f, 0.0f, 0);
        }
    }

    void DestroyFlashlight()
    {
        if (flashlight != null)
        {
            Destroy(flashlight);
            flashlight = null;
        }
    }
}
