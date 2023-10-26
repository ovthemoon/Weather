using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    Camera cam;
    public Transform spawnPosition;
    public GameObject EffectPrefab;
    public float fireSpeed = 10f;
    private GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 fireDirection = spawnPosition.transform.forward;
            effect=Instantiate(EffectPrefab,spawnPosition.position,spawnPosition.rotation);
            Rigidbody rb = effect.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = fireDirection * fireSpeed;
            }
            Destroy(effect, 5f);
        }
        
    }
}
