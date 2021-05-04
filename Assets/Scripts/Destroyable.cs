using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public GameObject explosion;
    public SkinnedMeshRenderer mesh;
    public GameObject flames;

    private bool shot = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Laser" || other.tag == "Moab") && !shot)
        {
            if (other.tag == "Laser")
            {
                GameObject g = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(g, 3);
                Destroy(other.gameObject);
            }
            gameObject.GetComponent<AudioSource>().Play();
            mesh.enabled = false;
            shot = true;
            flames.GetComponent<CapsuleCollider>().enabled = false;
            flames.GetComponent<ParticleSystem>().Stop();
            Destroy(gameObject, 4);
        }
    }
}
