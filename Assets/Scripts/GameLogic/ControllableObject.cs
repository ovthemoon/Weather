using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllableObject : MonoBehaviour
{
    public bool isOn { get; private set; }
    public bool isMoving { get; private set; } = false;

    //Shift
    public Vector3 moveOffset=Vector3.up*3;
    public float duration = 2.0f;

    public Vector3 moveDirection = Vector3.up;

    private Vector3 onPosition;
    private Vector3 offPosition;

    //Rotate
    public Vector3 rotationAngle = Vector3.zero;  // 목표 회전 각도
    private Quaternion startRotation;
    private Quaternion onRotation;
    private Quaternion offRotation;



    private void Start()
    {
        onPosition = transform.position+ moveDirection * moveOffset.magnitude;
        offPosition = transform.position;
        startRotation = transform.rotation;
        onRotation = Quaternion.Euler(rotationAngle) * startRotation;  // 켜진 상태의 회전
        offRotation = startRotation;  // 꺼진 상태의 회전
    }
    
    public void ToggleState()
    {

        isOn = !isOn;
        if (isOn)
        {
            StartCoroutine(MoveToTarget(onPosition));
        }
        else
        {
            StartCoroutine(MoveToTarget(offPosition));
        }
    }
    IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        isMoving = true;
        Vector3 startPosition = transform.position;

        Quaternion startRotationCurrent = transform.rotation;
        Quaternion targetRotation = isOn ? onRotation : offRotation;
        
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = elapsed / duration;

            //Shift
            transform.position = Vector3.Lerp(startPosition, targetPosition, normalizedTime);
            //Rotate
            transform.rotation = Quaternion.Lerp(startRotationCurrent, targetRotation, normalizedTime);
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;
        isMoving = false;
    }
}
