using UnityEngine;

public class ButtonPressManager : MonoBehaviour
{
    [SerializeField] private GameObject wallToDisable; // 비활성화할 벽 오브젝트
    [SerializeField] private PressButtonTest[] buttons; // 모든 버튼 스크립트의 배열

    public void CheckAllButtons()
    {
        foreach (PressButtonTest button in buttons)
        {
            if (!button.IsPressed) // IsPressed는 PressButtonTest에 있는 상태를 나타내는 프로퍼티가 될 것입니다.
            {
                return; // 하나라도 비활성화 상태면 함수를 종료
            }
        }

        // 모든 버튼이 활성화 상태
        wallToDisable.SetActive(false); // 벽 비활성화
    }
}
