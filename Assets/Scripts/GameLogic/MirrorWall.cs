using UnityEngine;
using System.Collections;
using Cinemachine;

public class MirrorWall : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera1; // 첫 번째 버추얼 카메라
    public CinemachineVirtualCamera virtualCamera2; // 두 번째 버추얼 카메라
    public float transitionTime = 2.0f; // 카메라 전환 시간

    private void OnMouseDown()
    {
        // 마우스 왼쪽 버튼을 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 첫 번째 버추얼 카메라를 비활성화하고 두 번째를 활성화합니다.
            StartCoroutine(SmoothTransition(virtualCamera2, 11));
        }
    }

    private IEnumerator SmoothTransition(CinemachineVirtualCamera cam, int pri)
    {
        int tmp = cam.Priority;
        cam.Priority = pri;
        yield return new WaitForSeconds(transitionTime);
        cam.Priority = tmp;
    }
}
