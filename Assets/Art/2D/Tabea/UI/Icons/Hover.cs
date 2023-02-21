using UnityEngine;
using UnityEngine.UI;

public class Hover : MonoBehaviour
{
    [SerializeField] private RawImage icon;

    public void ShowIcon()
    {
        icon.enabled = enabled;
    }


    public void HideIcon()
    {
        icon.enabled = false;
    }
      



}
