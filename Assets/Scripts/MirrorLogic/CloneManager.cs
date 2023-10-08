using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class CloneManager : MonoBehaviour
{
    //attach this script to mirror which will make a clone in front of
    //it would need a collider to detect a clone
    public Transform mirrorSurface;
    //public GameObject playerClonePrefab;
    public RuntimeAnimatorController animatorClonePrefab;

    public string targetLayerName = "StencilLayer1";
    private GameObject playerClone;
    private Vector3 mirrorNormal;
    private Dictionary<GameObject, GameObject> reflectedObjects=new Dictionary<GameObject, GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
       mirrorNormal = mirrorSurface.forward;
    }
    private void ClonePosition(Collider other,GameObject reflectedObj)
    {
        Vector3 directionToOriginal = other.transform.position - mirrorSurface.position;
        

        // 방향 벡터를 거울의 정규 벡터에 대해 반사시킵니다.
        Vector3 reflectedDirection = Vector3.Reflect(directionToOriginal, mirrorNormal);

        // 반사된 위치를 계산합니다.
        Vector3 reflectedPosition = mirrorSurface.position + reflectedDirection.normalized * directionToOriginal.magnitude;
        reflectedObj.transform.position = reflectedPosition;
    }
    private void CloneRotation(Collider other,GameObject reflectedObj)
    {
        Vector3 reflectedForward = Vector3.Reflect(other.transform.forward, mirrorNormal);
        Vector3 reflectedUp = Vector3.Reflect(other.transform.up, mirrorNormal);
        reflectedObj.transform.rotation = Quaternion.LookRotation(reflectedForward, reflectedUp);
    }
    public void ChangeLayer(Transform other)
    {
        int layerIndex = LayerMask.NameToLayer(targetLayerName);

        if (layerIndex == -1) // 유효한 레이어 이름인지 확인
        {
            Debug.LogWarning("Layer " + targetLayerName + " does not exist!");
            return;
        }
        
        ChangeLayerRecursively(other,layerIndex);
    }
    void ChangeLayerRecursively(Transform trans, int layer)
    {
        trans.gameObject.layer = layer;
        
        foreach (Transform child in trans)
        {
            ChangeLayerRecursively(child, layer);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
         if (!reflectedObjects.ContainsKey(other.gameObject)&& !other.gameObject.layer.Equals("stencilLayer1"))
         {
            GameObject cloneObject = Instantiate(other.gameObject);
            DeleteChildrenWithComponent(cloneObject.transform);
            ChangeLayer(cloneObject.transform);
            reflectedObjects[other.gameObject] = cloneObject;
         }


    }
    void DeleteChildrenWithComponent(Transform parentTransform)
    {
        if (parentTransform.GetComponent<CharacterMove>())
        {
            Destroy(parentTransform.GetComponent<CharacterMove>());
        }
        if (parentTransform.GetComponent<AniController>())
        {
            Destroy(parentTransform.GetComponent<AniController>());
        }
        if (parentTransform.GetComponent<Collider>())
        {
            Destroy(parentTransform.GetComponent<Collider>());
        }
        foreach (Transform child in parentTransform)
        {
            if (child.GetComponent<Camera>())
            {
                Destroy(child.gameObject);
            }
            if (child.GetComponent<PickObject>())
            {
                Destroy(child.gameObject.GetComponent<PickObject>());
            }
           
            if (child.GetComponent<Collider>())
            {
                Destroy(child.gameObject.GetComponent<Collider>());
            }
        }
    }
    private void SyncAnimationAndReflect(GameObject original, GameObject clone)
    {
        Animator originalAnimator = original.GetComponent<Animator>();
        Animator cloneAnimator = clone.GetComponent<Animator>();

        if (originalAnimator && cloneAnimator)
        {
            // 애니메이션 동기화
            cloneAnimator.runtimeAnimatorController = originalAnimator.runtimeAnimatorController;
            cloneAnimator.Play(originalAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 
                originalAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);

            // Z축을 반전하여 반사 효과 적용
            //Vector3 reflectedScale = clone.transform.localScale;
            //reflectedScale.z *= -1;
            //clone.transform.localScale = reflectedScale;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if(reflectedObjects.TryGetValue(other.gameObject,out GameObject reflectedObj))
        {
            SyncAnimationAndReflect(other.gameObject, reflectedObj);
            ClonePosition(other,reflectedObj);
            CloneRotation(other, reflectedObj);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (reflectedObjects.TryGetValue(other.gameObject, out GameObject reflectedObj))
        {
            Destroy(reflectedObj);
            reflectedObjects.Remove(other.gameObject);
        }
    }
}
