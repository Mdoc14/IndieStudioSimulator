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
        Debug.Log("Comporbando si la sala esta sucia");
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
        Debug.Log("Comporbando si la maquina esta vacia");
        return false; //Habria que preguntarle a la maquina si esta vacia y tal
    }

    public bool IsCatBoxDirty()
    {
        Debug.Log("Comporbando si la caja esta sucia");
        return true; //Habria que preguntarle a la caja si esta sucia
    }

    public Vector3 GetCatBoxPosition() 
    {
        Debug.Log("Comporbando si la caja esta sucia");
        return new Vector3(2.31f, 0.91f, 7.44f); //Habria que devolver la posicion de la caja
    }
}
