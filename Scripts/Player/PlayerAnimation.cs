using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerState playerState;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float movementSpeed = Mathf.Abs(horizontalInput);

        // Ходьба/Біг
        animator.SetFloat("Moving", movementSpeed);
        animator.SetBool("IsRunning", playerState.IsRunning);

        // Обертання вліво/вправо
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        // Стрибок
        animator.SetBool("IsJumping", rb.velocity.y > 0);

        // Падіння
        animator.SetBool("IsFalling", !playerState.IsGrounded && rb.velocity.y < 0);

        // Підйом по сходах
        if (playerState.IsClimbing && Mathf.Abs(verticalInput) > 0)
        {
            animator.SetBool("IsClimbing", true);
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsClimbing", false);
        }

        if (playerState.Lanter) { animator.SetBool("HasLantern", true);}

        else { animator.SetBool("HasLantern", false); }
    }

   
}
