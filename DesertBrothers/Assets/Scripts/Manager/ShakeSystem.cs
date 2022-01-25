using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeSystem : MonoBehaviour
{
    public GameObject player, EarthDragon;
    private bool once = false;
    void Update()
    {

        if(Manager.instance.shakeTime > 0f && Manager.instance.shakeTime <= 3.5f)
        {
            CameraControl.StartShake(0.003f, 0.2f, 1);
        }
        else if(Manager.instance.shakeTime > 3.5f && Manager.instance.shakeTime <= 6f)
        {
            CameraControl.StartShake(0.008f, 0.2f, 1);
        }
        else if(Manager.instance.shakeTime > 6f && Manager.instance.shakeTime <= 8.5f)
        {
            CameraControl.StartShake(0.01f, 0.2f, 1);
        }
        else if(Manager.instance.shakeTime > 8.5f)
        {
            CameraControl.StartShake(0.013f, 0.2f, 1);
        }
        
    }

    private void FixedUpdate()
    {
        if(Manager.instance.shaking == false && Manager.instance.shakeTime >= 0f)
        {
            Manager.instance.shakeTime -= Time.fixedDeltaTime*1.3f;
        }
        if(Manager.instance.shaking == true)
        {
            Manager.instance.shakeTime += Time.fixedDeltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(Manager.instance.shakeTime > 12f && once == false)
        {
            once = true;
            if(!GameObject.Find("EarthDragon(Clone)"))
            {
                Instantiate(EarthDragon, new Vector3(player.transform.position.x, -4.6f, player.transform.position.z), Quaternion.identity);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Manager.instance.shaking = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Manager.instance.shaking = false;
            once = false;
        }
    }
}
