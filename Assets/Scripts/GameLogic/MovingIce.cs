using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Ice;
    public Transform target;
    private Animator animator;
    private bool isOn = false;
    public float speed;

    private void Start()
    {
        animator = GetComponent<Animator>();

        

    }

    private void OnMouseDown(){
        Debug.Log(isOn);
        isOn = !isOn;
        animator.SetBool("IsOn", isOn);

        Vector3 a = Ice.transform.position;
        Vector3 b = target.position;
        Ice.transform.position = Vector3.Lerp(a, b, speed);
    }
}
