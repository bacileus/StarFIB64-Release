using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public Transform player;
    private float progress = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 400) {
            Color c = gameObject.GetComponent<Renderer>().material.color;
            progress = Mathf.Clamp(progress + 1 * Time.deltaTime, 0, 1);
            c.a = progress;
            gameObject.GetComponent<Renderer>().material.color = c;
        } else
        {
            Color c = gameObject.GetComponent<Renderer>().material.color;
            c.a = 0;
            gameObject.GetComponent<Renderer>().material.color = c;
        }
    }
}
