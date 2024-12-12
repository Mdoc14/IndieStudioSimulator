using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    private List<GameObject> trash = new List<GameObject>();
    [SerializeField] private GameObject trashcan;
    [SerializeField] private GameObject machine;
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
        Debug.Log("Accion: Ver si hay basura");
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

    public Vector3 GetTrashCanPosition() 
    {
        Debug.Log("Buscando papelera de la sala");
        return trashcan.transform.position;
    }

    public bool IsMachineEmpty()
    {
        Debug.Log("Comprobando si la maquina esta vacia");
        Debug.Log("Resultado: " + false);
        return false; //Habria que preguntarle a la maquina si esta vacia y tal
    }

    public Vector3 GetMachinePosition()
    {
        Debug.Log("Yendo a la maquina");
        return machine.transform.transform.position;
    }

    public bool IsCatBoxDirty()
    {
        Debug.Log("Comprobando si la caja esta sucia");
        Debug.Log("Resultado: " + false);
        return false; //Habria que preguntarle a la caja si esta sucia
    }

    public Vector3 GetCatBoxPosition() 
    {
        Debug.Log("Yendo a la caja de arena");
        return new Vector3(2.31f, 0.91f, 7.44f); //Habria que devolver la posicion de la caja
    }
}
