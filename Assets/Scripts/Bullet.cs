using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 40f;

    // Update is called once per frame
    void Update()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Player");

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direccion = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if (direccion.magnitude <= distance)
        {

            Player player = enemy.GetComponent<Player>();
            player.TakeDamage(1);
            return;
        }

        transform.Translate(direccion.normalized * distance, Space.World);

    }

    public void buscadorTarget(Transform targetEnemy)
    {
        target = targetEnemy;
    }
}
