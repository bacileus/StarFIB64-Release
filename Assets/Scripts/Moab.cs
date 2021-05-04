using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moab : MonoBehaviour
{
    public GameObject explosion;
    
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
        if (other.gameObject.layer == 12)
        {
            GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(e, 3);
            Destroy(gameObject);
        }
    }
}
