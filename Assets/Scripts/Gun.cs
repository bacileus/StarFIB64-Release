using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject laser;
    public GameObject target;
    public float laserSpeed;
    public float maxDelay;
    private float delay;

    public AudioSource audioS;
    public AudioClip soundShoot;

    // Start is called before the first frame update
    void Start()
    {
        delay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Fire1") == 1f && delay >= maxDelay)
        {
            audioS.clip = soundShoot;
            audioS.Play();

            // particles
            gameObject.GetComponent<ParticleSystem>().Play();

            GameObject l = Instantiate(laser, gameObject.transform.position, Quaternion.identity);
            Vector3 dir = target.transform.position - transform.position;
            Destroy(l, 0.9f);
            l.transform.LookAt(target.transform);
            l.transform.Rotate(Vector3.right, 90);
            l.GetComponent<Rigidbody>().AddForce(dir.normalized * laserSpeed);
            delay = 0f;
        }
        delay += Time.deltaTime;
    }
}
