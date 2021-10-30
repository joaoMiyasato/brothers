using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;
    public Transform player;
    private BoxCollider2D cameraBox;
    public float maskx,masky,maskz = -50f;
    public float damp = 0.1f;
    private Vector3 pos;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        cameraBox = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if(GameObject.Find("Boundary"))
        {
            pos = new Vector3(Mathf.Clamp(player.position.x + maskx, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.x + cameraBox.size.x /2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.x - cameraBox.size.x /2),
                            Mathf.Clamp(player.position.y + masky, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.y + cameraBox.size.y /2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.y - cameraBox.size.y /2),
                            maskz);
            transform.position = Vector3.SmoothDamp(gameObject.transform.position, pos, ref velocity, damp);
        }
    }
}
