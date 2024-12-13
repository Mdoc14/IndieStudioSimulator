using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshPro displayText;
    [SerializeField] private string textInfo;
    [SerializeField] private float outlineAmount = 1.02f;

    private void Awake()
    {
        displayText.text = textInfo;
        GetComponent<Renderer>().materials[0] = new Material(GetComponent<Renderer>().materials[0]);
        HoverExit();
    }

    public virtual void Interact()
    {
        Debug.Log("Se ha interactuado");
    }

    public void HoverEnter()
    {
        displayText.gameObject.SetActive(true);
        GetComponent<Renderer>().materials[0].SetFloat("_Value", outlineAmount);
    }

    public void HoverExit()
    {
        displayText.gameObject.SetActive(false);
        GetComponent<Renderer>().materials[0].SetFloat("_Value", 0);
    }
}
