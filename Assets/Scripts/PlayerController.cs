using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    //Component references
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    //Movement Variables
    public float speed = 5.0f;
    public float jumpForce = 300.0f;

    //Ground Check
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius = 0.02f;

    //Jump Check
    public bool isFalling;
    Coroutine jumpForceChange = null;

    //Size Increase
    Coroutine sizeChange = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (speed <= 0) speed = 5.0f;
        if (jumpForce <= 0) jumpForce = 300.0f;
        if (groundCheckRadius <= 0) groundCheckRadius = 0.02f;

        if (!groundCheck) groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        isFalling = rb.velocity.y < 0 ? true : false;

        if (isGrounded) rb.gravityScale = 1;

        if (curPlayingClips.Length > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && curPlayingClips[0].clip.name != "Player_Attack")
            {
                anim.SetTrigger("attack1");
            }
            else if (curPlayingClips[0].clip.name == "Player_Attack")
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                Vector2 mDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = mDirection;
            }
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Vector2 jumpDirection = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = jumpDirection;
        }

        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        if (hInput != 0) sr.flipX = (hInput < 0);

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFalling", isFalling);
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetTrigger("attack1");
        }

        if (!isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetTrigger("attack2");
        }
    }

    public void IncreaseGravity()
    {
        rb.gravityScale = 10;
    }

    public void StartJumpForceChange()
    {
        Debug.Log("Power up has been collected");
        if (jumpForceChange == null)
        {
            jumpForceChange = StartCoroutine(JumpForceChange());
            return;
        }
        //Restart process if pickups were collected while 'still under the influence'
        StopCoroutine(jumpForceChange);
        jumpForceChange = null;
        jumpForce /= 2;

        StartJumpForceChange();
    }

    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;

        yield return new WaitForSeconds(5.0f);

        jumpForce /= 2;
        jumpForceChange = null;
    }

    public void StartSizeChange()
    {
        Debug.Log("Power up has been collected");
        if (sizeChange == null)
        {
            sizeChange = StartCoroutine(SizeChange());
            return;
        }

        StopCoroutine(sizeChange);
        sizeChange = null;
        gameObject.transform.localScale /= 2;

        StartSizeChange();
    }

    IEnumerator SizeChange()
    {
        gameObject.transform.localScale *= 2;

        yield return new WaitForSeconds(4.0f);
        gameObject.transform.localScale /= 2;
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.localScale *= 2;
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.localScale /= 2;
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.localScale *= 2;
        yield return new WaitForSeconds(0.5f);

        gameObject.transform.localScale /= 2;
        sizeChange = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Squish"))
        {
            EnemyWalker enemy = collision.gameObject.transform.parent.GetComponent<EnemyWalker>();
            enemy.Squish();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2f);
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }*/
}
