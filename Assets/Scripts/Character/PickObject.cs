using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    GameObject item;
    public GameObject tempParent;
    public Transform guide;
    public float maxDistance = 3f;
    private bool isHolding = false;

    private void Start()
    {
        item = this.gameObject;
        item.GetComponent<Rigidbody>().useGravity = true;
    }

    private void Update()
    {
        // 마우스를 누르고 있는 동안에만 아이템을 이동시킵니다.
        if (isHolding)
        {
            Vector3 moveDirection = guide.position - item.transform.position;
            item.GetComponent<Rigidbody>().velocity = moveDirection * 10; // 이동 속도를 조정할 수 있습니다.
        }
    }
    
    private void OnMouseDown()
    {
        float distanceToCharacter = Vector3.Distance(transform.position, guide.position);

        if(distanceToCharacter <= maxDistance)
        {
            isHolding = true;
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous; // 연속적 충돌 검사
        }   
        
    }

    private void OnMouseUp()
    {
        if (isHolding)
        {
            isHolding = false;
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete; // 기본 충돌 검사
            item.GetComponent<Rigidbody>().velocity = Vector3.zero; // 속도를 초기화합니다.
        }
    }
}
