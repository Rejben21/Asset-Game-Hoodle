using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseController : MonoBehaviour
{
    public GameObject coin;
    [Range(0, 100)] public float chanceToDrop;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("HitBox"))
        {
            GetComponent<Animator>().Play("VaseBreak");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
            GetComponent<Animator>().Play("VaseBreak");
        }
    }

    public void DropObjects()
    {
        float dropSelect = Random.Range(0, 100f);

        if (dropSelect <= chanceToDrop)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
        }
    }
}
