using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public GameObject hitSparkle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealthController.instance.curHealth = 0;
        }
        if(other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Instantiate(hitSparkle, other.transform.position, Quaternion.identity);
        }
    }
}
