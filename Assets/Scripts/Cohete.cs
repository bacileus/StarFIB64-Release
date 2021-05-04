using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cohete : MonoBehaviour
{
    public GameObject laser;
    public GameObject target;
    public float laserSpeed;
    public float maxDelay;
    private float delay;

    public AudioSource audioS;
    public AudioClip soundShoot;

    public Transform rocketText;
    public float remainingRockets = 4;

    // Start is called before the first frame update
    void Start()
    {
        delay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Fire2") == 1f && delay >= maxDelay && remainingRockets > 0)
        {
            remainingRockets -= 1;

            audioS.clip = soundShoot;
            audioS.Play();

            // particles
            gameObject.GetComponent<ParticleSystem>().Play();

            GameObject l = Instantiate(laser, gameObject.transform.position, Quaternion.identity);
            Vector3 dir = target.transform.position - transform.position;
            Destroy(l, 3);
            l.GetComponent<Rigidbody>().AddForce(dir.normalized * laserSpeed);
            delay = 0f;
        }
        delay += Time.deltaTime;
    }
}
