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
    public PlayerStats stats;
    private GameObject flashlight; // Об'єкт світла
    private bool hasFlashlight = false;
    public PlayerAttacks playerAttacks;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
        playerAttacks = GetComponent<PlayerAttacks>();
     
    }

    void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isMoving = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f;
        bool isIdle = !isMoving;
        bool isAttacking = playerAttacks != null && playerAttacks.isAttacking;

        if (stats != null)
        {
            stats.isRegenerating = !isRunning && !isAttacking && (isIdle || isMoving);
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerState.IsRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = playerState.IsRunning ? speed * 2f : speed;


        if (!playerState.IsClimbing)
        {
            transform.Translate(horizontalInput * currentSpeed * Time.deltaTime * Vector2.right);
        }

        if (Input.GetButtonDown("Jump") && playerState.IsGrounded && !playerState.IsClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            playerState.IsGrounded = false;
        }

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
            Light2D light = flashlight.AddComponent<Light2D>(); 
            light.lightType = Light2D.LightType.Point;
            light.pointLightOuterRadius = 3f;
            light.intensity = 1.3f; 
            light.color = Color.yellow; 

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
