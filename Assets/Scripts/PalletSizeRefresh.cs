using TMPro;
using UnityEngine;

public class PalletSizeRefresh : MonoBehaviour
{
    [SerializeField] private PalletSize palletResize;
    [SerializeField] private TMP_InputField palletSizeXInput;
    [SerializeField] private TMP_InputField palletSizeYInput;
    [SerializeField] private TMP_InputField palletSizeZInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        var size = new Vector3(1.0f, 1.0f, 1.0f);
        if (palletResize != null)
        {
            size = palletResize.GetCurrentPalletSize();
        }

        if (palletSizeXInput != null)
        {
            palletSizeXInput.text = (size.x * 1000).ToString("F0");
        }

        if (palletSizeYInput != null)
        {
            palletSizeYInput.text = (size.y * 1000).ToString("F0");
        }

        if (palletSizeZInput != null)
        {
            palletSizeZInput.text = (size.z * 1000).ToString("F0");
        }
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void Refresh()
    {
        if(palletSizeXInput != null
            && palletSizeYInput != null
            && palletSizeZInput != null)
        {
            var size = new Vector3(1.0f, 1.0f, 1.0f);
            if (float.TryParse(palletSizeXInput.text, out float x))
            {
                size.x = Mathf.Max(x * 0.001f, 0.01f);
            }

            if (float.TryParse(palletSizeYInput.text, out float y))
            {
                size.y = Mathf.Max(y * 0.001f, 0.01f);
            }

            if (float.TryParse(palletSizeZInput.text, out float z))
            {
                size.z = Mathf.Max(z * 0.001f, 0.01f);
            }

            palletResize.ResizePallet(size.x, size.y, size.z);
        }   
    }
}
