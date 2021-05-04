using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private AudioSource audioS;
    public AudioClip explosion;
    public GameObject player, particles;
    public ParticleSystem explode;
    private float delay;
    public float maxDelay;

    private bool shot = false;

    // Start is called before the first frame update
    void Start()
    {
        audioS = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shot && player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            float ovf = float.MaxValue;
            delay += Time.deltaTime;

            if (distance < 70 && distance >= 55) ovf = maxDelay;
            else if (distance < 55 && distance >= 40) ovf = maxDelay / 4;
            else if (distance < 40 && distance >= 25) ovf = maxDelay / 8;
            else if (distance < 25) ovf = maxDelay / 16;

            if (delay > ovf)
            {
                if (distance < 25 && !player.GetComponent<Player>().isBarrelRolling)
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    explode.Play();
                    audioS.Stop();
                    audioS.volume = 1f;
                    audioS.clip = explosion;
                    Destroy(gameObject, 2);
                    maxDelay = float.MaxValue;
                    if (!player.GetComponent<Player>().godMode)
                        player.GetComponent<Player>().TakeDamage(2);
                }
                delay = 0;
                audioS.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser" && !shot)
        {
            Destroy(other.gameObject);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            explode.Play();
            audioS.volume = 1f;
            audioS.clip = explosion;
            audioS.Play();
            Destroy(gameObject, 2);
            shot = true;
            GameObject e = Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(e, 3);
        }

        if (other.tag == "Moab" && !shot)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            audioS.volume = 1f;
            audioS.clip = explosion;
            audioS.Play();
            Destroy(gameObject, 2);
            shot = true;
        }
    }
}
