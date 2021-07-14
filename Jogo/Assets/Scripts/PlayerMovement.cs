using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Speed;

    public float JumpForce;

    private Rigidbody2D rig;

    private bool isJumping;
    private bool doubleJump;
    private bool isGrounded;

    public int allowedJumpsOnAir = 1;
    public int jumpsOnAir = 0;

    Animator animator;

    public GameObject ball;


    // Start is called before the first frame update

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    void Update()
    {
        this.CheckIfGrounded();

        this.Move();
        this.ShouldJump();
        this.Shoot();
    }

    void CheckIfGrounded() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            print("grounded");
            Vector3 targetLocation = hit.point;
            targetLocation += new Vector3(0, transform.localScale.y / 2, 0);
            transform.position = targetLocation;
        }
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(ball, transform.position, transform.rotation);
        }
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        if (Input.GetAxis("Horizontal") > 0f)
        {
            animator.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (Input.GetAxis("Horizontal") < 0f)
        {
            animator.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        else animator.SetBool("run", false);
    }

    void Jump(float scale)
    {
        rig.AddForce(new Vector2(0f, JumpForce * scale), ForceMode2D.Impulse);
    }

    void ShouldJump()
    {
        if (Input.GetButtonDown("Jump"))
        {

            if (!isJumping)
            {
                this.Jump(1);
                this.jumpsOnAir = 0;
                print("normal jump");
            }

            else if (this.jumpsOnAir < this.allowedJumpsOnAir)
            {
                this.Jump(1.2f);
                animator.SetBool("doubleJump", true);
                doubleJump = true;

                this.jumpsOnAir++;
            }

        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 10)
        {
            isJumping = false;
            doubleJump = false;
            animator.SetBool("jump", false);
            animator.SetBool("doubleJump", false);
        }
    }



    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 10)
        {
            isJumping = true;
            animator.SetBool("jump", true);
        }

    }
}
