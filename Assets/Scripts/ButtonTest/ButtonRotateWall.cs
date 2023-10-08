using UnityEngine;

public class ButtonRotateWall : MonoBehaviour
{
    public Transform[] wallsToRotate; // 회전시킬 벽들의 Transform 배열
    public float rotationSpeed = 90f; // 회전 속도 (예: 90도/초)
    public Vector3 rotationAxis = Vector3.up; // 회전할 축
    public float rotationAngle = 90f; // 회전할 각도

    private bool isRotating = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isRotating)
        {
            foreach (Transform wall in wallsToRotate)
            {
                StartCoroutine(RotateWall(wall));
            }
        }
    }

    private System.Collections.IEnumerator RotateWall(Transform wall)
    {
        isRotating = true;

        Vector3 initialRotation = wall.eulerAngles;
        Vector3 targetRotation = initialRotation + rotationAxis * rotationAngle;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * (rotationSpeed / rotationAngle);
            wall.eulerAngles = Vector3.Lerp(initialRotation, targetRotation, t);
            yield return null;
        }

        wall.eulerAngles = targetRotation; // 정확한 최종 각도로 설정

        isRotating = false;
    }
}
