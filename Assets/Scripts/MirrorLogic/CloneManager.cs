using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.UI.Image;
using Cinemachine;

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
        

        // ���� ���͸� �ſ��� ���� ���Ϳ� ���� �ݻ��ŵ�ϴ�.
        Vector3 reflectedDirection = Vector3.Reflect(directionToOriginal, mirrorNormal);

        // �ݻ�� ��ġ�� ����մϴ�.
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

        if (layerIndex == -1) // ��ȿ�� ���̾� �̸����� Ȯ��
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
        // ���� ������Ʈ�� Renderer�� ���� �׸��� ���� ������ �����մϴ�.
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.shadowCastingMode = mode;
        }

        // ��� �ڽĿ� ���ؼ��� ���� �۾��� �ݺ��մϴ�.
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
            if (child.GetComponent<CinemachineVirtualCamera>())
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
            // �ִϸ��̼� ����ȭ
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
                    // Ʈ���Ŵ� �ܼ��� ���� Ȯ���Ͽ� �ش� Ʈ���Ű� Ȱ��ȭ�Ǿ� ������ Ŭ�п��� �����մϴ�.
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
