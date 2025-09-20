using System;
using UnityEngine;

public class PalletSize : MonoBehaviour
{
    [SerializeField]
    private Vector3 originalPalletSize
        = new Vector3(1.185f, 1.015f, 0.14f);
    private Vector3 currentPalletSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        ResizePallet(originalPalletSize.x,
            originalPalletSize.y, originalPalletSize.z);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void ResizePallet(float x, float y, float z)
    {
        currentPalletSize = new Vector3(x, y, z);

        var palletTransform = this.transform;
        palletTransform.localScale = new Vector3(
            currentPalletSize.x, currentPalletSize.y, currentPalletSize.z);
    }
    
    public Vector3 GetCurrentPalletSize()
    {
        return currentPalletSize;
    }

    public void OnChangedSizeX(string new_x)
    {
        if (float.TryParse(new_x, out float x))
        {
            x = Mathf.Max(x * 0.001f, 0.01f);
            ResizePallet(x, currentPalletSize.y, currentPalletSize.z);
        }
    }

    public void OnChangedSizeY(string new_y)
    {
        if (float.TryParse(new_y, out float y))
        {
            y = Mathf.Max(y*0.001f, 0.01f);
            ResizePallet(currentPalletSize.x, y, currentPalletSize.z);
        }
    }

    public void OnChangedSizeZ(string new_z)
    {
        if (float.TryParse(new_z, out float z))
        {
            z = Mathf.Max(z*0.001f, 0.01f);
            ResizePallet(currentPalletSize.x, currentPalletSize.y, z);
        }
    }
}
