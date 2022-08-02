using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool isCoin;
    private Animator anim;
    private bool isCollected;

    private Rigidbody2D rgBody;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rgBody = GetComponent<Rigidbody2D>();

        rgBody.velocity = new Vector2(Random.Range(-5, 5), Random.Range(10, 15));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isCollected)
        {
            if(isCoin)
            {
                LevelManager.instance.coinCollected++;

                isCollected = true;
                UIController.instance.UpdateCoinsCount();
                Destroy(this.gameObject, 2);
            }
            anim.Play("PickUp");
        }
    }
}
