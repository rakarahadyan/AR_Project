using UnityEngine;

public class CanvasEventCameraSetup : MonoBehaviour
{
    private Canvas canvas;
    
    void Start()
    {
        canvas = GetComponent<Canvas>();
        SetEventCamera();
    }

    void Update()
    {
        // Re-check setiap frame jika camera belum di-set
        if (canvas != null && canvas.worldCamera == null)
        {
            SetEventCamera();
        }
    }

    void SetEventCamera()
    {
        Camera mainCam = Camera.main;
        
        if (mainCam != null && canvas != null)
        {
            canvas.worldCamera = mainCam;
            Debug.Log($"✅ Canvas Event Camera set to: {mainCam.name}");
        }
        else
        {
            Debug.LogWarning("⚠️ Main Camera not found yet!");
        }
    }
}