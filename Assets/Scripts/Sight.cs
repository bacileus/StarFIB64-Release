using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    private Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(r, out hit))
        {
            if (hit.collider.gameObject.layer != 2 && hit.collider.gameObject.layer != 10) // 12 enemy
            {
                Vector3 pos = hit.point;
                transform.position = pos;
            }
        } else
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 80 - cam.transform.localPosition.z;
            Vector3 pos = cam.ScreenToWorldPoint(mousePosition);

            transform.position = pos;
        }

    }
}
