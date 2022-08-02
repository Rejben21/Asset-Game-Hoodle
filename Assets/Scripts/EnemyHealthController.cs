using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int maxHealth, curHealth;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(curHealth <= 0)
        {
            anim.Play("EnemyDead");
            Destroy(this.gameObject, 2);
        }
    }

    public void DealDamage()
    {
        curHealth--;
        anim.Play("EnemyHurt");
    }
}
