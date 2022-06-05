using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask wallLayer;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpPower;
    private float wallJumpCooldown; 

    // init the fields from the components in unity
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip the player direction when moving left or right
        if (horizontalInput > 0.01f) // right
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f) // left
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // interact with the run variable which controlls the animations
        // if horizontalInput = 0 this means that the player is not moving
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsPlayerGrounded());

        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (IsNextToWall() && !IsPlayerGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 3;
            }

            if (Input.GetKey(KeyCode.Space) && IsPlayerGrounded())
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
        }
    }

    private void Jump()
    {
        if (IsPlayerGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (IsNextToWall() && !IsPlayerGrounded())
        {
            wallJumpCooldown = 0;
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
        }


    }

    private bool IsPlayerGrounded()
        => Physics2D.BoxCast(boxCollider.bounds.center,
                             boxCollider.bounds.size,
                             0,
                             Vector2.down,
                             0.1f,
                             groundLayer).collider != null;

    private bool IsNextToWall()
        => Physics2D.BoxCast(boxCollider.bounds.center,
                             boxCollider.bounds.size,
                             0,
                             new Vector2(transform.localScale.x, 0),
                             0.1f,
                             wallLayer).collider != null;
}