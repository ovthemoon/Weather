using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveStencil : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");
        transform.position += new Vector3(-x * speed * Time.deltaTime, 0, -z * speed * Time.deltaTime);
    }
}
