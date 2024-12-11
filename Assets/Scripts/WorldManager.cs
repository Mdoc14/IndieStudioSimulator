using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }
    [SerializeField] private GameObject trashPrefab;
    private List<Chair> _baths = new List<Chair>();
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
        DontDestroyOnLoad(Instance);
        foreach(GameObject bathroom in GameObject.FindGameObjectsWithTag("Bath"))
        {
            _baths.Add(bathroom.GetComponent<Chair>());
        }
    }

    public void GenerateTrash(Vector3 position) //Instancia basura dada una posición
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(position, out navHit, 20, 1);
        GameObject.Instantiate(trashPrefab, position, Quaternion.identity);
    }

    public Chair GetBathroom(IAgent agent) //Devuelve un baño libre que ni está siendo usado ni ha sido seleccionado por un personaje para usarse
    {
        foreach(Chair bath in _baths)
        {
            if (!bath.IsOccupied() && !bath.selected) 
            {
                agent.SetBath(bath);
                bath.selected = true;
                return bath;
            }
        }
        return null;
    }
}
