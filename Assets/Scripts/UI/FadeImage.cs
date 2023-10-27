using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Playables;
public class FadeImage : MonoBehaviour
{
    public Image logoImage;
    public CanvasGroup gameTitle;
    public float fadeInDuration = 1f;
    public float stayDuration = 2f;
    public float fadeOutDuration = 1f;
    public PlayableDirector timelineDirector;

    void Start()
    {
        logoImage.gameObject.SetActive(true);
        gameTitle.gameObject.SetActive(true);
        logoImage.DOFade(0, 0); // 시작 시 로고를 투명하게 설정
        gameTitle.DOFade(0, 0);
        StartCoroutine(LogoSequence());
    }

    IEnumerator LogoSequence()
    {
        yield return logoImage.DOFade(1, fadeInDuration).WaitForCompletion(); // 페이드 인
        yield return new WaitForSeconds(stayDuration); // 로고 유지
        yield return logoImage.DOFade(0, fadeOutDuration).WaitForCompletion(); // 페이드 아웃

        // 로고 애니메이션이 끝난 후 타임라인 애니메이션을 시작합니다.
        StartTimelineAnimation();
        
    }
    
    void StartTimelineAnimation()
    {
        timelineDirector.Play();
    }
    public void titleFadeIn()
    {
        gameTitle.DOFade(1, fadeInDuration);
    }

}
