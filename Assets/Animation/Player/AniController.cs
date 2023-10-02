using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniController : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetTrigger("walk");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger("walk");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetTrigger("walk");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetTrigger("walk");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jumping");
        }
    }
}
