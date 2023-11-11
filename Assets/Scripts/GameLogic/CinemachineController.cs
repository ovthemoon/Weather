using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using LaserAssetPackage.Scripts.Laser.Scripting;

public class CinemachineController : MonoBehaviour
{
    public ScriptingExample ScriptingExample;
    public CinemachineVirtualCamera cam;
    bool isplayed = false;
    public GameObject button;
    public GameObject magic;
    public MaterialController[] materialController;

    // Start is called before the first frame update
    void Start()
    {
        cam.Priority = 9;
    }

    // Update is called once per frame
    void Update()
    {
        if(ScriptingExample.iscomplete && !isplayed)
        {
            StartCoroutine(CinemachineAnimation());
        }
    }

    IEnumerator CinemachineAnimation()
    {
        isplayed = true;
        cam.Priority = 11;
        yield return new WaitForSeconds(2.5f);

        button.SetActive(true);
        magic.SetActive(true);

        for(int i = 0; i < materialController.Length; i++)
        {
            materialController[i].StartFadeTo();
        }
        

        yield return new WaitForSeconds(2f);
        cam.Priority = 9;
        
    }
}
