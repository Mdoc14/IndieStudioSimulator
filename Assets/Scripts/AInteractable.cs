using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshPro displayText;
    [SerializeField] private string textInfo;
    [SerializeField] private GameObject outline;

    protected virtual void Awake()
    {
        displayText.text = textInfo;
        int numMaterials = GetComponent<Renderer>().materials.Length; //El shader va a ser siempre el último
        GetComponent<Renderer>().materials[numMaterials - 1] = new Material(GetComponent<Renderer>().materials[numMaterials - 1]);
        HoverExit();
    }

    public virtual void Interact()
    {
        GetComponent<Collider>().enabled = false;
        HoverExit();
        Debug.Log("Se ha interactuado");
    }

    public virtual void Repair() { }

    public void HoverEnter()
    {
        displayText.gameObject.SetActive(true);
        outline.SetActive(true);
    }

    public void HoverExit()
    {
        displayText.gameObject.SetActive(false);
        outline.SetActive(false);
    }

    public void ResetInteractable()
    {
        GetComponent<Collider>().enabled = true;
    }
}
