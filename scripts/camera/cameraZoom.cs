using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSensitivity = 0.5f;  // 进一步增加敏感度
    public float zoomMin = 1f;
    public float zoomMax = 3.5f;

    void Update()
    {
        float zoomChange = Input.GetAxis("Mouse ScrollWheel");
      

        if (Mathf.Abs(zoomChange) > 0.005f)
        {
            float newZoom = Camera.main.orthographicSize - zoomChange * zoomSensitivity;
            Camera.main.orthographicSize = Mathf.Clamp(newZoom, zoomMin, zoomMax);
            
        }
    }
}
