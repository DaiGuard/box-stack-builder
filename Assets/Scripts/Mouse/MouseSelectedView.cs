using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseSelectedView : MonoBehaviour
{
    [SerializeField] List<GameObject> viewObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        var selectedObject = MouseSelectedManager.instance.GetSelectedObject();
        if (this.gameObject == selectedObject)
        {
            SwitchView(true);
        }
        else
        {
            SwitchView(false);
        }
    }
    
    public void SwitchView(bool activae)
    {
        foreach(var obj in viewObjects)
        {
            obj.SetActive(activae);
        }
    }
}
