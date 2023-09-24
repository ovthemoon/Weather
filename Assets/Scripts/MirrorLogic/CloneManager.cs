using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour
{
    public GameObject clonePrefab;
    GameObject clone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            Vector3 clonePosition=new Vector3(-other.transform.position.x,other.transform.position.y, other.transform.position.z);
            clone = Instantiate(clonePrefab, clonePosition,other.transform.rotation);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(clone);
        }
    }
}
