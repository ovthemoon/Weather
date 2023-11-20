using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    Animator animator;
    public bool isActive = false;
    public Transform objTarget;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnAnimatorIK()
    {
        if (isActive)
        {
            Debug.Log("It's Working");
            if (objTarget != null)
            {
                animator.SetLookAtWeight(1,0,1);
                animator.SetLookAtPosition(objTarget.position);
            }
        }
        else
        {
            animator.SetLookAtWeight(0);
        }
    }
}
