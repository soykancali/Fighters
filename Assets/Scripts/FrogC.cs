using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogC : Enemy
{
    public float RightCap;
    public float LeftCap;
    public float jumplenght = 10f;
    public float jumpingheight = 15f;
    private bool facingLeft = true;
    public Collider2D coll;
    public LayerMask ground;
   
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        
    }
    private void Update()
    {
        if (anim.GetBool("jump"))
        {
            if (rb.velocity.y < .1)
            {
                anim.SetBool("fall", true);
                anim.SetBool("jump", false);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("fall"))
        {
            anim.SetBool("fall", false);
        }
        
    }
    
   
    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > LeftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumplenght, jumpingheight);
                    anim.SetBool("jump", true);

                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < RightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumplenght, jumpingheight);
                    anim.SetBool("jump", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }
}
