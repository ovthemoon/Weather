using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]  private Transform spawnPoint;
    [SerializeField]  private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       
        player.transform.position = spawnPoint.position;
        //spawnPoint = GameObject.Find("SpawnPoint");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
