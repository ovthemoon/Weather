using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableObjectDropSound : MonoBehaviour
{
    AudioSource source;
    private void Start()
    {
        source=GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        source.Play();
    }
}
