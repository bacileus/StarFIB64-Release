using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideAI : MonoBehaviour
{
    private GameObject player;
    public GameObject brokenObject;
    public GameObject explosion;
    public float speed;

    private bool shot = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            gameObject.transform.LookAt(player.transform);
            gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser" && !shot)
        {
            shot = true;
            GameObject g = Instantiate(brokenObject, transform.position, Quaternion.identity);
            g.transform.LookAt(player.transform);
            GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(e, 3);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.tag == "Moab" && !shot)
        {
            shot = true;
            GameObject g = Instantiate(brokenObject, transform.position, Quaternion.identity);
            g.transform.LookAt(player.transform);
            Destroy(gameObject);
        }

        if (other.tag == "Player")
        {
            GameObject g = Instantiate(brokenObject, transform.position, Quaternion.identity);
            g.transform.LookAt(player.transform);
            GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(e, 3);
            if (!player.GetComponent<Player>().godMode)
                player.GetComponent<Player>().TakeDamage(2);
            Destroy(gameObject);
        }
    }
}
