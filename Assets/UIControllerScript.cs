using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControllerScript : MonoBehaviour
{
    public Image talkBubble;
    public TextMeshProUGUI text;
    public string[] textArr;
    public CharacterMove player;
    public Image arrow;

    private int index = 0;
    private bool isTalking = false;
    private bool isTyping = false;
    private RadioOnOff radio;
    private PlaygroundControl playgroundControl;
    private void Start()
    {
        radio=GetComponent<RadioOnOff>();
        playgroundControl=GetComponent<PlaygroundControl>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTalking&&index!=0)
        {
            if (isTyping)
            {
                CompleteSentence();
            }
            else
            {
                NextSentence();
            }
        }
    }

    private void OnMouseDown()
    {
        
        if (!isTalking && talkBubble.gameObject.activeInHierarchy == false)
        {
            if (radio != null)
            {
                radio.turnOnOff();
            }
            StartTalk();
        }
    }

    void StartTalk()
    {
        isTalking = true;
        talkBubble.gameObject.SetActive(true);
        player.canMove = false;
        NextSentence();
    }

    void NextSentence()
    {
        arrow.gameObject.SetActive(false);
        if (index < textArr.Length)
        {
            StartCoroutine(TypeSentence(textArr[index]));
            index++;
        }
        else
        {
            EndTalk();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            talkBubble.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.05f); // 각 글자 사이의 간격
        }
        isTyping = false;
    }

    void CompleteSentence()
    {
        StopAllCoroutines(); // 현재 진행 중인 코루틴을 멈춥니다.
        text.text = textArr[index - 1]; // 현재 문장을 완성합니다.
        isTyping = false;
        arrow.gameObject.SetActive(true);
    }

    void EndTalk()
    {
        talkBubble.gameObject.SetActive(false);
        index = 0; // 인덱스를 다시 -1로 초기화
        isTalking = false;
        player.canMove = true;
        if (playgroundControl != null)
        {
            playgroundControl.enabled = true;
        }
    }
}
