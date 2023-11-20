using System.Collections;
using UnityEngine;

public class ControllableObject : MonoBehaviour
{
    public bool isOn { get; private set; }
    public bool isMoving { get; private set; } = false;

    // Shift
    public Vector3 moveOffset = Vector3.up * 3; // 이동할 거리를 설정합니다.
    public float duration = 2.0f;

    // 이동 방향을 인스펙터에서 설정할 수 있습니다.
    public Vector3 moveDirection = Vector3.up; // 로컬 축을 기준으로 합니다.
    AudioSource audioWhenMove;

    private Vector3 onPosition;
    private Vector3 offPosition;

    // Rotate
    public Vector3 rotationAngle = Vector3.zero; // 목표 회전 각도를 설정합니다.
    private Quaternion startRotation;
    private Quaternion onRotation;
    private Quaternion offRotation;
    

    private void Start()
    {
        // 로컬 축을 기준으로 moveOffset을 설정합니다.
        Vector3 localMoveDirection = transform.TransformDirection(moveDirection.normalized);
        Vector3 localMoveOffset = localMoveDirection * moveOffset.magnitude;

        onPosition = transform.position + localMoveOffset;
        offPosition = transform.position;

        startRotation = transform.rotation;
        onRotation = Quaternion.Euler(rotationAngle) * startRotation;
        offRotation = startRotation;
        audioWhenMove=GetComponent<AudioSource>();
    }

    public void ToggleState()
    {
        isOn = !isOn;
        if (isOn)
        {
            StartCoroutine(MoveToTarget(onPosition, onRotation));
        }
        else
        {
            StartCoroutine(MoveToTarget(offPosition, offRotation));
        }
    }

    IEnumerator MoveToTarget(Vector3 targetPosition, Quaternion targetRotation)
    {
        if (audioWhenMove!=null&&!audioWhenMove.isPlaying)
        {
            audioWhenMove.Play();
        }
        isMoving = true;
        Vector3 startPosition = transform.position;
        Quaternion startRotationCurrent = transform.rotation;

        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsed / duration); // 값을 0과 1 사이로 제한합니다.

            // Shift
            transform.position = Vector3.Lerp(startPosition, targetPosition, normalizedTime);
            // Rotate
            transform.rotation = Quaternion.Slerp(startRotationCurrent, targetRotation, normalizedTime);

            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;
        isMoving = false;
        if (audioWhenMove != null)
        {
            audioWhenMove.Stop();
        }
    }
}
