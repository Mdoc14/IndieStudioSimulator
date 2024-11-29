using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private List<GameObject> trash = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            trash.Add(other.gameObject);
            Debug.Log("Basura añadida a la habitación " + name);
        }
    }
}
