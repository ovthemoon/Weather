using System.Collections;
using UnityEngine;


public class KidInHouseMap2 : MonoBehaviour
{
    public Animator targetAnimator; // 애니메이션을 재생할 대상 오브젝트의 Animator 컴포넌트

    void Update()
    {
        // 마우스 왼쪽 버튼을 클릭하면
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 포인터 아래의 오브젝트를 감지합니다.
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // 클릭된 오브젝트가 이 스크립트가 붙어 있는 오브젝트인지 확인합니다.
                if (hit.transform.gameObject == this.gameObject)
                {
                    // 대상 오브젝트의 'Attack' 애니메이션을 재생합니다.
                    if (targetAnimator != null)
                    {
                        targetAnimator.SetTrigger("Act");
                    }
                }
            }
        }
    }
}
