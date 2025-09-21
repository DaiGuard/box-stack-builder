using UnityEngine;


public class MouseSelectedManager : MonoBehaviour
{
    public static MouseSelectedManager instance { get; private set; }

    [SerializeField] LayerMask selectableLayer;

    public GameObject selectedObject { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        selectedObject = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayer))
            {
                selectedObject = hit.collider.gameObject;
            }
            else
            {
                selectedObject = null;
            }
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            if (selectedObject != null)
            {
                selectedObject.GetComponent<MouseSelectedView>()?.SwitchView(false);
            }
            selectedObject = null;
        }
    }

    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }
}