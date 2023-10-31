using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class TimelineManager : MonoBehaviour
{
    [HideInInspector]
    public PlayableDirector playableDirector;
    public CharacterMove player;
    public Volume volume;
    private Vignette vignette;

    public float targetValue = 10f;  // 목표값 x
    public float changeDuration = 2.0f;  // 값이 변화하는데 걸리는 시간

    private float currentValue = 0f;
    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            // 초기 intensity 값을 설정할 수 있습니다.
            vignette.intensity.Override(currentValue);
        }
    }
    public void PlayTimeline()
    {
        player.canMove = false;
        playableDirector.Play();
        TweenVignetteIntensity();
    }
    private void TweenVignetteIntensity()
    {
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, targetValue, changeDuration);
    }
}
