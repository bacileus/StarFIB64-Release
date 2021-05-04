using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    public Transform rocketText;
    public Transform rocket1;
    public Transform rocket2;
    public Transform rocket3;
    public Transform rocket4;

    public Cohete cohete;

    void Update()
    {
        float remainingRockets = cohete.remainingRockets;

        if (remainingRockets > 0)
        {
            rocket1.GetComponent<Image>().enabled = true;
        }
        else
        {
            rocket1.GetComponent<Image>().enabled = false;
        }

        if (remainingRockets > 1)
        {
            rocket2.GetComponent<Image>().enabled = true;
        }
        else
        {
            rocket2.GetComponent<Image>().enabled = false;
        }

        if (remainingRockets > 2)
        {
            rocket3.GetComponent<Image>().enabled = true;
        }
        else
        {
            rocket3.GetComponent<Image>().enabled = false;
        }

        if (remainingRockets > 3)
        {
            rocket4.GetComponent<Image>().enabled = true;
        }
        else
        {
            rocket4.GetComponent<Image>().enabled = false;
        }
            
        rocketText.GetComponent<Text>().text = remainingRockets.ToString();
    }
}
