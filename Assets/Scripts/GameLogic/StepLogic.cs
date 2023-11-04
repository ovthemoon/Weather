using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepLogic : MonoBehaviour
{
    public GameObject targetWindow;
    public GameObject stencilMirror;
    public Steps[] step;
    [HideInInspector]
    public int cnt=1;

    private bool hasCompleted = false;
    
    private void Update()
    {
        if (cnt == step.Length+1&&!hasCompleted)
        {
            hasCompleted = true;
            Debug.Log("Completion");
            targetWindow.SetActive(true);
            stencilMirror.SetActive(false);
        }
    }
    
    public void ResetPuzzle()
    {
        cnt = 1;
       for(int i=0; i<step.Length; i++)
        {
            step[i].IsPressed = false;
            step[i].ColorReset();

        }
    }
    public void DebugThings()
    {
        for (int i = 0; i < step.Length; i++)
        {
            Debug.Log(step[i].myNum+ " " + step[i].IsPressed);

        }
    }

}
