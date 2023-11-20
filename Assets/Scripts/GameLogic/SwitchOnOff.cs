using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnOff : MonoBehaviour
{
    public ControllableObject[] boxes;
    private Animator animator;
    public bool isOn=false;

    [SerializeField] private AudioSource audioSource; // 오디오 소스 추가
    [SerializeField] private AudioClip leverSound; // 재생할 오디오 클립 추가
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

         // 오디오 재생
        if (audioSource != null && leverSound != null)
        {
            audioSource.PlayOneShot(leverSound);
        }


        for(int i=0; i<boxes.Length; i++)
        {
            boxes[i].ToggleState();
        }
    }

}
