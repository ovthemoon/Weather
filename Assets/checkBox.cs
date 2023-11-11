using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class checkBox : MonoBehaviour
{
    public TimelineManager manager;
    Camera mainCamera; // 기존 플레이어 카메라
    public CinemachineVirtualCamera animationCamera;
    public float duration = 2f;
    public float targetValue=.3f;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CloneOfClone")){
            Camera.main.DOShakePosition(duration, 2, 10, 90, true);

            SwitchCamera(11);
            manager.PlayTimeline(targetValue);
            manager.playableDirector.stopped += OnTimelineStopped;
        }
    }
    void OnTimelineStopped(PlayableDirector pd)
    {
        LoadingSceneManager.LoadScene("Map2_Desert");

    }

    void SwitchCamera(int priority)
    {
        animationCamera.Priority = priority;
    }
}
