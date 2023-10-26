using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class MirrorAnimationPlayer : MonoBehaviour
{
    public TimelineManager manager;
    public GameObject CloneCollider;
    public string sceneName;
    private void OnMouseDown()
    {
        CloneCollider.SetActive(false);
        manager.PlayTimeline();
        manager.playableDirector.stopped += OnTimelineStopped;
    }
    void OnTimelineStopped(PlayableDirector pd)
    {
        SceneManager.LoadScene(sceneName);
    }
}
