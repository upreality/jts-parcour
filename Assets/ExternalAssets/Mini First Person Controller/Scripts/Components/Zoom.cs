using UnityEngine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    Camera targetCamera;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 1;


    private void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        targetCamera = GetComponent<Camera>();
        if (targetCamera)
        {
            defaultFOV = targetCamera.fieldOfView;
        }
    }

    private void Update()
    {
        if(!targetCamera)
            targetCamera = GetComponent<Camera>();
        // Update the currentZoom and the camera's fieldOfView.
        currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
        currentZoom = Mathf.Clamp01(currentZoom);
        targetCamera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }

    public void ResetZoom()
    {
        if(!targetCamera)
            targetCamera = GetComponent<Camera>();
        targetCamera.fieldOfView = defaultFOV;
    }
}
