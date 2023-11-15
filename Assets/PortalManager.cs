using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public GameObject portalToDesert;
    public GameObject portalToPole;

    // Start is called before the first frame update
    void Start()
    {
        portalToDesert.SetActive(!GameManager.map2_DesertComplete);
        portalToPole.SetActive(!GameManager.map2_PoleComplete);
    }

    
}
