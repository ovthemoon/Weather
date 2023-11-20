using UnityEngine;
using System.Collections;

public class PortalPad : MonoBehaviour
{
    public GameObject linkedPad; // 이 발판과 연결된 대상 발판
    public bool isStartPad = false; // 이 발판이 스타트 발판인지 나타내는 플래그
    bool isCooldown = false;

    void OnTriggerEnter(Collider other)
    {
        // 플레이어가 'Alice' 태그를 가지고 있고, 쿨다운 중이 아니며, 이 발판이 스타트 발판인 경우에만 작동
        if (other.CompareTag("Player") && !isCooldown && isStartPad && linkedPad != null)
        {
            // 연결된 발판으로 이동하는 함수를 호출합니다.
            TeleportPlayer(other.gameObject);
            // 쿨다운을 시작합니다.
            StartCoroutine(Cooldown());
        }
    }

    void TeleportPlayer(GameObject player)
    {
        // 연결된 발판의 위치로 플레이어를 순간 이동시킵니다.
        player.transform.position = linkedPad.transform.position;
    }

    // 쿨다운을 위한 코루틴입니다.
    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(3f); // 3초 대기
        isCooldown = false;
    }
}
