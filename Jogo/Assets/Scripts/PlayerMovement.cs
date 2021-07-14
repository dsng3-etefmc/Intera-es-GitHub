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
        Move();
        Jump();
        Shoot();
    }

    void Shoot(){
        if(Input.GetButtonDown("Fire1")){
            Instantiate(ball, transform.position, transform.rotation);
            
        }
    }

    void Move(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        if(Input.GetAxis("Horizontal")>0f) {
            animator.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        } 

        else if (Input.GetAxis("Horizontal")<0f) {
            animator.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }

        else animator.SetBool("run", false);  
    }



    void Jump(){
        if(Input.GetButtonDown("Jump")){
            if(!isJumping){
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            }

            else if (!doubleJump){
                rig.AddForce(new Vector2(0f, JumpForce*1.2f), ForceMode2D.Impulse);
                animator.SetBool("doubleJump", true);
                doubleJump = true;
            }
        }
    }



    void OnCollisionEnter2D(Collision2D collision) {

        if(collision.gameObject.layer == 10){
            isJumping = false;
            doubleJump = false;
            animator.SetBool("jump", false);
            animator.SetBool("doubleJump", false);
        }
    }



    void OnCollisionExit2D(Collision2D collision) {

        if(collision.gameObject.layer == 10){
            isJumping = true;
            animator.SetBool("jump", true);
        }

    }
}
