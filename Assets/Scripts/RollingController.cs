using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingController : MonoBehaviour
{
    public Transform rightPoint, leftPoint;
    public float moveSpeed;

    private bool moveingRight;
    private Rigidbody2D rgBody;

    // Start is called before the first frame update
    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();

        rightPoint.parent = null;
        leftPoint.parent = null;

        moveingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveingRight)
        {
            rgBody.velocity = new Vector2(moveSpeed, rgBody.velocity.y);

            if(transform.position.x > rightPoint.position.x)
            {
                moveingRight = false;
            }
        }
        else
        {
            rgBody.velocity = new Vector2(-moveSpeed, rgBody.velocity.y);

            if(transform.position.x < leftPoint.position.x)
            {
                moveingRight = true;
            }
        }

        if(rgBody.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(rgBody.velocity.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
