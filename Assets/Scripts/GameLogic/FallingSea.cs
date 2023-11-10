using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSea : MonoBehaviour
{

    public Transform Target;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.position = Target.position;
        }
    }
}
