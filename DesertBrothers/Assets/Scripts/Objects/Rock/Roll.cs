using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
    private Animator anim;
    public GameObject player;
    public GameObject EarthDragon;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(player.transform.position.x > this.transform.position.x + 0.3f)
        {
            anim.SetTrigger("roll");
        }
    }

    public void DESTROY()
    {
        Destroy(this.gameObject);
    }

    public void SHAKE1()
    {
        CameraControl.StartShake(0.003f, 6.68f, 1);
    }
    public void SHAKE2()
    {
        CameraControl.StartShake(0.008f, 5.34f, 1);
    }
    public void SHAKE3()
    {
        CameraControl.StartShake(0.01f, 4.17f, 1);
    }
    public void SHAKE4()
    {
        CameraControl.StartShake(0.013f, 3, 1);
    }

    public void SUMMONEARTHDRAGON()
    {
        Instantiate(EarthDragon, new Vector3(this.transform.position.x, -4.6f, this.transform.position.z), Quaternion.identity);
    }
}
