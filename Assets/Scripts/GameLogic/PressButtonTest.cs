using UnityEngine;

public class PressButtonTest : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ButtonManager buttonManager;

    [SerializeField] private AudioSource audioSource; // 오디오 소스 추가
    [SerializeField] private AudioClip buttonPressClip; // 재생할 오디오 클립 추가

    public bool IsPressed { get; private set; } = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsPressed)
        {
            IsPressed = true;
            UpdateButtonVisuals(true);
            buttonManager.CheckButtons(); // 상태 변경을 버튼 관리자에게 알림
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (IsPressed)
        {
            IsPressed = false;
            UpdateButtonVisuals(false);
            buttonManager.CheckButtons(); // 상태 변경을 버튼 관리자에게 알림
        }
    }

    private void UpdateButtonVisuals(bool isDown)
    {
        animator.SetBool("Down", isDown);
        Renderer render = GetComponent<Renderer>();
        render.material.color = isDown ? Color.green : Color.red;

        if (isDown)
        {
            audioSource.PlayOneShot(buttonPressClip); // 버튼이 눌릴 때 오디오 재생
        }
    }

    
}
