using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : Enemy
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform opsm;
    [SerializeField] private float speed;
    private bool facingright = true;
    private void Update()
    {

        if (facingright)
        {
            if (transform.position.x > target.transform.position.x)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                if (facingright)
                {
                    transform.position = Vector3.MoveTowards(opsm.position, target.position, speed * Time.deltaTime);



                }
            }
            else
            {
                facingright = false;
            }
        }
        else
        {
            if (transform.position.x < target.transform.position.x)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (!facingright)
                {
                    transform.position = Vector3.MoveTowards(opsm.position, target.position, speed * Time.deltaTime);

                }
            }
            else
            {
                facingright = true;
            }

        }
    }
}
