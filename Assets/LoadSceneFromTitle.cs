using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneFromTitle : MonoBehaviour
{
    public string nextScene="Map0";
    void loadScene()
    {
        LoadingSceneManager.LoadScene(nextScene);
    }
}
