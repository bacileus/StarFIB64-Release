using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject w1, w2, w3, w4;
    public CinemachineDollyCart cdc;

    public float distance1, distance2, distance3;
    
    // Start is called before the first frame update
    void Start()
    {
        w1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (cdc.m_Position > distance1 && !w2.activeInHierarchy)
            {
                w2.SetActive(true);
            }
            if (cdc.m_Position > distance2 && !w3.activeInHierarchy)
            {
                w3.SetActive(true);
            }
            if (w4 != null & cdc.m_Position > distance3 && !w4.activeInHierarchy)
            {
                w4.SetActive(true);
            }
        } else
        {
            w1.SetActive(false);
            w2.SetActive(false);
            w3.SetActive(false);
        }
    }
}
