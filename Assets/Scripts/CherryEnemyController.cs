using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryEnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Transform rightPoint, leftPoint;

    private Rigidbody2D rgBody;
    private bool moveingRight;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveingRight = true;

        rightPoint.parent = null;
        leftPoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveingRight)
        {
            rgBody.velocity = new Vector2(moveSpeed, rgBody.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);

            if(transform.position.x > rightPoint.position.x)
            {
                moveingRight = false;
            }
        }
        else
        {
            rgBody.velocity = new Vector2(-moveSpeed, rgBody.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);

            if (transform.position.x < leftPoint.position.x)
            {
                moveingRight = true;
            }
        }

        anim.SetBool("isJumping", true);
    }
}
