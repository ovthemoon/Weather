using System.Collections;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public Animator targetAnimator; // 애니메이션을 재생할 대상 오브젝트의 Animator 컴포넌트
    public Transform spawnPoint; // 플레이어를 이동시킬 스폰 포인트의 Transform

    [SerializeField] private AudioSource audioSource; // 오디오 소스 추가
    [SerializeField] private AudioClip animationAudioClip; // 재생할 오디오 클립 추가

    void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 플레이어인지 확인합니다.
        if (other.gameObject.CompareTag("Player"))
        {
             // 애니메이션 재생 시 오디오 재생
            if (audioSource != null && animationAudioClip != null)
            {
                audioSource.PlayOneShot(animationAudioClip);
            }
            // 플레이어와 충돌했을 때, 대상 오브젝트의 애니메이션을 재생합니다.
            if (targetAnimator != null)
            {
                targetAnimator.SetTrigger("Attack");

            }

            // 3초 후에 플레이어를 스폰 포인트로 이동시키기 위해 코루틴을 시작합니다.
            StartCoroutine(MovePlayerAfterDelay(other.gameObject));
        }
    }

    IEnumerator MovePlayerAfterDelay(GameObject player)
    {
        // 3초 동안 대기합니다.
        yield return new WaitForSeconds(1.3f);

        // 플레이어의 위치를 스폰 포인트로 이동시킵니다.
        player.transform.position = spawnPoint.position;
    }
}
