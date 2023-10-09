using UnityEngine;

public class Sphere : MonoBehaviour
{
    public GameObject portalPrefab;
    private static GameObject portal1;
    private static GameObject portal2;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (this.gameObject.name.Contains("Sphere1"))
            {
                if (portal1) Destroy(portal1);
                portal1 = Instantiate(portalPrefab, collision.contacts[0].point, Quaternion.identity);
            }
            else if (this.gameObject.name.Contains("Sphere2"))
            {
                if (portal2) Destroy(portal2);
                portal2 = Instantiate(portalPrefab, collision.contacts[0].point, Quaternion.identity);
            }

            Destroy(this.gameObject);
        }
    }
}
