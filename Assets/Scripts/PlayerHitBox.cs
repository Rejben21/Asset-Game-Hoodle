using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    public static PlayerHitBox instance;
    public GameObject hitSparkle;
    public Transform hitSparklePoint;
    public GameObject mainCamera;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Environment"))
        {

        }
        else if(other.CompareTag("Enemy"))
        {
            Instantiate(hitSparkle, hitSparklePoint.position, Quaternion.identity);
            other.GetComponentInChildren<EnemyHealthController>().DealDamage();
            other.GetComponentInParent<EnemyMushroom>().isAttacked = true;
            mainCamera.GetComponent<Animator>().Play("MainCameraShake");
        }
        else
        {
            Instantiate(hitSparkle, hitSparklePoint.position, Quaternion.identity);
            mainCamera.GetComponent<Animator>().Play("MainCameraShake");
        }
    }
}
