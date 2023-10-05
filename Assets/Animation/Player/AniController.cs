using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ʼ� ������Ʈ
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class AniController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private CharacterMove characterMove;


    // Start is called before the first frame update
    void Start()
    {
        characterMove=GetComponent<CharacterMove>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveStateCheck();
        JumpStateCheck();
        
    }
    void MoveStateCheck()
    {
        if (characterMove.GetMovingState())
        {

            animator.SetBool("IsWalking", true);
            animator.SetFloat("HorizontalSpeed", Mathf.Clamp(characterMove.GetDirectionVector().x, -1, 1));
            animator.SetFloat("VerticalSpeed", Mathf.Clamp(characterMove.GetDirectionVector().z, -1, 1));

        }
        else
            animator.SetBool("IsWalking", false);
    }
    void JumpStateCheck()
    {
        
        animator.SetBool("IsGround",characterMove.GetGroundState());
        animator.SetFloat("VelocityY", rb.velocity.y);
    }
    
}
