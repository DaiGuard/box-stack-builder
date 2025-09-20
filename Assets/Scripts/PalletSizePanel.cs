using System.Threading.Tasks;
using UnityEngine;

public class PalletSizePanel : MonoBehaviour
{
    private Animator panelAnimator;

    private void Start()
    {
        panelAnimator = this.GetComponent<Animator>();
    }

    private void Update()
    {

    }

    public void ActivatePanel(bool activate)
    {
        if(panelAnimator != null)
        {
            panelAnimator.SetTrigger(activate ? "OpenTrigger" : "CloseTrigger");
        }
    }
}
