using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenCleared : MonoBehaviour
{
    public string[] textArr;
    UIControllerScript uiControl;
    // Start is called before the first frame update
    void Start()
    {
        uiControl = GetComponent<UIControllerScript>();
        if (GameManager.map2_DesertComplete && GameManager.map2_PoleComplete)
        {
            if (GetComponent<PlaygroundControl>()!=null)
            {
                Destroy(GetComponent<PlaygroundControl>());
            }
            uiControl.textArr= textArr;
        }
    }

   
}
