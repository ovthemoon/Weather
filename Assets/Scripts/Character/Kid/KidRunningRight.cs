using System.Collections;
using UnityEngine;

public class KidRunningRight : MonoBehaviour
{
    public Animator targetAnimator; // 'Alice' 오브젝트의 Animator 컴포넌트
    public float runAnimationLength = 1.0f; // 'Run' 애니메이션의 길이를 초 단위로 설정
    private int runAnimationCount = 0; // 'Run' 애니메이션 반복 횟수를 추적합니다

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (targetAnimator != null)
            {
                // 애니메이션 반복 횟수를 초기화합니다.
                runAnimationCount = 0;
                // 'isRunningComplete' 파라미터를 false로 설정하여 전환 조건을 리셋합니다.
                targetAnimator.SetBool("isFinish", false);
                StartCoroutine(PlayAnimations());
            }
        }
    }

    IEnumerator PlayAnimations()
    {
        targetAnimator.SetTrigger("Right");
        yield return new WaitForSeconds(runAnimationLength); // 'Left' 애니메이션 길이만큼 대기

        targetAnimator.SetTrigger("Run");
        while (runAnimationCount < 2)
        {
            yield return new WaitForSeconds(runAnimationLength); // 'Run' 애니메이션 길이만큼 대기
            runAnimationCount++;
        }

        // 3회 반복 후 'isRunningComplete' 파라미터를 true로 설정하여 'Breathing Idle' 상태로 전환합니다.
        targetAnimator.SetBool("isFinish", true);

        // 'Breathing Idle' 애니메이션 상태로의 전환을 기다립니다.
        yield return null; // 한 프레임 대기하여 애니메이션 상태 전환을 확실하게 합니다.

        // 'Alice' 오브젝트를 삭제합니다.
        Destroy(targetAnimator.gameObject);
    }
}
