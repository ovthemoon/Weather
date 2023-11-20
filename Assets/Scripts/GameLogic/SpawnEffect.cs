using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
     private Material _material;
    public float fadeTime=2f;
    // Start is called before the first frame update
    void Start()
    {
        _material = _renderer.material;
        FadeSplitValue(0f, 2f);
    }

    
    public void FadeSplitValue(float from, float to)
    {
        DOTween.To(() => from, x => {
            _material.SetFloat("_SplitValue", x);
            from = x;
        }, to, fadeTime);
    }
}
