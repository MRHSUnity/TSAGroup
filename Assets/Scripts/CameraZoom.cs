using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom2D : MonoBehaviour
{
    public CinemachineCamera vcam;
    public float zoomSpeed = 1f;
    public float minSize = 2f;
    public float maxSize = 10f;

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0f)
        {
            // Adjust the orthographic size
            vcam.Lens.OrthographicSize -= scrollInput * zoomSpeed;
            // Clamp the size value
            vcam.Lens.OrthographicSize = Mathf.Clamp(vcam.Lens.OrthographicSize, minSize, maxSize);
        }
    }
}