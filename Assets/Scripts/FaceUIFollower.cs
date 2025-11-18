using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FaceUIFollower : MonoBehaviour
{
    [Header("Canvas yang ingin ditempel ke wajah")]
    public Canvas quizCanvas;

    [Header("Offset posisi relatif ke wajah")]
    public Vector3 positionOffset = new Vector3(0, 0.1f, 0.25f);

    [Header("Offset rotasi")]
    public Vector3 rotationOffset = new Vector3(0, 180f, 0);

    private ARFace arFace;
    private GameObject uiAnchor;

    void Start()
    {
        // Cari ARFace otomatis dari parent
        arFace = GetComponent<ARFace>();

        if (arFace == null)
        {
            Debug.LogError("ARFace tidak ditemukan!");
            return;
        }

        // Buat anchor kosong sebagai anak ARFace (aman)
        uiAnchor = new GameObject("FaceUIAnchor");
        uiAnchor.transform.SetParent(arFace.transform);

        // Buat offset posisi & rotasi
        uiAnchor.transform.localPosition = positionOffset;
        uiAnchor.transform.localEulerAngles = rotationOffset;

        // Pindahkan canvas ke anchor ini
        if (quizCanvas != null)
        {
            quizCanvas.renderMode = RenderMode.WorldSpace;
            quizCanvas.transform.SetParent(uiAnchor.transform, worldPositionStays: false);
        }
    }

    void LateUpdate()
    {
        if (uiAnchor == null) return;

        // Pastikan anchor selalu menghadap depan wajah
        uiAnchor.transform.localPosition = positionOffset;
        uiAnchor.transform.localEulerAngles = rotationOffset;
    }
}
