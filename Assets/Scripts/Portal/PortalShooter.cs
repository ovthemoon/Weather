using UnityEngine;

public class PortalShooter : MonoBehaviour
{
    public GameObject spherePrefab1;
    public GameObject spherePrefab2;
    public Transform launchPoint;

    void Update()
    {
        ShootSpheres();
    }

    void ShootSpheres()
    {
        // 오른쪽 마우스 버튼으로 스피어1 발사
        if (Input.GetMouseButtonDown(1))
        {
            GameObject sphere1 = Instantiate(spherePrefab1, launchPoint.position, Quaternion.identity);
            sphere1.GetComponent<Rigidbody>().velocity = transform.forward * 10f;
        }

        // 왼쪽 마우스 버튼으로 스피어2 발사
        if (Input.GetMouseButtonDown(0))
        {
            GameObject sphere2 = Instantiate(spherePrefab2, launchPoint.position, Quaternion.identity);
            sphere2.GetComponent<Rigidbody>().velocity = transform.forward * 10f;
        }
    }
}
