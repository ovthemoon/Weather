
using UnityEngine;

public class ButtonTeleport : MonoBehaviour
{
    public Transform button2Transform; // 버튼2의 위치 (Transform 컴포넌트)

    private void OnCollisionEnter(Collision collision)
    {
        
        GameObject collidingObject = collision.gameObject;

        // 플레이어 또는 이동 가능한 오브젝트와의 충돌을 감지
        if (collidingObject.CompareTag("Player") || collidingObject.CompareTag("MovableObject"))
        {
            collidingObject.transform.position = button2Transform.position;
            // 플레이어의 물리 효과 초기화 (필요한 경우)
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
       
    }

}
