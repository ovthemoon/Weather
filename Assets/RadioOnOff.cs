using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioOnOff : MonoBehaviour
{
    private string on = "라디오를 켰다.";
    private string off = "라디오를 껐다.";
    private bool isOn = true;
    private AudioSource source;
    private UIControllerScript uiControllerScript;
    private void Start()
    {
        source=GetComponent<AudioSource>();
        uiControllerScript=GetComponent<UIControllerScript>();
        
    }
    public void turnOnOff()
    {

        isOn=!isOn;
        if (isOn)
        {
            uiControllerScript.textArr[0] = on;
            source.Play();
        }
        else
        {
            uiControllerScript.textArr[0] = off;
            source.Stop();
        }
    }
}
