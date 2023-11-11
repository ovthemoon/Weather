using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pol_PressButton : MonoBehaviour
{
    [SerializeField] private Animator Buttonanimator;
    [SerializeField] private GameObject door;
    [SerializeField] private Transform target;

    private void Start()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        Buttonanimator.SetBool("Down", true);
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.green;

        Vector3 a = door.transform.position;
        Vector3 b = target.position;
        door.transform.position = Vector3.Lerp(a, b, 0.1f);

    }

    private void OnCollisionExit(Collision collision)
    {
        Buttonanimator.SetBool("Down", false);
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.red;

        Vector3 a = door.transform.position;
        Vector3 b = target.position;
        door.transform.position = Vector3.Lerp(b, a, 0.5f);


    }
}
