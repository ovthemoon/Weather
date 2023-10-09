using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllableObject : MonoBehaviour
{
    public bool isOn { get; private set; }
    public bool isMoving { get; private set; } = false;
    public Vector3 moveOffset=Vector3.up*3;
    public float duration = 2.0f;

    private Vector3 onPosition;
    private Vector3 offPosition;
    private void Start()
    {
        onPosition = transform.position+moveOffset;
        offPosition = transform.position - moveOffset;
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
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, normalizedTime);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }
}
