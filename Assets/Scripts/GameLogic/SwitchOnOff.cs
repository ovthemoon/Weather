using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnOff : MonoBehaviour
{
    public ControllableObject[] boxes;
    public Vector3[] moveDirections;  // 각 박스의 이동 방향

    private Animator animator;
    private bool isOn=false;
    private void Start()
    {
        animator=GetComponent<Animator>();

        // 각 박스에 이동 방향 지정
        for (int i = 0; i < boxes.Length && i < moveDirections.Length; i++)
        {
            boxes[i].moveDirection = moveDirections[i];
        }


    }
    private void OnMouseDown()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[0].isMoving)
                return;
        }
        
        Debug.Log(isOn);
        isOn = !isOn;
        animator.SetBool("IsOn", isOn);
        for(int i=0; i<boxes.Length; i++)
        {
            boxes[i].ToggleState();
        }
    }

}
