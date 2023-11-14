using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUIControllerScript : MonoBehaviour
{
    Collider col;
    UIControllerScript ui;
    private void Start()
    {
        col=GetComponent<Collider>();
        ui=GetComponent<UIControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (col.isTrigger)
        {
            ui.enabled = false;
        }
        else
        {
            ui.enabled = true;
        }
    }
}
