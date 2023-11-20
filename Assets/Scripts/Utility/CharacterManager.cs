using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] Characters;
    bool isPlayingAnimation;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeCharacter()
    {
        index=(index+1)%Characters.Length;
        for(int i = 0; i < Characters.Length; i++)
        {
            Characters[i].SetActive(false);
        }
        Characters[index].SetActive(true);
    }
    public void setAnimation()
    {
        isPlayingAnimation=!isPlayingAnimation;
        
        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i].GetComponent<Animator>().enabled=isPlayingAnimation;
        }
        
    }
}
