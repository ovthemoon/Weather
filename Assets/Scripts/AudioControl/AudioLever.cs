using UnityEngine;

public class AudioLever : MonoBehaviour
{
    public AudioClip newBackgroundMusic; // 레버 작동 시 변경할 배경음악

    private void OnMouseDown()
    {
        // GameManager를 통해 배경음악 변경
        AudioManager.instance.ChangeBackgroundMusic(newBackgroundMusic);
    }
}
