using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    [Header("Coyote time")]
    [SerializeField] private float coyoteTime; // how much time the player can be in the air and still make a jump
    private float coyoteCounter;

    [Header("Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;
    [SerializeField] private float wallJumpX; // horizontal jump force
    [SerializeField] private float wallJumpY; // vertical jump force

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header ("Audio")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxColider;
    //private float wallJumpCooldown;
    private float horizontalInput;

    private float initialGravity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxColider = GetComponent<BoxCollider2D>();
        initialGravity = body.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //flip player when moving left and right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector2.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector2(-1,1);
        }

        //Set animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", isGrounded());

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1)) {
            Jump();
        }

        //Adjust jump height
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(1)) && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        }

        if (onWall())
        {
            //stop if on wall
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        } else
        {
            //move normally if not on wall
            body.gravityScale = initialGravity;
            body.velocity = new Vector2(horizontalInput * horizontalSpeed, body.velocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime; // reset coyote counter
                jumpCounter = extraJumps;
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }
        }
    }

    private void Jump()
    {
        //if coyote timer is 0 or less, don't do the jump
        if (coyoteCounter < 0 && !onWall() && jumpCounter <= 0)
        {
            return;
        }

        SFXManager.instance.PlaySound(jumpSound);

        if (onWall())
        {
            WallJump();
        } else
        {
            if (isGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, verticalSpeed);
            } else
            {
                // coyote jump
                if (coyoteCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, verticalSpeed);
                } else
                {
                    if (jumpCounter > 0)
                    {
                        body.velocity = new Vector2(body.velocity.x, verticalSpeed);
                        jumpCounter--;
                    }
                }
            }

            coyoteCounter = 0;
        }
    }

    private void WallJump()
    {
        //ToDo: I don't like how it feels
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        //wallJumpCooldown = 0;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxColider.bounds.center, boxColider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxColider.bounds.center, boxColider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);

        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
