using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStoneBehaviour : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            anim.SetBool("Near", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            anim.SetBool("Near", false);
        }
    }
}
