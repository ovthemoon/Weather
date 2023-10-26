using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    public int myNum;
    public bool IsPressed=false;
    StepLogic stepLogic;
    [SerializeField]
    Renderer targetRenderer;

    Color basicColor;
    private void Start()
    {
        stepLogic = GetComponentInParent<StepLogic>();
        basicColor=targetRenderer.material.color;
    }
    public void ColorReset()
    {
        targetRenderer.material.color = basicColor;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (IsPressed) return;
            if (stepLogic.cnt == myNum)
            {
                stepLogic.cnt++;
                Debug.Log("Correct");
                IsPressed = true;
                targetRenderer.material.color = Color.green;
            }
            else
            {
                Debug.Log("Wrong");
                stepLogic.ResetPuzzle();
                Debug.Log("Reset Complete");
                stepLogic.DebugThings();
            }
        }
        
    }
}
