using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlaygroundControl : MonoBehaviour
{
    public string[] additiveText;
    public UIControllerScript uiControllerScript;
    public CinemachineVirtualCamera virtualCamera1;
    public CinemachineVirtualCamera virtualCamera2;
    public CinemachineVirtualCamera virtualCamera3;

    private List<CinemachineVirtualCamera> cameras;

    private void Start()
    {
        uiControllerScript.textArr = additiveText;
        // Initialize camera list and add cameras
        cameras = new List<CinemachineVirtualCamera> { virtualCamera1, virtualCamera2, virtualCamera3 };

        // Start the coroutine to switch cameras
        StartCoroutine(CycleCameras());
    }

    IEnumerator CycleCameras()
    {
            foreach (var cam in cameras)
            {
                int currentPriority = cam.Priority;
                cam.Priority = 11; // Activate the camera
                yield return new WaitForSeconds(2f); // Wait for 2 seconds
                cam.Priority = currentPriority; // Deactivate the camera
            }
    }

    // Your existing SwitchCamera method can be removed or modified as needed
}
