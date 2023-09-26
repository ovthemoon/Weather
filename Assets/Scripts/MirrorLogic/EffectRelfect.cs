using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRelfect : MonoBehaviour
{
    public float effectSpeed = 10f;
    public LayerMask mirrorLayer; // 거울 레이어를 여기에 지정합니다.

    private Vector3 currentDirection;
    private Renderer effectRenderer;

    private void Start()
    {
        currentDirection = new Vector3(1,0,0); // 초기 방향 설정
        effectRenderer = GetComponent<Renderer>(); // 이펙트의 렌더러를 가져옵니다.
    }

    private void Update()
    {
        EffectReflection();
    }

    private void EffectReflection()
    {
        // 이펙트를 현재 방향으로 이동
        transform.position += currentDirection * effectSpeed * Time.deltaTime;

        // 레이캐스팅하여 거울과의 교차점을 찾습니다.
        if (Physics.Raycast(transform.position, currentDirection, out RaycastHit hit, Mathf.Infinity, mirrorLayer))
        {
            // 거울의 표면 법선을 사용하여 반사 각도를 계산합니다.
            currentDirection = Vector3.Reflect(currentDirection, hit.normal);

            // 거울의 색깔 값을 가져와 이펙트의 색깔을 변경합니다.
            MirrorColor mirrorColorComponent = hit.collider.gameObject.GetComponent<MirrorColor>();
            if (mirrorColorComponent != null && effectRenderer != null)
            {
                effectRenderer.material.color = mirrorColorComponent.mirrorColor;
            }
        }
    }
}
