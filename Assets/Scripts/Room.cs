using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    private List<GameObject> trash = new List<GameObject>();
    [SerializeField] private GameObject trashcan;
    public VendingMachineManager machine;
    [SerializeField] private CatBoxManager catBox;
    public event Action<GameObject> OnColliderTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            trash.Add(other.gameObject);
            Debug.Log("Basura añadida a la habitación " + name);
        }

        if (other.gameObject.name == "Janitor")
        {
            OnColliderTriggered?.Invoke(other.gameObject);
        }
    }

    public bool IsDirty()
    {
        //agent.SetBark("Look");
        //agent.SetAnimation("Looking");
        
        return trash.Count > 0;
    }

    public Vector3 GetTrashPosition()
    {
        return trash[0].gameObject.transform.position;
    }

    public void HideTrash() 
    {
        trash[0].gameObject.SetActive(false);
    }
    public void DeleteTrash()
    {
        GameObject trashGO = trash[0];
        Destroy(trashGO);
        trash.RemoveAt(0);
    }

    public Vector3 GetTrashcanPosition() 
    {
        Debug.Log("Buscando papelera de la sala");
        return trashcan.transform.position;
    }

    public GameObject GetTrashcan()
    {
        return trashcan;
    }

    public bool IsMachineEmpty()
    {
        Debug.Log("Comprobando si la maquina esta vacia");
        return machine.IsEmpty();
    }

    public Vector3 GetMachinePosition()
    {
        Debug.Log("Yendo a la maquina");
        return machine.transform.position;
    }

    public VendingMachineManager GetVendingMachine() { return machine; }

    public bool IsCatBoxDirty()
    {
        Debug.Log("Comprobando si la caja esta sucia");
        return catBox.IsDirty();
    }

    public Vector3 GetCatBoxPosition() 
    {
        Debug.Log("Yendo a la caja de arena");
        return catBox.transform.position;
    }

    public CatBoxManager GetCatBox() { return catBox; }
}
