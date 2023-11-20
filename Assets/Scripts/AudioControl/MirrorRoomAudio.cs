using UnityEngine;

public class MirrorRoomAudio : MonoBehaviour
{
    [SerializeField] private AudioClip newBackgroundMusic; // 새로 재생할 배경음악 클립
    [SerializeField] private AudioClip originalBackgroundMusic; // 원래 배경음악 클립

    private bool isInsideTrigger = false; // 플레이어가 트리거 내부에 있는지 여부
    private float cooldown = 2.0f; // 쿨다운 시간
    private float lastTriggerTime = -2.0f; // 마지막 트리거 활성화 시간

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= lastTriggerTime + cooldown)
        {
            lastTriggerTime = Time.time;
            isInsideTrigger = !isInsideTrigger;

            AudioClip clipToPlay = isInsideTrigger ? newBackgroundMusic : originalBackgroundMusic;
            if (clipToPlay != null)
            {
                AudioManager.instance.ChangeBackgroundMusic(clipToPlay);
            }
        }
    }
}
