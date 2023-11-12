using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMap : MonoBehaviour
{
    public GameObject map1;
    public GameObject map1_glass;
    private void OnCollisionEnter(Collision collision)
    {
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.green;
        map1_glass.SetActive(true);
        map1.SetActive(false);
    }
}
