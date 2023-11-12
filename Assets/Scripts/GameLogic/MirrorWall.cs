using UnityEngine;
using System.Collections;
using Cinemachine;

public class MirrorWall : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera1; // 첫 번째 버추얼 카메라
    public CinemachineVirtualCamera virtualCamera2; // 두 번째 버추얼 카메라
    public float transitionTime = 2.0f; // 카메라 전환 시간

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어와의 충돌 감지
        {
            // 첫 번째 버추얼 카메라를 비활성화하고 두 번째를 활성화합니다.
            StartCoroutine(SmoothTransition(virtualCamera2,11));
        }
    }

    private IEnumerator SmoothTransition(CinemachineVirtualCamera cam,int pri)
    {
        int tmp=cam.Priority;
        cam.Priority=pri;
        yield return new WaitForSeconds(transitionTime);
        cam.Priority=tmp;



        // float startTime = Time.time;

        // while (Time.time - startTime <= duration)
        // {
        //     timeElapsed = Time.time - startTime;
        //     float t = timeElapsed / duration;

        //     // 부드러운 전환을 위해 Lerp 함수를 사용하여 우선순위를 조절합니다.
        //     fromCam.Priority
        //     toCam.Priority
        //     yield return null;
        // }

        // // 전환 완료 후 최종적으로 우선순위 설정
        // fromCam.Priority = 0;
        // toCam.Priority = 10;
    }
}
