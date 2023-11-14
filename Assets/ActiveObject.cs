using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : MonoBehaviour
{
    public GameObject brokenGlass1;
    public GameObject brokenGlass2;
    public GameObject stecilMirror;
    public Collider wall;
    // Start is called before the first frame update
    void Start()
    {
        brokenGlass1.SetActive(GameManager.map2_DesertComplete);
        brokenGlass1.SetActive(GameManager.map2_PoleComplete);

        if(GameManager.map2_DesertComplete&& GameManager.map2_PoleComplete)
        {
            stecilMirror.SetActive(true);
            wall.isTrigger = true;
            gameObject.SetActive(false);
            
        }
    }
}
