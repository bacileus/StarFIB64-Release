using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipAI : MonoBehaviour
{
    public GameObject player, laser, brokenObject, explosion;
    public ParticleSystem shoot;
    public float fireRate, speed, laserSpeed;

    private float time = 0;
    private Rigidbody rig;
    private bool isClose = false;

    private bool shot = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > fireRate && isClose && player != null)
        {
            time = 0;
            shoot.Play();
            GameObject l = Instantiate(laser, gameObject.transform.position + Vector3.up, Quaternion.identity);
            Vector3 dir = player.transform.position - transform.position;
            Destroy(l, 5);
            l.transform.LookAt(player.transform);
            l.transform.Rotate(Vector3.right, 90);
            l.GetComponent<Rigidbody>().AddForce(dir.normalized * laserSpeed);
        }
        time += Time.deltaTime;
        
        // esquivar
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 dir = player.transform.position - transform.position;
            if (!isClose)
            {
                rig.position += dir.normalized * speed * Time.fixedDeltaTime;
                if (dir.magnitude < 50) isClose = true;
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "level01") dir.x = 0;
                else dir.z = 0;

                if (Mathf.Abs(dir.x) > 2 || Mathf.Abs(dir.z) > 2)
                    rig.position += dir.normalized * speed / 2 * Time.fixedDeltaTime;
            }
            transform.LookAt(player.transform);
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
    }
}
