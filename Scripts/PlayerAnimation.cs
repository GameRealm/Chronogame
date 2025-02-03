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
        float movementSpeed = Mathf.Abs(horizontalInput);

        // ���� �� ������, ��������� ������� ������� ������
        if (playerState.IsOnStairs)
        {
            animator.SetFloat("Moving", 1f); // ��������� �������� ������� ������
            animator.SetBool("IsRunning", false); // �������� ��, ������� �� ������
        }
        else
        {
            // �������� ���� ��� ������
            animator.SetFloat("Moving", movementSpeed);
            animator.SetBool("IsRunning", playerState.IsRunning);
        }

        // ���������� �������
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        // �������
        if (rb.velocity.y > 0)
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }

        // ������
        if (!playerState.IsGrounded && rb.velocity.y < 0)
        {
            animator.SetBool("IsFalling", true);
        }
        else if (playerState.IsGrounded)
        {
            animator.SetBool("IsFalling", false);
        }
    }
}
