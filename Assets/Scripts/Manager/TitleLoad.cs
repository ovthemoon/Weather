using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleLoad : MonoBehaviour
{
    public string titleGameSceneName = "Title"; // 메인 게임 씬 이름
    public string uiSceneName = "UIScene"; // UI 씬 이름

    void Start()
    {
        LoadScenes();
    }

    void LoadScenes()
    {
        // 메인 게임 씬을 Single 모드로 로드합니다.
        SceneManager.LoadScene(titleGameSceneName, LoadSceneMode.Single);

        // UI 씬을 Additive 모드로 로드합니다.
        SceneManager.LoadSceneAsync(uiSceneName, LoadSceneMode.Additive);
    }
}
