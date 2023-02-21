using System;
using TMPro;
using UnityEngine;

public class StatSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI value;
    [SerializeField] private TextMeshProUGUI type;
    [SerializeField] private String inputType;
    
    private void Start()
    {
        type.text = inputType;
    }

    public void UpdateSlot(float value)
    {
        this.value.text = string.Format("{0:0}", value);
    } 
    
    public void UpdateSlot(string value)
    {
        this.value.text = value;
    }
}