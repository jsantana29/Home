using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed;
    private float playerVelocity;
    public float terminalVelocity;
    public float jumpHeight;

    public bool grounded;
    public bool usedDoubleJump;

    public bool hangingOffWall;
    public bool canHang;
    public float wallDrag;
    public float kickBack;
    public float hangDelay;

    private float jumpTimer = 0.3f;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundCheckRadius;

    public Transform wallChecker;

    
    public LayerMask whatIsWall;
    public float wallCheckRadius;

    public float animSpeedThreshold;

    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        checkMove();
        checkJump();

        updateAnim();

    }



    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        hangingOffWall = Physics2D.OverlapCircle(wallChecker.position, wallCheckRadius, whatIsWall);

    }

    public void checkMove()
    {
        playerVelocity = playerSpeed * Input.GetAxisRaw("Horizontal");
       
            

            if (playerVelocity < 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + playerVelocity, GetComponent<Rigidbody2D>().velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            else if (playerVelocity > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + playerVelocity, GetComponent<Rigidbody2D>().velocity.y);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                //idle movement code here if necessary
            }

            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.magnitude) > terminalVelocity){
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x - playerVelocity, GetComponent<Rigidbody2D>().velocity.y);
            }
        
    }

    public void updateAnim()
    {
        if (GetComponent<Rigidbody2D>().velocity.x > animSpeedThreshold && transform.localScale.x == 1f)
        {

        anim.SetBool("isMoving", true);
        }
        else if (-GetComponent<Rigidbody2D>().velocity.x > animSpeedThreshold && transform.localScale.x == -1f)
        {

            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }


    }

    public void checkJump()
    {
        checkHanging();

        if (grounded || (hangingOffWall && canHang))
        {
            usedDoubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && (grounded || hangingOffWall))
        {
            //StartCoroutine("hasJumped");
            anim.SetTrigger("hasJumped");
            anim.SetBool("grounded", grounded);
            if (Input.GetButtonDown("Jump") && hangingOffWall)
            {
                if (transform.localScale.x == -1f)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(kickBack, GetComponent<Rigidbody2D>().velocity.y);
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    StartCoroutine("stopHanging");
                }
                else if (transform.localScale.x == 1f)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-kickBack, GetComponent<Rigidbody2D>().velocity.y);
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    StartCoroutine("stopHanging");
                }
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        }

        if (Input.GetButtonDown("Jump") && !usedDoubleJump && !grounded && !(hangingOffWall && canHang))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            usedDoubleJump = true;
        }
    }

    public void checkHanging()
    {
        if (hangingOffWall && canHang)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -wallDrag);
        }
    }

    public IEnumerator stopHanging()
    {
        canHang = false;
        //hangingOffWall = false;
        yield return new WaitForSeconds(hangDelay);
        canHang = true;

    }

    public IEnumerator hasJumped()
    {
        anim.SetTrigger("hasJumped");
        //hangingOffWall = false;
        yield return new WaitForSeconds(jumpTimer);
        //anim.SetBool("hasJumped", false);

    }


}