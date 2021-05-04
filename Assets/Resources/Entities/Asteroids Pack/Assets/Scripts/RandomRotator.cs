using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private float tumble;

    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Laser")
        {
            //GameObject g = Instantiate(brokenObject, transform.position, Quaternion.identity);
            //g.transform.LookAt(player.transform);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}