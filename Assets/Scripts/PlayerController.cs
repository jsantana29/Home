using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float playerSpeed;
    public float playerVelocity;
    public float jumpHeight;

    public bool grounded;
    public bool usedDoubleJump;

    public bool hangingOffWall;
    public bool canHang;
    public float wallDrag;
    public float kickBack;
    public float hangDelay;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundCheckRadius;

    public Transform wallChecker;
    public LayerMask whatIsWall;
    public float wallCheckRadius;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        checkMove();
        checkJump();
        
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
            GetComponent<Rigidbody2D>().velocity = new Vector2(playerVelocity, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (playerVelocity > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(playerVelocity, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void checkJump()
    {
        checkHanging();

        if (grounded || hangingOffWall)
        {
            usedDoubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && (grounded || hangingOffWall)) 
        {
            if (Input.GetButtonDown("Jump") && hangingOffWall)
            {
                if (transform.localScale.x == -1f)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(kickBack, GetComponent<Rigidbody2D>().velocity.y);
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    StartCoroutine("stopHanging");
                }
                else if(transform.localScale.x == 1f)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-kickBack, GetComponent<Rigidbody2D>().velocity.y);
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    StartCoroutine("stopHanging");
                }
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        } 

        if(Input.GetButtonDown("Jump") && !usedDoubleJump && !grounded && !hangingOffWall)
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
        yield return new WaitForSeconds(hangDelay);
        canHang = true;

    }


}