using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    
    GameObject item;
    public GameObject tempParent;
    public Transform guide;
    public float maxDistance = 3f;


    private void Start()
    {
        item = this.gameObject;
        item.GetComponent<Rigidbody>().useGravity = true;
    }


    
    private void OnMouseDown()
    {
        //if(Vector3.Distance(alice.transform.position, item.transform.position) <= 10f)

        float distanceToCharacter = Vector3.Distance(transform.position, guide.position);

        if(distanceToCharacter <= maxDistance)
        {
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.transform.position = guide.transform.position;
            item.transform.rotation = guide.transform.rotation;
            item.transform.parent = tempParent.transform;
        }   
        
    }

    private void OnMouseUp()
    {
        float distanceToCharacter = Vector3.Distance(transform.position, guide.position);

        if (distanceToCharacter <= maxDistance)
        {

            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.transform.parent = null;
            item.transform.position = guide.transform.position;
        }
    }
   
    


}
