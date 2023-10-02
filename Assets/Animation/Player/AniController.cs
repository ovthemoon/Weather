using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniController : MonoBehaviour
{
    private Rigidbody rb;
    Animator animator;

    bool isWalking;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            isWalking = true;
        }
        else
            isWalking = false;
        animator.SetBool("IsWalking", isWalking);
    }
}
