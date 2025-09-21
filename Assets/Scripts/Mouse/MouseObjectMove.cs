using UnityEngine;

public class MouseObjectMove : MonoBehaviour
{
    private Vector3 lastMousePosition;
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float rotationSpeed = 8.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

            var selectedObject = MouseSelectedManager.instance.GetSelectedObject();
            if (selectedObject == null)
            {
                return;
            }

            if (selectedObject.transform.IsChildOf(this.transform))
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 delta = Input.mousePosition - lastMousePosition;
                    this.transform.position += selectedObject.transform.up * delta.y * moveSpeed * Time.deltaTime;
                }
                else if (Input.GetMouseButton(1))
                {
                    Vector3 delta = Input.mousePosition - lastMousePosition;
                    this.transform.RotateAround(this.transform.position, selectedObject.transform.up, -delta.x * rotationSpeed * Time.deltaTime);
                }
            }
        }
        finally
        {
            lastMousePosition = Input.mousePosition;
        }
    }
}
