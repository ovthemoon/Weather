using UnityEngine;
using System.Collections;

public class PortalPad : MonoBehaviour
{
    GameObject[] portals;
    bool isCooldown = false;

    void Start()
    {
        // "Portal" 태그가 붙은 모든 발판을 찾습니다.
        portals = GameObject.FindGameObjectsWithTag("Pad");
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어의 이름이 'PlayerAlice'인지 확인하고, 쿨다운 중이 아닌지 확인합니다.
        if (other.CompareTag("Player") && !isCooldown)
        {
            // 다른 "Portal"로 이동하는 함수를 호출합니다.
            TeleportPlayer(other.gameObject);
            // 쿨다운을 시작합니다.
            StartCoroutine(Cooldown());
        }
    }

    void TeleportPlayer(GameObject player)
    {
        foreach (var portal in portals)
        {
            // 현재 발판을 제외하고, 플레이어를 다른 발판으로 이동시킵니다.
            if (portal != gameObject)
            {
                // 발판의 위치에 플레이어를 순간 이동시킵니다.
                player.transform.position = portal.transform.position;
                break; // 첫 번째 찾은 다른 발판으로만 이동하면 됩니다.
            }
        }
    }

    // 쿨다운을 위한 코루틴입니다.
    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(3f); // 3초 대기
        isCooldown = false;
    }
}
