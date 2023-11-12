using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum mouseMode{
    Grab,
    Normal,
    Info
};
public class RayHoldObject : MonoBehaviour
{
    public float pickupAbleDistance = 10.0f;
    public float holdDistance = 5.0f;
    public string[] pickableTags = { "CloneOfClone","HoldableObject" };
    public Image cursorNormal;
    public Image cursorGrab;
    public Image cursorInfo;
    private GameObject pickedObject;
    private Rigidbody pickedObjectRb;


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        int layerMask = ~LayerMask.GetMask("Glass");
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 클릭했을 때
        {
             // "Glass" 레이어를 제외한 모든 레이어를 포함하는 레이어 마스크
            
            if (Physics.Raycast(ray, out hit, pickupAbleDistance, layerMask))
            {
                Debug.Log(layerMask + "Touched");
                //Debug.Log(hit.collider.gameObject.name);
                foreach (string tag in pickableTags)
                {
                    if (hit.collider.CompareTag(tag))
                    {
                        Debug.Log("working");
                        PickObject(hit.collider.gameObject);
                        break; // 태그를 찾았으므로 반복문을 빠져나옵니다.
                    }
                }
                if (hit.collider.CompareTag("Clone"))
                {

                    hit.collider.GetComponent<CopyCloneObject>().copyClone();
                    //PickObject(CopyCloneObject.cloneOfClone);
                    //offset = pickedObject.transform.position - hit.point;
                }
                
            }
            
        }
        if (Physics.Raycast(ray, out hit, pickupAbleDistance, layerMask))
        {
            if (hit.collider.GetComponent<UIControllerScript>())
            {
                cursorChange(mouseMode.Info);
            }
            else
            {
                cursorChange(mouseMode.Normal);
            }
        }
        if (pickedObject != null)
        {
            Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
            pickedObject.transform.position = Vector3.Lerp(pickedObject.transform.position, targetPosition, Time.deltaTime * 10);
            cursorChange(mouseMode.Grab);
        }


        if (Input.GetMouseButtonUp(0)) // 마우스 왼쪽 버튼을 놓았을 때
        {
            DropObject();
        }   
    }

    private void cursorChange(mouseMode mode)
    {
        cursorGrab.gameObject.SetActive(false);
        cursorNormal.gameObject.SetActive(false);
        cursorInfo.gameObject.SetActive(false);
        switch (mode)
        {
            case mouseMode.Grab:
                cursorGrab.gameObject.SetActive(true); break;
            case mouseMode.Normal:
                cursorNormal.gameObject.SetActive(true); break;
            case mouseMode.Info:
                cursorInfo.gameObject.SetActive(true); break;
            default:
                break;

        }
    }
    private void PickObject(GameObject obj)
    {
        pickedObject = obj;
        pickedObjectRb = obj.GetComponent<Rigidbody>();
        
        if (pickedObjectRb != null)
        {
            pickedObjectRb.isKinematic = true;
        }
    }

    private void DropObject()
    {
        if (pickedObjectRb != null)
        {
            pickedObjectRb.isKinematic = false;
            pickedObjectRb.useGravity = true;
            pickedObjectRb.velocity = Vector3.zero;
            pickedObjectRb.angularVelocity = Vector3.zero;
        }
        pickedObject = null;
        pickedObjectRb = null;


    }
   
}
