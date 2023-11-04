using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButtonTest : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject portal;

    //Mesh변경 추가
    [SerializeField] private Material activeMaterial; // 활성화될 때 적용할 Material


    private Material originalMaterial; // 원래의 Material을 저장할 변수
    private MeshRenderer portalRenderer; // 포털의 MeshRenderer 컴포넌트를 캐싱할 변수
   
    private void OnCollisionStay(Collision collision)
    {
        animator.SetBool("Down", true);
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.green;
        Debug.Log("��ư ����");

        portal.GetComponent<Collider>().isTrigger = true;

        // 포털의 Material을 변경합니다.
        if (portalRenderer != null)
        {
            portalRenderer.material = activeMaterial;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        animator.SetBool("Down", false);
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.red;
        Debug.Log("��ư �ȴ���");

        portal.GetComponent<Collider>().isTrigger = false;


        // 포털의 Material을 원래대로 돌립니다.
        if (portalRenderer != null)
        {
            portalRenderer.material = originalMaterial;
        }
    }


}
