using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public Animator animator;
    public Rigidbody2D rb;
    public Text cherrytext;
    public Text diamondtxt;
    public float hurtForce = 20f;
    public float runSpeed = 40f;
    public int cherrys=0,diamond=0;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public static bool falling = false;

    
    void Update()
    {

        horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
            falling = false;
        }
        if (CrossPlatformInputManager.GetButtonUp("Jump"))
        {
            jump = false;
            animator.SetBool("IsJumping",false);
            animator.SetBool("IsFalling", true);
            falling = true;
        }
        if (CrossPlatformInputManager.GetButtonDown("Crouch"))
        {
            crouch = true;
            animator.SetBool("IsCrouching", true);
        }
        if (CrossPlatformInputManager.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("IsCrouching", false);
        }
        if (horizontalMove < 0)
        {
           
            rb.velocity = new Vector2(-2,rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
           
        }
        if (horizontalMove > 0)
        {
            
            rb.velocity = new Vector2(2, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
           
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFalling", false);
        animator.SetBool("IsHurting", false);
        
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
        animator.SetBool("IsFalling", false);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            cherrys += 1;
            cherrytext.text = cherrys.ToString();
        }
        if (collision.gameObject.tag == "pike")
        {
            Destroy(collision.gameObject);
            diamond += 1;
            diamondtxt.text = diamond.ToString();
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" )
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            
            if (falling == true)
            {
                enemy.jumponDeath();
                
            }
            else if(falling==false)
            {
               
                
                Debug.Log("ishurting");
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                    animator.SetBool("IsHurting", true);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                    animator.SetBool("IsHurting", true);
                }
                
            }
            
            
        }
        

    }


    
    void FixedUpdate()
    {
       
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        
    }
    
}
