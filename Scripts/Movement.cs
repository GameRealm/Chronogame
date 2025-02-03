using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float jumpForce = 5f;
    public float climbSpeed = 1f;
    private Rigidbody2D rb;
    private PlayerState playerState;
    public Transform groundCheck;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ��������� ���� ���
        playerState.IsRunning = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = playerState.IsRunning ? speed * 1.5f : speed;

        // ��� ������
        transform.Translate(horizontalInput * currentSpeed * Time.deltaTime * Vector2.right);

        // �������
        if (Input.GetButtonDown("Jump") && playerState.IsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            playerState.IsGrounded = false;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ����������, �� ��'���, � ���� �������� ��������, �������� �� ���� "Ground"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            playerState.IsGrounded = true;
        }
    }

}