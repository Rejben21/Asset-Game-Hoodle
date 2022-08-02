using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankeyController : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    public int curPoint;

    private Animator anim;

    public float distanceToPlayer, chaseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        for(int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToPlayer)
        {

            transform.position = Vector3.MoveTowards(transform.position, points[curPoint].position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, points[curPoint].position) < .05f)
            {
                curPoint++;

                if (curPoint >= points.Length)
                {
                    curPoint = 0;
                }
            }

            anim.SetBool("Attack", false);

            if (transform.position.x < points[curPoint].position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (transform.position.x > points[curPoint].position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, chaseSpeed * Time.deltaTime);
            anim.SetBool("Attack", true);
        }
    }
}
