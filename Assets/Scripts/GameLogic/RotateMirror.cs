using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMirror : MonoBehaviour
{
    float rotationSpeed = 5f;

    private void OnMouseDrag()
    {
        float y = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + y, 0);
    }
}
