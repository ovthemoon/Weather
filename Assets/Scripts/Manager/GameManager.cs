using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]  private Transform spawnPoint;
    [SerializeField]  private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       if(spawnPoint != null)
            player.transform.position = spawnPoint.position;
        //spawnPoint = GameObject.Find("SpawnPoint");
        
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void moveScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void setActiveFalse(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void setActiveTrue(GameObject obj)
    {
        obj.SetActive(true);
    }

}
