using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public float minForce, maxForce, radius;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioS = gameObject.GetComponent<AudioSource>();
        audioS.Play();

        foreach(Transform t in transform)
        {
            Rigidbody rig = t.GetComponent<Rigidbody>();
            if(rig != null) rig.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            Destroy(gameObject, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
