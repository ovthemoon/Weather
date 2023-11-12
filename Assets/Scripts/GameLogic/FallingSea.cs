using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSea : MonoBehaviour
{
    Renderer rend;
    Material material;
    public Transform Target;
    public Vector2 scrollSpeed = new Vector2(0.1f, 0.1f);
    private void Start()
    {
        rend = GetComponent<Renderer>();
        material = rend.material;
    }
    private void Update()
    {
        if (material != null)
        {
            Vector2 offset = material.mainTextureOffset;

            // Update the offset based on scroll speed and time
            offset.x += scrollSpeed.x * Time.deltaTime;
            offset.y += scrollSpeed.y * Time.deltaTime;

            // Apply the updated offset to the material
            material.mainTextureOffset = offset;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.position = Target.position;
        }
    }
}
