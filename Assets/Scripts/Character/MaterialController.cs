using DG.Tweening;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _renderer;
    private Material _material;
    public float fadeTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("StartMarte");
        if (_renderer == null) // Make sure the renderer is not null
        {
            _renderer = GetComponent<SkinnedMeshRenderer>();
        }
        
        if (_renderer != null) // If the renderer is found
        {
            _material = _renderer.sharedMaterial; // Get the material from the renderer
            _material.SetFloat("_SplitValue", -0.1f); // Initialize the _SplitValue
        }
        else
        {
            Debug.LogError("Renderer not found!", this);
        }
    }

    
    public void StartFadeTo()
    {
        Debug.Log("It's working");
        float to = 1; // Define the target value for _SplitValue
        // Start the fade
        FadeSplitValue(_material.GetFloat("_SplitValue"), to);
    }
    

    public void FadeSplitValue(float from, float to)
    {
        if (_material != null)
        {
            DOTween.To(() => _material.GetFloat("_SplitValue"), x => _material.SetFloat("_SplitValue", x), to, fadeTime);
        }
        else
        {
            Debug.LogError("Material is not assigned", this);
        }
    }
}
