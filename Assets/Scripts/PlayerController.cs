using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed, jumpForce;
    public float jumpTime;
    public Rigidbody2D rgBody;
    public GameObject dust1, dust2;
    private bool makeDust;

    public float hangTime;
    private float hangCounter;

    public float jumpBufferLenght;
    private float jumpBufferCount;

    public float dashSpeed, startDashTime;
    public float dashTime;
    private int direction;
    private float timeToDash;

    public GameObject echoEffect;
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;

    private bool isGrounded;
    public Transform groundPoint;
    public LayerMask whatIsGround;

    private bool upAttack;

    public float knockBackLenght, knockBackForce;
    private float knockBackCounter;

    public Animator anim;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeToDash = 2;
        dashTime = startDashTime;
        rgBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.instance.isPaused == false)
        {
            if (knockBackCounter <= 0)
            {

                //Move
                rgBody.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rgBody.velocity.y);

                //Jump
                isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

                if (isGrounded)
                {
                    hangCounter = hangTime;
                }
                else
                {
                    hangCounter -= Time.deltaTime;
                }

                if (Input.GetButtonDown("Jump"))
                {
                    jumpBufferCount = jumpBufferLenght;
                }
                else
                {
                    jumpBufferCount -= Time.deltaTime;
                }

                if (jumpBufferCount >= 0 && hangCounter > 0)
                {
                    rgBody.velocity = new Vector2(rgBody.velocity.x, jumpForce);
                    jumpBufferCount = 0;
                    Instantiate(dust1, transform.position, Quaternion.identity);
                }

                if (Input.GetButtonUp("Jump") && rgBody.velocity.y > 0)
                {
                    rgBody.velocity = new Vector2(rgBody.velocity.x, rgBody.velocity.y * 0.5f);
                }

                if (isGrounded)
                {
                    if (makeDust)
                    {
                        Instantiate(dust2, transform.position, Quaternion.identity);
                        makeDust = false;
                    }
                }
                else
                {
                    makeDust = true;
                }

                //Swich
                if (rgBody.velocity.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (rgBody.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                //Dash
                timeToDash += Time.deltaTime;
                if (direction == 0)
                {
                    if (transform.localScale == new Vector3(1, 1, 1) && Input.GetKeyDown(KeyCode.Mouse1) && timeToDash >= .5f)
                    {
                        direction = 1;
                        timeToDash = 0;
                    }
                    else if (transform.localScale == new Vector3(-1, 1, 1) && Input.GetKeyDown(KeyCode.Mouse1) && timeToDash >= .5f)
                    {
                        direction = 2;
                        timeToDash = 0;
                    }
                }
                else
                {
                    if (dashTime <= 0)
                    {
                        direction = 0;
                        dashTime = startDashTime;
                        rgBody.velocity = Vector2.zero;
                    }
                    else
                    {
                        dashTime -= Time.deltaTime;
                        anim.Play("PlayerDash");

                        if (timeBtwSpawns <= 0)
                        {
                            if (transform.localScale == new Vector3(1, 1, 1))
                            {
                                echoEffect.transform.localScale = new Vector3(1, 1, 1);
                            }
                            else
                            {
                                echoEffect.transform.localScale = new Vector3(-1, 1, 1);
                            }
                            Instantiate(echoEffect, transform.position, Quaternion.identity);
                            timeBtwSpawns = startTimeBtwSpawns;
                        }
                        else
                        {
                            timeBtwSpawns -= Time.deltaTime;
                        }

                        if (direction == 1)
                        {
                            rgBody.velocity = Vector2.right * dashSpeed;
                        }
                        else if (direction == 2)
                        {
                            rgBody.velocity = Vector2.left * dashSpeed;
                        }
                    }
                }

                //Attack
                if (Input.GetKey(KeyCode.W))
                {
                    upAttack = true;
                }
                else
                {
                    upAttack = false;
                }

                if (Input.GetKeyDown(KeyCode.Mouse0) && upAttack)
                {
                    anim.Play("PlayerAttackUp");
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    anim.Play("PlayerAttack");
                }

            }
            else
            {
                knockBackCounter -= Time.deltaTime;
                if (transform.localScale == new Vector3(1, 1, 1))
                {
                    rgBody.velocity = new Vector2(-knockBackForce, 5);
                }
                else if (transform.localScale == new Vector3(-1, 1, 1))
                {
                    rgBody.velocity = new Vector2(knockBackForce, 5);
                }
            }

            //Animations
            anim.SetFloat("Force", rgBody.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(rgBody.velocity.x));
            anim.SetBool("IsGrounded", isGrounded);
        }
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLenght;
        anim.Play("PlayerHurt");
    }
}
