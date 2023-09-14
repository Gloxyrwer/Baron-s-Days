using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerShooting playerShoot;
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource jumpSound;

    [Header("Move Info")]
    public float speed = 10f;
    public float jumpForce = 18f;
    [HideInInspector] public int jumpCount = 0;

    [HideInInspector] public bool canMove = true;

    private float movingInput;
    private bool jumpInput;
    private bool jumpInputReleased;

    private bool facingRight = true;
    private float facingDirection = 1;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    private float timeToAfk = 10f;
    private float timer;

    private void Awake()
    {
        playerShoot = GetComponent<PlayerShooting>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        canMove = true;
        speed = 10f;
        jumpForce = 18f;
        jumpCount = 0;
    }

    private void Update()
    {
        if (canMove)
        {
            CollisionCheck();
            FlipController();
            AnimatorController();
            CheckInput();

            if (isGrounded)
            {
                jumpCount = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CheckInput()
    {
        movingInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        jumpInputReleased = Input.GetKeyUp(KeyCode.Space);

        JumpButton();
    }

    private void JumpButton()
    {
        if (isGrounded)
        {
            Jump();
            if (jumpInputReleased)
                jumpCount = 1;
        }
        else if (jumpCount < 2)
        {
            Jump();
            if (jumpInputReleased)
                jumpCount += 1;
        }
    }

    private void Jump()
    {
        if (jumpInput)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSound.Play();
        }

        if (jumpInputReleased && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(rb.transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void AnimatorController()
    {
        bool isMoving = rb.velocity.x != 0;
        bool isJumping = rb.velocity.y != 0;

        AfkAnimation(isMoving, isJumping, playerShoot.canShoot);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void Move()
    {
        rb.velocity = new Vector2(movingInput * speed, rb.velocity.y);
    }

    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void AfkAnimation(bool isMoving, bool isJumping, bool playerCanShoot)
    {
        if (!isMoving && !isJumping && playerCanShoot)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAfk)
            {
                anim.SetBool("isAfk", true);
            }
        }
        else
        {
            timer = 0f;
            anim.SetBool("isAfk", false);
        }
    }
}
