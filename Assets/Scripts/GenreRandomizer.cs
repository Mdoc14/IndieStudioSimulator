using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenreRandomizer : MonoBehaviour
{
    [SerializeField] private GameObject ribbon;

    void Awake()
    {
        int random = Random.Range(0, 2);
        GetComponent<AgentBehaviour>().male = (random == 0);
        ribbon.SetActive(!GetComponent<AgentBehaviour>().male);
    }
}
