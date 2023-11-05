using UnityEngine;

public class FloatObjects : MonoBehaviour
{
    public Vector3 floatOffset = new Vector3(0, 2, 0);
    public float amplitude = 1f;
    public float floatSpeed = 1f;
    public float rotationSpeed = 50f; // 회전 속도

    private Vector3 startPosition;
    private Vector3 randomRotation;

    void Start()
    {
        startPosition = transform.position;
        randomRotation = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized; // 랜덤한 회전 방향을 설정합니다.
    }

    void Update()
    {
        // 둥둥 뜨는 효과
        float sinWave = Mathf.Sin(Time.time * floatSpeed) * amplitude;
        transform.position = startPosition + floatOffset * sinWave;

        // 랜덤 회전
        transform.Rotate(randomRotation, rotationSpeed * Time.deltaTime);
    }
}
