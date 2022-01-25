using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    public float Speed;
    public float jumpHeight, jumpForce;

    private bool facingRight = true;
    private bool onGround;

    private bool moving;
    private bool jumping;
    private bool withBook;
    private bool squatting;

    private float blinkTime;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = this.transform.Find("Sprite").gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(facingRight)
        {
            jDir = 1;
        }
        else
        {
            jDir = -1;
        }

        blinkTime += Time.deltaTime;
        if(blinkTime > 6f)
        {
            blinkTime = 0;
            anim.SetTrigger("Blink");
        }
        if(facingRight && moveInput < 0)
        {
            Flip();
        }
        else if(!facingRight && moveInput > 0)
        {
            Flip();
        }
        
        Jump();
        Squat();
        Book();
    }

    private void FixedUpdate()
    {
        FallSpeed();
        if(!jumping)
            Move();
    }

    private void Move()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);

        if(moveInput != 0)
        {
            if(!moving)
            {
                anim.SetTrigger("Move");
            }
            moving = true;
        }
        else
        {
            moving = false;
        }

        if(moving)
        {
            withBook = false;
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }

    public float jTime;
    private float jCount;
    private bool startJump;
    private float jDir;
    private Vector2 jumpVector;
    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            jumpVector = new Vector2(jDir* jumpForce, 1 * jumpHeight);
            jumping= true;

            StartCoroutine(startJumping());
        }
        if(startJump && jumping)
        {
            jCount += Time.deltaTime;

            rb.velocity = jumpVector;

            if(jCount > jTime)
            {
                // jumping = false;
                startJump = false;
            }
        }
    }

    private IEnumerator startJumping()
    {
        jCount = 0;

        yield return new WaitForSeconds(0.15f);

        startJump = true;
        anim.SetTrigger("Jump");
        anim.SetBool("Jumping", true);
    }

    private void Squat()
    {
        if(Input.GetKey(KeyCode.DownArrow) && !withBook && onGround)
        {
            if(!squatting)
            {
                anim.SetTrigger("Squat");
                anim.SetBool("Squatting", true);
            }
            squatting = true;
        }
        else
        {
            squatting = false;
            anim.SetBool("Squatting", false);
        }
    }

    private void Book()
    {
        if(Input.GetKeyDown(KeyCode.X) && !withBook && !squatting && onGround)
        {
            withBook = true;
            anim.SetTrigger("Pick_Book");
        }
        else if(Input.GetKeyDown(KeyCode.X) && withBook && !squatting && onGround)
        {
            withBook = false;
            anim.SetTrigger("Put_Book");
        }
    }

    public float fallMaxSpeed;
    private void FallSpeed()
    {
        if(rb.velocity.y < -fallMaxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -fallMaxSpeed);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            onGround = false;
            anim.SetBool("OnGround", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            jumping = false;
            anim.SetBool("OnGround", true);
            anim.SetBool("Jumping", false);
        }
    }
    
    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
