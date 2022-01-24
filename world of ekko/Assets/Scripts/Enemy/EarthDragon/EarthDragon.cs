using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthDragon : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 startPoint;
    public Vector3 moveSpeed;
    private Vector3 moveSpeedDown;
    private bool down;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPoint = transform.position;
        moveSpeedDown = new Vector3(moveSpeed.x, moveSpeed.y-1f, moveSpeed.z);
    }

    void Update()
    {
        if(transform.position.y >= (startPoint.y + 3f))
        {
            down = true;
        }
        else if(transform.position.y <= (startPoint.y - 1f))
        {
            Destroy(this.gameObject);
        }
        
        if(down == false)
        {
            rb.velocity = moveSpeed;
        }
        else
        {
            rb.velocity = -moveSpeedDown;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Die
        }
        if(other.gameObject.tag == "Rock")
        {
            //Return
        }
    }
}
