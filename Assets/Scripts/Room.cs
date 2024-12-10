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
        return trash.Count > 0;
    }

    public Vector3 GetTrashPosition()
    {
        return trash[0].gameObject.transform.position;
    }

    public bool IsMachineEmpty()
    {
        return true; //Habria que preguntarle a la maquina si esta vacia y tal
    }

    public bool IsCatBoxDirty()
    {
        return true; //Habria que preguntarle a la caja si esta sucia
    }

    public Vector3 GetCatBoxPosition() 
    {
        return new Vector3(1.2f, 0.91f, 14.97f); //Habria que devolver la posicion de la caja
    }
}
