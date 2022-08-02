using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Image heart1, heart2, heart3;

    private Animator h1anim, h2anim, h3anim;

    public Text coinsText;

    public Image fadeScreen;
    public float fadeSpeed;

    public bool fadeToBlack, fadeFromBlack;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinsCount();
        FadeFromBlack();

        h1anim = heart1.GetComponent<Animator>();
        h2anim = heart2.GetComponent<Animator>();
        h3anim = heart3.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
        else if(fadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

    public void UpdateHealthDisplay()
    {
        switch(PlayerHealthController.instance.curHealth)
        {
            case 3:
                h1anim.Play("HeartAdd");
                h2anim.Play("HeartAdd");
                h3anim.Play("HeartAdd");

                break;

            case 2:
                h1anim.Play("HeartLost");
                h2anim.Play("HeartAdd");
                h3anim.Play("HeartAdd");

                break;

            case 1:
                h1anim.Play("HeartLost");
                h2anim.Play("HeartLost");
                h3anim.Play("HeartAdd");

                break;

            case 0:
                h1anim.Play("HeartLost");
                h2anim.Play("HeartLost");
                h3anim.Play("HeartLost");

                break;

            default:
                h1anim.Play("HeartLost");
                h2anim.Play("HeartLost");
                h3anim.Play("HeartLost");

                break;
        }
    }

    public void UpdateCoinsCount()
    {
        coinsText.text = LevelManager.instance.coinCollected.ToString();
    }

    public void FadeToBlack()
    {
        fadeToBlack = true;
        fadeFromBlack = false;
    }
    
    public void FadeFromBlack()
    {
        fadeFromBlack = true;
        fadeToBlack = false;
    }
}
