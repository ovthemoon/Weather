using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickMirror : MonoBehaviour
{
    public GameObject clearUI;
    public CharacterMove player;
    
    // Start is called before the first frame update
    void Start()
    {
        clearUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Mirror")
                {
                    AudioSource clickMirror = GetComponent<AudioSource>();
                    clickMirror.Play();
                    clearUI.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    player.canMove = false;
                }

            }
        }
    }
}
