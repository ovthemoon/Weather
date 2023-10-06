using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorEffectReflection : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        
        Vector3 quadGlobalNormal = GetGlobalNormal(this.transform);
        Debug.Log(quadGlobalNormal);
    }
    Vector3 GetGlobalNormal(Transform quadTransform)
    {
        // 로컬 노말 정의 (Quad의 경우)
        Vector3 localNormal = this.transform.forward;

        // 로컬 노말을 글로벌 노말로 변환
        Vector3 globalNormal = quadTransform.TransformDirection(localNormal);

        return globalNormal;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Effect"))
        {
            Vector3 normalCol = collision.contacts[0].normal;

        }
    }
}
