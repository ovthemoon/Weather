using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCloneObject : MonoBehaviour
{
    public static GameObject cloneOfClone;
    public float fadeTime = 4f;
    public string materialName = "MirrorEffect";
    public string newLayerName = "Default";

    public void copyClone()
    {
        
        if (cloneOfClone == null)
        {
            cloneOfClone = Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(cloneOfClone.transform.GetComponent<CopyCloneObject>());
            ChangeLayerOfObject(cloneOfClone, newLayerName);
            Renderer cloneRenderer = cloneOfClone.GetComponent<Renderer>();
            Material loadedMaterial = Resources.Load<Material>(materialName);
            if (loadedMaterial != null)
            {
                cloneRenderer.material = loadedMaterial;
                FadeSplitValue(cloneRenderer.material, 0f, fadeTime);
            }
            cloneOfClone.tag = "CloneOfClone";
            
           
        }
        else
        {
            Renderer cloneRenderer = cloneOfClone.GetComponent<Renderer>();
            cloneOfClone.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
            FadeSplitValue(cloneRenderer.material, 0f, fadeTime);
        }


    }
    private void FadeSplitValue(Material material, float from, float to)
    {
        DOTween.To(() => from, x =>
        {
            material.SetFloat("_SplitValue", x);
            from = x;
        }, to, fadeTime);
    }
    void ChangeLayerOfObject(GameObject obj, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        if (layer != -1)
        {
            obj.layer = layer;
        }
    }
}
