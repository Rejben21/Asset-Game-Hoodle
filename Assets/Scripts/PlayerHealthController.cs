using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int maxHealth, curHealth;
    public GameObject playerDeathEffect;

    public float invincibleLenght;
    private float invincibleCounter;

    public SpriteRenderer sR;

    public GameObject hitBox, hitBoxUp, sEffect, sEffectUp;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if(invincibleCounter <= 0)
            {
                sR.color = new Color(sR.color.r, sR.color.g, sR.color.b, 1);
            }
        }
    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {
            curHealth--;

            if (curHealth <= 0)
            {
                curHealth = 0;
                if (transform.localScale == new Vector3(1, 1, 1))
                {
                    playerDeathEffect.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (transform.localScale == new Vector3(-1, 1, 1))
                {
                    playerDeathEffect.transform.localScale = new Vector3(-1, 1, 1);
                }

                Instantiate(playerDeathEffect, transform.position, Quaternion.identity);

                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLenght;
                sR.color = new Color(sR.color.r, sR.color.g, sR.color.b, 0.5f);

                PlayerController.instance.KnockBack();
            }

            UIController.instance.UpdateHealthDisplay();
        }
    }
}
