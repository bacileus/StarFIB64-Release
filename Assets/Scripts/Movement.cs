using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;


using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Movement : MonoBehaviour
{
    public float speed;
    private Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 mov = new Vector3(hor, ver, 0);
        mov.Normalize();
        Vector3 offset = mov * speed * Time.fixedDeltaTime;
        transform.localPosition += offset;

        // clamp
        Vector3 clamp = cam.WorldToViewportPoint(transform.position);
        clamp.x = Mathf.Clamp(clamp.x, 0.25f, 0.75f);
        clamp.y = Mathf.Clamp(clamp.y, 0.25f, 0.75f);
        transform.position = cam.ViewportToWorldPoint(clamp);
    }
}
