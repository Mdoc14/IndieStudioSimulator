using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }
    [SerializeField] private GameObject trashPrefab;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
        DontDestroyOnLoad(Instance);
    }

    public void GenerateTrash(Vector3 position)
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(position, out navHit, 20, 1);
        GameObject.Instantiate(trashPrefab, navHit.position, Quaternion.identity);
    }
}
