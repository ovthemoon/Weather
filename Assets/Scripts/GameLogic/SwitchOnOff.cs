using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnOff : MonoBehaviour
{
    public ControllableObject[] boxes;
    private Animator animator;
    private bool isOn=false;
    private void Start()
    {
        animator=GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i].isMoving)
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
