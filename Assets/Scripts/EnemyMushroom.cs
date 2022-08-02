using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : MonoBehaviour
{
    public float moveSpeed;
    public Transform leftPoint, rightPoint;

    private bool movingRight;
    public bool isAttacked = false;

    public float bounceForce;

    public float knockbackForce;
    private float distance;
    public Transform player;

    public GameObject coin;
    [Range(0, 100)]public float chanceToDrop;

    private Rigidbody2D rgBody;
    private Animator anim;
    private EnemyHealthController healthCon;

    // Start is called before the first frame update
    void Start()
    {
        healthCon = GetComponentInChildren<EnemyHealthController>();
        anim = GetComponent<Animator>();
        rgBody = GetComponent<Rigidbody2D>();

        leftPoint.parent = null;
        rightPoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacked == false)
        {
            if (movingRight)
            {
                rgBody.velocity = new Vector2(moveSpeed, rgBody.velocity.y);
                if (transform.position.x >= rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                rgBody.velocity = new Vector2(-moveSpeed, rgBody.velocity.y);
                if (transform.position.x <= leftPoint.position.x)
                {
                    movingRight = true;
                }
            }
        }
        else
        {
            distance = transform.position.x - player.position.x;
            if (distance < 0)
            {
                rgBody.velocity = new Vector2(-knockbackForce, rgBody.velocity.y);
            }
            else if (distance > 0)
            {
                rgBody.velocity = new Vector2(knockbackForce, rgBody.velocity.y);

            }
        }

        if (rgBody.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rgBody.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if(healthCon == null)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, bounceForce);
            anim.Play("MushroomBounce");
        }
    }

    public void DropObjects()
    {
        float dropSelect = Random.Range(0, 100f);

        if(dropSelect <= chanceToDrop)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
        }
    }

    public void NotAttacked()
    {
        isAttacked = false;
    }
}
