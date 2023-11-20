using UnityEngine;

public class FloatObject : MonoBehaviour
{
    public float amplitude = 0.5f; // 떠다니는 높이의 범위
    public float frequency = 1f; // 떠다니는 속도
    public Vector3 floatDirection = Vector3.up; // 부유하는 방향

    private Vector3 startPos; // 초기 위치

    void Start()
    {
        // 초기 위치를 저장합니다.
        startPos = transform.position;
    }

    void Update()
    {
        // 사인 함수를 사용하여 새로운 위치를 계산합니다.
        Vector3 floatY = floatDirection * amplitude * Mathf.Sin(Time.time * frequency);

        // 오브젝트의 위치를 업데이트합니다.
        transform.position = startPos + floatY;
    }
}
