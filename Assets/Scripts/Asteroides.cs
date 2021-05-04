using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroides : MonoBehaviour
{
    private GameObject player;
    public GameObject brokenObject;

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

        if (other.tag == "Laser" || other.tag == "Moab")
        {
            GameObject g = Instantiate(brokenObject, transform.position, Quaternion.identity);
            g.transform.rotation = transform.rotation;
            g.transform.localScale = transform.localScale;
            if (other.tag == "Laser") Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.tag == "Player")
        {
            GameObject g = Instantiate(brokenObject, transform.position, Quaternion.identity);
            g.transform.rotation = transform.rotation;
            g.transform.localScale = transform.localScale;
            Destroy(gameObject);
        }


    }

}
