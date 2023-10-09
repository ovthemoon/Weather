using UnityEngine;

public class Portal : MonoBehaviour
{
    public static Transform exitPortal;

    void Start()
    {
        // 아이디어: 스피어2로 생성된 포탈을 출구 포탈로 설정
        if (this.gameObject.name.Contains("Portal(Clone)") && !exitPortal)
        {
            exitPortal = this.transform;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 다른 오브젝트가 포탈에 닿았을 때, exitPortal로 이동
        if (exitPortal)
        {
            other.transform.position = exitPortal.position + exitPortal.forward * 2;
        }
    }
}
