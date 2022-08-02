using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOutController : MonoBehaviour
{
    public float jumpForce;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.attachedRigidbody.velocity = new Vector2(other.attachedRigidbody.velocity.x, jumpForce);
            anim.Play("PopOutUp");
        }
    }
}
