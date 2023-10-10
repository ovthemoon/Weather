using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{

    public Texture2D cursorIcon;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorIcon, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        //Locked? None? Confined?

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
