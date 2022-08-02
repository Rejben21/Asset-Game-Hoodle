using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float height;
    public float smoothSpeed;

    public Transform backGround, farBackGround;
    private float lastXPos;

    void Start()
    {
        lastXPos = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10), new Vector3(target.position.x, target.position.y + height, -10), smoothSpeed / 10);
        }
        else
        {

        }

        float amountToMoveX = transform.position.x - lastXPos;

        farBackGround.position = farBackGround.position + new Vector3(amountToMoveX * -.2f, 0, 0);
        backGround.position = backGround.position + new Vector3(amountToMoveX * -.4f, 0, 0);

        lastXPos = transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            LevelManager.instance.RespawnPlayer();
        }
    }
}
