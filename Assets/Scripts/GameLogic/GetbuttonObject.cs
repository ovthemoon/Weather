using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetbuttonObject : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject obj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "MagicAlice")
                {
                    obj.SetActive(true);
                }

            }
        }
    }
}
