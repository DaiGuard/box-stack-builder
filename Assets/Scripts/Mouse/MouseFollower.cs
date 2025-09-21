using UnityEngine;

public class CameraMouseFollower : MonoBehaviour
{
    private Transform initCameraTransform;
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float panSpeed = 10.0f;
    [SerializeField] private float zoomSpeed = 5.0f;

    private Vector3 lastMousePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initCameraTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (lastMousePosition.x < 0 || lastMousePosition.x > Screen.width
            || lastMousePosition.y < 0 || lastMousePosition.y > Screen.height)
            {
                return;
            }

            if (MouseSelectedManager.instance.selectedObject != null)
            {
                return;
            }

            // Rotation with left mouse button
            if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;
                this.transform.RotateAround(Vector3.zero, this.transform.up, delta.x * rotationSpeed * Time.deltaTime);
                this.transform.RotateAround(Vector3.zero, this.transform.right, -delta.y * rotationSpeed * Time.deltaTime);
            }

            // Panning with right mouse button
            if (Input.GetMouseButton(1))
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;
                transform.Translate(-delta.x * panSpeed * Time.deltaTime, -delta.y * panSpeed * Time.deltaTime, 0);
            }

            // Zooming with mouse wheel
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(0, 0, scroll * zoomSpeed);
        }
        finally
        {
            lastMousePosition = Input.mousePosition;
        }
    }
}
