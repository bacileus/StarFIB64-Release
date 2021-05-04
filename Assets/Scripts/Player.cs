using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;


using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    // translation
    public float followSpeed, maxAngle;
    public Transform sight;

    // input
    private float hor;

    // rotation
    private float angle = 0;
    private int laps = 0;
    public bool isBarrelRolling = false;
    public GameObject trail1;
    public GameObject trail2;
    public AudioClip roll;

    // boost
    private float trailSpeed = 20;
    public float zCamera = 0;
    public float maxZ = 10;
    public CinemachineDollyCart cinemachineDollyCart;
    public ParticleSystem p1;
    public ParticleSystem p2;
    public ParticleSystem p3;
    public ParticleSystem p4;
    public float startSize = 1.5f;

    // other
    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthBar;
    private Camera cam;

    public bool godMode = false;
    public GameObject nave;
    public Material material;
    public Material material0;
    public Material materialDaño;
    public Material materialLife;
    Renderer rend;
    public SphereCollider naveCollider;

    // sound
    public AudioSource audioS;
    public AudioClip soundShoot;
    public AudioClip soundLife;
    public AudioClip portalSound;

    // gameover
    public GameObject broken, explosion;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        hor = Input.GetAxisRaw("Horizontal");
        cam = Camera.main;
        // healthBar.SetMaxHealth(maxHealth);

        rend = nave.GetComponent<Renderer>();
        rend.enabled = true;
        naveCollider = GetComponent<SphereCollider>();
        naveCollider = GetComponent<SphereCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            isBarrelRolling = true;
            audioS.PlayOneShot(roll);
        }
        if (Input.GetKeyDown("g"))
        {
            if(godMode == false)
            {
                godMode = true;
                rend.sharedMaterial = material;
                //naveCollider.isTrigger = false;
            }
            else
            {
                godMode = false;
                rend.sharedMaterial = material0;
                //naveCollider.isTrigger = true;
            }

        }
    }

    private void FixedUpdate()
    {
        doMove();
        doRotation();
        doBoost();
        camFollow();
    }

    void doMove()
    {
        Vector3 trail = sight.localPosition - transform.localPosition;
        trail.z = 0;

        transform.localPosition += trail * followSpeed * Time.fixedDeltaTime;
    }

    void doRotation()
    {
        transform.LookAt(sight);
        if (!isBarrelRolling)
        {
            hor = Input.GetAxisRaw("Horizontal");
            if (hor != 0)
            {
                Vector3 look = transform.position - sight.position;
                if (look.y < 1 && look.y > -1)
                {
                    angle += hor * -2;
                    angle = Mathf.Clamp(angle, -maxAngle, maxAngle);
                }
            }
            else if (angle < 0) angle++;
            else if (angle > 0) angle--;
            transform.Rotate(Vector3.forward, angle);

            if (trail1.GetComponent<TrailRenderer>().time > 0.2) trail1.GetComponent<TrailRenderer>().time -= 0.02f;
            if (trail2.GetComponent<TrailRenderer>().time > 0.2) trail2.GetComponent<TrailRenderer>().time -= 0.02f;
        }
        else
        {
            trail1.GetComponent<TrailRenderer>().time = 0.5f;
            trail2.GetComponent<TrailRenderer>().time = 0.5f;

            angle += hor * -16;
            laps++;
            transform.Rotate(Vector3.forward, angle);
            if ((Mathf.Abs(angle) % 360) <= 16 && laps > 5)
            {
                isBarrelRolling = false;
                angle = 0;
                laps = 0;
                transform.Rotate(Vector3.forward, angle);
            }
        }
    }

    void doBoost()
    {
        // boost
        ParticleSystem.MainModule m1 = p1.main;
        ParticleSystem.MainModule m2 = p2.main;
        ParticleSystem.MainModule m3 = p3.main;
        ParticleSystem.MainModule m4 = p4.main;

        if (Input.GetKey("left shift"))
        {
            cinemachineDollyCart.m_Speed = trailSpeed * 2;
            m1.startSize = startSize * 2;
            m2.startSize = startSize * 1.5f;
            m3.startSize = startSize * 2;
            m4.startSize = startSize * 1.5f;
            if (zCamera > -maxZ) zCamera--;
        }
        else if (Input.GetKey("left ctrl"))
        {
            cinemachineDollyCart.m_Speed = trailSpeed / 2;
            m1.startSize = startSize / 2;
            m2.startSize = startSize / 1.5f;
            m3.startSize = startSize / 2;
            m4.startSize = startSize / 1.5f;
            if (zCamera < maxZ) zCamera++;
        }
        else
        {
            cinemachineDollyCart.m_Speed = trailSpeed;
            m1.startSize = startSize;
            m2.startSize = startSize;
            m3.startSize = startSize;
            m4.startSize = startSize;
            if (zCamera > 0) zCamera--;
            else if (zCamera < 0) zCamera++;
        }
    }

    void camFollow()
    {
        Vector3 speed = Vector3.zero;
        Vector3 camPos = Vector3.SmoothDamp(cam.transform.localPosition, transform.localPosition, ref speed, 0.1f);
        camPos.x = Mathf.Clamp(camPos.x, -20, 20);
        camPos.y = Mathf.Clamp(camPos.y, -20, 20);
        camPos.z = zCamera;
        cam.transform.localPosition = camPos;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        rend.sharedMaterial = material0;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        rend.sharedMaterial = materialDaño;
        StartCoroutine(Wait());

        audioS.clip = soundShoot;
        audioS.Play();

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Instantiate(broken, transform.position, transform.rotation);
            GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(e, 3);
            Destroy(gameObject);
        }
    }

    public void PlusLife(int damage)
    {
        if (currentHealth < 10)
        {
            currentHealth = Mathf.Clamp(currentHealth + damage, 0, 10);

            rend.sharedMaterial = materialLife;
            StartCoroutine(Wait());

            audioS.clip = soundLife;
            audioS.Play();

            healthBar.SetHealth(currentHealth);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBarrelRolling && !godMode)
        {
            if (other.tag == "BalaTorreta")
            {
                TakeDamage(1);
            }

            if (other.tag == "Recuperador")
            {
                PlusLife(4);
                Destroy(other.gameObject);
            }

            if (other.tag == "Asteroid")
            {
                
                TakeDamage(1);
            }

            if (other.tag == "Suicida")
            {
                
                TakeDamage(1);
            }

            if (other.tag == "SatFlame")
            {
                TakeDamage(10);
            }

            if (other.tag == "Turret")
            {
                TakeDamage(1);
            }
        }
    }
}