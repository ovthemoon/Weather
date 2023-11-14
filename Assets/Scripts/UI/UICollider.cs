using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICollider : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와의 충돌을 확인합니다.
        if (other.gameObject.CompareTag("Player") && !isTalking && talkBubble.gameObject.activeInHierarchy == false)
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

        StartCoroutine(HideTalkBubbleAfterDelay());
    }

    IEnumerator HideTalkBubbleAfterDelay()
    {
    // 1초 동안 대기합니다.
    yield return new WaitForSeconds(2f);

    // 말풍선을 비활성화합니다.
    talkBubble.gameObject.SetActive(false);
    isTalking = false;
    player.canMove = true;
    if (playgroundControl != null)
    {
        playgroundControl.enabled = true;
    }
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
            yield return new WaitForSeconds(0.05f); // �� ���� ������ ����
        }
        yield return new WaitForSeconds(2f); // 텍스트가 모두 표시된 후 2초간 대기
        isTyping = false;
        arrow.gameObject.SetActive(true); // 다음 문장으로 넘어갈 수 있음을 표시하는 화살표를 표시

    }

    void CompleteSentence()
    {
        StopAllCoroutines(); // ���� ���� ���� �ڷ�ƾ�� ����ϴ�.
        text.text = textArr[index - 1]; // ���� ������ �ϼ��մϴ�.
        isTyping = false;
        arrow.gameObject.SetActive(true);
    }

    void EndTalk()
    {
        talkBubble.gameObject.SetActive(false);
        index = 0; // �ε����� �ٽ� -1�� �ʱ�ȭ
        isTalking = false;
        player.canMove = true;
        if (playgroundControl != null)
        {
            playgroundControl.enabled = true;
        }
    }
}
