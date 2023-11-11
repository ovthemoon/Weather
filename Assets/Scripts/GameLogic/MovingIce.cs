using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    /*
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 클릭했을 때
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
   
            if (Physics.Raycast(ray, out hit)
            {
                
            }

        }
    }
    */
    

    private void OnMouseDown(){
        Debug.Log(isOn);
        isOn = !isOn;
        animator.SetBool("IsOn", isOn);

        Vector3 a = Ice.transform.position;
        Vector3 b = target.position;
        Ice.transform.position = Vector3.Lerp(a, b, speed);
    }
}
