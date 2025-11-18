using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public bool reverseFace = true; // Set true untuk mobile, false untuk editor
    private Camera mainCamera;

    void Start()
    {
        // Get AR camera
        mainCamera = Camera.main;
        
        if (mainCamera == null)
        {
            Debug.LogWarning("⚠️ Main Camera not found!");
        }
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // Make canvas always face the camera
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        
        if (reverseFace)
        {
            // For mobile AR - face towards camera
            transform.rotation = Quaternion.LookRotation(directionToCamera);
        }
        else
        {
            // For editor testing - face away from camera
            transform.rotation = Quaternion.LookRotation(-directionToCamera);
        }
    }
}