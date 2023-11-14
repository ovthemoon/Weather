using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public string sceneName;
    private void Start()
    {
        gameObject.SetActive(!GameManager.map2_DesertComplete);
        gameObject.SetActive(!GameManager.map2_PoleComplete);
    }
    private void OnMouseDown()
    {
        LoadingSceneManager.LoadScene(sceneName);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadingSceneManager.LoadScene(sceneName);
        }
    }

}
