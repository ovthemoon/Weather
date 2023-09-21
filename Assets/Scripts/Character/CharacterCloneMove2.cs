using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCloneMove2 : MonoBehaviour
{
    GameObject middlePoint;
    private GameObject player;
    private Vector3 playerPosition;
    private Vector3 playerClonePosition;
    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        middlePoint= GameObject.Find("middlePoint");

    }
    public void Update()
    {
        CloneMovement();
        CloneRotation();
    }
    public void CloneMovement()
    {
        playerPosition = player.transform.position - middlePoint.transform.position;
        playerClonePosition=new Vector3(-playerPosition.x,playerPosition.y,playerPosition.z)+middlePoint.transform.position;
        transform.position = playerClonePosition;
    }
    public void CloneRotation()
    {
        transform.rotation = Quaternion.Euler(0, -player.transform.rotation.y*180, 0);
    }
}
