using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEffectShooter : MonoBehaviour
{
    public GameObject testEffect;
    public Vector3 spawnVelocity = new Vector3(0, 0, 10);
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
       rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(testEffect,transform.position,transform.rotation);
            rb.velocity=spawnVelocity;
        }
    }
}
