using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    float inputx;
    float inputz;

    public float speed=1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputx = Input.GetAxis("Horizontal");
        inputz = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(inputx, 0, inputz)*speed*Time.deltaTime);
    }
}
