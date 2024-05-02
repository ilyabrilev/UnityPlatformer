using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float vertivalSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxColider;
    private float wallJumpCooldown;
    private float horizontalInput;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        this.body = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.boxColider= GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //flip player when moving left and right
        if (horizontalInput > 0.01f)
        {
            this.transform.localScale = Vector2.one;
        }
        else if (horizontalInput < -0.01f)
        {
            this.transform.localScale = new Vector2(-1,1);
        }

        //Set animator parameters
        this.anim.SetBool("Run", horizontalInput != 0);
        this.anim.SetBool("Grounded", this.isGrounded());

        if (wallJumpCooldown > 0.2f)
        {

            this.body.velocity = new Vector2(horizontalInput * horizontalSpeed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                this.body.gravityScale = 0;
                this.body.velocity = Vector2.zero;
            } else
            {
                this.body.gravityScale = 7f;
            }

            //jump
            if (Input.GetKey(KeyCode.Space))
            {
                this.Jump();
            }
        }
        else
        {
            this.wallJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (this.isGrounded())
        {
            this.body.velocity = new Vector2(this.body.velocity.x, vertivalSpeed);
            this.anim.SetTrigger("Jump");
        } else if (onWall() && !isGrounded()) {

            if (horizontalInput == 0)
            {
                float pushBackPower = 10;
                float wallJumpPower = 0;
                //push back and up
                this.body.velocity = new Vector2(-Mathf.Sign(this.transform.localScale.x) * pushBackPower, wallJumpPower);
                this.transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), transform.localScale.y);
            } else
            {
                float pushBackPower = 3;
                float wallJumpPower = 6;
                //push back and up
                this.body.velocity = new Vector2(-Mathf.Sign(this.transform.localScale.x) * pushBackPower, wallJumpPower);
            }

            this.wallJumpCooldown = 0;

        }
        /*this.grounded = false;*/
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(this.boxColider.bounds.center, this.boxColider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(this.boxColider.bounds.center, this.boxColider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
