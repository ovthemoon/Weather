using UnityEngine;
using System.Collections;

public class PortalTouch : MonoBehaviour
{
    public GameObject linkedPad; // 이 발판과 연결된 대상 발판
    public bool isStartPad = false; // 이 발판이 스타트 발판인지 나타내는 플래그
    bool isCooldown = false;

    void Update()
    {
        // 마우스 왼쪽 버튼을 눌렀는지 체크합니다.
        if (Input.GetMouseButtonDown(0) && !isCooldown && isStartPad && linkedPad != null)
        {
            // 레이캐스트를 사용하여 클릭된 오브젝트가 이 스크립트가 붙어 있는 오브젝트인지 확인합니다.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                // 플레이어를 순간 이동시키는 함수를 호출합니다.
                TeleportPlayer(GameObject.FindGameObjectWithTag("Player"));
                // 쿨다운을 시작합니다.
                StartCoroutine(Cooldown());
            }
        }
    }

    void TeleportPlayer(GameObject player)
    {
        // 연결된 발판의 위치로 플레이어를 순간 이동시킵니다.
        player.transform.position = linkedPad.transform.position + new Vector3(0, 1, 0); // 플레이어가 발판 바로 위에 위치하도록 조정합니다.
    }

    // 쿨다운을 위한 코루틴입니다.
    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(3f); // 3초 대기
        isCooldown = false;
    }
}
