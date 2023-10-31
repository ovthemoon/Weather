using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MirrorAnimationPlayer : MonoBehaviour
{
    public TimelineManager manager;
    public CloneManager cloneManager;
    public string sceneName;
    public Image image; // 조정하려는 메터리얼
    public float duration = 2f; // 트위닝에 걸리는 시간
    public float targetAlphaClipValue = 0f; // 목표 알파 클리핑 값
    
    Material material;
    private void Start()
    {
        material = image.material;
        material.color = new Color(material.color.r, material.color.g, material.color.b, 0);
    }

    private void OnMouseDown()
    {
        cloneManager.DeleteAllClones();
        manager.PlayTimeline();
        manager.playableDirector.stopped += OnTimelineStopped;
    }
    void OnTimelineStopped(PlayableDirector pd)
    {
        image.gameObject.SetActive(true);
        Camera.main.DOShakePosition(duration, 2, 10, 90, true);
        material.color = new Color(material.color.r, material.color.g, material.color.b, 1);
        DOTween.To(() => material.GetFloat("_Cutoff"), x => material.SetFloat("_Cutoff", x), targetAlphaClipValue, duration)
              .OnComplete(() => {DOVirtual.DelayedCall(2f, LoadNextScene);});

    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
