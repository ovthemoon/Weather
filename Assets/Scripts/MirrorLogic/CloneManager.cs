using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.UI.Image;

public class CloneManager : MonoBehaviour
{
    //attach this script to mirror which will make a clone in front of
    //it would need a collider to detect a clone
    public Transform mirrorSurface;

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
    private void SetShadowCastingMode(GameObject obj, ShadowCastingMode mode)
    {
        // 현재 오브젝트의 Renderer에 대해 그림자 생성 설정을 변경합니다.
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.shadowCastingMode = mode;
        }

        // 모든 자식에 대해서도 같은 작업을 반복합니다.
        foreach (Transform child in obj.transform)
        {
            SetShadowCastingMode(child.gameObject, mode);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
         if (!reflectedObjects.ContainsKey(other.gameObject)&& other.gameObject.layer != LayerMask.NameToLayer("StencilLayer1")&&!other.gameObject.CompareTag("CloneOfClone"))
         {
            GameObject cloneObject = Instantiate(other.gameObject);
            //복사된 객체의 그림자 제거
            SetShadowCastingMode(cloneObject, ShadowCastingMode.Off);
            if (!cloneObject.GetComponent<Animator>())
            {
                cloneObject.tag = "Clone";
                cloneObject.AddComponent<CopyCloneObject>();
            }
            
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
        }
    }
    private void SyncAnimatorParameters(GameObject original, GameObject clone)
    {
        Animator originalAnimator = original.GetComponent<Animator>();
        Animator cloneAnimator = clone.GetComponent<Animator>();
        if (originalAnimator && cloneAnimator)
        {
            foreach (AnimatorControllerParameter parameter in originalAnimator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Float)
                {
                    if (parameter.name.Equals("HorizontalSpeed"))
                    {
                        cloneAnimator.SetFloat(parameter.name, -originalAnimator.GetFloat(parameter.name)); continue;
                    }
                    cloneAnimator.SetFloat(parameter.name, originalAnimator.GetFloat(parameter.name));
                }
                else if (parameter.type == AnimatorControllerParameterType.Int)
                {
                    cloneAnimator.SetInteger(parameter.name, originalAnimator.GetInteger(parameter.name));
                }
                else if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    cloneAnimator.SetBool(parameter.name, originalAnimator.GetBool(parameter.name));
                }
                else if (parameter.type == AnimatorControllerParameterType.Trigger)
                {
                    // 트리거는 단순히 값을 확인하여 해당 트리거가 활성화되어 있으면 클론에도 설정합니다.
                    if (originalAnimator.GetBool(parameter.name))
                    {
                        cloneAnimator.SetTrigger(parameter.name);
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if(reflectedObjects.TryGetValue(other.gameObject,out GameObject reflectedObj))
        {
            SyncAnimatorParameters(other.gameObject, reflectedObj);
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
    public void DeleteAllClones()
    {
        foreach (var clone in reflectedObjects.Values)
        {
            if (clone != null)
            {
                Destroy(clone);
            }
        }
        reflectedObjects.Clear();
    }
}
