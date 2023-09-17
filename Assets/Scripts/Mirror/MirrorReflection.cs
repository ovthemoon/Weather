using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
    public Vector3 rotationOffset = new Vector3(0, 180, 0);
    GameObject player;
    private Vector3 mirrorNormal;
    private Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        cameraTransform = this.transform.GetChild(0).gameObject.transform;
        if(player == null)
        {
            Debug.Log("Player is not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPlayer = player.transform.position - this.transform.position;
        Vector3 mirrorRelativePosition=this.transform.InverseTransformPoint(player.transform.position);
        mirrorRelativePosition.z = -mirrorRelativePosition.z;
        Vector3 worldPosition=this.transform.TransformPoint(mirrorRelativePosition);
        cameraTransform.LookAt(worldPosition);
        cameraTransform.rotation *= Quaternion.Euler(rotationOffset);
    }
}
