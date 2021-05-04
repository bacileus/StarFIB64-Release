using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    private Transform targetPlayer;
    public float range = 100f;

    public float disparos = 1f;
    private float cuantosDisparos = 0f;

    public GameObject bulletLaser;
    public Transform gun;

    public GameObject explosion;
    public GameObject parent;

    public bool isUpside = true;
    private bool shot = false;

    private AudioSource audioS;
    public SkinnedMeshRenderer mesh;

    private void Start()
    {
        audioS = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Player");
        if (enemy != null && !shot)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy <= range)
            {
                targetPlayer = enemy.transform;
            }
            else return;

            //Rotation
            transform.LookAt(enemy.transform.position);
            if (!isUpside) transform.Rotate(Vector3.forward * 180);
            transform.Rotate(Vector3.right * -90);

            if (cuantosDisparos <= 0f)
            {
                Shoot();
                cuantosDisparos = 1f / disparos;
            }

            cuantosDisparos -= Time.deltaTime;
        }
    }

    void Shoot ()
    {
        GameObject bala1 = (GameObject)Instantiate(bulletLaser, gun.position, gun.rotation);
        Destroy(bala1, 5);

        Vector3 dir = targetPlayer.transform.position - transform.position;

        bala1.transform.LookAt(targetPlayer.transform);
        bala1.transform.Rotate(Vector3.right, 90);
        bala1.GetComponent<Rigidbody>().AddForce(dir.normalized * 4000f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!shot && (other.tag == "Laser" || other.tag == "Moab" || other.tag == "Player")) {
            if (other.tag != "Moab")
            {
                GameObject g = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(g, 3);
                Destroy(other.gameObject);
            }
            audioS.Play();
            mesh.enabled = false;
            Destroy(parent, 4);
            shot = true;
        }
    }
}
