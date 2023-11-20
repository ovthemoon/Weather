
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject portal;

    //Mesh 변경 추가
    [SerializeField] private Material activeMaterial; // 활성화될 때 적용할 Material
    

    private Material originalMaterial; // 원래의 Material을 저장할 변수
    private MeshRenderer portalRenderer; // 포털의 MeshRenderer 컴포넌트를 캐싱할 변수

    public bool IsPressed { get; internal set; }

    private void Start()
    {
        // 포털의 MeshRenderer 컴포넌트를 가져옵니다.
        portalRenderer = portal.GetComponent<MeshRenderer>();

        // 원래의 Material을 저장합니다.
        if (portalRenderer != null)
        {
            originalMaterial = portalRenderer.material;
        }
      
    }

    private void OnCollisionStay(Collision collision)
    {
        animator.SetBool("Down", true);
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.green;
        Debug.Log("버튼 눌림");

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
        Debug.Log("버튼 해제됨");

        portal.GetComponent<Collider>().isTrigger = false;

        // // 포털의 Material을 원래대로 돌립니다.
        if (portalRenderer != null)
        {
            portalRenderer.material = originalMaterial;
        }
    }
}

