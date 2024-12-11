using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private List<GameObject> trash = new List<GameObject>();
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
        Debug.Log("Comprobando si la sala esta sucia");
        Debug.Log("Resultado: " + false);
        return trash.Count > 0;
    }

    public Vector3 GetTrashPosition()
    {
        Debug.Log("Caminando a la basura");
        return trash[0].gameObject.transform.position;
        //return new Vector3(8.72f, 0.91f, 14.97f);
    }

    public bool IsMachineEmpty()
    {
        Debug.Log("Comprobando si la maquina esta vacia");
        Debug.Log("Resultado: " + false);
        return false; //Habria que preguntarle a la maquina si esta vacia y tal
    }

    public bool IsCatBoxDirty()
    {
        Debug.Log("Comprobando si la caja esta sucia");
        Debug.Log("Resultado: " + true);
        return true; //Habria que preguntarle a la caja si esta sucia
    }

    public Vector3 GetCatBoxPosition() 
    {
        Debug.Log("Yendo a la caja de arena");
        return new Vector3(2.31f, 0.91f, 7.44f); //Habria que devolver la posicion de la caja
    }
}
