using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEffectShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // 발사할 프로젝트 타입(물체)
    public Transform spawnPoint;
    public float shootForce = 10f; // 발사되는 힘

    // Update는 프레임마다 한 번씩 호출됩니다
    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Fire1은 기본적으로 마우스 왼쪽 버튼을 의미합니다.
        {
            Debug.Log("Shoot");
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation); // 현재 위치와 회전에서 프로젝트를 생성합니다.
        Destroy(projectile,2f);
        Rigidbody rb = projectile.GetComponent<Rigidbody>(); // Rigidbody 컴포넌트에 접근합니다.
        
        if (rb)
        {
            rb.AddForce(transform.up * shootForce, ForceMode.Impulse); // forward vector를 사용하여 물체를 발사합니다.
        }
    }
}
