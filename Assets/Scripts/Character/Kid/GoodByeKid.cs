using System.Collections;
using UnityEngine;

public class GoodByeKid : MonoBehaviour
{
    public Animator targetAnimator; // 애니메이션을 재생할 대상 오브젝트의 Animator 컴포넌트

    void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 플레이어인지 확인합니다.
        if (other.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌했을 때, 대상 오브젝트의 애니메이션을 재생합니다.
            if (targetAnimator != null)
            {
                targetAnimator.SetTrigger("Bye");
            }

        }
    }

}
